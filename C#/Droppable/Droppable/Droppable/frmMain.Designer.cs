namespace Droppable
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.themeMain = new Droppable.Theme.WhiteScaleTheme();
            this.cbMain = new Droppable.Theme.WhiteScaleControlBox();
            this.pnlDrop = new Droppable.Theme.TransparentPanel();
            this.tpnlAlternate = new Droppable.Theme.TransparentPanel();
            this.tbSearchQuery = new System.Windows.Forms.TextBox();
            this.lblBePrecise = new System.Windows.Forms.Label();
            this.lblAlternative = new System.Windows.Forms.Label();
            this.themeMain.SuspendLayout();
            this.pnlDrop.SuspendLayout();
            this.tpnlAlternate.SuspendLayout();
            this.SuspendLayout();
            // 
            // themeMain
            // 
            this.themeMain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.themeMain.BorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.themeMain.Controls.Add(this.cbMain);
            this.themeMain.Controls.Add(this.pnlDrop);
            this.themeMain.Customization = "yMjI//////8AAAD/";
            this.themeMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.themeMain.Font = new System.Drawing.Font("Tahoma", 9F);
            this.themeMain.Image = null;
            this.themeMain.Location = new System.Drawing.Point(0, 0);
            this.themeMain.Movable = true;
            this.themeMain.Name = "themeMain";
            this.themeMain.NoRounding = false;
            this.themeMain.ShowFooter = true;
            this.themeMain.Sizable = false;
            this.themeMain.Size = new System.Drawing.Size(525, 328);
            this.themeMain.SmartBounds = true;
            this.themeMain.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.themeMain.TabIndex = 0;
            this.themeMain.Text = "whiteScaleTheme1";
            this.themeMain.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.themeMain.Transparent = false;
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
            this.cbMain.Location = new System.Drawing.Point(446, 4);
            this.cbMain.Movable = true;
            this.cbMain.Name = "cbMain";
            this.cbMain.NoRounding = false;
            this.cbMain.SettingsButtonVisible = true;
            this.cbMain.Sizable = false;
            this.cbMain.Size = new System.Drawing.Size(72, 20);
            this.cbMain.SmartBounds = true;
            this.cbMain.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.cbMain.TabIndex = 1;
            this.cbMain.Text = "whiteScaleControlBox1";
            this.cbMain.TransparencyKey = System.Drawing.Color.Empty;
            this.cbMain.Transparent = true;
            this.cbMain.SettingsButtonClicked += new System.EventHandler(this.cbMain_SettingsButtonClicked);
            // 
            // pnlDrop
            // 
            this.pnlDrop.AllowDrop = true;
            this.pnlDrop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlDrop.Controls.Add(this.tpnlAlternate);
            this.pnlDrop.Location = new System.Drawing.Point(9, 30);
            this.pnlDrop.Name = "pnlDrop";
            this.pnlDrop.Size = new System.Drawing.Size(507, 276);
            this.pnlDrop.TabIndex = 1;
            this.pnlDrop.DragLeave += new System.EventHandler(this.pnlDrop_DragLeave);
            this.pnlDrop.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlDrop_Paint);
            // 
            // tpnlAlternate
            // 
            this.tpnlAlternate.AllowDrop = true;
            this.tpnlAlternate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.tpnlAlternate.Controls.Add(this.tbSearchQuery);
            this.tpnlAlternate.Controls.Add(this.lblBePrecise);
            this.tpnlAlternate.Controls.Add(this.lblAlternative);
            this.tpnlAlternate.Location = new System.Drawing.Point(72, 152);
            this.tpnlAlternate.Name = "tpnlAlternate";
            this.tpnlAlternate.Size = new System.Drawing.Size(357, 109);
            this.tpnlAlternate.TabIndex = 3;
            // 
            // tbSearchQuery
            // 
            this.tbSearchQuery.Location = new System.Drawing.Point(62, 31);
            this.tbSearchQuery.Name = "tbSearchQuery";
            this.tbSearchQuery.Size = new System.Drawing.Size(235, 22);
            this.tbSearchQuery.TabIndex = 1;
            this.tbSearchQuery.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbSearchQuery.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearchQuery_KeyDown);
            // 
            // lblBePrecise
            // 
            this.lblBePrecise.AutoSize = true;
            this.lblBePrecise.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBePrecise.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblBePrecise.Location = new System.Drawing.Point(42, 58);
            this.lblBePrecise.Name = "lblBePrecise";
            this.lblBePrecise.Size = new System.Drawing.Size(276, 39);
            this.lblBePrecise.TabIndex = 2;
            this.lblBePrecise.Text = "Be as precise as you can and follow the following format\r\nArtist Name - Song Name" +
    "\r\nfor best results";
            this.lblBePrecise.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAlternative
            // 
            this.lblAlternative.AutoSize = true;
            this.lblAlternative.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAlternative.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(90)))), ((int)(((byte)(90)))), ((int)(((byte)(90)))));
            this.lblAlternative.Location = new System.Drawing.Point(8, 10);
            this.lblAlternative.Name = "lblAlternative";
            this.lblAlternative.Size = new System.Drawing.Size(342, 14);
            this.lblAlternative.TabIndex = 0;
            this.lblAlternative.Text = "Or alternatively, type in your own search query below:";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 328);
            this.Controls.Add(this.themeMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Droppable";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.themeMain.ResumeLayout(false);
            this.pnlDrop.ResumeLayout(false);
            this.tpnlAlternate.ResumeLayout(false);
            this.tpnlAlternate.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Theme.WhiteScaleTheme themeMain;
        private Theme.TransparentPanel pnlDrop;
        private Theme.WhiteScaleControlBox cbMain;
        private System.Windows.Forms.Label lblBePrecise;
        private System.Windows.Forms.TextBox tbSearchQuery;
        private System.Windows.Forms.Label lblAlternative;
        private Theme.TransparentPanel tpnlAlternate;
    }
}

