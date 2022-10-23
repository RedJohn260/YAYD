using System;
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
            WriteColorLine("YAYD " + GetVersion + " Started!", ConsoleColor.Cyan);
            ver_label.Text = "YAYD " + GetVersion;
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
                        await downloadSong();
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
            // a progress handler with a callback that updates a progress bar
            var bar_progress = new Progress<DownloadProgress>((p) => showProgress(p));
            //Console.WriteLine(bar_progress.ToString());
            // a cancellation token source used for cancelling the download
            // use `cts.Cancel();` to perform cancellation
            cancelDownloadTokken = new CancellationTokenSource();
            // audio options
            var options = new OptionSet()
            {
                Format = "bestaudio/best",
                AudioFormat = AudioConversionFormat.Mp3,
                AudioQuality = 0
            };
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

        private async Task getSongTitle()
        {
            var ytdl = new YoutubeDL();
            ytdl.YoutubeDLPath = app_directory + ytdl_path + ytdl_app;
            ytdl.FFmpegPath = app_directory + ytdl_path + ffmpeg_app;
            var res1 = await ytdl.RunVideoDataFetch(source_textbox.Text);
            VideoData video = res1.Data;
            var songTitle = video.Title;
            WriteColorLine($"\n\nLink from: Youtube \nSong Name: {video.Title}\nChannel Name: {video.Channel}\nLikes: {video.LikeCount}\n\n", ConsoleColor.DarkYellow);
            textBox2.Text = songTitle;
            textBox2.ForeColor = Color.OrangeRed;
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
            textBox1.Text =progress_state;
            downloadProgressBar.Value = (int)(p.Progress * 100.0f);
            WriteColorLine( $"speed: {p.DownloadSpeed} | left: {p.ETA}\n", ConsoleColor.Magenta);
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
    }
}
