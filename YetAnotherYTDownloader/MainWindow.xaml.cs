using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Documents;
using YoutubeDLSharp;
using YoutubeDLSharp.Options;
using YoutubeDLSharp.Metadata;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.ComponentModel;

namespace YetAnotherYTDownloader
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        private readonly string ytdl_app = "yt-dlp.exe";
        private readonly string ffmpeg_app = "ffmpeg.exe";
        private readonly string ytdl_path = @"ytdl\";
        private readonly string app_directory = Environment.CurrentDirectory + @"\";
        private bool isVideoPostProcessing = false;
        private bool isVideoDownloading = false;
        private bool convertToMp3 = false;
        private CancellationTokenSource? cancelDownloadTokken;
        private bool _isDownloadButtonEnabled = true;
        private string version = "Version: 1.1.0.0";

        public event PropertyChangedEventHandler? PropertyChanged;

        public bool IsDownloadButtonEnabled
        {
            get => _isDownloadButtonEnabled;
            set
            {
                _isDownloadButtonEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsDownloadButtonEnabled)));
            }
        }

        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this; // Set DataContext for binding
            converttomp3_chk.IsChecked = false;
            AppendLog($"YAYD {version} Started!", Colors.Cyan);
            ver_label.Content = version;
            DownloadPrereqisites();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                ReleaseCapture();
                SendMessage(new System.Windows.Interop.WindowInteropHelper(this).Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                PlaySound("SoundBT1.wav");
                var folderDialog = new OpenFolderDialog
                {
                    Title = "Select a folder to save downloads",
                    InitialDirectory = destination_textbox.Text
                };
                if (folderDialog.ShowDialog() == true)
                {
                    destination_textbox.Text = folderDialog.FolderName;
                }
            }
            catch (Exception exception)
            {
                AppendLogError($"Error: {exception.Message}");
                MessageBox.Show($"Error: {exception.Message}", "Error!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private async void BtnDownload_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AppendLog("Download button clicked", Colors.Cyan);
                if (string.IsNullOrEmpty(source_textbox.Text))
                {
                    AppendLogError("Video link is missing, please paste your YouTube link.");
                    MessageBox.Show("Video link is missing!\nPlease paste your YouTube link.", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                if (!Directory.Exists(destination_textbox.Text))
                {
                    AppendLogError("Invalid or non-existent destination folder. Please select a valid folder.");
                    MessageBox.Show("Invalid or non-existent destination folder.\nPlease select a valid folder.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (isVideoPostProcessing || isVideoDownloading)
                {
                    AppendLogError("Download or post-processing already in progress.");
                    MessageBox.Show("Download or post-processing already in progress.", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                PlaySound("SoundBT1.wav");
                IsDownloadButtonEnabled = false; // Disable button
                AppendLog("Starting download process...", Colors.Yellow);
                await GetSongTitle();
                if (convertToMp3)
                {
                    await DownloadSong();
                }
                else
                {
                    await DownloadVideo();
                }
            }
            catch (Exception exception)
            {
                AppendLogError($"Download error: {exception.Message}");
                MessageBox.Show($"Error: {exception.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                IsDownloadButtonEnabled = true; // Re-enable button on error
            }
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            PlaySound("SoundBT1.wav");
            if (isVideoDownloading || isVideoPostProcessing)
            {
                AppendLog("Stopping download...", Colors.Yellow);
                textBox1.Text ="Status:  " + "Download stopped...";
                textBox1.Foreground = new SolidColorBrush(Colors.Red);
                cancelDownloadTokken?.Cancel();
                cancelDownloadTokken = null;
                isVideoDownloading = false;
                isVideoPostProcessing = false;
                IsDownloadButtonEnabled = true; // Re-enable button
            }
        }

        private async Task DownloadSong()
        {
            try
            {
                var ytdl = new YoutubeDL
                {
                    YoutubeDLPath = Path.Combine(app_directory, ytdl_path, ytdl_app),
                    FFmpegPath = Path.Combine(app_directory, ytdl_path, ffmpeg_app),
                    OutputFolder = destination_textbox.Text + @"\"
                };

                var options = new OptionSet
                {
                    Format = "bestaudio/best",
                    AudioFormat = AudioConversionFormat.Mp3,
                    AudioQuality = 0,
                    Update = true
                };

                var bar_progress = new Progress<DownloadProgress>(p => ShowProgress(p));
                var cts = new CancellationTokenSource();
                cancelDownloadTokken = cts;

                AppendLog("Starting audio download...", Colors.Yellow);
                var result = await ytdl.RunAudioDownload(source_textbox.Text, AudioConversionFormat.Mp3, progress: bar_progress, ct: cts.Token, overrideOptions: options);

                if (result.Success)
                {
                    AppendLog("Audio download completed successfully!", Colors.LimeGreen);
                    textBox1.Text = "Status:  " + "Processing Finished!";
                    MessageBox.Show("Video Postprocessing Finished!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    AppendLogError($"Audio download failed: {string.Join("\n", result.ErrorOutput)}");
                    textBox1.Text = "Status:  " + "Download Error !!!";
                    MessageBox.Show($"Failed to process '{source_textbox.Text}'. Output:\n\n{string.Join("\n", result.ErrorOutput)}", "YoutubeDLSharp - ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                AppendLogError($"Audio download exception: {ex.Message}");
            }
            finally
            {
                cancelDownloadTokken?.Cancel();
                cancelDownloadTokken = null;
                textBox2.Text = "Title:  Finished!";
                isVideoDownloading = false;
                isVideoPostProcessing = false;
                IsDownloadButtonEnabled = true; // Re-enable button
            }
        }

        private async Task DownloadVideo()
        {
            try
            {
                var ytdl = new YoutubeDL
                {
                    YoutubeDLPath = Path.Combine(app_directory, ytdl_path, ytdl_app),
                    FFmpegPath = Path.Combine(app_directory, ytdl_path, ffmpeg_app),
                    OutputFolder = destination_textbox.Text + @"\"
                };

                var options = new OptionSet
                {
                    Format = "best",
                    AudioFormat = AudioConversionFormat.Best,
                    AudioQuality = 0,
                    CheckAllFormats = true,
                    Update = true
                };

                var bar_progress = new Progress<DownloadProgress>(p => ShowProgress(p));
                var cts = new CancellationTokenSource();
                cancelDownloadTokken = cts;

                AppendLog("Starting video download...", Colors.Yellow);
                var result = await ytdl.RunVideoDownload(source_textbox.Text, progress: bar_progress, ct: cts.Token, overrideOptions: options);

                if (result.Success)
                {
                    AppendLog("Video download completed successfully!", Colors.LimeGreen);
                    textBox1.Text = "Status:  " + "Processing Finished!";
                    MessageBox.Show("Video Postprocessing Finished!", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    AppendLogError($"Video download failed: {string.Join("\n", result.ErrorOutput)}");
                    textBox1.Text = "Download Error !!!";
                    MessageBox.Show($"Failed to process '{source_textbox.Text}'. Output:\n\n{string.Join("\n", result.ErrorOutput)}", "YoutubeDLSharp - ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                AppendLogError($"Video download exception: {ex.Message}");
            }
            finally
            {
                cancelDownloadTokken?.Cancel();
                cancelDownloadTokken = null;
                textBox2.Text = "Title:  ";
                isVideoDownloading = false;
                isVideoPostProcessing = false;
                IsDownloadButtonEnabled = true; // Re-enable button
            }
        }

        private async Task GetSongTitle()
        {
            var ytdl = new YoutubeDL
            {
                YoutubeDLPath = Path.Combine(app_directory, ytdl_path, ytdl_app),
                FFmpegPath = Path.Combine(app_directory, ytdl_path, ffmpeg_app)
            };

            var options = new OptionSet
            {
                Format = "bestaudio/best",
                AudioFormat = AudioConversionFormat.Mp3,
                AudioQuality = 0,
                Update = true
            };

            try
            {
                AppendLog("Fetching video title...", Colors.Yellow);
                var res = await ytdl.RunVideoDataFetch(source_textbox.Text, overrideOptions: options);
                VideoData video = res.Data;
                string songTitle = video.Title;
                AppendLog($"\nLink from: YouTube\nVideo Name: {video.Title}\nChannel Name: {video.Channel}\nLikes: {video.LikeCount}\n", Colors.White);
                textBox2.Text = "Title:  " + songTitle;
                textBox2.Foreground = new SolidColorBrush(Colors.OrangeRed);
            }
            catch (Exception ex)
            {
                AppendLogError($"Exception caught while fetching title: {ex.Message}");
            }
        }

        private void ShowProgress(DownloadProgress p)
        {
            Dispatcher.Invoke(() =>
            {
                AppendLog($"Progress update: State={p.State}, Progress={p.Progress:P0}, Speed={p.DownloadSpeed}, ETA={p.ETA}", Colors.LimeGreen);
                string progress_state = p.State.ToString();
                if (progress_state.Contains("Processing"))
                {
                    textBox1.Foreground = new SolidColorBrush(Colors.Yellow);
                    isVideoPostProcessing = true;
                    isVideoDownloading = false;
                }
                else if (progress_state.Contains("Downloading"))
                {
                    textBox1.Foreground = new SolidColorBrush(Colors.Lime);
                    isVideoPostProcessing = false;
                    isVideoDownloading = true;
                }
                else
                {
                    textBox1.Foreground = new SolidColorBrush(Colors.Lime);
                    isVideoPostProcessing = false;
                    isVideoDownloading = false;
                }
                textBox1.Text = "Status:  " + progress_state;
            });
        }

        private void ConvertToMp3_Checked(object sender, RoutedEventArgs e)
        {
            PlaySound("SoundBT1.wav");
            convertToMp3 = true;
            AppendLog("Convert video to mp3 enabled!", Colors.Cyan);
        }

        private void ConvertToMp3_Unchecked(object sender, RoutedEventArgs e)
        {
            PlaySound("SoundBT1.wav");
            convertToMp3 = false;
            AppendLog("Convert video to mp3 disabled!", Colors.Cyan);
        }

        private void AppendLog(string message, Color color)
        {
            Dispatcher.Invoke(() =>
            {
                var paragraph = new Paragraph(new Run(message))
                {
                    Foreground = new SolidColorBrush(color),
                    Margin = new Thickness(0, 2, 0, 2)
                };
                logTextBox.Document.Blocks.Add(paragraph);
                logTextBox.ScrollToEnd();
            });
        }

        private void AppendLogError(string message)
        {
            AppendLog(message, Colors.DarkRed);
        }

        private void PlaySound(string audioFileName)
        {
            try
            {
                string audioFolder = Path.Combine(app_directory, "audio");
                var player = new MediaPlayer();
                player.Open(new Uri(Path.Combine(audioFolder, audioFileName)));
                player.Play();
            }
            catch (Exception ex)
            {
                AppendLogError($"Error playing sound: {ex.Message}");
            }
        }

        private async void DownloadPrereqisites()
        {
            try
            {
                if (!Directory.Exists(Path.Combine(app_directory, ytdl_path)))
                {
                    Directory.CreateDirectory(Path.Combine(app_directory, ytdl_path));
                }
                //AppendLog("Checking if YtDlp.exe exists.", Colors.White);
                //AppendLog("Checking if FFmpeg.exe exists.", Colors.White);
                if (!File.Exists(Path.Combine(app_directory, ytdl_path, ytdl_app)))
                {
                    var result = MessageBox.Show("Download YtDlp.exe?", "Download", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        AppendLog("Downloading YtDlp.exe...\nPlease Wait...", Colors.Yellow);
                        await YoutubeDLSharp.Utils.DownloadYtDlp(Path.Combine(app_directory, ytdl_path));
                        AppendLog("YtDlp.exe downloaded successfully.", Colors.LimeGreen);
                    }
                    else
                    {
                        AppendLogError("YtDlp.exe is required for this software to work correctly.");
                    }
                }
                else
                {
                    AppendLog("YtDlp.exe found!", Colors.LimeGreen);
                }

                if (!File.Exists(Path.Combine(app_directory, ytdl_path, ffmpeg_app)))
                {
                    var result = MessageBox.Show("Download FFmpeg.exe?", "Download", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        AppendLog("Downloading FFmpeg.exe...\nPlease Wait...", Colors.Yellow);
                        await YoutubeDLSharp.Utils.DownloadFFmpeg(Path.Combine(app_directory, ytdl_path));
                        AppendLog("FFmpeg.exe downloaded successfully.", Colors.LimeGreen);
                    }
                    else
                    {
                        AppendLogError("FFmpeg.exe is required for this software to work correctly.");
                    }
                }
                else
                {
                    AppendLog("FFmpeg.exe found!", Colors.LimeGreen);
                }
            }
            catch (Exception ex)
            {
                AppendLogError($"Exception caught: {ex.Message}");
            }
            finally
            {
                if (!Directory.Exists(Path.Combine(app_directory, ytdl_path)))
                {
                    Directory.CreateDirectory(Path.Combine(app_directory, ytdl_path));
                }
                if (!File.Exists(Path.Combine(app_directory, ytdl_path, ytdl_app)) && !File.Exists(Path.Combine(app_directory, ytdl_path, ffmpeg_app)))
                {
                    AppendLogError($"YtDlp & FFmpeg are missing.\nPlease download them manually and put them in this directory:\n{Path.Combine(app_directory, ytdl_path)}");
                    MessageBox.Show($"YtDlp & FFmpeg are missing.\nPlease download them manually and put them in this directory:\n{Path.Combine(app_directory, ytdl_path)}", "Message", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }
    }
}