import json
import logging
import os
import os.path as path
import secrets
import string

import aiofiles
import aiohttp

import lambentlight.server as server


logger = logging.getLogger("lambentlight")
default = {
    "token_api": "".join(secrets.choice(string.ascii_letters + string.digits) for _ in range(32)),
    "token_cfx": "",
    "dl_builds": "https://raw.githubusercontent.com/LambentLight/Builds/master/builds.json"
}


class Manager:
    """
    The main manager of LambentLight.
    """
    def __init__(self):
        self.session = None
        self.config_path = path.join(server.arguments.work_dir, "config.json")
        self.config = default
        self.builds = []
        self.folders = []
        self.ws_clients = []

    async def initialize(self):
        """
        Initializes the basics of the Manager.
        """
        # If the configuration exists, load it
        if path.isfile(self.config_path):
            async with aiofiles.open(self.config_path) as file:
                loaded = json.loads(await file.read())
                self.config = {}
                for key, value in default.items():
                    if key in loaded:
                        self.config[key] = loaded[key]
                    else:
                        self.config[key] = value
            logger.info(f"Loaded configuration from {self.config_path}")
        # Otherwise, write the default values
        else:
            try:
                os.makedirs(server.arguments.work_dir, exist_ok=True)
                async with aiofiles.open(self.config_path, "w+") as file:
                    js = json.dumps(default, indent=4)
                    await file.write(js)
                logger.warning(f"Created default configuration at {self.config_path}")
            except PermissionError:
                logger.warning("Unable to save the Default Configuration (no permission)")

        # Create the Client Session
        self.session = aiohttp.ClientSession()
        # And fetch the information required by the manager
        await self.update_builds()
        await self.update_folders()

        # Then, check if one of the data folders should be started on boot
        for folder in self.folders:
            # If this one does not, continue to the next one
            if not folder.config["auto_start"]:
                continue
            # Otherwise, try to get the build required or use the latest one
            name = folder.config["auto_start_build"]
            if name:
                found = [x for x in self.builds if x.name == name]
                if not found:
                    logger.error(f"Unable to find Build {name} to Auto Start Folder {folder.name}")
                    continue
                build = found[0]
            else:
                build = self.builds[0]

            # Then, try to start the server
            if not await folder.start(build):
                logger.error(f"Unable to automatically start Folder {folder.name}")
                continue

        # Finally, tell the main script that we finished the init
        return True

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

    async def update_builds(self):
        """
        Updates the list of known CFX Builds.
        """
        # Request the list of builds
        async with self.session.get(self.config["dl_builds"]) as resp:
            # If the code is not 200, log it and return
            if resp.status != 200:
                logger.error(f"Unable to fetch updated builds: Code {resp.status}")
                return
            # Otherwise, convert it to JSON
            try:
                new = await resp.json(content_type=None)
            except json.JSONDecodeError:
                logger.error(f"Unable to fetch updated builds because response is not JSON")
                return
        # If the request didn't included the list of builds, return
        if "builds" not in new:
            logger.error("Request didn't included list of builds.")
            return
        # Then, continue and iterate the builds that we got
        remote = []
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
            # If we got here, create a new one if it matches the os
            if (build["target"] == 0 and server.is_windows) or (build["target"] == 1 and server.is_ubuntu):
                remote.append(server.Build(name=build["name"], download=build["download"]))

        # Now, load the local builds that do not match existing ones
        local = []
        for directory in os.scandir(os.path.join(server.arguments.work_dir, "builds")):
            if not any(x for x in remote if directory.is_dir() and x.name == directory.name):
                local.append(server.Build(folder=directory))

        # At this point, replace the builds and notify it
        self.builds = remote + local
        logger.info(f"Builds have been updated (Total: {len(self.builds)})")

    async def update_folders(self):
        """
        Updates the list of Data Folders.
        """
        # Get the subdirectories of data and save them as Data Folders
        dpath = os.path.join(server.arguments.work_dir, "data")
        if os.path.isdir(dpath):
            local = [server.DataFolder(x, True) for x in os.scandir(dpath) if x.is_dir()]
        else:
            logger.warning("Directory with Data Folder does not exists, skipping...")
            local = []
        logger.info(f"Data Folders have been updated (Total: {len(local)})")
        self.folders = local

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


manager = Manager()
