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
using System.Net;
using System.Diagnostics;
using System.IO;

using Droppable.IO;

namespace Droppable
{
    public partial class frmDownloader : Form
    {
        private WebClient Downloader { get; set; }

        private string Filepath { get; set; }

        public frmDownloader()
        {
            InitializeComponent();

            this.Downloader = new WebClient();
            this.Downloader.DownloadProgressChanged += DownloadProgressChangedEventHandler;
            this.Downloader.DownloadFileCompleted += DownloadFileCompletedEventHandler;
        }

        private void frmDownloader_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Downloader.Dispose();
        }

        private void DownloadFileCompletedEventHandler(object sender, EventArgs e)
        {
            Process.Start(Path.GetDirectoryName(this.Filepath));

            try
            {
                new frmMain().Show();
            }
            catch (InvalidOperationException)
            // this is perfectly normal
            { }

            this.Close();
        }

        private void DownloadProgressChangedEventHandler(object sender, DownloadProgressChangedEventArgs e)
        {
            this.pbDownloadProgress.Value = e.ProgressPercentage;
        }

        public bool Download(SongInfo info, string url)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Title = "Choose save location";
                sfd.Filter = "MP3 File (*.mp3)|*.mp3";
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                sfd.FileName = info.ArtistName + " - " + info.SongName;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    this.lblArtistName.Text = info.ArtistName;
                    this.lblSongName.Text = info.SongName;
                    this.lblAlbumName.Text = info.AlbumName;
                    this.pbAlbumCover.ImageLocation = info.AlbumCoverURL;

                    this.Filepath = sfd.FileName;

                    this.Downloader.DownloadFileAsync(new Uri(url), sfd.FileName);

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool DownloadDirty(string searchQuery, string url)
        {
            using (var sfd = new SaveFileDialog())
            {
                sfd.Title = "Choose save location";
                sfd.Filter = "MP3 File (*.mp3)|*.mp3";
                sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
                sfd.FileName = searchQuery;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    this.lblArtistName.Text = "Your song is downloading!";
                    this.lblSongName.Text = "Song: " + searchQuery;
                    this.lblAlbumName.Text = string.Empty;
                    this.pbAlbumCover.Image = Droppable.Properties.Resources.sign_question_icon as Image;

                    this.Filepath = sfd.FileName;

                    this.Downloader.DownloadFileAsync(new Uri(url), sfd.FileName);

                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
