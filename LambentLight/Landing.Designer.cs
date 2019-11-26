﻿namespace LambentLight
{
    partial class Landing
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Landing));
            this.BuildsGroupBox = new System.Windows.Forms.GroupBox();
            this.BuildsListBox = new System.Windows.Forms.ListBox();
            this.BuildsRefreshButton = new System.Windows.Forms.Button();
            this.DataFolderGroupBox = new System.Windows.Forms.GroupBox();
            this.DataFolderBrowseButton = new System.Windows.Forms.Button();
            this.DataFolderRefreshButton = new System.Windows.Forms.Button();
            this.DataFolderComboBox = new System.Windows.Forms.ComboBox();
            this.TopMenuStrip = new System.Windows.Forms.MenuStrip();
            this.StartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RestartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.CreateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ExitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.LogsTabPage = new System.Windows.Forms.TabPage();
            this.ClearLogButton = new System.Windows.Forms.Button();
            this.ConsoleButton = new System.Windows.Forms.Button();
            this.ConsoleTextBox = new System.Windows.Forms.TextBox();
            this.LogsTextBox = new System.Windows.Forms.TextBox();
            this.MainTabControl = new System.Windows.Forms.TabControl();
            this.ResourcesTabPage = new System.Windows.Forms.TabPage();
            this.UninstallerGroupBox = new System.Windows.Forms.GroupBox();
            this.UninstallerRefreshButton = new System.Windows.Forms.Button();
            this.UninstallerListBox = new System.Windows.Forms.ListBox();
            this.UninstallerRemoveButton = new System.Windows.Forms.Button();
            this.InstallerGroupBox = new System.Windows.Forms.GroupBox();
            this.InstallerInstallButton = new System.Windows.Forms.Button();
            this.InstallerRefreshButton = new System.Windows.Forms.Button();
            this.InstallerVersionsListBox = new System.Windows.Forms.ListBox();
            this.InstallerResourcesListBox = new System.Windows.Forms.ListBox();
            this.ConfigurationTabPage = new System.Windows.Forms.TabPage();
            this.ConfigurationSaveButton = new System.Windows.Forms.Button();
            this.ConfigurationGenerateButton = new System.Windows.Forms.Button();
            this.ConfigurationLoadButton = new System.Windows.Forms.Button();
            this.ConfigurationTextBox = new System.Windows.Forms.TextBox();
            this.BuildsTabPage = new System.Windows.Forms.TabPage();
            this.BuildsImportButton = new System.Windows.Forms.Button();
            this.AboutTabPage = new System.Windows.Forms.TabPage();
            this.AboutRichTextBox = new System.Windows.Forms.RichTextBox();
            this.MainProgressBar = new System.Windows.Forms.ProgressBar();
            this.BottomStrip = new System.Windows.Forms.StatusStrip();
            this.BottomToolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.BuildFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.BuildsGroupBox.SuspendLayout();
            this.DataFolderGroupBox.SuspendLayout();
            this.TopMenuStrip.SuspendLayout();
            this.LogsTabPage.SuspendLayout();
            this.MainTabControl.SuspendLayout();
            this.ResourcesTabPage.SuspendLayout();
            this.UninstallerGroupBox.SuspendLayout();
            this.InstallerGroupBox.SuspendLayout();
            this.ConfigurationTabPage.SuspendLayout();
            this.BuildsTabPage.SuspendLayout();
            this.AboutTabPage.SuspendLayout();
            this.BottomStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // BuildsGroupBox
            // 
            this.BuildsGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BuildsGroupBox.Controls.Add(this.BuildsListBox);
            this.BuildsGroupBox.Location = new System.Drawing.Point(6, 6);
            this.BuildsGroupBox.Name = "BuildsGroupBox";
            this.BuildsGroupBox.Size = new System.Drawing.Size(575, 267);
            this.BuildsGroupBox.TabIndex = 0;
            this.BuildsGroupBox.TabStop = false;
            this.BuildsGroupBox.Text = "Builds Available";
            // 
            // BuildsListBox
            // 
            this.BuildsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BuildsListBox.FormattingEnabled = true;
            this.BuildsListBox.Location = new System.Drawing.Point(6, 19);
            this.BuildsListBox.Name = "BuildsListBox";
            this.BuildsListBox.Size = new System.Drawing.Size(563, 238);
            this.BuildsListBox.TabIndex = 2;
            // 
            // BuildsRefreshButton
            // 
            this.BuildsRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BuildsRefreshButton.Location = new System.Drawing.Point(587, 6);
            this.BuildsRefreshButton.Name = "BuildsRefreshButton";
            this.BuildsRefreshButton.Size = new System.Drawing.Size(150, 23);
            this.BuildsRefreshButton.TabIndex = 1;
            this.BuildsRefreshButton.Text = "Refresh";
            this.BuildsRefreshButton.UseVisualStyleBackColor = true;
            this.BuildsRefreshButton.Click += new System.EventHandler(this.BuildsRefreshButton_Click);
            // 
            // DataFolderGroupBox
            // 
            this.DataFolderGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DataFolderGroupBox.Controls.Add(this.DataFolderBrowseButton);
            this.DataFolderGroupBox.Controls.Add(this.DataFolderRefreshButton);
            this.DataFolderGroupBox.Controls.Add(this.DataFolderComboBox);
            this.DataFolderGroupBox.Location = new System.Drawing.Point(36, 12);
            this.DataFolderGroupBox.Name = "DataFolderGroupBox";
            this.DataFolderGroupBox.Size = new System.Drawing.Size(751, 52);
            this.DataFolderGroupBox.TabIndex = 1;
            this.DataFolderGroupBox.TabStop = false;
            this.DataFolderGroupBox.Text = "Data Folder";
            // 
            // DataFolderBrowseButton
            // 
            this.DataFolderBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DataFolderBrowseButton.Location = new System.Drawing.Point(589, 18);
            this.DataFolderBrowseButton.Name = "DataFolderBrowseButton";
            this.DataFolderBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.DataFolderBrowseButton.TabIndex = 2;
            this.DataFolderBrowseButton.Text = "Browse";
            this.DataFolderBrowseButton.UseVisualStyleBackColor = true;
            this.DataFolderBrowseButton.Click += new System.EventHandler(this.DataFolderBrowseButton_Click);
            // 
            // DataFolderRefreshButton
            // 
            this.DataFolderRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DataFolderRefreshButton.Location = new System.Drawing.Point(670, 18);
            this.DataFolderRefreshButton.Name = "DataFolderRefreshButton";
            this.DataFolderRefreshButton.Size = new System.Drawing.Size(75, 23);
            this.DataFolderRefreshButton.TabIndex = 1;
            this.DataFolderRefreshButton.Text = "Refresh";
            this.DataFolderRefreshButton.UseVisualStyleBackColor = true;
            this.DataFolderRefreshButton.Click += new System.EventHandler(this.DataFolderRefreshButton_Click);
            // 
            // DataFolderComboBox
            // 
            this.DataFolderComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataFolderComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DataFolderComboBox.FormattingEnabled = true;
            this.DataFolderComboBox.Location = new System.Drawing.Point(6, 19);
            this.DataFolderComboBox.Name = "DataFolderComboBox";
            this.DataFolderComboBox.Size = new System.Drawing.Size(577, 21);
            this.DataFolderComboBox.TabIndex = 0;
            this.DataFolderComboBox.SelectedIndexChanged += new System.EventHandler(this.DataFolderComboBox_SelectedIndexChanged);
            // 
            // TopMenuStrip
            // 
            this.TopMenuStrip.Dock = System.Windows.Forms.DockStyle.Left;
            this.TopMenuStrip.ImageScalingSize = new System.Drawing.Size(25, 25);
            this.TopMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StartToolStripMenuItem,
            this.StopToolStripMenuItem,
            this.RestartToolStripMenuItem,
            this.CreateToolStripMenuItem,
            this.SettingsToolStripMenuItem,
            this.ExitToolStripMenuItem});
            this.TopMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.TopMenuStrip.Name = "TopMenuStrip";
            this.TopMenuStrip.Padding = new System.Windows.Forms.Padding(0, 2, 0, 2);
            this.TopMenuStrip.Size = new System.Drawing.Size(120, 444);
            this.TopMenuStrip.TabIndex = 2;
            this.TopMenuStrip.Text = "menuStrip1";
            // 
            // StartToolStripMenuItem
            // 
            this.StartToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StartToolStripMenuItem.Image = global::LambentLight.Properties.Resources.Play;
            this.StartToolStripMenuItem.Name = "StartToolStripMenuItem";
            this.StartToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2);
            this.StartToolStripMenuItem.Size = new System.Drawing.Size(119, 33);
            this.StartToolStripMenuItem.Text = "Start Server";
            this.StartToolStripMenuItem.Click += new System.EventHandler(this.StartToolStripMenuItem_Click);
            // 
            // StopToolStripMenuItem
            // 
            this.StopToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.StopToolStripMenuItem.Image = global::LambentLight.Properties.Resources.Stop;
            this.StopToolStripMenuItem.Name = "StopToolStripMenuItem";
            this.StopToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2);
            this.StopToolStripMenuItem.Size = new System.Drawing.Size(119, 33);
            this.StopToolStripMenuItem.Text = "Stop Server";
            this.StopToolStripMenuItem.Click += new System.EventHandler(this.StopToolStripMenuItem_Click);
            // 
            // RestartToolStripMenuItem
            // 
            this.RestartToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.RestartToolStripMenuItem.Image = global::LambentLight.Properties.Resources.Restart;
            this.RestartToolStripMenuItem.Name = "RestartToolStripMenuItem";
            this.RestartToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2);
            this.RestartToolStripMenuItem.Size = new System.Drawing.Size(119, 33);
            this.RestartToolStripMenuItem.Text = "Restart Server";
            this.RestartToolStripMenuItem.Click += new System.EventHandler(this.RestartToolStripMenuItem_Click);
            // 
            // CreateToolStripMenuItem
            // 
            this.CreateToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CreateToolStripMenuItem.Image = global::LambentLight.Properties.Resources.Add;
            this.CreateToolStripMenuItem.Name = "CreateToolStripMenuItem";
            this.CreateToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2);
            this.CreateToolStripMenuItem.Size = new System.Drawing.Size(119, 33);
            this.CreateToolStripMenuItem.Text = "Create Data Folder";
            this.CreateToolStripMenuItem.Click += new System.EventHandler(this.CreateToolStripMenuItem_Click);
            // 
            // SettingsToolStripMenuItem
            // 
            this.SettingsToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SettingsToolStripMenuItem.Image = global::LambentLight.Properties.Resources.Settings;
            this.SettingsToolStripMenuItem.Name = "SettingsToolStripMenuItem";
            this.SettingsToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2);
            this.SettingsToolStripMenuItem.Size = new System.Drawing.Size(119, 33);
            this.SettingsToolStripMenuItem.Text = "Application Settings";
            this.SettingsToolStripMenuItem.Click += new System.EventHandler(this.SettingsToolStripMenuItem_Click);
            // 
            // ExitToolStripMenuItem
            // 
            this.ExitToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ExitToolStripMenuItem.Image = global::LambentLight.Properties.Resources.Exit;
            this.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem";
            this.ExitToolStripMenuItem.Padding = new System.Windows.Forms.Padding(2);
            this.ExitToolStripMenuItem.Size = new System.Drawing.Size(119, 33);
            this.ExitToolStripMenuItem.Text = "Exit";
            this.ExitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // LogsTabPage
            // 
            this.LogsTabPage.Controls.Add(this.ClearLogButton);
            this.LogsTabPage.Controls.Add(this.ConsoleButton);
            this.LogsTabPage.Controls.Add(this.ConsoleTextBox);
            this.LogsTabPage.Controls.Add(this.LogsTextBox);
            this.LogsTabPage.Location = new System.Drawing.Point(4, 22);
            this.LogsTabPage.Name = "LogsTabPage";
            this.LogsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.LogsTabPage.Size = new System.Drawing.Size(743, 294);
            this.LogsTabPage.TabIndex = 0;
            this.LogsTabPage.Text = "Logs";
            this.LogsTabPage.UseVisualStyleBackColor = true;
            // 
            // ClearLogButton
            // 
            this.ClearLogButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ClearLogButton.Location = new System.Drawing.Point(6, 261);
            this.ClearLogButton.Name = "ClearLogButton";
            this.ClearLogButton.Size = new System.Drawing.Size(75, 23);
            this.ClearLogButton.TabIndex = 3;
            this.ClearLogButton.Text = "Clear Log";
            this.ClearLogButton.UseVisualStyleBackColor = true;
            this.ClearLogButton.Click += new System.EventHandler(this.ClearLogButton_Click);
            // 
            // ConsoleButton
            // 
            this.ConsoleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ConsoleButton.Location = new System.Drawing.Point(662, 261);
            this.ConsoleButton.Name = "ConsoleButton";
            this.ConsoleButton.Size = new System.Drawing.Size(75, 23);
            this.ConsoleButton.TabIndex = 2;
            this.ConsoleButton.Text = "Send";
            this.ConsoleButton.UseVisualStyleBackColor = true;
            this.ConsoleButton.Click += new System.EventHandler(this.ConsoleButton_Click);
            // 
            // ConsoleTextBox
            // 
            this.ConsoleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConsoleTextBox.Location = new System.Drawing.Point(87, 263);
            this.ConsoleTextBox.Name = "ConsoleTextBox";
            this.ConsoleTextBox.Size = new System.Drawing.Size(569, 20);
            this.ConsoleTextBox.TabIndex = 1;
            // 
            // LogsTextBox
            // 
            this.LogsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LogsTextBox.Location = new System.Drawing.Point(6, 6);
            this.LogsTextBox.Multiline = true;
            this.LogsTextBox.Name = "LogsTextBox";
            this.LogsTextBox.ReadOnly = true;
            this.LogsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.LogsTextBox.Size = new System.Drawing.Size(731, 251);
            this.LogsTextBox.TabIndex = 0;
            // 
            // MainTabControl
            // 
            this.MainTabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainTabControl.Controls.Add(this.LogsTabPage);
            this.MainTabControl.Controls.Add(this.ResourcesTabPage);
            this.MainTabControl.Controls.Add(this.ConfigurationTabPage);
            this.MainTabControl.Controls.Add(this.BuildsTabPage);
            this.MainTabControl.Controls.Add(this.AboutTabPage);
            this.MainTabControl.Location = new System.Drawing.Point(36, 70);
            this.MainTabControl.Name = "MainTabControl";
            this.MainTabControl.SelectedIndex = 0;
            this.MainTabControl.Size = new System.Drawing.Size(751, 320);
            this.MainTabControl.TabIndex = 3;
            this.MainTabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.MainTabControl_Selected);
            // 
            // ResourcesTabPage
            // 
            this.ResourcesTabPage.Controls.Add(this.UninstallerGroupBox);
            this.ResourcesTabPage.Controls.Add(this.InstallerGroupBox);
            this.ResourcesTabPage.Location = new System.Drawing.Point(4, 22);
            this.ResourcesTabPage.Name = "ResourcesTabPage";
            this.ResourcesTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.ResourcesTabPage.Size = new System.Drawing.Size(743, 294);
            this.ResourcesTabPage.TabIndex = 2;
            this.ResourcesTabPage.Text = "Resources";
            this.ResourcesTabPage.UseVisualStyleBackColor = true;
            // 
            // UninstallerGroupBox
            // 
            this.UninstallerGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.UninstallerGroupBox.Controls.Add(this.UninstallerRefreshButton);
            this.UninstallerGroupBox.Controls.Add(this.UninstallerListBox);
            this.UninstallerGroupBox.Controls.Add(this.UninstallerRemoveButton);
            this.UninstallerGroupBox.Location = new System.Drawing.Point(6, 6);
            this.UninstallerGroupBox.Name = "UninstallerGroupBox";
            this.UninstallerGroupBox.Size = new System.Drawing.Size(262, 267);
            this.UninstallerGroupBox.TabIndex = 2;
            this.UninstallerGroupBox.TabStop = false;
            this.UninstallerGroupBox.Text = "Installed";
            // 
            // UninstallerRefreshButton
            // 
            this.UninstallerRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.UninstallerRefreshButton.Location = new System.Drawing.Point(6, 238);
            this.UninstallerRefreshButton.Name = "UninstallerRefreshButton";
            this.UninstallerRefreshButton.Size = new System.Drawing.Size(122, 23);
            this.UninstallerRefreshButton.TabIndex = 2;
            this.UninstallerRefreshButton.Text = "Refresh";
            this.UninstallerRefreshButton.UseVisualStyleBackColor = true;
            this.UninstallerRefreshButton.Click += new System.EventHandler(this.UninstallerRefreshButton_Click);
            // 
            // UninstallerListBox
            // 
            this.UninstallerListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UninstallerListBox.FormattingEnabled = true;
            this.UninstallerListBox.Location = new System.Drawing.Point(6, 19);
            this.UninstallerListBox.Name = "UninstallerListBox";
            this.UninstallerListBox.Size = new System.Drawing.Size(250, 212);
            this.UninstallerListBox.TabIndex = 1;
            this.UninstallerListBox.SelectedIndexChanged += new System.EventHandler(this.UninstallerListBox_SelectedIndexChanged);
            // 
            // UninstallerRemoveButton
            // 
            this.UninstallerRemoveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.UninstallerRemoveButton.Enabled = false;
            this.UninstallerRemoveButton.Location = new System.Drawing.Point(134, 238);
            this.UninstallerRemoveButton.Name = "UninstallerRemoveButton";
            this.UninstallerRemoveButton.Size = new System.Drawing.Size(122, 23);
            this.UninstallerRemoveButton.TabIndex = 0;
            this.UninstallerRemoveButton.Text = "Uninstall";
            this.UninstallerRemoveButton.UseVisualStyleBackColor = true;
            this.UninstallerRemoveButton.Click += new System.EventHandler(this.UninstallerRemoveButton_Click);
            // 
            // InstallerGroupBox
            // 
            this.InstallerGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InstallerGroupBox.Controls.Add(this.InstallerInstallButton);
            this.InstallerGroupBox.Controls.Add(this.InstallerRefreshButton);
            this.InstallerGroupBox.Controls.Add(this.InstallerVersionsListBox);
            this.InstallerGroupBox.Controls.Add(this.InstallerResourcesListBox);
            this.InstallerGroupBox.Location = new System.Drawing.Point(281, 6);
            this.InstallerGroupBox.Name = "InstallerGroupBox";
            this.InstallerGroupBox.Size = new System.Drawing.Size(456, 267);
            this.InstallerGroupBox.TabIndex = 0;
            this.InstallerGroupBox.TabStop = false;
            this.InstallerGroupBox.Text = "Available for Installation";
            // 
            // InstallerInstallButton
            // 
            this.InstallerInstallButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.InstallerInstallButton.Enabled = false;
            this.InstallerInstallButton.Location = new System.Drawing.Point(262, 238);
            this.InstallerInstallButton.Name = "InstallerInstallButton";
            this.InstallerInstallButton.Size = new System.Drawing.Size(188, 23);
            this.InstallerInstallButton.TabIndex = 0;
            this.InstallerInstallButton.Text = "Install";
            this.InstallerInstallButton.UseVisualStyleBackColor = true;
            this.InstallerInstallButton.Click += new System.EventHandler(this.InstallerInstallButton_Click);
            // 
            // InstallerRefreshButton
            // 
            this.InstallerRefreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InstallerRefreshButton.Location = new System.Drawing.Point(6, 238);
            this.InstallerRefreshButton.Name = "InstallerRefreshButton";
            this.InstallerRefreshButton.Size = new System.Drawing.Size(250, 23);
            this.InstallerRefreshButton.TabIndex = 2;
            this.InstallerRefreshButton.Text = "Refresh";
            this.InstallerRefreshButton.UseVisualStyleBackColor = true;
            this.InstallerRefreshButton.Click += new System.EventHandler(this.InstallerRefreshButton_Click);
            // 
            // InstallerVersionsListBox
            // 
            this.InstallerVersionsListBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InstallerVersionsListBox.FormattingEnabled = true;
            this.InstallerVersionsListBox.Location = new System.Drawing.Point(262, 19);
            this.InstallerVersionsListBox.Name = "InstallerVersionsListBox";
            this.InstallerVersionsListBox.Size = new System.Drawing.Size(188, 212);
            this.InstallerVersionsListBox.TabIndex = 1;
            this.InstallerVersionsListBox.SelectedIndexChanged += new System.EventHandler(this.InstallerVersionsListBox_SelectedIndexChanged);
            // 
            // InstallerResourcesListBox
            // 
            this.InstallerResourcesListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.InstallerResourcesListBox.FormattingEnabled = true;
            this.InstallerResourcesListBox.Location = new System.Drawing.Point(6, 19);
            this.InstallerResourcesListBox.Name = "InstallerResourcesListBox";
            this.InstallerResourcesListBox.Size = new System.Drawing.Size(250, 212);
            this.InstallerResourcesListBox.TabIndex = 0;
            this.InstallerResourcesListBox.SelectedIndexChanged += new System.EventHandler(this.InstallerResourcesListBox_SelectedIndexChanged);
            // 
            // ConfigurationTabPage
            // 
            this.ConfigurationTabPage.Controls.Add(this.ConfigurationSaveButton);
            this.ConfigurationTabPage.Controls.Add(this.ConfigurationGenerateButton);
            this.ConfigurationTabPage.Controls.Add(this.ConfigurationLoadButton);
            this.ConfigurationTabPage.Controls.Add(this.ConfigurationTextBox);
            this.ConfigurationTabPage.Location = new System.Drawing.Point(4, 22);
            this.ConfigurationTabPage.Name = "ConfigurationTabPage";
            this.ConfigurationTabPage.Size = new System.Drawing.Size(743, 294);
            this.ConfigurationTabPage.TabIndex = 3;
            this.ConfigurationTabPage.Text = "Server Configuration";
            this.ConfigurationTabPage.UseVisualStyleBackColor = true;
            // 
            // ConfigurationSaveButton
            // 
            this.ConfigurationSaveButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigurationSaveButton.Location = new System.Drawing.Point(665, 61);
            this.ConfigurationSaveButton.Name = "ConfigurationSaveButton";
            this.ConfigurationSaveButton.Size = new System.Drawing.Size(75, 23);
            this.ConfigurationSaveButton.TabIndex = 3;
            this.ConfigurationSaveButton.Text = "Save";
            this.ConfigurationSaveButton.UseVisualStyleBackColor = true;
            this.ConfigurationSaveButton.Click += new System.EventHandler(this.ConfigurationSaveButton_Click);
            // 
            // ConfigurationGenerateButton
            // 
            this.ConfigurationGenerateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigurationGenerateButton.Location = new System.Drawing.Point(665, 32);
            this.ConfigurationGenerateButton.Name = "ConfigurationGenerateButton";
            this.ConfigurationGenerateButton.Size = new System.Drawing.Size(75, 23);
            this.ConfigurationGenerateButton.TabIndex = 2;
            this.ConfigurationGenerateButton.Text = "Generate";
            this.ConfigurationGenerateButton.UseVisualStyleBackColor = true;
            this.ConfigurationGenerateButton.Click += new System.EventHandler(this.ConfigurationGenerateButton_Click);
            // 
            // ConfigurationLoadButton
            // 
            this.ConfigurationLoadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigurationLoadButton.Location = new System.Drawing.Point(665, 3);
            this.ConfigurationLoadButton.Name = "ConfigurationLoadButton";
            this.ConfigurationLoadButton.Size = new System.Drawing.Size(75, 23);
            this.ConfigurationLoadButton.TabIndex = 1;
            this.ConfigurationLoadButton.Text = "Load";
            this.ConfigurationLoadButton.UseVisualStyleBackColor = true;
            this.ConfigurationLoadButton.Click += new System.EventHandler(this.ConfigurationLoadButton_Click);
            // 
            // ConfigurationTextBox
            // 
            this.ConfigurationTextBox.AcceptsReturn = true;
            this.ConfigurationTextBox.AcceptsTab = true;
            this.ConfigurationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigurationTextBox.Location = new System.Drawing.Point(2, 3);
            this.ConfigurationTextBox.Multiline = true;
            this.ConfigurationTextBox.Name = "ConfigurationTextBox";
            this.ConfigurationTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ConfigurationTextBox.Size = new System.Drawing.Size(657, 265);
            this.ConfigurationTextBox.TabIndex = 0;
            this.ConfigurationTextBox.WordWrap = false;
            // 
            // BuildsTabPage
            // 
            this.BuildsTabPage.Controls.Add(this.BuildsImportButton);
            this.BuildsTabPage.Controls.Add(this.BuildsRefreshButton);
            this.BuildsTabPage.Controls.Add(this.BuildsGroupBox);
            this.BuildsTabPage.Location = new System.Drawing.Point(4, 22);
            this.BuildsTabPage.Name = "BuildsTabPage";
            this.BuildsTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.BuildsTabPage.Size = new System.Drawing.Size(743, 294);
            this.BuildsTabPage.TabIndex = 5;
            this.BuildsTabPage.Text = "Build Selector";
            this.BuildsTabPage.UseVisualStyleBackColor = true;
            // 
            // BuildsImportButton
            // 
            this.BuildsImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BuildsImportButton.Location = new System.Drawing.Point(587, 35);
            this.BuildsImportButton.Name = "BuildsImportButton";
            this.BuildsImportButton.Size = new System.Drawing.Size(150, 23);
            this.BuildsImportButton.TabIndex = 2;
            this.BuildsImportButton.Text = "Import Build from ZIP";
            this.BuildsImportButton.UseVisualStyleBackColor = true;
            this.BuildsImportButton.Click += new System.EventHandler(this.BuildsImportButton_Click);
            // 
            // AboutTabPage
            // 
            this.AboutTabPage.Controls.Add(this.AboutRichTextBox);
            this.AboutTabPage.Location = new System.Drawing.Point(4, 22);
            this.AboutTabPage.Name = "AboutTabPage";
            this.AboutTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.AboutTabPage.Size = new System.Drawing.Size(743, 294);
            this.AboutTabPage.TabIndex = 4;
            this.AboutTabPage.Text = "About";
            this.AboutTabPage.UseVisualStyleBackColor = true;
            // 
            // AboutRichTextBox
            // 
            this.AboutRichTextBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.AboutRichTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.AboutRichTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.AboutRichTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AboutRichTextBox.Location = new System.Drawing.Point(3, 3);
            this.AboutRichTextBox.Name = "AboutRichTextBox";
            this.AboutRichTextBox.ReadOnly = true;
            this.AboutRichTextBox.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.None;
            this.AboutRichTextBox.Size = new System.Drawing.Size(737, 288);
            this.AboutRichTextBox.TabIndex = 0;
            this.AboutRichTextBox.Text = "";
            // 
            // MainProgressBar
            // 
            this.MainProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainProgressBar.Location = new System.Drawing.Point(36, 396);
            this.MainProgressBar.Name = "MainProgressBar";
            this.MainProgressBar.Size = new System.Drawing.Size(751, 23);
            this.MainProgressBar.TabIndex = 4;
            // 
            // BottomStrip
            // 
            this.BottomStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BottomToolStripStatusLabel});
            this.BottomStrip.Location = new System.Drawing.Point(120, 422);
            this.BottomStrip.Name = "BottomStrip";
            this.BottomStrip.Size = new System.Drawing.Size(679, 22);
            this.BottomStrip.TabIndex = 5;
            // 
            // BottomToolStripStatusLabel
            // 
            this.BottomToolStripStatusLabel.Name = "BottomToolStripStatusLabel";
            this.BottomToolStripStatusLabel.Size = new System.Drawing.Size(151, 17);
            this.BottomToolStripStatusLabel.Text = "Welcome to LambentLight!";
            // 
            // BuildFileDialog
            // 
            this.BuildFileDialog.Filter = "All Supported Compressed Files|*.zip";
            // 
            // Landing
            // 
            this.AcceptButton = this.ConsoleButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(799, 444);
            this.Controls.Add(this.BottomStrip);
            this.Controls.Add(this.MainProgressBar);
            this.Controls.Add(this.MainTabControl);
            this.Controls.Add(this.DataFolderGroupBox);
            this.Controls.Add(this.TopMenuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.TopMenuStrip;
            this.MinimumSize = new System.Drawing.Size(815, 483);
            this.Name = "Landing";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LambentLight: A FiveM Server Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Landing_FormClosing);
            this.Load += new System.EventHandler(this.Landing_Load);
            this.BuildsGroupBox.ResumeLayout(false);
            this.DataFolderGroupBox.ResumeLayout(false);
            this.TopMenuStrip.ResumeLayout(false);
            this.TopMenuStrip.PerformLayout();
            this.LogsTabPage.ResumeLayout(false);
            this.LogsTabPage.PerformLayout();
            this.MainTabControl.ResumeLayout(false);
            this.ResourcesTabPage.ResumeLayout(false);
            this.UninstallerGroupBox.ResumeLayout(false);
            this.InstallerGroupBox.ResumeLayout(false);
            this.ConfigurationTabPage.ResumeLayout(false);
            this.ConfigurationTabPage.PerformLayout();
            this.BuildsTabPage.ResumeLayout(false);
            this.AboutTabPage.ResumeLayout(false);
            this.BottomStrip.ResumeLayout(false);
            this.BottomStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox BuildsGroupBox;
        private System.Windows.Forms.GroupBox DataFolderGroupBox;
        private System.Windows.Forms.ComboBox DataFolderComboBox;
        private System.Windows.Forms.MenuStrip TopMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem StartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StopToolStripMenuItem;
        private System.Windows.Forms.TabPage LogsTabPage;
        private System.Windows.Forms.TabControl MainTabControl;
        private System.Windows.Forms.ToolStripMenuItem CreateToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ExitToolStripMenuItem;
        private System.Windows.Forms.Button BuildsRefreshButton;
        private System.Windows.Forms.Button DataFolderRefreshButton;
        private System.Windows.Forms.TabPage ResourcesTabPage;
        private System.Windows.Forms.GroupBox InstallerGroupBox;
        private System.Windows.Forms.Button InstallerInstallButton;
        private System.Windows.Forms.ListBox InstallerVersionsListBox;
        private System.Windows.Forms.ListBox InstallerResourcesListBox;
        private System.Windows.Forms.Button InstallerRefreshButton;
        public System.Windows.Forms.TextBox LogsTextBox;
        public System.Windows.Forms.ProgressBar MainProgressBar;
        private System.Windows.Forms.StatusStrip BottomStrip;
        public System.Windows.Forms.ToolStripStatusLabel BottomToolStripStatusLabel;
        private System.Windows.Forms.TabPage ConfigurationTabPage;
        private System.Windows.Forms.Button ConfigurationLoadButton;
        private System.Windows.Forms.TextBox ConfigurationTextBox;
        private System.Windows.Forms.Button ConfigurationGenerateButton;
        private System.Windows.Forms.Button ConfigurationSaveButton;
        private System.Windows.Forms.Button ConsoleButton;
        private System.Windows.Forms.TextBox ConsoleTextBox;
        private System.Windows.Forms.TabPage AboutTabPage;
        private System.Windows.Forms.RichTextBox AboutRichTextBox;
        private System.Windows.Forms.GroupBox UninstallerGroupBox;
        private System.Windows.Forms.Button UninstallerRemoveButton;
        private System.Windows.Forms.ListBox UninstallerListBox;
        private System.Windows.Forms.Button UninstallerRefreshButton;
        private System.Windows.Forms.ToolStripMenuItem SettingsToolStripMenuItem;
        private System.Windows.Forms.TabPage BuildsTabPage;
        private System.Windows.Forms.ListBox BuildsListBox;
        private System.Windows.Forms.Button DataFolderBrowseButton;
        private System.Windows.Forms.Button ClearLogButton;
        private System.Windows.Forms.Button BuildsImportButton;
        private System.Windows.Forms.OpenFileDialog BuildFileDialog;
        private System.Windows.Forms.ToolStripMenuItem RestartToolStripMenuItem;
    }
}

