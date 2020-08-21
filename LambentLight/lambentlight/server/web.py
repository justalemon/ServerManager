import json
import logging

import aiohttp
from aiohttp import web

from .manager import manager
from .server import Server
from lambentlight import __version__

logger = logging.getLogger("lambentlight")


@web.middleware
async def auth(request: web.Request, handler):
    """
    Checks the token auth.
    """
    # If the request does not has an authentication header, return a 401
    if "Authorization" not in request.headers:
        return web.json_response({"message": "Token not specified."}, status=401)
    # If the header does not starts with Bearer
    elif not request.headers["Authorization"].lower().startswith("bearer "):
        return web.json_response({"message": "Auth Token is not using the right format."}, status=401)
    # If the second part does not matches the token in the config, return
    elif request.headers["Authorization"].split(" ")[1] != manager.config["token_api"]:
        return web.json_response({"message": "Auth Token is not valid."}, status=401)
    # Otherwise, return a the response of the handler
    return await handler(request)


app = web.Application(middlewares=[auth])
routes = web.RouteTableDef()


@routes.get("/ws")
async def websocket(request: web.Request):
    """
    WebSocket used for the communication of changes.
    """
    logger.info(f"WebSocket connection opened from {request.remote}")
    # Prepare the WebSocket Response for the Connection
    ws = web.WebSocketResponse()
    await ws.prepare(request)
    # Add it to the list of clients
    manager.ws_clients.append(ws)
    # And start checking the messages that are coming in
    async for message in ws:
        if message.type == aiohttp.WSMessage.CLOSE:
            break
    # Finally, remove the client from the list
    manager.ws_clients.remove(ws)
    logger.info(f"WebSocket connection of {request.remote} has been closed")
    return ws


@routes.get("/")
async def info(request):
    """
    Shows the LambentLight information.
    """
    pinfo = {
        "version": __version__
    }
    headers = {
        "Cache-Control": f"max-age={60 * 60}"  # 1 hour
    }
    return web.json_response(pinfo, headers=headers)


@routes.get("/builds")
async def builds(request):
    """
    Shows a list of builds.
    """
    headers = {
        "Cache-Control": f"max-age={2 * 60}"  # 2 minutes
    }
    return web.json_response([dict(x) for x in manager.builds], headers=headers)


@routes.post("/builds")
async def update_builds(request):
    """
    Updates the list of Builds.
    """
    # Just call the method to update builds and return the total number
    await manager.update_builds()
    return web.json_response({"count": len(manager.builds)})


@routes.post("/builds/download")
async def download_build(request: web.Request):
    """
    Downloads a specific build.
    """
    # Try to get the request as JSON
    try:
        data = await request.json()
    except json.JSONDecodeError:
        return web.json_response({"message": "Body is not JSON or is malformed."}, status=400)
    # If the request does not has an ID, return a 400
    if "name" not in data:
        return web.json_response({"message": "No Build name was specified."}, status=400)
    # Try to get the builds that match the ID
    matches = [x for x in manager.builds if x.name == data["name"]]
    # If no matches were found, return a 404
    if not matches:
        return web.json_response({"message": "No Builds were found with that name"}, status=404)
    # Get the build that we plan to manage
    build = matches[0]
    # If is already ready to be used and is not forced, return
    if build.is_ready and not data.get("force", False):
        return web.json_response({"message": "Build is already Downloaded and Ready."}, status=409)
    # Otherwise, start the download and notify if it was a success
    else:
        success = await build.download(manager.session)
        if success:
            return web.json_response({"message": "Build was downloaded"})
        else:
            return web.json_response({"messages": "Error when downloading the build."}, status=500)


@routes.get("/folders")
async def folders(request):
    """
    Gets the Data Folders known by LambentLight.
    """
    headers = {
        "Cache-Control": f"max-age={2 * 60}"  # 2 minutes
    }
    return web.json_response([dict(x) for x in manager.folders], headers=headers)


@routes.get("/servers")
async def servers_get(request):
    """
    Returns a list of servers.
    """
    headers = {
        "Cache-Control": f"max-age={2 * 60}"  # 2 minutes
    }
    return web.json_response([dict(x) for x in manager.servers], headers=headers)


@routes.put("/servers")
async def servers_put(request):
    """
    Starts a new CFX Server.
    """
    # Try to get the request as JSON and return a 400 if failed
    try:
        data = await request.json()
    except json.JSONDecodeError:
        return web.json_response({"message": "Body is not JSON or is malformed."}, status=400)

    # Make sure that the required parameters are present
    if "data" not in data:
        return web.json_response({"message": "Data Folder was not specified."}, status=400)
    elif "build" not in data:
        return web.json_response({"message": "Build was not specified."}, status=400)

    # Go ahead and search for the Data Folder
    found_folders = [x for x in manager.folders if x.name == data["data"]]
    if not found_folders:
        return web.json_response({"message": "Data Folder was not found."}, status=404)
    folder = found_folders[0]
    # Then, check if there is a Server running with the same Data folder
    found_servers = [x for x in manager.servers if x.folder == folder]
    if found_servers:
        return web.json_response({"message": "Found a server running with the specified Data Folder."}, status=409)

    # Now, time to find the Build
    found_builds = [x for x in manager.builds if x.name == data["build"]]
    if not found_builds:
        return web.json_response({"message": "CFX Build was not found."}, status=404)
    build = found_builds[0]

    # If we have everything, go ahead and start it
    server = Server(build, folder)
    if await server.start():
        manager.servers.append(server)
        return web.json_response({"pid": server.process.pid}, status=201)
    else:
        return web.json_response({"message": "Unable to start the server. Check the console."}, status=500)


app.add_routes(routes)
