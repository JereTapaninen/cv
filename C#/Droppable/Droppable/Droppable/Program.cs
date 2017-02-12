using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Droppable
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ApplicationExit += Exit;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SharedProperties.Settings.Load();
            SharedProperties.PluginAgent.Set();

            if (!SharedProperties.Settings.SkipEngineCheck)
                new frmEngineCheck().Show();
            else
                new frmMain().Show();

            Application.Run();
        }

        static void Exit(object sender, EventArgs e)
        {
            SharedProperties.Settings.Save();
            SharedProperties.PluginAgent.Unset();
        }
    }
}
