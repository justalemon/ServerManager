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
                StartItem.Enabled = !value;
                StopItem.Enabled = value;
                CreateItem.Enabled = !value;
                ExitItem.Enabled = !value;

                BuildsBox.Enabled = !value;
                BuildRefreshButton.Enabled = !value;
                DataBox.Enabled = !value;
                FolderRefreshButton.Enabled = !value;

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
            BuildsBox.Fill(BuildManager.Builds, true);
            DataBox.Fill(DataFolderManager.Folders, true);
            ResourcesListBox.Fill(ResourceManager.Resources);
            // Set the elements to unlocked
            Locked = false;

            // Tell the Web Clients to use TLS 1.2 instead of SSL3
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // Load the settings
            ReloadSettings();
        }

        #endregion

        #region Top Strip

        private async void StartItem_Click(object sender, EventArgs e)
        {
            // Ensure that we have an item available
            if (BuildsBox.SelectedItem == null)
            {
                // If not, notify the user and return
                Logger.Info("You have not selected a FiveM/CitizenFX server build");
                return;
            }
            // Do the same with server data folders
            if (DataBox.SelectedItem == null)
            {
                // Notify and return
                Logger.Info("You have not selected a Server Data folder");
                return;
            }

            // Start the build with the selected options
            Locked = await ProcessManager.Start((Build)BuildsBox.SelectedItem, (DataFolder)DataBox.SelectedItem);
        }

        private void StopItem_Click(object sender, EventArgs e)
        {
            // Stop the server if is present and unlock the controls
            ProcessManager.Stop();
            Locked = false;
        }

        private async void CreateItem_Click(object sender, EventArgs e)
        {
            // Ask the user for inputing a server data folder name
            string FolderName = Microsoft.VisualBasic.Interaction.InputBox("Please insert a name for the new Server Data Folder:", "New Server Data Folder");
            // Lock the fields
            Locked = true;
            // Create a server data folder
            DataFolder NewFolder = await DataFolderManager.Create(FolderName);
            // If the creation of the new folder succeded
            if (NewFolder != null)
            {
                // Update the fields
                DataFolderManager.Refresh();
                DataBox.Fill(DataFolderManager.Folders);
                // And select the new item
                DataBox.SelectedItem = NewFolder;
            }
            // Then unlock the fields
            Locked = false;
        }

        private void ExitItem_Click(object sender, EventArgs e)
        {
            // Close the current form
            Close();
        }

        #endregion

        #region Server Console

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

        private void BuildRefreshButton_Click(object sender, EventArgs e)
        {
            // Refresh the list of builds
            BuildManager.Refresh();
            BuildsBox.Fill(BuildManager.Builds, true);
        }

        private void FolderRefreshButton_Click(object sender, EventArgs e)
        {
            // Refresh the folders of data
            DataFolderManager.Refresh();
            DataBox.Fill(DataFolderManager.Folders);
        }

        #endregion

        #region Resource Installer

        private void ResourcesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If there is something selected
            if (ResourcesListBox.SelectedItem != null)
            {
                // Add the builds to our version ListBox
                VersionsListBox.Fill(((Resource)ResourcesListBox.SelectedItem).Versions);
            }
            // Otherwise
            else
            {
                // Wipe the Versions
                VersionsListBox.Items.Clear();
                // And disable the install button
                InstallButton.Enabled = false;
            }
        }

        private void VersionsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // If there is a version to install, enable the button
            InstallButton.Enabled = VersionsListBox.SelectedItem != null;
        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {
            // Disable the install button
            InstallButton.Enabled = false;
            // And add the updated set of resource
            ResourceManager.Refresh();
            ResourcesListBox.Fill(ResourceManager.Resources);
        }

        private async void InstallButton_Click(object sender, EventArgs e)
        {
            // If there is no data folder selected, notify the user and return
            if (DataBox.SelectedItem == null)
            {
                Logger.Error("You need to select a Server Data folder!");
                return;
            }
            
            // Get all of the requirements by the selected resource
            Dictionary<Resource, Managers.Version> Collected = ResourceManager.GetRequirements((Resource)ResourcesListBox.SelectedItem, (Managers.Version)VersionsListBox.SelectedItem);
            // Create the readable list of resources
            string ReadableResources = string.Join(" ", Collected.Select(x => $"{x.Key.Name}-{x.Value.ReadableVersion}"));

            // Notify the user that we are going to install the keys
            Logger.Info("Installing requirements {0} for {1}", ReadableResources, ResourcesListBox.SelectedItem);

            // Iterate over the list of required resources
            foreach (KeyValuePair<Resource, Managers.Version> Requirement in Collected)
            {
                // And install it
                await ((DataFolder)DataBox.SelectedItem).InstallResource(Requirement.Key, Requirement.Value);
            }

            // Notify that we have installed all of the resources
            Logger.Info("Successfully installed {0}", ReadableResources);
        }

        #endregion

        #region Server Configuration

        private void Tabs_Selected(object sender, TabControlEventArgs e)
        {
            // If the user selected the Server Configuration tab and there is a data folder selected
            if (e.TabPage == ConfigurationTab && DataBox.SelectedItem != null)
            {
                // Set the text to the configuration of the server
                ConfigTextBox.Text = ((DataFolder)DataBox.SelectedItem).Configuration;
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            // If there is a data folder selected
            if (DataBox.SelectedItem != null)
            {
                // Set the text to the configuration of the server
                ConfigTextBox.Text = ((DataFolder)DataBox.SelectedItem).Configuration;
            }
        }

        private void GenerateButton_Click(object sender, EventArgs e)
        {
            // If there is a data folder selected
            if (DataBox.SelectedItem != null)
            {
                // Ask the user if he is sure about this
                DialogResult Response = MessageBox.Show("Are you sure that you want to replace the existing configuration?", "Replace Configuration", MessageBoxButtons.YesNo);

                // If the response was yes
                if (Response == DialogResult.Yes)
                {
                    // Set the text of the configuration
                    ConfigTextBox.Text = ((DataFolder)DataBox.SelectedItem).GenerateConfig();
                }
            }
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            // If there is a data folder selected
            if (DataBox.SelectedItem != null)
            {
                // Set the text of the configuration
                ((DataFolder)DataBox.SelectedItem).Configuration = ConfigTextBox.Text;
            }
        }

        #endregion

        #region Settings

        private void ReloadSettings()
        {
            // Disable the license check box
            VisibleCheckBox.Checked = false;

            // And load all of the settings
            DownloadScriptsCheckBox.Checked = Settings.Default.DownloadScripts;
            CreateConfigCheckBox.Checked = Settings.Default.CreateConfig;

            RestartEveryCheckBox.Checked = Settings.Default.RestartEvery;
            RestartAtCheckBox.Checked = Settings.Default.RestartAt;
            RestartEveryTextBox.Text = Settings.Default.RestartEveryTime.ToString();
            RestartAtTextBox.Text = Settings.Default.RestartAtTime.ToString();

            BuildsWinTextBox.Text = Settings.Default.BuildsWindows;
            BuildsLinTextBox.Text = Settings.Default.BuildsLinux;
            ResourcesTextBox.Text = Settings.Default.Resources;

            AutoRestartCheckBox.Checked = Settings.Default.AutoRestart;
            ClearCacheCheckBox.Checked = Settings.Default.ClearCache;
        }

        private void VisibleTextBox_CheckedChanged(object sender, EventArgs e)
        {
            // Change the enabled status of the License TextBox
            LicenseTextBox.Enabled = VisibleCheckBox.Checked;
            SaveLicenseButton.Enabled = VisibleCheckBox.Checked;
            // If the CheckBox is enabled
            if (VisibleCheckBox.Checked)
            {
                // Fill the text box with the license
                LicenseTextBox.Text = Settings.Default.License;
            }
            // Otherwise
            else
            {
                // Delete it
                LicenseTextBox.Text = string.Empty;
            }
        }

        private void GenerateLicenseButton_Click(object sender, EventArgs e)
        {
            // Open the FiveM Keymaster page
            Process.Start("https://keymaster.fivem.net");
        }

        private void SaveLicenseButton_Click(object sender, EventArgs e)
        {
            // Save the license on the text box
            Settings.Default.License = LicenseTextBox.Text;
            Settings.Default.Save();
        }

        private void SaveAPIsButton_Click(object sender, EventArgs e)
        {
            // Save the URLs on the configuration
            Settings.Default.Resources = ResourcesTextBox.Text;
            Settings.Default.BuildsWindows = BuildsWinTextBox.Text;
            Settings.Default.BuildsLinux = BuildsLinTextBox.Text;
            Settings.Default.Save();
        }

        private void DownloadScriptsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Save the curent status on the settings
            Settings.Default.DownloadScripts = DownloadScriptsCheckBox.Checked;
            Settings.Default.Save();
        }

        private void CreateConfigCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Save the curent status on the settings
            Settings.Default.CreateConfig = CreateConfigCheckBox.Checked;
            Settings.Default.Save();
        }

        private void AutoRestartCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Save the curent status on the settings
            Settings.Default.AutoRestart = AutoRestartCheckBox.Checked;
            Settings.Default.Save();
        }

        private void ClearCacheCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Save the curent status on the settings
            Settings.Default.ClearCache = ClearCacheCheckBox.Checked;
            Settings.Default.Save();
        }

        private void RestartEveryCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Save the curent status on the settings
            Settings.Default.RestartEvery = RestartEveryCheckBox.Checked;
            Settings.Default.Save();
        }

        private void RestartAtCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Save the curent status on the settings
            Settings.Default.RestartAt = RestartAtCheckBox.Checked;
            Settings.Default.Save();
        }

        private void RestartEveryButton_Click(object sender, EventArgs e)
        {
            // Try to parse the text box contents
            try
            {
                Settings.Default.RestartEveryTime = TimeSpan.Parse(RestartEveryTextBox.Text);
            }
            // If we have failed
            catch (FormatException)
            {
                MessageBox.Show("The format for the 'Restart every' time is invalid.");
                return;
            }
            // If we succeeded, save it
            Settings.Default.Save();
        }

        private void RestartAtButton_Click(object sender, EventArgs e)
        {
            // Try to parse the text box contents
            try
            {
                Settings.Default.RestartAtTime = TimeSpan.Parse(RestartAtTextBox.Text);
            }
            // If we have failed
            catch (FormatException)
            {
                MessageBox.Show("The format for the 'Restart daily at' time is invalid.");
                return;
            }
            // If we succeeded, save it
            Settings.Default.Save();
        }

        private void ResetSettingsButton_Click(object sender, EventArgs e)
        {
            // Ask the user if he is sure
            DialogResult Result = MessageBox.Show("Are you sure that you want to reset the settings?", "Resetting Settings", MessageBoxButtons.YesNo);

            // If the user is sure
            if (Result == DialogResult.Yes)
            {
                // Reset the settings
                Settings.Default.Reset();
                // And reload them
                ReloadSettings();
            }
        }

        #endregion
    }
}
