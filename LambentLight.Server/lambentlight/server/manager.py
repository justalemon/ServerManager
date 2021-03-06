import json
import logging
import os
import os.path as path

import aiofiles
import aiohttp
from git import Repo

import lambentlight.server as server
from .build import Build
from .checks import is_ubuntu, is_windows
from .config import default_server as default
from .datafolder import DataFolder
from .exceptions import ConfigurationMissingException

logger = logging.getLogger("lambentlight")


class Manager:
    """
    The main manager of LambentLight.
    """
    def __init__(self, directory):
        self.dir = directory
        self.builds_dir = os.path.join(directory, "builds")
        config = os.path.join(directory, "config.json")

        # If the configuration file does not exists, raise an exception
        if not path.isfile(config):
            raise ConfigurationMissingException(config)

        # Otherwise, just load it
        with open(config) as file:
            loaded = json.loads(file.read())
        # And patch the missing keys from the default values
        self.config = {}
        for key, value in default.items():
            if key in loaded:
                self.config[key] = loaded[key]
            else:
                self.config[key] = value
        logger.info(f"Loaded configuration from {config}")

        self.session = aiohttp.ClientSession()
        self.config = default
        self.builds = []
        self.folders = []
        self.ws_clients = []

    async def autostart_servers(self):
        """
        Automatically starts the servers configured to do so.
        """
        # Iterate over the list of folders
        for folder in self.folders:
            # If the folder is not set to auto start, skip it
            if not folder.config.get("auto_start", False):
                continue
            # Then, find the build set on the options
            name = folder.config.get("auto_start_build", "")
            if name:
                found = [x for x in self.builds if x.name == name]
                if not found:
                    logger.warning(f"Unable to find Build {name} to Auto Start Folder {folder.name}")
                    continue
                build = found[0]
            # If no build was specified, use the latest one
            else:
                build = self.builds[0]

            # Then, try to start the server
            if not await folder.start(build):
                logger.error(f"Unable to automatically start Folder {folder.name}")
                continue

        # Finally, tell the main script that we finished the init
        return True

    async def save_settings(self):
        """
        Saves the settings.
        """
        async with aiofiles.open(os.path.join(self.dir, "config.json"), "w") as file:
            text = json.dumps(self.config, indent=4) + "\n"
            await file.write(text)
            logger.info("Settings have been saved")

    async def close(self):
        """
        Stops all of the servers and web sessions in use.
        """
        logger.info("Stopping LambentLight")
        # Close the session used for aiohttp web requests
        await self.session.close()
        # And disconnect all of the WS Clients
        for ws in self.ws_clients:
            if not ws.closed:
                await ws.close(code=aiohttp.WSCloseCode.GOING_AWAY,
                               message="LambentLight is Closing")

    async def create_folder(self, name: str, clone: bool):
        """
        Creates a new Data Folder.
        """
        path = os.path.join(self.dir, "data", name)

        # Create the folder or clone the repository
        if clone:
            # From https://gitpython.readthedocs.io/en/stable/intro.html:
            # GitPython is not suited for long-running processes (like daemons) as
            # it tends to leak system resources. It was written in a time where destructors
            # (as implemented in the __del__ method) still ran deterministically.
            # In case you still want to use it in such a context, you will want to search the
            # codebase for __del__ implementations and call these yourself when you see fit.
            repo = Repo.clone_from("https://github.com/LambentLight/ServerData.git", path)  # noqa: F841
            del repo
        else:
            os.makedirs(path, exist_ok=True)

        # Then, create an empty configuration file
        async with aiofiles.open(os.path.join(path, "lambentlight.json"), "w") as file:
            await file.write("{\n}\n")
        # Now, update the list of folders
        await self.update_folders()
        # And return the one with the same exact name
        # TODO: Make this safer
        return [x for x in self.folders if x.name == name][0]

    async def update_builds(self):
        """
        Updates the list of known CFX Builds.
        """
        # Start with the remote builds from the URLs
        remote = []
        for url in self.config["builds"]:
            logger.info(f"Fetching builds from '{url}'")
            # Try to make the request
            async with self.session.get(url) as resp:
                # Discard any code 200 errors
                if resp.status != 200:
                    logger.error(f"Unable to fetch builds from '{url}': Code {resp.status}")
                    continue
                # And try to convert it to a dict from JSON
                try:
                    new = await resp.json(content_type=None)
                except json.JSONDecodeError as e:
                    logger.error(f"Unable to parse builds from '{url}': {e}")
                    continue
                # If no builds are present in the json, skip it
                if "builds" not in new:
                    logger.error(f"Response from {url} does not has any builds.")
                    continue

                # Now, time to add the builds one by one
                for build in new["builds"]:
                    # If one of the parts is missing, log it and continue
                    if "name" not in build:
                        logger.error("Name of Build is missing.")
                        continue
                    elif "download" not in build:
                        logger.error("Download URL of Build is missing.")
                        continue
                    elif "target" not in build:
                        logger.error("Target Operating System of Build was not found.")
                        continue

                    # If there is already a build with the same name, skip it and raise a warning
                    name = build["name"]
                    if any(x for x in remote if x.name == name):
                        logger.warning(f"Ignoring build '{name}' because one with the same name already exists")
                        continue

                    # If we got here, create a new one if it matches the os
                    if (build["target"] == 0 and is_windows) or (build["target"] == 1 and is_ubuntu):
                        remote.append(Build(self, name=name, download=build["download"]))

        # Now is time for the local builds
        local = []
        if os.path.isdir(self.builds_dir):
            # Iterate over the entries in the directory
            for entry in os.scandir(self.builds_dir):
                # If the entry is not a directory, skip it
                if not entry.is_dir():
                    continue
                # If there is no remote build that matches the name, add it as a local build
                if not any(x for x in remote if x.name == entry.name):
                    local.append(server.Build(folder=entry))

        # At this point, replace the builds and notify it
        self.builds = remote + local
        logger.info(f"Builds have been updated (Total: {len(self.builds)})")

    async def update_folders(self):
        """
        Updates the list of Data Folders.
        """
        dpath = os.path.join(self.dir, "data")

        # If the data directory exists, get the folders
        if os.path.isdir(dpath):
            local = []
            for entry in os.scandir(dpath):
                if not entry.is_dir():
                    continue

                # Check if there are any data folders with a matching name
                name = os.path.basename(os.path.abspath(entry))
                found = [x for x in self.folders if x.name == name]

                # If there are, reuse it
                if found:
                    logger.info(f"Using existing {name} Data Folder object")
                    local.append(found[0])
                # Otherwise, create a new one
                else:
                    logger.info(f"Creating new Data Folder object for {name}")
                    local.append(DataFolder(self, entry))
        else:
            logger.warning("Directory with Data Folder does not exists, skipping...")
            local = []
        logger.info(f"Data Folders have been updated (Total: {len(local)})")
        self.folders = local

    async def remove(self, obj, *, stop=True):
        """
        Removes a Data Folder or Build.
        """
        # If is on none of the lists, return
        if obj not in self.folders and obj not in self.builds:
            return False

        # If is a Data Folder
        if isinstance(obj, DataFolder):
            # If there is a server running, stop it if required
            # Raise an exception otherwise
            if obj.is_running:
                if not stop:
                    raise server.ServerRunningException()
                await obj.stop(terminate=False)
            # And then delete it
            self.folders.remove(obj)
            return True
        # If is a Build
        elif isinstance(obj, Build):
            # Try to find servers running with the Data Folder
            servers = [x for x in self.folders if x.build == obj]
            if servers:
                if not stop:
                    raise server.ServerRunningException()
                for srv in servers:
                    await srv.stop()
            # If the build is installed, delete it
            if obj.is_ready:
                await obj.delete()
            # And then remove it
            self.builds.remove(obj)
        # If we got here, say that we were unable to delete it
        return False

    async def send_data(self, event: str, data):
        """
        Sends some data to the connected Clients via WebSockets.
        """
        for ws in self.ws_clients:
            data = {
                "e": event,
                "d": data
            }
            await ws.send_json(data)

    async def process(self):
        """
        Processes checks for the Manager.
        """
        await self.restart_on_crash()

    async def restart_on_crash(self):
        """
        Checks if the servers have exited with non zero codes and restart them if they do.
        """
        for folder in self.folders:
            # If there is no process or there is one and is running, continue
            if folder.process is None or folder.is_running:
                continue
            # If we got here, the process is no longer running
            # If the exit code is zero, notify it and continue
            code = folder.process.returncode
            if code == 0:
                logger.info(f"Server {folder.name} exited with Code {code}")
                await folder.stop()
            # Otherwise, restart the process
            else:
                logger.warning(f"Restarting Server {folder.name} because it exited with Code {code}")
                await folder.start(folder.build, terminate=True)
