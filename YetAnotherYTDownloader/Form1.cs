﻿using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;
using YoutubeDLSharp.Metadata;
using System.Reflection;
using System.Media;

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
        private CancellationTokenSource cancelDownloadTokken;
        private IntPtr consoleHandle;
        private bool isVideoPostProcessing = false;
        private bool isVideoDownloading = false;
        private bool convertToMp3 = false;


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
            converttomp3_chk.Checked = false;
            converttomp3_chk.ImageIndex = 0;
            WriteColorLine("YAYD " + GetVersion + " Started!", ConsoleColor.Cyan);
            ver_label.Text = GetVersion;
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
                PlaySound("SoundBT1.wav");
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    var onlyFileName = Path.GetFullPath(folderBrowserDialog1.SelectedPath);
                    destination_textbox.Text = onlyFileName.ToString();
                }
            }
            catch (Exception exception1)
            {
                MessageBox.Show("Error while browsing for files:" + "\n" + exception1.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                WriteColorLineError("Error while browsing for files:" + "\n" + exception1.Message);
            }
        }

        private async void BtnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (source_textbox.Text == String.Empty)
                {
                    WriteColorLineError("Video link is missing, please paste your youtube link.");
                    MessageBox.Show("Video link is missing!\nPlease paste your youtube link.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    PlaySound("SoundBT1.wav");
                    if (!isVideoPostProcessing)
                    {
                        speedlb.ForeColor = Color.DeepSkyBlue;
                        etalb.ForeColor = Color.Yellow;
                        await getSongTitle();
                        if (convertToMp3)
                        {
                            await downloadSong();
                        }
                        else
                        {
                            await downloadVideo();
                        }
                    }
                    else
                    {
                        WriteColorLineError("Wait!\nVideo is still postprocessing.");
                        MessageBox.Show("Wait!\nVideo is still postprocessing.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception exception3)
            {
                WriteColorLineError(exception3.Message);
            }
        }

        private void BtnStop_Click(object sender, EventArgs e)
        {
            PlaySound("SoundBT1.wav");
            if (isVideoDownloading || isVideoPostProcessing)
            {
                textBox1.Text = "Download stopped...";
                textBox1.ForeColor = Color.Red;
                speedlb.Text = "Speed: " + "0.0 KB/S";
                speedlb.ForeColor = Color.Red;
                etalb.Text = "ETA: " + "00:00";
                etalb.ForeColor = Color.Red;
                cancelDownloadTokken.Cancel();
            }
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
            var ytdl = new YoutubeDL();
            // set the path of the youtube-dl and FFmpeg if they're not in PATH or current directory
            ytdl.YoutubeDLPath = app_directory + ytdl_path + ytdl_app;
            ytdl.FFmpegPath = app_directory + ytdl_path + ffmpeg_app;
            // optional: set a different download folder
            ytdl.OutputFolder = destination_textbox.Text + @"\";
            // audio options
            var options = new OptionSet()
            {
                Format = "bestaudio/best",
                AudioFormat = AudioConversionFormat.Mp3,
                AudioQuality = 0,
                Update = true
            };
            // a progress handler with a callback that updates a progress bar
            var bar_progress = new Progress<DownloadProgress>((p) => showProgress(p));
            //Console.WriteLine(bar_progress.ToString());
            // a cancellation token source used for cancelling the download
            // use `cts.Cancel();` to perform cancellation
            cancelDownloadTokken = new CancellationTokenSource();
            // download a audio
            var result = await ytdl.RunAudioDownload(source_textbox.Text, AudioConversionFormat.Mp3, progress: bar_progress, ct: cancelDownloadTokken.Token, overrideOptions: options);
            // the path of the downloaded file
            string path = result.Data;
            if (result.Success)
            {
                cancelDownloadTokken.Cancel();
                textBox1.Text = "Processing Finished!";
                MessageBox.Show("Video Postprocessing Finished!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //textBox1.ForeColor = Color.Red;
                speedlb.Text = "Speed: " + "0.0 KB/S";
                speedlb.ForeColor = Color.Red;
                etalb.Text = "ETA: " + "00:00";
                etalb.ForeColor = Color.Red;
                textBox2.Text = "";
            }
            else
            {
                cancelDownloadTokken.Cancel();
                textBox1.Text = "Download Error !!!";
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

        private async Task downloadVideo()
        {
            var ytdl = new YoutubeDL();
            // set the path of the youtube-dl and FFmpeg if they're not in PATH or current directory
            ytdl.YoutubeDLPath = app_directory + ytdl_path + ytdl_app;
            ytdl.FFmpegPath = app_directory + ytdl_path + ffmpeg_app;
            // optional: set a different download folder
            ytdl.OutputFolder = destination_textbox.Text + @"\";
            // audio options
            var options = new OptionSet()
            {
                Format = "best",
                AudioFormat = AudioConversionFormat.Best,
                AudioQuality = 0,
                CheckAllFormats = true,
                FfmpegLocation = app_directory + ytdl_path + ffmpeg_app,
                Update = true
            };
            // a progress handler with a callback that updates a progress bar
            var bar_progress = new Progress<DownloadProgress>((p) => showProgress(p));
            //Console.WriteLine(bar_progress.ToString());
            // a cancellation token source used for cancelling the download
            // use `cts.Cancel();` to perform cancellation
            cancelDownloadTokken = new CancellationTokenSource();
            // download a audio
            //var result = await ytdl.RunAudioDownload(source_textbox.Text, AudioConversionFormat.Mp3, progress: bar_progress, ct: cancelDownloadTokken.Token, overrideOptions: options);
            var result = await ytdl.RunVideoDownload(source_textbox.Text, progress: bar_progress, ct: cancelDownloadTokken.Token, overrideOptions: options);
            // the path of the downloaded file
            string path = result.Data;
            if (result.Success)
            {
                cancelDownloadTokken.Cancel();
                textBox1.Text = "Processing Finished!";
                MessageBox.Show("Video Postprocessing Finished!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //textBox1.ForeColor = Color.Red;
                speedlb.Text = "Speed: " + "0.0 KB/S";
                speedlb.ForeColor = Color.Red;
                etalb.Text = "ETA: " + "00:00";
                etalb.ForeColor = Color.Red;
                textBox2.Text = "";
            }
            else
            {
                cancelDownloadTokken.Cancel();
                textBox1.Text = "Download Error !!!";
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
            var options = new OptionSet()
            {
                Format = "bestaudio/best",
                AudioFormat = AudioConversionFormat.Mp3,
                AudioQuality = 0,
                Update = true
            };

            try
            {
                var res1 = await ytdl.RunVideoDataFetch(source_textbox.Text, overrideOptions: options);
                VideoData video = res1.Data;
                string songTitle = video.Title;
                WriteColorLine($"\n\nLink from: Youtube \nVideo Name: {video.Title}\nChannel Name: {video.Channel}\nLikes: {video.LikeCount}\n\n", ConsoleColor.DarkYellow);
                textBox2.Text = songTitle;
                textBox2.ForeColor = Color.OrangeRed;
            }
            catch (Exception ex)
            {
                WriteColorLineError($"Exception caught: {ex.Message}");
            }
        }

        private void showProgress(DownloadProgress p)
        {
            string progress_state = p.State.ToString();
            if (progress_state.Contains("Processing"))
            {
                textBox1.ForeColor = Color.Yellow;
                isVideoPostProcessing = true;
                isVideoDownloading = false;
            }
            else
            {
                textBox1.ForeColor = Color.Lime;
                isVideoPostProcessing = false;
            }
            if (progress_state.Contains("Downloading"))
            {
                isVideoDownloading = true;
            }
            else
            {
                isVideoDownloading = false;
            }
            textBox1.Text = progress_state;
            downloadProgressBar.Value = (int)(p.Progress * 100.0f);
            WriteColorLine($"speed: {p.DownloadSpeed} | left: {p.ETA}\n", ConsoleColor.Magenta);
            speedlb.Text = "Speed: " + p.DownloadSpeed;
            etalb.Text = "ETA: " + p.ETA;
        }
        private void showErrorMessage(string url, string error)
            => MessageBox.Show($"Failed to process '{url}'. Output:\n\n{error}", "YoutubeDLSharp - ERROR",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void Form1_Load(object sender, EventArgs e)
        {
            showconsole_chk.ImageIndex = 1;
            ShowWindow(consoleHandle, SW_SHOW);
            DownloadPrereqisites();
        }

        private void showconsole_chk_CheckedChanged_1(object sender, EventArgs e)
        {
            PlaySound("SoundBT1.wav");
            if (showconsole_chk.Checked)
            {
                showconsole_chk.ImageIndex = 1;
                ShowWindow(consoleHandle, SW_SHOW);
                WriteColorLine("Debug console enabled!", ConsoleColor.Green);
            }
            else
            {
                showconsole_chk.ImageIndex = 0;
                ShowWindow(consoleHandle, SW_HIDE);
                WriteColorLine("Debug console disabled!", ConsoleColor.Red);
            }
        }

        public string GetVersion
        {
            get
            {
                if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
                {
                    Version ver = System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
                    return string.Format("Version: {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision, Assembly.GetEntryAssembly().GetName().Name);
                }
                else
                {
                    var ver = Assembly.GetExecutingAssembly().GetName().Version;
                    return string.Format("Version: {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision, Assembly.GetEntryAssembly().GetName().Name);
                }
            }
        }

        private void WriteColorLine(string value, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.WriteLine(value.PadRight(Console.WindowWidth - 1));
            Console.ResetColor();
        }
        private void WriteColorLineError(string value)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(value.PadRight(Console.WindowWidth - 1));
            Console.ResetColor();
        }

        private void PlaySound(string audioFileName)
        {
            string audioFolder = app_directory + @"audio\";
            SoundPlayer soundPlayer = new SoundPlayer(soundLocation: audioFolder + audioFileName);
            soundPlayer.Play();
        }

        private async void DownloadPrereqisites()
        {
            try
            {
                WriteColorLine("Checking if YtDlp.exe exists.", ConsoleColor.Yellow);
                WriteColorLine("Checking if FFmpeg.exe exists.", ConsoleColor.Yellow);
                if (!File.Exists(app_directory + ytdl_path + ytdl_app))
                {
                    DialogResult yt_dialog = MessageBox.Show("Download YtDlp.exe?", "Download", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (yt_dialog)
                    {
                        case DialogResult.Yes:
                            Task ytdl_download = YoutubeDLSharp.Utils.DownloadYtDlp(app_directory + ytdl_path);
                            WriteColorLine("Downloading YtDlp.exe...\n" +
                                "Please Wait...", ConsoleColor.Yellow);
                            await ytdl_download;

                            switch (ytdl_download.Status)
                            {
                                case TaskStatus.RanToCompletion:
                                    WriteColorLine("YtDlp.exe downloaded successfully.", ConsoleColor.Green);
                                    break;
                                case TaskStatus.Faulted:
                                    WriteColorLineError($"Downloading YtDlp.exe faulted with exception: {YoutubeDLSharp.Utils.DownloadYtDlp(app_directory + ytdl_path).Exception}");
                                    MessageBox.Show($"Downloading YtDlp.exe faulted with exception: {YoutubeDLSharp.Utils.DownloadYtDlp(app_directory + ytdl_path).Exception}", "Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                                    break;
                            }
                            break;
                        case DialogResult.No:
                            WriteColorLineError("YtDlp.exe is required for this software to work correctly.");
                            break;
                        case DialogResult.Cancel:
                            WriteColorLineError("YtDlp.exe is required for this software to work correctly.");
                            break;
                    }
                }
                else
                {
                    WriteColorLine("YtDlp.exe found!", ConsoleColor.Green);
                }

                if (!File.Exists(app_directory + ytdl_path + ffmpeg_app))
                {

                    DialogResult ff_dialog = MessageBox.Show("Download FFmpeg.exe?", "Download", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (ff_dialog)
                    {
                        case DialogResult.Yes:
                            Task ffmpeg_download = YoutubeDLSharp.Utils.DownloadFFmpeg(app_directory + ytdl_path);
                            WriteColorLine("Downloading FFmpeg.exe...\n " +
                                "Please Wait...", ConsoleColor.Yellow);
                            await ffmpeg_download;

                            switch (ffmpeg_download.Status)
                            {
                                case TaskStatus.Running:
                                    WriteColorLine("Downloading FFmpeg.exe", ConsoleColor.Yellow);
                                    break;
                                case TaskStatus.RanToCompletion:
                                    WriteColorLine("FFmpeg.exe downloaded successfully.", ConsoleColor.Green);
                                    break;
                                case TaskStatus.Faulted:
                                    WriteColorLineError($"Downloading FFmpeg.exe faulted with exception: {YoutubeDLSharp.Utils.DownloadYtDlp(app_directory + ytdl_path).Exception}");
                                    MessageBox.Show($"Downloading FFmpeg.exe faulted with exception: {YoutubeDLSharp.Utils.DownloadYtDlp(app_directory + ytdl_path).Exception}", "Error", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
                                    break;
                            }
                            break;
                        case DialogResult.No:
                            WriteColorLineError("FFmpeg.exe is required for this software to work correctly.");
                            break;
                        case DialogResult.Cancel:
                            WriteColorLineError("FFmpeg.exe is required for this software to work correctly.");
                            break;
                    }
                }
                else
                {
                    WriteColorLine("FFmpeg.exe found!", ConsoleColor.Green);
                }
            }
            catch (Exception ex)
            {
                WriteColorLineError($"Exception caught: {ex.Message}");
            }
            finally
            {
                if (!File.Exists(app_directory + ytdl_path + ytdl_app) && !File.Exists(app_directory + ytdl_path + ffmpeg_app))
                {
                    MessageBox.Show("YtDlp & FFmpeg are missing. \n " +
                    "Please download them manually and put them in this directory: \n " +
                    app_directory + ytdl_path, "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    WriteColorLine("YtDlp & FFmpeg are missing. \n " +
                        "Please download them manually and put them in this directory: \n " +
                        app_directory + ytdl_path, ConsoleColor.Yellow);
                }
            }
        }
        private void converttomp3_chk_CheckedChanged(object sender, EventArgs e)
        {
            PlaySound("SoundBT1.wav");
            if (converttomp3_chk.Checked)
            {
                converttomp3_chk.ImageIndex = 1;
                convertToMp3 = true;
                WriteColorLine("Convert video to mp3 enabled!", ConsoleColor.Green);
            }
            else
            {
                converttomp3_chk.ImageIndex = 0;
                convertToMp3 = false;
                WriteColorLine("Convert video to mp3 disabled!", ConsoleColor.Red);
            }
        }
    }
}
