namespace Droppable
{
    partial class frmSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSettings));
            this.themeMain = new Droppable.Theme.WhiteScaleTheme();
            this.btnClose = new Droppable.Theme.WhiteScaleButton();
            this.btnSave = new Droppable.Theme.WhiteScaleButton();
            this.cbMain = new Droppable.Theme.WhiteScaleControlBox();
            this.dnbtbMain = new DotNetBarTabControl();
            this.tbSearchSettings = new System.Windows.Forms.TabPage();
            this.gbMisc = new System.Windows.Forms.GroupBox();
            this.nudMinimumDownloadSize = new System.Windows.Forms.NumericUpDown();
            this.lblMinimumSizeKB = new System.Windows.Forms.Label();
            this.cbFastSearch = new System.Windows.Forms.CheckBox();
            this.gbMatching = new System.Windows.Forms.GroupBox();
            this.rbNoMatch = new System.Windows.Forms.RadioButton();
            this.rbPartialMatch = new System.Windows.Forms.RadioButton();
            this.rbExactMatch = new System.Windows.Forms.RadioButton();
            this.gbFiltering = new System.Windows.Forms.GroupBox();
            this.cbFilterNightcore = new System.Windows.Forms.CheckBox();
            this.cbFilterRemixes = new System.Windows.Forms.CheckBox();
            this.cbFilterCovers = new System.Windows.Forms.CheckBox();
            this.lblSearchSettings = new System.Windows.Forms.Label();
            this.tbProgramSettings = new System.Windows.Forms.TabPage();
            this.gbEngineSettings = new System.Windows.Forms.GroupBox();
            this.cbSkipEngineCheck = new System.Windows.Forms.CheckBox();
            this.nudEngineTimeout = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblProgramSettings = new System.Windows.Forms.Label();
            this.tbPlugins = new System.Windows.Forms.TabPage();
            this.themeMain.SuspendLayout();
            this.dnbtbMain.SuspendLayout();
            this.tbSearchSettings.SuspendLayout();
            this.gbMisc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinimumDownloadSize)).BeginInit();
            this.gbMatching.SuspendLayout();
            this.gbFiltering.SuspendLayout();
            this.tbProgramSettings.SuspendLayout();
            this.gbEngineSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEngineTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // themeMain
            // 
            this.themeMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.themeMain.BorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.themeMain.Controls.Add(this.btnClose);
            this.themeMain.Controls.Add(this.btnSave);
            this.themeMain.Controls.Add(this.cbMain);
            this.themeMain.Controls.Add(this.dnbtbMain);
            this.themeMain.Customization = "0tLS//////8AAAD/";
            this.themeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.themeMain.Font = new System.Drawing.Font("Tahoma", 9F);
            this.themeMain.Image = null;
            this.themeMain.Location = new System.Drawing.Point(0, 0);
            this.themeMain.Movable = true;
            this.themeMain.Name = "themeMain";
            this.themeMain.NoRounding = false;
            this.themeMain.ShowFooter = false;
            this.themeMain.Sizable = false;
            this.themeMain.Size = new System.Drawing.Size(532, 387);
            this.themeMain.SmartBounds = true;
            this.themeMain.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.themeMain.TabIndex = 0;
            this.themeMain.Text = "whiteScaleTheme1";
            this.themeMain.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.themeMain.Transparent = false;
            // 
            // btnClose
            // 
            this.btnClose.Customization = "+vr6//Dw8P//////AAAA/9PT0/8=";
            this.btnClose.Font = new System.Drawing.Font("Verdana", 8F);
            this.btnClose.Image = null;
            this.btnClose.Location = new System.Drawing.Point(6, 357);
            this.btnClose.Name = "btnClose";
            this.btnClose.NoRounding = false;
            this.btnClose.Size = new System.Drawing.Size(260, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "Close";
            this.btnClose.Transparent = false;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Customization = "+vr6//Dw8P//////AAAA/9PT0/8=";
            this.btnSave.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Image = null;
            this.btnSave.Location = new System.Drawing.Point(265, 357);
            this.btnSave.Name = "btnSave";
            this.btnSave.NoRounding = false;
            this.btnSave.Size = new System.Drawing.Size(260, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Save";
            this.btnSave.Transparent = false;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // cbMain
            // 
            this.cbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbMain.BackColor = System.Drawing.Color.Transparent;
            this.cbMain.BorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.cbMain.Colors = new Bloom[0];
            this.cbMain.Customization = "";
            this.cbMain.Font = new System.Drawing.Font("Verdana", 8F);
            this.cbMain.Image = null;
            this.cbMain.Location = new System.Drawing.Point(450, 4);
            this.cbMain.Movable = true;
            this.cbMain.Name = "cbMain";
            this.cbMain.NoRounding = false;
            this.cbMain.SettingsButtonVisible = false;
            this.cbMain.Sizable = false;
            this.cbMain.Size = new System.Drawing.Size(72, 20);
            this.cbMain.SmartBounds = true;
            this.cbMain.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.cbMain.TabIndex = 0;
            this.cbMain.Text = "whiteScaleControlBox1";
            this.cbMain.TransparencyKey = System.Drawing.Color.Empty;
            this.cbMain.Transparent = true;
            // 
            // dnbtbMain
            // 
            this.dnbtbMain.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.dnbtbMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dnbtbMain.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(187)))), ((int)(((byte)(204)))));
            this.dnbtbMain.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(127)))), ((int)(((byte)(207)))));
            this.dnbtbMain.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(147)))), ((int)(((byte)(227)))));
            this.dnbtbMain.Color3 = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(248)))), ((int)(((byte)(252)))));
            this.dnbtbMain.Controls.Add(this.tbSearchSettings);
            this.dnbtbMain.Controls.Add(this.tbProgramSettings);
            this.dnbtbMain.Controls.Add(this.tbPlugins);
            this.dnbtbMain.ItemSize = new System.Drawing.Size(44, 136);
            this.dnbtbMain.Location = new System.Drawing.Point(6, 26);
            this.dnbtbMain.Multiline = true;
            this.dnbtbMain.Name = "dnbtbMain";
            this.dnbtbMain.SelectedIndex = 0;
            this.dnbtbMain.ShowOuterBorders = false;
            this.dnbtbMain.Size = new System.Drawing.Size(519, 331);
            this.dnbtbMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.dnbtbMain.TabIndex = 0;
            // 
            // tbSearchSettings
            // 
            this.tbSearchSettings.BackColor = System.Drawing.Color.White;
            this.tbSearchSettings.Controls.Add(this.gbMisc);
            this.tbSearchSettings.Controls.Add(this.gbMatching);
            this.tbSearchSettings.Controls.Add(this.gbFiltering);
            this.tbSearchSettings.Controls.Add(this.lblSearchSettings);
            this.tbSearchSettings.Location = new System.Drawing.Point(140, 4);
            this.tbSearchSettings.Name = "tbSearchSettings";
            this.tbSearchSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tbSearchSettings.Size = new System.Drawing.Size(375, 323);
            this.tbSearchSettings.TabIndex = 0;
            this.tbSearchSettings.Text = "Search Settings";
            // 
            // gbMisc
            // 
            this.gbMisc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMisc.Controls.Add(this.nudMinimumDownloadSize);
            this.gbMisc.Controls.Add(this.lblMinimumSizeKB);
            this.gbMisc.Controls.Add(this.cbFastSearch);
            this.gbMisc.Location = new System.Drawing.Point(10, 211);
            this.gbMisc.Name = "gbMisc";
            this.gbMisc.Size = new System.Drawing.Size(355, 106);
            this.gbMisc.TabIndex = 5;
            this.gbMisc.TabStop = false;
            this.gbMisc.Text = "Miscellaneous";
            // 
            // nudMinimumDownloadSize
            // 
            this.nudMinimumDownloadSize.Location = new System.Drawing.Point(182, 40);
            this.nudMinimumDownloadSize.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.nudMinimumDownloadSize.Name = "nudMinimumDownloadSize";
            this.nudMinimumDownloadSize.Size = new System.Drawing.Size(167, 22);
            this.nudMinimumDownloadSize.TabIndex = 5;
            this.nudMinimumDownloadSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lblMinimumSizeKB
            // 
            this.lblMinimumSizeKB.AutoSize = true;
            this.lblMinimumSizeKB.Location = new System.Drawing.Point(7, 42);
            this.lblMinimumSizeKB.Name = "lblMinimumSizeKB";
            this.lblMinimumSizeKB.Size = new System.Drawing.Size(169, 14);
            this.lblMinimumSizeKB.TabIndex = 4;
            this.lblMinimumSizeKB.Text = "Minimum Download Size (KB):";
            // 
            // cbFastSearch
            // 
            this.cbFastSearch.AutoSize = true;
            this.cbFastSearch.Location = new System.Drawing.Point(10, 21);
            this.cbFastSearch.Name = "cbFastSearch";
            this.cbFastSearch.Size = new System.Drawing.Size(89, 18);
            this.cbFastSearch.TabIndex = 3;
            this.cbFastSearch.Text = "Fast Search";
            this.cbFastSearch.UseVisualStyleBackColor = true;
            // 
            // gbMatching
            // 
            this.gbMatching.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbMatching.Controls.Add(this.rbNoMatch);
            this.gbMatching.Controls.Add(this.rbPartialMatch);
            this.gbMatching.Controls.Add(this.rbExactMatch);
            this.gbMatching.Location = new System.Drawing.Point(10, 139);
            this.gbMatching.Name = "gbMatching";
            this.gbMatching.Size = new System.Drawing.Size(355, 66);
            this.gbMatching.TabIndex = 4;
            this.gbMatching.TabStop = false;
            this.gbMatching.Text = "Matching";
            // 
            // rbNoMatch
            // 
            this.rbNoMatch.AutoSize = true;
            this.rbNoMatch.Location = new System.Drawing.Point(121, 31);
            this.rbNoMatch.Name = "rbNoMatch";
            this.rbNoMatch.Size = new System.Drawing.Size(77, 18);
            this.rbNoMatch.TabIndex = 2;
            this.rbNoMatch.TabStop = true;
            this.rbNoMatch.Text = "No Match";
            this.rbNoMatch.UseVisualStyleBackColor = true;
            // 
            // rbPartialMatch
            // 
            this.rbPartialMatch.AutoSize = true;
            this.rbPartialMatch.Location = new System.Drawing.Point(10, 39);
            this.rbPartialMatch.Name = "rbPartialMatch";
            this.rbPartialMatch.Size = new System.Drawing.Size(94, 18);
            this.rbPartialMatch.TabIndex = 1;
            this.rbPartialMatch.TabStop = true;
            this.rbPartialMatch.Text = "Partial Match";
            this.rbPartialMatch.UseVisualStyleBackColor = true;
            // 
            // rbExactMatch
            // 
            this.rbExactMatch.AutoSize = true;
            this.rbExactMatch.Location = new System.Drawing.Point(10, 21);
            this.rbExactMatch.Name = "rbExactMatch";
            this.rbExactMatch.Size = new System.Drawing.Size(92, 18);
            this.rbExactMatch.TabIndex = 0;
            this.rbExactMatch.TabStop = true;
            this.rbExactMatch.Text = "Exact Match";
            this.rbExactMatch.UseVisualStyleBackColor = true;
            // 
            // gbFiltering
            // 
            this.gbFiltering.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFiltering.Controls.Add(this.cbFilterNightcore);
            this.gbFiltering.Controls.Add(this.cbFilterRemixes);
            this.gbFiltering.Controls.Add(this.cbFilterCovers);
            this.gbFiltering.Location = new System.Drawing.Point(10, 48);
            this.gbFiltering.Name = "gbFiltering";
            this.gbFiltering.Size = new System.Drawing.Size(355, 85);
            this.gbFiltering.TabIndex = 3;
            this.gbFiltering.TabStop = false;
            this.gbFiltering.Text = "Filtering";
            // 
            // cbFilterNightcore
            // 
            this.cbFilterNightcore.AutoSize = true;
            this.cbFilterNightcore.Location = new System.Drawing.Point(10, 57);
            this.cbFilterNightcore.Name = "cbFilterNightcore";
            this.cbFilterNightcore.Size = new System.Drawing.Size(109, 18);
            this.cbFilterNightcore.TabIndex = 2;
            this.cbFilterNightcore.Text = "Filter Nightcore";
            this.cbFilterNightcore.UseVisualStyleBackColor = true;
            // 
            // cbFilterRemixes
            // 
            this.cbFilterRemixes.AutoSize = true;
            this.cbFilterRemixes.Location = new System.Drawing.Point(10, 39);
            this.cbFilterRemixes.Name = "cbFilterRemixes";
            this.cbFilterRemixes.Size = new System.Drawing.Size(100, 18);
            this.cbFilterRemixes.TabIndex = 1;
            this.cbFilterRemixes.Text = "Filter Remixes";
            this.cbFilterRemixes.UseVisualStyleBackColor = true;
            // 
            // cbFilterCovers
            // 
            this.cbFilterCovers.AutoSize = true;
            this.cbFilterCovers.Location = new System.Drawing.Point(10, 21);
            this.cbFilterCovers.Name = "cbFilterCovers";
            this.cbFilterCovers.Size = new System.Drawing.Size(92, 18);
            this.cbFilterCovers.TabIndex = 0;
            this.cbFilterCovers.Text = "Filter Covers";
            this.cbFilterCovers.UseVisualStyleBackColor = true;
            // 
            // lblSearchSettings
            // 
            this.lblSearchSettings.AutoSize = true;
            this.lblSearchSettings.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearchSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(225)))));
            this.lblSearchSettings.Location = new System.Drawing.Point(8, 7);
            this.lblSearchSettings.Name = "lblSearchSettings";
            this.lblSearchSettings.Size = new System.Drawing.Size(189, 32);
            this.lblSearchSettings.TabIndex = 0;
            this.lblSearchSettings.Text = "Search Settings";
            // 
            // tbProgramSettings
            // 
            this.tbProgramSettings.BackColor = System.Drawing.Color.White;
            this.tbProgramSettings.Controls.Add(this.gbEngineSettings);
            this.tbProgramSettings.Controls.Add(this.lblProgramSettings);
            this.tbProgramSettings.Location = new System.Drawing.Point(140, 4);
            this.tbProgramSettings.Name = "tbProgramSettings";
            this.tbProgramSettings.Padding = new System.Windows.Forms.Padding(3);
            this.tbProgramSettings.Size = new System.Drawing.Size(375, 323);
            this.tbProgramSettings.TabIndex = 1;
            this.tbProgramSettings.Text = "Program Settings";
            // 
            // gbEngineSettings
            // 
            this.gbEngineSettings.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbEngineSettings.Controls.Add(this.cbSkipEngineCheck);
            this.gbEngineSettings.Controls.Add(this.nudEngineTimeout);
            this.gbEngineSettings.Controls.Add(this.label1);
            this.gbEngineSettings.Location = new System.Drawing.Point(10, 48);
            this.gbEngineSettings.Name = "gbEngineSettings";
            this.gbEngineSettings.Size = new System.Drawing.Size(355, 85);
            this.gbEngineSettings.TabIndex = 4;
            this.gbEngineSettings.TabStop = false;
            this.gbEngineSettings.Text = "Engine Settings";
            // 
            // cbSkipEngineCheck
            // 
            this.cbSkipEngineCheck.AutoSize = true;
            this.cbSkipEngineCheck.Location = new System.Drawing.Point(9, 44);
            this.cbSkipEngineCheck.Name = "cbSkipEngineCheck";
            this.cbSkipEngineCheck.Size = new System.Drawing.Size(126, 18);
            this.cbSkipEngineCheck.TabIndex = 8;
            this.cbSkipEngineCheck.Text = "Skip Engine Check";
            this.cbSkipEngineCheck.UseVisualStyleBackColor = true;
            // 
            // nudEngineTimeout
            // 
            this.nudEngineTimeout.Location = new System.Drawing.Point(181, 20);
            this.nudEngineTimeout.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nudEngineTimeout.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudEngineTimeout.Name = "nudEngineTimeout";
            this.nudEngineTimeout.Size = new System.Drawing.Size(167, 22);
            this.nudEngineTimeout.TabIndex = 7;
            this.nudEngineTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nudEngineTimeout.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 14);
            this.label1.TabIndex = 6;
            this.label1.Text = "Engine Timeout (s):\r\n";
            // 
            // lblProgramSettings
            // 
            this.lblProgramSettings.AutoSize = true;
            this.lblProgramSettings.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProgramSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(100)))), ((int)(((byte)(225)))));
            this.lblProgramSettings.Location = new System.Drawing.Point(8, 7);
            this.lblProgramSettings.Name = "lblProgramSettings";
            this.lblProgramSettings.Size = new System.Drawing.Size(214, 32);
            this.lblProgramSettings.TabIndex = 1;
            this.lblProgramSettings.Text = "Program Settings";
            // 
            // tbPlugins
            // 
            this.tbPlugins.BackColor = System.Drawing.Color.White;
            this.tbPlugins.Location = new System.Drawing.Point(140, 4);
            this.tbPlugins.Name = "tbPlugins";
            this.tbPlugins.Size = new System.Drawing.Size(375, 323);
            this.tbPlugins.TabIndex = 2;
            this.tbPlugins.Text = "Plugins";
            // 
            // frmSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 387);
            this.Controls.Add(this.themeMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Droppable - Settings";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.Load += new System.EventHandler(this.frmSettings_Load);
            this.themeMain.ResumeLayout(false);
            this.dnbtbMain.ResumeLayout(false);
            this.tbSearchSettings.ResumeLayout(false);
            this.tbSearchSettings.PerformLayout();
            this.gbMisc.ResumeLayout(false);
            this.gbMisc.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudMinimumDownloadSize)).EndInit();
            this.gbMatching.ResumeLayout(false);
            this.gbMatching.PerformLayout();
            this.gbFiltering.ResumeLayout(false);
            this.gbFiltering.PerformLayout();
            this.tbProgramSettings.ResumeLayout(false);
            this.tbProgramSettings.PerformLayout();
            this.gbEngineSettings.ResumeLayout(false);
            this.gbEngineSettings.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudEngineTimeout)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Theme.WhiteScaleTheme themeMain;
        private Theme.WhiteScaleControlBox cbMain;
        private DotNetBarTabControl dnbtbMain;
        private System.Windows.Forms.TabPage tbSearchSettings;
        private System.Windows.Forms.TabPage tbProgramSettings;
        private System.Windows.Forms.Label lblSearchSettings;
        private System.Windows.Forms.Label lblProgramSettings;
        private System.Windows.Forms.TabPage tbPlugins;
        private Theme.WhiteScaleButton btnClose;
        private Theme.WhiteScaleButton btnSave;
        private System.Windows.Forms.GroupBox gbFiltering;
        private System.Windows.Forms.CheckBox cbFilterNightcore;
        private System.Windows.Forms.CheckBox cbFilterRemixes;
        private System.Windows.Forms.CheckBox cbFilterCovers;
        private System.Windows.Forms.GroupBox gbMatching;
        private System.Windows.Forms.RadioButton rbPartialMatch;
        private System.Windows.Forms.RadioButton rbExactMatch;
        private System.Windows.Forms.GroupBox gbMisc;
        private System.Windows.Forms.CheckBox cbFastSearch;
        private System.Windows.Forms.NumericUpDown nudMinimumDownloadSize;
        private System.Windows.Forms.Label lblMinimumSizeKB;
        private System.Windows.Forms.GroupBox gbEngineSettings;
        private System.Windows.Forms.NumericUpDown nudEngineTimeout;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox cbSkipEngineCheck;
        private System.Windows.Forms.RadioButton rbNoMatch;
    }
}