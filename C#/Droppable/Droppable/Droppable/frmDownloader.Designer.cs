namespace Droppable
{
    partial class frmDownloader
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDownloader));
            this.themeMain = new Droppable.Theme.WhiteScaleTheme();
            this.pbDownloadProgress = new Droppable.Theme.WhiteScaleProgressBar();
            this.lblAlbumName = new System.Windows.Forms.Label();
            this.lblSongName = new System.Windows.Forms.Label();
            this.lblArtistName = new System.Windows.Forms.Label();
            this.pbAlbumCover = new System.Windows.Forms.PictureBox();
            this.themeMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAlbumCover)).BeginInit();
            this.SuspendLayout();
            // 
            // themeMain
            // 
            this.themeMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.themeMain.BorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.themeMain.Controls.Add(this.pbDownloadProgress);
            this.themeMain.Controls.Add(this.lblAlbumName);
            this.themeMain.Controls.Add(this.lblSongName);
            this.themeMain.Controls.Add(this.lblArtistName);
            this.themeMain.Controls.Add(this.pbAlbumCover);
            this.themeMain.Customization = "/////wAAAP8=";
            this.themeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.themeMain.Font = new System.Drawing.Font("Tahoma", 9F);
            this.themeMain.Image = null;
            this.themeMain.Location = new System.Drawing.Point(0, 0);
            this.themeMain.Movable = true;
            this.themeMain.Name = "themeMain";
            this.themeMain.NoRounding = false;
            this.themeMain.ShowFooter = false;
            this.themeMain.Sizable = false;
            this.themeMain.Size = new System.Drawing.Size(312, 133);
            this.themeMain.SmartBounds = true;
            this.themeMain.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.themeMain.TabIndex = 0;
            this.themeMain.Text = "whiteScaleTheme1";
            this.themeMain.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.themeMain.Transparent = false;
            // 
            // pbDownloadProgress
            // 
            this.pbDownloadProgress.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pbDownloadProgress.Customization = "/5YZ//9kGf/T09P/";
            this.pbDownloadProgress.Font = new System.Drawing.Font("Verdana", 8F);
            this.pbDownloadProgress.Image = null;
            this.pbDownloadProgress.Location = new System.Drawing.Point(12, 101);
            this.pbDownloadProgress.MaxValue = 100;
            this.pbDownloadProgress.MinValue = 0;
            this.pbDownloadProgress.Name = "pbDownloadProgress";
            this.pbDownloadProgress.NoRounding = false;
            this.pbDownloadProgress.Size = new System.Drawing.Size(286, 18);
            this.pbDownloadProgress.TabIndex = 4;
            this.pbDownloadProgress.Transparent = false;
            this.pbDownloadProgress.Value = 0;
            // 
            // lblAlbumName
            // 
            this.lblAlbumName.AutoSize = true;
            this.lblAlbumName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlbumName.Location = new System.Drawing.Point(80, 74);
            this.lblAlbumName.Name = "lblAlbumName";
            this.lblAlbumName.Size = new System.Drawing.Size(84, 16);
            this.lblAlbumName.TabIndex = 3;
            this.lblAlbumName.Text = "ALBUM NAME";
            // 
            // lblSongName
            // 
            this.lblSongName.AutoSize = true;
            this.lblSongName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSongName.Location = new System.Drawing.Point(80, 55);
            this.lblSongName.Name = "lblSongName";
            this.lblSongName.Size = new System.Drawing.Size(161, 16);
            this.lblSongName.TabIndex = 2;
            this.lblSongName.Text = "SONG NAME - SONG NAME";
            // 
            // lblArtistName
            // 
            this.lblArtistName.AutoSize = true;
            this.lblArtistName.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArtistName.Location = new System.Drawing.Point(80, 36);
            this.lblArtistName.Name = "lblArtistName";
            this.lblArtistName.Size = new System.Drawing.Size(94, 16);
            this.lblArtistName.TabIndex = 1;
            this.lblArtistName.Text = "ARTIST NAME";
            // 
            // pbAlbumCover
            // 
            this.pbAlbumCover.BackColor = System.Drawing.Color.Transparent;
            this.pbAlbumCover.Location = new System.Drawing.Point(12, 31);
            this.pbAlbumCover.Name = "pbAlbumCover";
            this.pbAlbumCover.Size = new System.Drawing.Size(64, 64);
            this.pbAlbumCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbAlbumCover.TabIndex = 0;
            this.pbAlbumCover.TabStop = false;
            // 
            // frmDownloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(312, 133);
            this.Controls.Add(this.themeMain);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDownloader";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Droppable";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmDownloader_FormClosing);
            this.themeMain.ResumeLayout(false);
            this.themeMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbAlbumCover)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Theme.WhiteScaleTheme themeMain;
        private Theme.WhiteScaleProgressBar pbDownloadProgress;
        private System.Windows.Forms.Label lblAlbumName;
        private System.Windows.Forms.Label lblSongName;
        private System.Windows.Forms.Label lblArtistName;
        private System.Windows.Forms.PictureBox pbAlbumCover;
    }
}