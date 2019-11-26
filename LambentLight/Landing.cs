﻿using LambentLight.Extensions;
using LambentLight.Managers;
using LambentLight.Properties;
using LambentLight.Targets;
using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace LambentLight
{
    public partial class Landing : Form
    {
        #region Properties

        /// <summary>
        /// The logger for our current class.
        /// </summary>
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Sets the locked status of some of the UI elements.
        /// </summary>
        public bool Locked
        {
            set
            {
                StartToolStripMenuItem.Enabled = !value;
                StopToolStripMenuItem.Enabled = value;
                RestartToolStripMenuItem.Enabled = value;
                CreateToolStripMenuItem.Enabled = !value;

                BuildsListBox.Enabled = !value;
                BuildsRefreshButton.Enabled = !value;
                DataFolderComboBox.Enabled = !value;
                DataFolderRefreshButton.Enabled = !value;

                ConsoleTextBox.Enabled = value;
                ConsoleButton.Enabled = value;
            }
        }

        #endregion

        #region Constructor and Loading

        public Landing()
        {
            // Initialize the UI elements
            InitializeComponent();
        }

        private void Landing_Load(object sender, EventArgs e)
        {
            // Create a new configuration for NLog
            LoggingConfiguration NewConfig = new LoggingConfiguration();
            // Add new rules for logging into specific places
            NewConfig.AddRule(LogLevel.Debug, LogLevel.Fatal, new TextBoxTarget() { Layout = "[${date}] [${level}] ${message}" });
            NewConfig.AddRule(LogLevel.Info, LogLevel.Fatal, new BottomStripTarget() { Layout = "${message}" });
            // Set the already created configuration
            LogManager.Configuration = NewConfig;
            // Update the list of builds, folders and resources
            BuildManager.Refresh();
            DataFolderManager.Refresh();
            ResourceManager.Refresh();
            // And filll the Builds and Data folders
            BuildsListBox.Fill(BuildManager.Builds, true);
            DataFolderComboBox.Fill(DataFolderManager.Folders, true);
            InstallerResourcesListBox.Fill(ResourceManager.Resources);
            // Set the elements to unlocked
            Locked = false;
            // Load the RTF text
            AboutRichTextBox.Rtf = Resources.About;

            // Tell the Web Clients to use TLS 1.2 instead of SSL3
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        private void Landing_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Stop the server if is running
            if (ProcessManager.IsServerRunning)
            {
                ProcessManager.Stop();
            }
        }

        private void MainTabControl_Selected(object sender, TabControlEventArgs e)
        {
            // If the user selected the Server Configuration tab and there is a data folder selected
            if (e.TabPage == ConfigurationTabPage && DataFolderComboBox.SelectedItem != null)
            {
                // Set the text to the configuration of the server
                ConfigurationTextBox.Text = ((DataFolder)DataFolderComboBox.SelectedItem).Configuration;
            }
            // If the user selected the Resources tab and there is a data folder selected and it exists
            else if (e.TabPage == ResourcesTabPage && DataFolderComboBox.SelectedItem != null)
            {
                // Update the list of installed resources
                RefreshInstalledResources();
            }
        }

        #endregion

        #region Top Strip

        private async void StartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Ensure that we have an item available
            if (BuildsListBox.SelectedItem == null)
            {
                // If not, notify the user and return
                Logger.Info("You have not selected a FiveM/CitizenFX server build");
                return;
            }
            // Do the same with server data folders
            if (DataFolderComboBox.SelectedItem == null)
            {
                // Notify and return
                Logger.Info("You have not selected a Server Data folder");
                return;
            }

            // Start the build with the selected options
            Locked = await ProcessManager.Start((Build)BuildsListBox.SelectedItem, (DataFolder)DataFolderComboBox.SelectedItem);
        }

        private void StopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Stop the server if is present and unlock the controls
            ProcessManager.Stop();
            Locked = false;
        }

        private async void RestartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Tell the process manager to restart the existing server
            await ProcessManager.Restart();
        }

        private async void CreateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create the new form
            Creator CreatorForm = new Creator();
            // Show the form as a dialog
            CreatorForm.ShowDialog();
            // Finally, dispose the dialog
            CreatorForm.Dispose();

            // If there is a valid server data folder
            if (CreatorForm.NewDataFolder != null)
            {
                // Lock the fields
                Locked = true;
                // Task the Data Folder to recreate itself
                await CreatorForm.NewDataFolder.Recreate(CreatorForm.RCONTextBox.Text, CreatorForm.SHVCheckBox.Checked);
                // Update the fields
                DataFolderManager.Refresh();
                DataFolderComboBox.Fill(DataFolderManager.Folders);
                // And select the new item
                DataFolderComboBox.SelectedItem = CreatorForm.NewDataFolder;
                // Then unlock the fields
                Locked = false;
            }
        }

        private void SettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create a new settings window and open it up
            Config config = new Config();
            config.ShowDialog();
            config.Dispose();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If the server is running
            if (ProcessManager.IsServerRunning)
            {
                // Ask the user if he wants to close the server
                DialogResult result = MessageBox.Show("This will stop the FiveM server.\nAre you sure that you want to Exit?", "Server is Running", MessageBoxButtons.YesNo);

                // If the result is not yes, return
                if (result != DialogResult.Yes)
                {
                    return;
                }
                // Otherwise, stop the server
                ProcessManager.Stop();
            }

            // Close the current form
            Close();
        }

        #endregion

        #region Server Console

        private void ClearLogButton_Click(object sender, EventArgs e)
        {
            // Just wipe everything on the LogTextBox
            LogsTextBox.Text = string.Empty;
        }

        private void ConsoleButton_Click(object sender, EventArgs e)
        {
            // If the server is running
            if (ProcessManager.IsServerRunning)
            {
                // Write the text from the text box and flush it
                ProcessManager.Server.Process.StandardInput.WriteLine(ConsoleTextBox.Text);
                ProcessManager.Server.Process.StandardInput.Flush();
                // Finally, set the text to empty on the box
                ConsoleTextBox.Text = string.Empty;
            }
            // If is not
            else
            {
                // Log an error
                Logger.Error("Attempted to send text into the console but the server is not running");
            }
        }

        #endregion

        #region Builds and Data Folders

        private void BuildsRefreshButton_Click(object sender, EventArgs e)
        {
            // Refresh the list of builds
            BuildManager.Refresh();
            BuildsListBox.Fill(BuildManager.Builds, true);
        }

        private async void BuildsImportButton_Click(object sender, EventArgs e)
        {
            // Open the file dialog
            // If the user canceled the operation, return
            if (BuildFileDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            // Then, install the build from the dialog
            await BuildManager.Install(BuildFileDialog.FileName);
        }

        private void DataFolderComboBox_SelectedIndexChanged(object sender, EventArgs e) => RefreshInstalledResources();

        private void DataFolderRefreshButton_Click(object sender, EventArgs e)
        {
            // Refresh the folders of data
            DataFolderManager.Refresh();
            DataFolderComboBox.Fill(DataFolderManager.Folders);
        }

        private void DataFolderBrowseButton_Click(object sender, EventArgs e)
        {
            // If there is something selected
            if (DataFolderComboBox.SelectedItem != null)
            {
                // Open the folder
                Process.Start(((DataFolder)DataFolderComboBox.SelectedItem).Absolute);
            }
        }

        #endregion

        #region Resources - Uninstaller

        private void UninstallerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If there is a resource to uninstall, enable the button
            UninstallerRemoveButton.Enabled = UninstallerListBox.SelectedItem != null;
        }

        private void UninstallerRefreshButton_Click(object sender, EventArgs e) => RefreshInstalledResources();

        private void UninstallerRemoveButton_Click(object sender, EventArgs e)
        {
            // Get the resource that we are trying to uninstall
            InstalledResource Installed = ((InstalledResource)UninstallerListBox.SelectedItem);
            // Try to find the resource with the same folder as the one to be installed
            Resource Found = ResourceManager.Resources.Where(x => x.Folder.ToLower() == Installed.Name.ToLower()).FirstOrDefault();
            // Select the correct name for the resource
            string Name = Found == null ? Installed.Name : Found.Name;

            // Ask the user if he really wants to remove the resource
            DialogResult Result = MessageBox.Show($"Are you sure that you want to uninstall {Name}?", "Uninstall Confirmation", MessageBoxButtons.YesNo);

            // If the user really wants to remove the game
            if (Result == DialogResult.Yes)
            {
                // Remove the selected resource
                Installed.Dispose();
                // And update the list of installed resources
                RefreshInstalledResources();
            }
        }

        #endregion

        #region Resources - Installer

        private void InstallerResourcesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If there is something selected
            if (InstallerResourcesListBox.SelectedItem != null)
            {
                // Add the builds to our version ListBox
                InstallerVersionsListBox.Fill(((Resource)InstallerResourcesListBox.SelectedItem).Versions);
            }
            // Otherwise
            else
            {
                // Wipe the Versions
                InstallerVersionsListBox.Items.Clear();
                // And disable the install button
                InstallerInstallButton.Enabled = false;
            }
        }

        private void InstallerVersionsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If there is a version to install, enable the button
            InstallerInstallButton.Enabled = InstallerVersionsListBox.SelectedItem != null;
        }

        private void InstallerRefreshButton_Click(object sender, EventArgs e)
        {
            // Disable the install button
            InstallerInstallButton.Enabled = false;
            // And add the updated set of resource
            ResourceManager.Refresh();
            InstallerResourcesListBox.Fill(ResourceManager.Resources);
        }

        private async void InstallerInstallButton_Click(object sender, EventArgs e)
        {
            // If there is no data folder selected, notify the user and return
            if (DataFolderComboBox.SelectedItem == null)
            {
                Logger.Error("You need to select a Server Data folder!");
                return;
            }

            // Cast the data folder and resource to their real values
            DataFolder Folder = (DataFolder)DataFolderComboBox.SelectedItem;
            Resource NewResource = (Resource)InstallerResourcesListBox.SelectedItem;

            // Get all of the requirements by the selected resource
            Dictionary<Resource, Managers.Version> Collected = ResourceManager.GetRequirements(NewResource, (Managers.Version)InstallerVersionsListBox.SelectedItem);
            // Create the readable list of resources
            string ReadableResources = string.Join(" ", Collected.Select(x => $"{x.Key.Name}-{x.Value.ReadableVersion}"));

            // Notify the user that we are going to install the keys
            Logger.Info("Installing requirements {0} for {1}", ReadableResources, InstallerResourcesListBox.SelectedItem);

            // Iterate over the list of required resources
            foreach (KeyValuePair<Resource, Managers.Version> Requirement in Collected)
            {
                // And install it
                await Folder.InstallResource(Requirement.Key, Requirement.Value);
            }

            // If the user wants to auto-start the resource
            if (Settings.Default.AddToConfig)
            {
                // If the resource is already set to auto start
                if (Regex.IsMatch(Folder.Configuration, string.Format(Patterns.Resource, NewResource.Folder)))
                {
                    // Notify the user
                    Logger.Warn("The resource '{0}' is already on the configuration set to auto start", ((Resource)InstallerResourcesListBox.SelectedItem).Folder);
                }
                // Otherwise
                else
                {
                    // Set the new installed resourced to auto start
                    Folder.Configuration = Folder.Configuration + $"start {NewResource.Folder}" + Environment.NewLine;
                }
            }

            // Notify that we have installed all of the resources
            Logger.Info("Successfully installed {0}", ReadableResources);
            // And finally, update the list of installed resources
            RefreshInstalledResources();
        }

        #endregion

        #region Server Configuration

        private void ConfigurationLoadButton_Click(object sender, EventArgs e)
        {
            // If there is a data folder selected
            if (DataFolderComboBox.SelectedItem != null)
            {
                // Set the text to the configuration of the server
                ConfigurationTextBox.Text = ((DataFolder)DataFolderComboBox.SelectedItem).Configuration;
            }
        }

        private void ConfigurationGenerateButton_Click(object sender, EventArgs e)
        {
            // If there is a data folder selected
            if (DataFolderComboBox.SelectedItem != null)
            {
                // Ask the user if he is sure about this
                DialogResult Response = MessageBox.Show("Are you sure that you want to replace the existing configuration?", "Replace Configuration", MessageBoxButtons.YesNo);

                // If the response was yes
                if (Response == DialogResult.Yes)
                {
                    // Set the text of the configuration
                    ConfigurationTextBox.Text = ((DataFolder)DataFolderComboBox.SelectedItem).GenerateConfig();
                }
            }
        }

        private void ConfigurationSaveButton_Click(object sender, EventArgs e)
        {
            // If there is a data folder selected
            if (DataFolderComboBox.SelectedItem != null)
            {
                // Set the text of the configuration
                ((DataFolder)DataFolderComboBox.SelectedItem).Configuration = ConfigurationTextBox.Text;
            }
        }

        #endregion
        
        #region Tools

        private void RefreshInstalledResources()
        {
            // Disable the uninstall button
            UninstallerRemoveButton.Enabled = false;

            // If there is no server data folder selected or it does not exists
            if (DataFolderComboBox.SelectedItem == null || !((DataFolder)DataFolderComboBox.SelectedItem).Exists)
            {
                // Wipe all of the items and return
                UninstallerListBox.Items.Clear();
                return;
            }

            // Update the list of installed resources
            UninstallerListBox.Fill(((DataFolder)DataFolderComboBox.SelectedItem).InstalledResources);
        }

        #endregion
    }
}
