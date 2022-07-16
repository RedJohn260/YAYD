using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Threading;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;
using YoutubeDLSharp.Metadata;

namespace YetAnotherYTDownloader
{
    public partial class Form1 : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        private readonly string ytdl_app = "yt-dlp.exe";
        private readonly string ffmpeg_app = "ffmpeg.exe";
        private readonly string ytdl_path = @"ytdl\";
        private readonly string app_directory = Environment.CurrentDirectory + @"\";//AppDomain.CurrentDomain.BaseDirectory;
        private bool IsDownloadingStarted = false;
        private string video_path = Environment.CurrentDirectory + @"\videos\"; //AppDomain.CurrentDomain.BaseDirectory + @"videos\";
        private CancellationTokenSource cancelDownloadTokken;
        private bool IsNotDownloading;
        private IntPtr consoleHandle;


        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        const int SW_HIDE = 0;
        const int SW_SHOW = 5;

        public Form1()
        {
            InitializeComponent();
            consoleHandle = GetConsoleWindow();
            showconsole_chk.Checked = false;
            showconsole_chk.ImageIndex = 0;
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                Notify_icon.Visible = true;
                this.ShowInTaskbar = false;
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                Notify_icon.Visible = true;
                this.ShowInTaskbar = true;
            }
        }

        private void Notify_icon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            Notify_icon.Visible = false;
            WindowState = FormWindowState.Normal;
        }

        private void BtnBrowse2_Click(object sender, EventArgs e)
        {
            try
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    var onlyFileName = Path.GetFullPath(folderBrowserDialog1.SelectedPath);
                    destination_textbox.Text = onlyFileName.ToString();
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("Error while browsing for files:" + "\n" + exception1.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Console.WriteLine("Error while browsing for files:" + "\n" + exception1.Message);
            }
        }

        private async void BtnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                IsDownloadingStarted = true;
                if (IsDownloadingStarted == true)
                {
                    try
                    {
                        await getSongTitle();
                        await downloadSong();
                    }
                    catch (Exception exception2)
                    {
                        MessageBox.Show(exception2.Message.ToString(), "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Console.WriteLine(exception2.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Downloading is Stopped, Press Start Button", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception exception3)
            {
                Console.WriteLine(exception3.Message);
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            IsDownloadingStarted = false;
            IsNotDownloading = true;
            cancelDownloadTokken.Cancel();
            //textBox1.Text = "Download stopped...";
            textBox1.ForeColor = Color.Red;
            speedlb.Text = "Speed: " + "0.0 KB/S";
            speedlb.ForeColor = Color.Red;
            etalb.Text = "ETA: " + "00:00";
            etalb.ForeColor = Color.Red;
        }

        private void Label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private async Task downloadSong()
        {
            IsNotDownloading = false;
            var ytdl = new YoutubeDL();
            // set the path of the youtube-dl and FFmpeg if they're not in PATH or current directory
            ytdl.YoutubeDLPath = app_directory + ytdl_path + ytdl_app;
            ytdl.FFmpegPath = app_directory + ytdl_path + ffmpeg_app;
            // optional: set a different download folder
            ytdl.OutputFolder = destination_textbox.Text + @"\";
            // a progress handler with a callback that updates a progress bar
            var bar_progress = new Progress<DownloadProgress>((p) => showProgress(p));
            Console.WriteLine(bar_progress.ToString());
            // a cancellation token source used for cancelling the download
            // use `cts.Cancel();` to perform cancellation
            cancelDownloadTokken = new CancellationTokenSource();
            // download a audio
            var result = await ytdl.RunAudioDownload(source_textbox.Text, AudioConversionFormat.Mp3, progress: bar_progress, ct: cancelDownloadTokken.Token);
            // the path of the downloaded file
            string path = result.Data;
            if (result.Success)
            {
                IsNotDownloading = true;
                cancelDownloadTokken.Cancel();
                //textBox1.Text = "Download stopped...";
                textBox1.ForeColor = Color.Red;
                speedlb.Text = "Speed: " + "0.0 KB/S";
                speedlb.ForeColor = Color.Red;
                etalb.Text = "ETA: " + "00:00";
                etalb.ForeColor = Color.Red;
                textBox2.Text = "";
            }
            else
            {
                IsNotDownloading = true;
                cancelDownloadTokken.Cancel();
                //textBox1.Text = "Download stopped...";
                textBox1.ForeColor = Color.Red;
                speedlb.Text = "Speed: " + "0.0 KB/S";
                speedlb.ForeColor = Color.Red;
                etalb.Text = "ETA: " + "00:00";
                etalb.ForeColor = Color.Red;
                showErrorMessage(source_textbox.Text, String.Join("\n", result.ErrorOutput));
                Console.WriteLine(($"Failed to process '{source_textbox.Text}'. Output:\n\n{result.ErrorOutput}", "YoutubeDLSharp - ERROR"));
                textBox2.Text = "";
            }
        }

        private async Task getSongTitle()
        {
            var ytdl = new YoutubeDL();
            ytdl.YoutubeDLPath = app_directory + ytdl_path + ytdl_app;
            ytdl.FFmpegPath = app_directory + ytdl_path + ffmpeg_app;
            var res1 = await ytdl.RunVideoDataFetch(source_textbox.Text);
            VideoData video = res1.Data;
            var songTitle = video.Title;
            textBox2.Text = songTitle;
            textBox2.ForeColor = Color.OrangeRed;
        }

        private void showProgress(DownloadProgress p)
        {
            textBox1.ForeColor = Color.Lime;
            textBox1.Text = p.State.ToString();
            downloadProgressBar.Value = (int)(p.Progress * 100.0f);
            Console.WriteLine( $"speed: {p.DownloadSpeed} | left: {p.ETA}");
            speedlb.Text = "Speed: " + p.DownloadSpeed;
            etalb.Text = "ETA: " + p.ETA;
        }
        private void showErrorMessage(string url, string error)
            => MessageBox.Show($"Failed to process '{url}'. Output:\n\n{error}", "YoutubeDLSharp - ERROR",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void Form1_Load(object sender, EventArgs e)
        {
            showconsole_chk.ImageIndex = 0;
            ShowWindow(consoleHandle, SW_HIDE);
        }

        private void showconsole_chk_CheckedChanged_1(object sender, EventArgs e)
        {

            if (showconsole_chk.Checked)
            {
                showconsole_chk.ImageIndex = 1;
                ShowWindow(consoleHandle, SW_SHOW);
                Console.WriteLine("Debug console disabled!");
            }
            else
            {
                showconsole_chk.ImageIndex = 0;
                ShowWindow(consoleHandle, SW_HIDE);
                Console.WriteLine("Debug console enabled!");
            }
        }
    }
}
