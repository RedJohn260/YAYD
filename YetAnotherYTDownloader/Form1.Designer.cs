namespace YetAnotherYTDownloader
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.Notify_icon = new System.Windows.Forms.NotifyIcon(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.source_textbox = new System.Windows.Forms.TextBox();
            this.souce_label = new System.Windows.Forms.Label();
            this.destination_label = new System.Windows.Forms.Label();
            this.destination_textbox = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.downloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.textBox2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.speedlb = new System.Windows.Forms.Label();
            this.etalb = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.chk_imageList = new System.Windows.Forms.ImageList(this.components);
            this.showconsole_chk = new System.Windows.Forms.CheckBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ver_label = new System.Windows.Forms.Label();
            this.BtnStop = new ePOSOne.btnProduct.Custom_Button();
            this.BtnDownload = new ePOSOne.btnProduct.Custom_Button();
            this.BtnBrowse2 = new ePOSOne.btnProduct.Custom_Button();
            this.SuspendLayout();
            // 
            // Notify_icon
            // 
            this.Notify_icon.Icon = ((System.Drawing.Icon)(resources.GetObject("Notify_icon.Icon")));
            this.Notify_icon.Text = "VladYtDownloader";
            this.Notify_icon.Visible = true;
            this.Notify_icon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.Notify_icon_MouseDoubleClick);
            // 
            // label1
            // 
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.label1.ForeColor = System.Drawing.Color.Silver;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(776, 37);
            this.label1.TabIndex = 3;
            this.label1.Text = "Yet Another Youtube Downloader";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Label1_MouseDown);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.label3.ForeColor = System.Drawing.Color.Silver;
            this.label3.Location = new System.Drawing.Point(-9, 380);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(820, 63);
            this.label3.TabIndex = 6;
            this.label3.Text = "_________________________________________________________________________________" +
    "___________________________________________";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.label4.ForeColor = System.Drawing.Color.Silver;
            this.label4.Location = new System.Drawing.Point(470, 421);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(318, 24);
            this.label4.TabIndex = 7;
            this.label4.Text = "© 2022 RedJohn260 All Rights Reversed";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "Application|*.txt|All files|*.*";
            // 
            // source_textbox
            // 
            this.source_textbox.AcceptsReturn = true;
            this.source_textbox.BackColor = System.Drawing.Color.Silver;
            this.source_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.source_textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.source_textbox.Location = new System.Drawing.Point(12, 98);
            this.source_textbox.Name = "source_textbox";
            this.source_textbox.Size = new System.Drawing.Size(776, 28);
            this.source_textbox.TabIndex = 9;
            this.source_textbox.WordWrap = false;
            // 
            // souce_label
            // 
            this.souce_label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.souce_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.souce_label.ForeColor = System.Drawing.Color.Silver;
            this.souce_label.Location = new System.Drawing.Point(13, 58);
            this.souce_label.Name = "souce_label";
            this.souce_label.Size = new System.Drawing.Size(776, 37);
            this.souce_label.TabIndex = 10;
            this.souce_label.Text = "Paste youtube video link:";
            this.souce_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // destination_label
            // 
            this.destination_label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.destination_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.destination_label.ForeColor = System.Drawing.Color.Silver;
            this.destination_label.Location = new System.Drawing.Point(12, 128);
            this.destination_label.Name = "destination_label";
            this.destination_label.Size = new System.Drawing.Size(776, 37);
            this.destination_label.TabIndex = 11;
            this.destination_label.Text = "Select save folder:";
            this.destination_label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // destination_textbox
            // 
            this.destination_textbox.BackColor = System.Drawing.Color.Silver;
            this.destination_textbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.destination_textbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.destination_textbox.Location = new System.Drawing.Point(12, 165);
            this.destination_textbox.Name = "destination_textbox";
            this.destination_textbox.Size = new System.Drawing.Size(651, 28);
            this.destination_textbox.TabIndex = 12;
            this.destination_textbox.WordWrap = false;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.RootFolder = System.Environment.SpecialFolder.MyComputer;
            // 
            // downloadProgressBar
            // 
            this.downloadProgressBar.BackColor = System.Drawing.Color.Silver;
            this.downloadProgressBar.ForeColor = System.Drawing.Color.Lime;
            this.downloadProgressBar.Location = new System.Drawing.Point(12, 293);
            this.downloadProgressBar.Name = "downloadProgressBar";
            this.downloadProgressBar.Size = new System.Drawing.Size(776, 30);
            this.downloadProgressBar.Step = 1;
            this.downloadProgressBar.TabIndex = 21;
            // 
            // textBox2
            // 
            this.textBox2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.textBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.textBox2.ForeColor = System.Drawing.Color.Orange;
            this.textBox2.Location = new System.Drawing.Point(13, 259);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(776, 37);
            this.textBox2.TabIndex = 10;
            this.textBox2.Text = "Song Title";
            this.textBox2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.textBox1.ForeColor = System.Drawing.Color.Lime;
            this.textBox1.Location = new System.Drawing.Point(246, 216);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(303, 37);
            this.textBox1.TabIndex = 10;
            this.textBox1.Text = "Downloading State";
            this.textBox1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.label5.ForeColor = System.Drawing.Color.Silver;
            this.label5.Location = new System.Drawing.Point(-9, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(820, 63);
            this.label5.TabIndex = 5;
            this.label5.Text = "_________________________________________________________________________________" +
    "___________________________________________";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.label6.ForeColor = System.Drawing.Color.Silver;
            this.label6.Location = new System.Drawing.Point(-9, 183);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(820, 63);
            this.label6.TabIndex = 5;
            this.label6.Text = "_________________________________________________________________________________" +
    "___________________________________________";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // speedlb
            // 
            this.speedlb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.speedlb.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.speedlb.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.speedlb.Location = new System.Drawing.Point(7, 216);
            this.speedlb.Name = "speedlb";
            this.speedlb.Size = new System.Drawing.Size(233, 37);
            this.speedlb.TabIndex = 10;
            this.speedlb.Text = "Download Speed";
            this.speedlb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // etalb
            // 
            this.etalb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.etalb.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.etalb.ForeColor = System.Drawing.Color.Yellow;
            this.etalb.Location = new System.Drawing.Point(555, 216);
            this.etalb.Name = "etalb";
            this.etalb.Size = new System.Drawing.Size(233, 37);
            this.etalb.TabIndex = 10;
            this.etalb.Text = "Estimated Time";
            this.etalb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.label2.ForeColor = System.Drawing.Color.Silver;
            this.label2.Location = new System.Drawing.Point(-9, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(820, 63);
            this.label2.TabIndex = 5;
            this.label2.Text = "_________________________________________________________________________________" +
    "___________________________________________";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chk_imageList
            // 
            this.chk_imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("chk_imageList.ImageStream")));
            this.chk_imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.chk_imageList.Images.SetKeyName(0, "SwitchOFF.png");
            this.chk_imageList.Images.SetKeyName(1, "SwitchON.png");
            // 
            // showconsole_chk
            // 
            this.showconsole_chk.Appearance = System.Windows.Forms.Appearance.Button;
            this.showconsole_chk.AutoSize = true;
            this.showconsole_chk.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.showconsole_chk.FlatAppearance.BorderSize = 0;
            this.showconsole_chk.FlatAppearance.CheckedBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.showconsole_chk.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.showconsole_chk.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.showconsole_chk.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.showconsole_chk.ImageIndex = 0;
            this.showconsole_chk.ImageList = this.chk_imageList;
            this.showconsole_chk.Location = new System.Drawing.Point(151, 417);
            this.showconsole_chk.Name = "showconsole_chk";
            this.showconsole_chk.Size = new System.Drawing.Size(66, 34);
            this.showconsole_chk.TabIndex = 22;
            this.showconsole_chk.UseVisualStyleBackColor = false;
            this.showconsole_chk.CheckedChanged += new System.EventHandler(this.showconsole_chk_CheckedChanged_1);
            // 
            // label7
            // 
            this.label7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.label7.ForeColor = System.Drawing.Color.Silver;
            this.label7.Location = new System.Drawing.Point(9, 421);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(153, 24);
            this.label7.TabIndex = 7;
            this.label7.Text = "[DEBUG console]: ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ver_label
            // 
            this.ver_label.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ver_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.ver_label.ForeColor = System.Drawing.Color.Silver;
            this.ver_label.Location = new System.Drawing.Point(223, 421);
            this.ver_label.Name = "ver_label";
            this.ver_label.Size = new System.Drawing.Size(241, 24);
            this.ver_label.TabIndex = 7;
            this.ver_label.Text = "Version : 1.0.1";
            this.ver_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // BtnStop
            // 
            this.BtnStop.BorderColor = System.Drawing.Color.Silver;
            this.BtnStop.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BtnStop.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BtnStop.FlatAppearance.BorderSize = 0;
            this.BtnStop.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BtnStop.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BtnStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.BtnStop.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BtnStop.Location = new System.Drawing.Point(433, 337);
            this.BtnStop.Name = "BtnStop";
            this.BtnStop.OnHoverBorderColor = System.Drawing.Color.Cyan;
            this.BtnStop.OnHoverButtonColor = System.Drawing.Color.Cyan;
            this.BtnStop.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BtnStop.Size = new System.Drawing.Size(355, 53);
            this.BtnStop.TabIndex = 18;
            this.BtnStop.Text = "Stop";
            this.BtnStop.TextColor = System.Drawing.Color.Silver;
            this.BtnStop.UseVisualStyleBackColor = true;
            this.BtnStop.Click += new System.EventHandler(this.BtnStop_Click);
            // 
            // BtnDownload
            // 
            this.BtnDownload.BorderColor = System.Drawing.Color.Silver;
            this.BtnDownload.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BtnDownload.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BtnDownload.FlatAppearance.BorderSize = 0;
            this.BtnDownload.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BtnDownload.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BtnDownload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.BtnDownload.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BtnDownload.Location = new System.Drawing.Point(12, 337);
            this.BtnDownload.Name = "BtnDownload";
            this.BtnDownload.OnHoverBorderColor = System.Drawing.Color.Cyan;
            this.BtnDownload.OnHoverButtonColor = System.Drawing.Color.Cyan;
            this.BtnDownload.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BtnDownload.Size = new System.Drawing.Size(355, 53);
            this.BtnDownload.TabIndex = 15;
            this.BtnDownload.Text = "Start";
            this.BtnDownload.TextColor = System.Drawing.Color.Silver;
            this.BtnDownload.UseVisualStyleBackColor = true;
            this.BtnDownload.Click += new System.EventHandler(this.BtnDownload_Click);
            // 
            // BtnBrowse2
            // 
            this.BtnBrowse2.BorderColor = System.Drawing.Color.Silver;
            this.BtnBrowse2.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BtnBrowse2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BtnBrowse2.FlatAppearance.BorderSize = 0;
            this.BtnBrowse2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BtnBrowse2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BtnBrowse2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnBrowse2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
            this.BtnBrowse2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BtnBrowse2.Location = new System.Drawing.Point(669, 165);
            this.BtnBrowse2.Name = "BtnBrowse2";
            this.BtnBrowse2.OnHoverBorderColor = System.Drawing.Color.Cyan;
            this.BtnBrowse2.OnHoverButtonColor = System.Drawing.Color.Cyan;
            this.BtnBrowse2.OnHoverTextColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.BtnBrowse2.Size = new System.Drawing.Size(111, 29);
            this.BtnBrowse2.TabIndex = 13;
            this.BtnBrowse2.Text = "Browse";
            this.BtnBrowse2.TextColor = System.Drawing.Color.Silver;
            this.BtnBrowse2.UseVisualStyleBackColor = true;
            this.BtnBrowse2.Click += new System.EventHandler(this.BtnBrowse2_Click);
            // 
            // Form1
            // 
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Window;
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(25)))), ((int)(((byte)(25)))));
            this.ClientSize = new System.Drawing.Size(795, 454);
            this.Controls.Add(this.showconsole_chk);
            this.Controls.Add(this.downloadProgressBar);
            this.Controls.Add(this.BtnStop);
            this.Controls.Add(this.BtnDownload);
            this.Controls.Add(this.BtnBrowse2);
            this.Controls.Add(this.destination_textbox);
            this.Controls.Add(this.destination_label);
            this.Controls.Add(this.etalb);
            this.Controls.Add(this.speedlb);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.souce_label);
            this.Controls.Add(this.source_textbox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ver_label);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "YAYD";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.NotifyIcon Notify_icon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox source_textbox;
        private System.Windows.Forms.Label souce_label;
        private System.Windows.Forms.Label destination_label;
        private System.Windows.Forms.TextBox destination_textbox;
        private ePOSOne.btnProduct.Custom_Button BtnBrowse2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private ePOSOne.btnProduct.Custom_Button BtnDownload;
        private ePOSOne.btnProduct.Custom_Button BtnStop;
        private System.Windows.Forms.ProgressBar downloadProgressBar;
        private System.Windows.Forms.Label textBox2;
        private System.Windows.Forms.Label textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label speedlb;
        private System.Windows.Forms.Label etalb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ImageList chk_imageList;
        private System.Windows.Forms.CheckBox showconsole_chk;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label ver_label;
    }
}

