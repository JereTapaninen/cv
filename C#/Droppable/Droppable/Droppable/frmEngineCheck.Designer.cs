namespace Droppable
{
    partial class frmEngineCheck
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEngineCheck));
            this.themeMain = new Droppable.Theme.WhiteScaleTheme();
            this.cbContinueAutomatically = new System.Windows.Forms.CheckBox();
            this.lvEngines = new System.Windows.Forms.ListView();
            this.chSuccess = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chEngineName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chPing = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chErr = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ilEnginelv = new System.Windows.Forms.ImageList(this.components);
            this.btnSkip = new Droppable.Theme.WhiteScaleButton();
            this.cbMain = new Droppable.Theme.WhiteScaleControlBox();
            this.pbCheckProgress = new Droppable.Theme.WhiteScaleProgressBar();
            this.lblMalfunctioningCount = new System.Windows.Forms.Label();
            this.lblWorkingCount = new System.Windows.Forms.Label();
            this.lblMalfunctioning = new System.Windows.Forms.Label();
            this.lblWorking = new System.Windows.Forms.Label();
            this.lblChecking = new System.Windows.Forms.Label();
            this.themeMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // themeMain
            // 
            this.themeMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.themeMain.BorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.themeMain.Controls.Add(this.cbContinueAutomatically);
            this.themeMain.Controls.Add(this.lvEngines);
            this.themeMain.Controls.Add(this.btnSkip);
            this.themeMain.Controls.Add(this.cbMain);
            this.themeMain.Controls.Add(this.pbCheckProgress);
            this.themeMain.Controls.Add(this.lblMalfunctioningCount);
            this.themeMain.Controls.Add(this.lblWorkingCount);
            this.themeMain.Controls.Add(this.lblMalfunctioning);
            this.themeMain.Controls.Add(this.lblWorking);
            this.themeMain.Controls.Add(this.lblChecking);
            this.themeMain.Customization = "yMjI//////8AAAD/";
            this.themeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.themeMain.Font = new System.Drawing.Font("Tahoma", 9F);
            this.themeMain.Image = null;
            this.themeMain.Location = new System.Drawing.Point(0, 0);
            this.themeMain.Movable = true;
            this.themeMain.Name = "themeMain";
            this.themeMain.NoRounding = false;
            this.themeMain.ShowFooter = false;
            this.themeMain.Sizable = false;
            this.themeMain.Size = new System.Drawing.Size(382, 341);
            this.themeMain.SmartBounds = true;
            this.themeMain.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.themeMain.TabIndex = 0;
            this.themeMain.Text = "whiteScaleTheme1";
            this.themeMain.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.themeMain.Transparent = false;
            // 
            // cbContinueAutomatically
            // 
            this.cbContinueAutomatically.AutoSize = true;
            this.cbContinueAutomatically.Checked = true;
            this.cbContinueAutomatically.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbContinueAutomatically.Location = new System.Drawing.Point(18, 247);
            this.cbContinueAutomatically.Name = "cbContinueAutomatically";
            this.cbContinueAutomatically.Size = new System.Drawing.Size(149, 18);
            this.cbContinueAutomatically.TabIndex = 9;
            this.cbContinueAutomatically.Text = "Continue automatically";
            this.cbContinueAutomatically.UseVisualStyleBackColor = true;
            // 
            // lvEngines
            // 
            this.lvEngines.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lvEngines.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chSuccess,
            this.chEngineName,
            this.chPing,
            this.chErr});
            this.lvEngines.FullRowSelect = true;
            this.lvEngines.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvEngines.Location = new System.Drawing.Point(18, 102);
            this.lvEngines.MultiSelect = false;
            this.lvEngines.Name = "lvEngines";
            this.lvEngines.Size = new System.Drawing.Size(345, 138);
            this.lvEngines.SmallImageList = this.ilEnginelv;
            this.lvEngines.TabIndex = 8;
            this.lvEngines.UseCompatibleStateImageBehavior = false;
            this.lvEngines.View = System.Windows.Forms.View.Details;
            // 
            // chSuccess
            // 
            this.chSuccess.Text = "";
            this.chSuccess.Width = 27;
            // 
            // chEngineName
            // 
            this.chEngineName.Text = "Engine";
            this.chEngineName.Width = 87;
            // 
            // chPing
            // 
            this.chPing.Text = "Ping";
            this.chPing.Width = 65;
            // 
            // chErr
            // 
            this.chErr.Text = "Message";
            this.chErr.Width = 140;
            // 
            // ilEnginelv
            // 
            this.ilEnginelv.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ilEnginelv.ImageStream")));
            this.ilEnginelv.TransparentColor = System.Drawing.Color.Transparent;
            this.ilEnginelv.Images.SetKeyName(0, "Success");
            this.ilEnginelv.Images.SetKeyName(1, "Error");
            // 
            // btnSkip
            // 
            this.btnSkip.Customization = "+vr6//Dw8P//////AAAA/9PT0/8=";
            this.btnSkip.Font = new System.Drawing.Font("Verdana", 8F);
            this.btnSkip.Image = null;
            this.btnSkip.Location = new System.Drawing.Point(19, 299);
            this.btnSkip.Name = "btnSkip";
            this.btnSkip.NoRounding = false;
            this.btnSkip.Size = new System.Drawing.Size(344, 29);
            this.btnSkip.TabIndex = 7;
            this.btnSkip.Text = "Skip the checking procedure";
            this.btnSkip.Transparent = false;
            this.btnSkip.Click += new System.EventHandler(this.btnSkip_Click);
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
            this.cbMain.Location = new System.Drawing.Point(303, 4);
            this.cbMain.Movable = true;
            this.cbMain.Name = "cbMain";
            this.cbMain.NoRounding = false;
            this.cbMain.SettingsButtonVisible = false;
            this.cbMain.Sizable = false;
            this.cbMain.Size = new System.Drawing.Size(72, 20);
            this.cbMain.SmartBounds = true;
            this.cbMain.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.cbMain.TabIndex = 6;
            this.cbMain.Text = "whiteScaleControlBox1";
            this.cbMain.TransparencyKey = System.Drawing.Color.Empty;
            this.cbMain.Transparent = true;
            // 
            // pbCheckProgress
            // 
            this.pbCheckProgress.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pbCheckProgress.Customization = "/5YZ//9kGf/T09P/";
            this.pbCheckProgress.Font = new System.Drawing.Font("Verdana", 8F);
            this.pbCheckProgress.Image = null;
            this.pbCheckProgress.Location = new System.Drawing.Point(18, 270);
            this.pbCheckProgress.MaxValue = 100;
            this.pbCheckProgress.MinValue = 0;
            this.pbCheckProgress.Name = "pbCheckProgress";
            this.pbCheckProgress.NoRounding = false;
            this.pbCheckProgress.Size = new System.Drawing.Size(345, 23);
            this.pbCheckProgress.TabIndex = 5;
            this.pbCheckProgress.Text = "whiteScaleProgressBar1";
            this.pbCheckProgress.Transparent = false;
            this.pbCheckProgress.Value = 0;
            // 
            // lblMalfunctioningCount
            // 
            this.lblMalfunctioningCount.AutoSize = true;
            this.lblMalfunctioningCount.BackColor = System.Drawing.Color.Transparent;
            this.lblMalfunctioningCount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMalfunctioningCount.ForeColor = System.Drawing.Color.Red;
            this.lblMalfunctioningCount.Location = new System.Drawing.Point(348, 82);
            this.lblMalfunctioningCount.Name = "lblMalfunctioningCount";
            this.lblMalfunctioningCount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblMalfunctioningCount.Size = new System.Drawing.Size(15, 14);
            this.lblMalfunctioningCount.TabIndex = 4;
            this.lblMalfunctioningCount.Text = "0";
            // 
            // lblWorkingCount
            // 
            this.lblWorkingCount.AutoSize = true;
            this.lblWorkingCount.BackColor = System.Drawing.Color.Transparent;
            this.lblWorkingCount.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkingCount.ForeColor = System.Drawing.Color.Green;
            this.lblWorkingCount.Location = new System.Drawing.Point(348, 63);
            this.lblWorkingCount.Name = "lblWorkingCount";
            this.lblWorkingCount.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblWorkingCount.Size = new System.Drawing.Size(15, 14);
            this.lblWorkingCount.TabIndex = 3;
            this.lblWorkingCount.Text = "0";
            // 
            // lblMalfunctioning
            // 
            this.lblMalfunctioning.AutoSize = true;
            this.lblMalfunctioning.BackColor = System.Drawing.Color.Transparent;
            this.lblMalfunctioning.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMalfunctioning.ForeColor = System.Drawing.Color.Red;
            this.lblMalfunctioning.Location = new System.Drawing.Point(15, 82);
            this.lblMalfunctioning.Name = "lblMalfunctioning";
            this.lblMalfunctioning.Size = new System.Drawing.Size(103, 14);
            this.lblMalfunctioning.TabIndex = 2;
            this.lblMalfunctioning.Text = "Malfunctioning:";
            // 
            // lblWorking
            // 
            this.lblWorking.AutoSize = true;
            this.lblWorking.BackColor = System.Drawing.Color.Transparent;
            this.lblWorking.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorking.ForeColor = System.Drawing.Color.Green;
            this.lblWorking.Location = new System.Drawing.Point(15, 63);
            this.lblWorking.Name = "lblWorking";
            this.lblWorking.Size = new System.Drawing.Size(62, 14);
            this.lblWorking.TabIndex = 1;
            this.lblWorking.Text = "Working:";
            // 
            // lblChecking
            // 
            this.lblChecking.AutoSize = true;
            this.lblChecking.BackColor = System.Drawing.Color.Transparent;
            this.lblChecking.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChecking.Location = new System.Drawing.Point(16, 34);
            this.lblChecking.Name = "lblChecking";
            this.lblChecking.Size = new System.Drawing.Size(347, 16);
            this.lblChecking.TabIndex = 0;
            this.lblChecking.Text = "Please wait while Droppable checks all the engines...";
            // 
            // frmEngineCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 341);
            this.Controls.Add(this.themeMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEngineCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Droppable - Checking engines...";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmEngineCheck_FormClosing);
            this.Shown += new System.EventHandler(this.frmEngineCheck_Shown);
            this.themeMain.ResumeLayout(false);
            this.themeMain.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Theme.WhiteScaleTheme themeMain;
        private Theme.WhiteScaleControlBox cbMain;
        private Theme.WhiteScaleProgressBar pbCheckProgress;
        private System.Windows.Forms.Label lblMalfunctioningCount;
        private System.Windows.Forms.Label lblWorkingCount;
        private System.Windows.Forms.Label lblMalfunctioning;
        private System.Windows.Forms.Label lblWorking;
        private System.Windows.Forms.Label lblChecking;
        private Theme.WhiteScaleButton btnSkip;
        private System.Windows.Forms.ListView lvEngines;
        private System.Windows.Forms.ColumnHeader chSuccess;
        private System.Windows.Forms.ColumnHeader chEngineName;
        private System.Windows.Forms.ColumnHeader chPing;
        private System.Windows.Forms.ColumnHeader chErr;
        private System.Windows.Forms.ImageList ilEnginelv;
        private System.Windows.Forms.CheckBox cbContinueAutomatically;
    }
}