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
using System.Web;
using System.Net;
using System.Net.NetworkInformation;

using Droppable.IO;
using Droppable.IO.Utils;
using Droppable.IO.Engines;

namespace Droppable
{
    public partial class frmEngineCheck : Form
    {
        private List<Engine> WorkingEngines = new List<Engine>();
        private List<Engine> MalfunctioningEngines = new List<Engine>();

        private bool IsRunning = true;

        public frmEngineCheck()
        {
            InitializeComponent();
        }

        private void frmEngineCheck_Shown(object sender, EventArgs e)
        {
            this.OnUpdate();
            this.EngineCheck();
        }

        private void OnUpdate()
        {
            new Thread(() =>
            {
                while (IsRunning)
                {
                    try
                    {
                        Invoke(new MethodInvoker(() =>
                        {
                            lblWorkingCount.Text = WorkingEngines.Count.ToString();
                            lblMalfunctioningCount.Text = MalfunctioningEngines.Count.ToString();
                        }));

                        var newPos = new Point((Width / 2) - (lblChecking.Width / 2), lblChecking.Location.Y);

                        Invoke(new MethodInvoker(() => lblChecking.Location = newPos));
                    }
                    catch (ObjectDisposedException)
                    // this is perfectly normal during form closing
                    { }

                    Thread.Sleep(20);
                }
            }).Start();
        }

        private void EngineCheck()
        {
            this.pbCheckProgress.MaxValue = SharedProperties.Engines.Length;

            new Thread(() =>
            {
                using (var client = new DroppableWebClient(true))
                {
                    client.HeadOnly = true;

                    using (var pong = new Ping())
                    {
                        foreach (var eng in SharedProperties.Engines)
                        {
                            PingReply pingResult = null;
                            var errMsg = string.Empty;
                            var pingError = string.Empty;

                            try
                            {
                                pingResult = pong.Send(new Uri(eng.GetBaseURL()).Host);

                                if (pingResult.Status != IPStatus.Success)
                                    pingError = "Error";
                            }
                            catch (PingException)
                            {
                                pingError = "Error";
                            }

                            try
                            {
                                client.DownloadString(eng.GetBaseURL());

                                this.WorkingEngines.Add(eng);
                            }
                            catch (Exception e)
                            {
                                this.MalfunctioningEngines.Add(eng);
                                errMsg = e.Message;
                            }

                            var lvi = new ListViewItem();
                            lvi.ImageKey = (string.IsNullOrEmpty(errMsg) && string.IsNullOrEmpty(pingError)) ? "Success" : "Error";

                            lvi.SubItems.Add(eng.GetEngineName());
                            lvi.SubItems.Add((string.IsNullOrEmpty(pingError) && pingResult != null) ? pingResult.RoundtripTime + " ms" : pingError);
                            lvi.SubItems.Add((string.IsNullOrEmpty(errMsg)) ? "Success" : errMsg);

                            if (!IsRunning)
                                break;

                            try
                            {
                                Invoke(new MethodInvoker(() => lvEngines.Items.Add(lvi)));
                            }
                            catch (InvalidOperationException)
                            { }

                            this.pbCheckProgress.Value++;
                        }
                    }
                }

                if (IsRunning)
                {
                    if (this.MalfunctioningEngines.Count > 0)
                    {
                        var sbEngines = new StringBuilder();

                        foreach (var eng in this.MalfunctioningEngines)
                        {
                            sbEngines.AppendLine("- " + eng.GetEngineName());
                        }

                        MessageBox.Show("Malfunctioning search engines found!\n\nBelow is a list of all the malfunctioning engines:\n" + sbEngines.ToString()
                            + "\nPlease wait for a patch to fix this issue!\nWe are sorry about the inconvenience.", "Malfunctioning engines found",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        // Do we need this? Maybe enable later?
                        //MessageBox.Show("No malfunctioning engines found! Droppable is good to go!", "All engines are working",
                        //    MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    Invoke(new MethodInvoker(() =>
                    {
                        lblChecking.Text = "Checking completed!";
                        btnSkip.Text = "Continue to Droppable";
                    }));

                    if (cbContinueAutomatically.Checked)
                    {
                        try
                        {
                            Invoke(new MethodInvoker(() => this.Close()));
                        }
                        catch (ObjectDisposedException)
                        // this is perfectly normal during form closing
                        { }
                    }
                }
            }).Start();
        }

        private void frmEngineCheck_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.IsRunning = false;

            new frmMain().Show();
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
