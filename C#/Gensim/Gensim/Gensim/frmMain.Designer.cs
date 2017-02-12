namespace Gensim
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
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.pnlSidebar = new System.Windows.Forms.Panel();
            this.pnlVisual = new Gensim.Theme.DoubleBufferedPanel();
            this.lbCreatureInfo = new System.Windows.Forms.ListBox();
            this.pnlSidebar.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsMain
            // 
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(1207, 25);
            this.tsMain.TabIndex = 0;
            this.tsMain.Text = "toolStrip1";
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlSidebar.Controls.Add(this.lbCreatureInfo);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSidebar.Location = new System.Drawing.Point(1007, 25);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Size = new System.Drawing.Size(200, 662);
            this.pnlSidebar.TabIndex = 1;
            // 
            // pnlVisual
            // 
            this.pnlVisual.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlVisual.Location = new System.Drawing.Point(0, 25);
            this.pnlVisual.Name = "pnlVisual";
            this.pnlVisual.Size = new System.Drawing.Size(1007, 662);
            this.pnlVisual.TabIndex = 2;
            this.pnlVisual.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlVisual_Paint);
            // 
            // lbCreatureInfo
            // 
            this.lbCreatureInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbCreatureInfo.FormattingEnabled = true;
            this.lbCreatureInfo.Location = new System.Drawing.Point(0, 0);
            this.lbCreatureInfo.Name = "lbCreatureInfo";
            this.lbCreatureInfo.Size = new System.Drawing.Size(198, 660);
            this.lbCreatureInfo.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 687);
            this.Controls.Add(this.pnlVisual);
            this.Controls.Add(this.pnlSidebar);
            this.Controls.Add(this.tsMain);
            this.DoubleBuffered = true;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gensim";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmMain_FormClosing);
            this.pnlSidebar.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.Panel pnlSidebar;
        private Theme.DoubleBufferedPanel pnlVisual;
        private System.Windows.Forms.ListBox lbCreatureInfo;
    }
}

