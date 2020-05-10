﻿using LambentLight.Builds;
using LambentLight.DataFolders;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace LambentLight.Runtime
{
    /// <summary>
    /// Manages the Startup and Stopping of the server.
    /// </summary>
    public class RuntimeManager : BaseManager
    {
        #region Properties

        /// <summary>
        /// If there is a server running or not.
        /// </summary>
        public bool IsRunning => Program.Landing.ServerConsoleControl.IsProcessRunning;
        /// <summary>
        /// The Data Folder being used.
        /// </summary>
        public DataFolder Folder { get; private set; } = null;
        /// <summary>
        /// The CFX Build being used.
        /// </summary>
        public Build Build { get; private set; } = null;

        #endregion

        #region Constructor

        public RuntimeManager()
        {
        }

        #endregion

        #region Public Functions

        /// <summary>
        /// Starts the server with the specified Build and Data Folder.
        /// </summary>
        /// <param name="build">The Build to use.</param>
        /// <param name="folder">The Data Folder to use.</param>
        public async Task Start(Build build, DataFolder folder)
        {
            // If the build is not available, download it
            if (!build.IsInstalled)
            {
                await build.Download();
            }

            // Select the correct game and license
            string game = folder.Config.Game == Game.RedDeadRedemption2 ? "rdr3 " : "gtav";
            string license = folder.Config.LicenseUseCustom ? folder.Config.LicenseCustom : Program.Config.CFXLicense;
            // Format the launch parameters
            string arguments = $"+set citizen_dir \"{Path.Combine(build.Folder, "citizen")}\" " +
                $"+set sv_licenseKey \"{license}\" " +
                $"+set steam_webApiKey \"{Program.Config.SteamKey}\" " +
                $"+set onesync_enabled " + (folder.Config.OneSync || folder.Config.OneSyncInfinity ? $"1 " : "0 ") +
                $"+set onesync_enableInfinity " + (folder.Config.OneSyncInfinity ? $"1 " : "0 ") +
                $"+set gamename {game} " +
                $"+exec {folder.Config.Config}";
            // And launch the server
            Program.Landing.ServerConsoleControl.StartProcess(build.Executable, arguments);
        }

        #endregion
    }
}