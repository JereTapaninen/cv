using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Threading;

using Droppable.IO;
using Droppable.IO.Utils;
using Droppable.IO.Engines;

namespace Droppable
{
    public partial class frmMain : Form
    {
        const string SPOTIFY_TRACK_URL = "https://open.spotify.com/track/";

        private frmSettings SettingsForm;

        private bool FileDownloadClose = false;
        private bool IsRunning = true;
        private bool DraggedOver = false;
        private bool SearchingForFile = false;

        private Brush TextBrushIdle = new SolidBrush(Color.FromArgb(90, 90, 90));
        private Brush TextBrushActive = new SolidBrush(Color.FromArgb(30, 30, 30));

        private GifImage _loadingGif;
        public GifImage LoadingGif
        {
            get { return _loadingGif; }
        }

        private string CurrentEngineName = string.Empty;

        public frmMain()
        {
            InitializeComponent();

            SettingsForm = new frmSettings();

            _loadingGif = new GifImage(Droppable.Properties.Resources.ring_alt as Image);

            Activate();

            pnlDrop.DragEnter += DragEnterEventHandler;
            pnlDrop.DragDrop += DragDropEventHandler;

            tpnlAlternate.DragEnter += DragEnterEventHandler;
            tpnlAlternate.DragDrop += DragDropEventHandler;

            OnUpdate();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            IsRunning = false;

            if (!FileDownloadClose)
                Application.Exit();
        }

        private void DragEnterEventHandler(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                DraggedOver = true;

                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void pnlDrop_DragLeave(object sender, EventArgs e)
        {
            DraggedOver = false;
        }

        private void DragDropEventHandler(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.Text).ToString();

            if (data.Contains(SPOTIFY_TRACK_URL))
            {
                DraggedOver = false;

                SearchClean(data);
            }
            else
            {
                MessageBox.Show("Invalid data.\n\nTo use Droppable, drag and drop a song you wish to download onto the form.",
                                "Invalid data", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbMain_SettingsButtonClicked(object sender, EventArgs e)
        {
            if (Application.OpenForms["frmSettings"] == null)
            {
                SettingsForm = new frmSettings();
                SettingsForm.Show();
            }
        }

        private void pnlDrop_Paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;

            if (!SearchingForFile)
            {
                var fontIncrease = 0;

                if (DraggedOver)
                    fontIncrease = 2;

                var text = "Drag and drop a song here!";
                var textFont = new Font("Tahoma", 16.0f + fontIncrease, FontStyle.Bold);

                var gfxMeasure = g.MeasureString(text, textFont);

                var textPosition = new PointF((pnlDrop.Width / 2) - (gfxMeasure.Width / 2),
                                                (pnlDrop.Height / 2) - (gfxMeasure.Height / 2));

                g.DrawString(text, textFont, DraggedOver ? TextBrushActive : TextBrushIdle, textPosition);
            }
            else
            {
                var imgPosition = new Point((pnlDrop.Width / 2) - ((LoadingGif.Width / 2) / 2),
                                (pnlDrop.Height / 2) - ((LoadingGif.Height / 2) / 2));

                g.DrawImage(LoadingGif.Get(), new Rectangle(imgPosition, new Size(LoadingGif.Width / 2, LoadingGif.Height / 2)), 
                    new Rectangle(0, 0, LoadingGif.Width, LoadingGif.Height), GraphicsUnit.Pixel);

                var text = "Current engine: " + CurrentEngineName;
                var textFont = new Font("Tahoma", 12.0f, FontStyle.Bold);

                var gfxMeasure = g.MeasureString(text, textFont);

                var textPosition = new PointF((pnlDrop.Width / 2) - (gfxMeasure.Width / 2),
                                                ((pnlDrop.Height / 2) - (gfxMeasure.Height / 2)) + ((LoadingGif.Height / 2) / 2) + (gfxMeasure.Height / 2));

                g.DrawString(text, textFont, TextBrushActive, textPosition);
            }
        }

        private void OnUpdate()
        {
            new Thread(() =>
            {
                while (IsRunning)
                {
                    try
                    {
                        Invoke(new MethodInvoker(() => pnlDrop.Invalidate()));
                    }
                    // this is perfectly normal during form closing
                    catch (ObjectDisposedException)
                    { }
                    // this too is perfectly normal
                    catch (InvalidOperationException)
                    { }

                    Thread.Sleep(20);
                }
            }).Start();
        }

        private void BeginSearch()
        {
            SearchingForFile = true;

            Invoke(new MethodInvoker(() => tpnlAlternate.Visible = false));
        }

        private void EndSearch()
        {
            SearchingForFile = false;

            try
            {
                Invoke(new MethodInvoker(() => tpnlAlternate.Visible = true));
            }
            catch (InvalidOperationException)
            { }
        }

        private void SearchClean(string data)
        {
            var searchThread = new Thread(() =>
            {
                BeginSearch();

                var content = string.Empty;
                var downloadURL = string.Empty;
                var songInfo = new SongInfo();

                if (data.Contains(" - "))
                {
                    songInfo = new SongInfo(data.Explode(" - ")[0], data.Explode(" - ")[1]);
                }
                else
                {
                    content = new DroppableWebClient().DownloadStringDispose(data);

                    songInfo = ParseSongInfo(content);
                }

                var filteredSongInfo = songInfo;
                filteredSongInfo.ArtistName = filteredSongInfo.ArtistName.FillSpaces();
                filteredSongInfo.SongName = filteredSongInfo.SongName.FillSpaces().Replace("(", "").Replace(")", "");

                foreach (var eng in SharedProperties.Engines)
                {
                    CurrentEngineName = eng.GetEngineName();

                    var manResEv = new ManualResetEvent(false);
                    var result = string.Empty;
                    var doBreak = false;

                    manResEv.Reset();

                    new Thread(() =>
                    {
                        var tickTock = new Thread(() =>
                        {
                            try
                            {
                                Thread.Sleep(SharedProperties.Settings.TimeoutSeconds * 1000);

                                manResEv.Set();
                            }
                            catch (ThreadInterruptedException)
                            { }
                        });

                        tickTock.Start();

                        result = eng.Search(filteredSongInfo);

                        if (!string.IsNullOrEmpty(result))
                        {
                            URLFixer(ref result);

                            downloadURL = result;

                            if (MP3Utils.CheckMP3(songInfo, downloadURL))
                                doBreak = true;
                        }

                        tickTock.Interrupt();

                        manResEv.Set();
                    }).Start();

                    manResEv.WaitOne();

                    if (doBreak)
                        break;
                }

                if (MP3Utils.CheckMP3(songInfo, downloadURL))
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        var frmDl = new frmDownloader();

                        if (frmDl.Download(songInfo, downloadURL))
                        {
                            frmDl.Show();

                            FileDownloadClose = true;
                            Close();
                        }
                        else
                        {
                            frmDl.Close();
                        }
                    }));
                }
                else
                {
                    MessageBox.Show("We are sorry, Droppable couldn't find the song you were looking for!\n\nHopefully in the future this gets fixed!",
                                    "Droppable didn't find the song", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                EndSearch();
            });

            searchThread.SetApartmentState(ApartmentState.STA);
            searchThread.Start();
        }

        private void SearchDirty(string searchQuery)
        {
            var searchThread = new Thread(() =>
            {
                BeginSearch();

                var downloadURL = string.Empty;

                var filteredSearchQuery = searchQuery.FillSpaces().Replace("(", "").Replace(")", "");

                foreach (var eng in SharedProperties.Engines)
                {
                    CurrentEngineName = eng.GetEngineName();

                    var manResEv = new ManualResetEvent(false);
                    var result = string.Empty;
                    var doBreak = false;

                    manResEv.Reset();

                    new Thread(() =>
                    {
                        var tickTock = new Thread(() =>
                        {
                            try
                            {
                                Thread.Sleep(SharedProperties.Settings.TimeoutSeconds * 1000);

                                manResEv.Set();
                            }
                            catch (ThreadInterruptedException)
                            { }
                        });

                        tickTock.Start();

                        result = eng.SearchDirty(filteredSearchQuery);

                        if (!string.IsNullOrEmpty(result))
                        {
                            URLFixer(ref result);

                            downloadURL = result;

                            if (MP3Utils.CheckMP3(searchQuery, downloadURL))
                                doBreak = true;
                        }

                        tickTock.Interrupt();

                        manResEv.Set();
                    }).Start();

                    manResEv.WaitOne();

                    if (doBreak)
                        break;
                }

                if (MP3Utils.CheckMP3(searchQuery, downloadURL))
                {
                    Invoke(new MethodInvoker(() =>
                    {
                        var frmDl = new frmDownloader();

                        if (frmDl.DownloadDirty(searchQuery, downloadURL))
                        {
                            frmDl.Show();

                            FileDownloadClose = true;
                            Close();
                        }
                        else
                        {
                            frmDl.Close();
                        }
                    }));
                }
                else
                {
                    MessageBox.Show("We are sorry, Droppable couldn't find the song you were looking for!\n\nHopefully in the future this gets fixed!",
                                    "Droppable didn't find the song", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                EndSearch();
            });

            searchThread.SetApartmentState(ApartmentState.STA);
            searchThread.Start();
        }

        private void URLFixer(ref string url)
        {
            if (url.Contains(".php?f=http"))
                url = "";
        }

        private SongInfo ParseSongInfo(string content)
        {
            var description = content.Explode("<meta property=\"description\" content=\"")[1];
            description = description.Explode("\">")[0];

            var songName = description.Explode(", a song by ")[0];
            var artistName = description.Explode(", a song by ")[1];
            artistName = artistName.Explode(" on Spotify")[0];

            var albumName = content.Explode("<div class=\"media-img\"><a href=\"/album/")[1].Explode("alt=\"")[1].Explode("\" style=\"")[0];

            var albumCoverURL = content.Explode("<meta property=\"og:image\" content=\"")[1].Explode("\">")[0];

            return new SongInfo(artistName, songName, albumName, albumCoverURL);
        }

        private void tbSearchQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SearchDirty((sender as TextBox).Text);
                (sender as TextBox).Clear();

                e.Handled = true;
                e.SuppressKeyPress = true;
            }
        }
    }
}
