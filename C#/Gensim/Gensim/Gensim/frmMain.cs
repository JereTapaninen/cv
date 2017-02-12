using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Gensim.Lib;
using Gensim.Theme;

namespace Gensim
{
    public partial class frmMain : Form
    {
        private World World;

        private bool IsRunning = true;

        public frmMain()
        {
            InitializeComponent();

            this.World = new World(pnlVisual.Size);

            OnUpdate();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.IsRunning = false;
        }

        private void pnlVisual_Paint(object sender, PaintEventArgs e)
        {
            World.Draw(e.Graphics);
        }

        private void OnUpdate()
        {
            new Thread(() =>
            {
                while (IsRunning)
                {
                    new Thread(() =>
                    {
                        World.Update(DateTime.Now);

                        Invoke(new MethodInvoker(() => pnlVisual.Invalidate()));
                    }).Start();

                    Thread.Sleep(200);
                }
            }).Start();
        }
    }
}
