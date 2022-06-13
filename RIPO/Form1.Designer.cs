namespace RIPO
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btn_file = new System.Windows.Forms.Button();
            this.timeLabel = new System.Windows.Forms.Label();
            this.bar_video = new RIPO.ProgressBarEx();
            this.btn_pause = new System.Windows.Forms.Button();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.red_slider = new System.Windows.Forms.TrackBar();
            this.blue_slider = new System.Windows.Forms.TrackBar();
            this.green_slider = new System.Windows.Forms.TrackBar();
            this.red_label = new System.Windows.Forms.Label();
            this.blue_label = new System.Windows.Forms.Label();
            this.green_label = new System.Windows.Forms.Label();
            this.hred_slider = new System.Windows.Forms.TrackBar();
            this.hred_label = new System.Windows.Forms.Label();
            this.hgreen_label = new System.Windows.Forms.Label();
            this.hgreen_slider = new System.Windows.Forms.TrackBar();
            this.hblue_label = new System.Windows.Forms.Label();
            this.hblue_slider = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.red_slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.blue_slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.green_slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hred_slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hgreen_slider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hblue_slider)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.Location = new System.Drawing.Point(-3, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1280, 720);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // btn_file
            // 
            this.btn_file.BackColor = System.Drawing.Color.Snow;
            this.btn_file.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_file.FlatAppearance.BorderColor = System.Drawing.Color.Snow;
            this.btn_file.FlatAppearance.BorderSize = 0;
            this.btn_file.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Snow;
            this.btn_file.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Snow;
            this.btn_file.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_file.Location = new System.Drawing.Point(12, 761);
            this.btn_file.Name = "btn_file";
            this.btn_file.Size = new System.Drawing.Size(75, 23);
            this.btn_file.TabIndex = 1;
            this.btn_file.Text = "File";
            this.btn_file.UseVisualStyleBackColor = false;
            this.btn_file.Click += new System.EventHandler(this.btn_file_Click);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.Location = new System.Drawing.Point(579, 743);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(102, 15);
            this.timeLabel.TabIndex = 3;
            this.timeLabel.Text = "00:00:00 / 00:00:00";
            this.timeLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // bar_video
            // 
            this.bar_video.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bar_video.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.bar_video.Location = new System.Drawing.Point(0, 720);
            this.bar_video.Margin = new System.Windows.Forms.Padding(0);
            this.bar_video.Name = "bar_video";
            this.bar_video.Size = new System.Drawing.Size(1267, 12);
            this.bar_video.TabIndex = 2;
            this.bar_video.Click += new System.EventHandler(this.bar_video_Click);
            // 
            // btn_pause
            // 
            this.btn_pause.BackColor = System.Drawing.Color.Snow;
            this.btn_pause.Location = new System.Drawing.Point(590, 761);
            this.btn_pause.Name = "btn_pause";
            this.btn_pause.Size = new System.Drawing.Size(75, 23);
            this.btn_pause.TabIndex = 4;
            this.btn_pause.Text = "Pause";
            this.btn_pause.UseVisualStyleBackColor = false;
            this.btn_pause.Click += new System.EventHandler(this.btn_pause_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(1076, 761);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(105, 19);
            this.checkBox1.TabIndex = 5;
            this.checkBox1.Text = "Pre-processing";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // red_slider
            // 
            this.red_slider.Location = new System.Drawing.Point(304, 814);
            this.red_slider.Name = "red_slider";
            this.red_slider.Size = new System.Drawing.Size(214, 45);
            this.red_slider.TabIndex = 6;
            this.red_slider.Scroll += new System.EventHandler(this.red_slider_Scroll);
            // 
            // blue_slider
            // 
            this.blue_slider.Location = new System.Drawing.Point(744, 814);
            this.blue_slider.Name = "blue_slider";
            this.blue_slider.Size = new System.Drawing.Size(214, 45);
            this.blue_slider.TabIndex = 7;
            this.blue_slider.Scroll += new System.EventHandler(this.blue_slider_Scroll);
            // 
            // green_slider
            // 
            this.green_slider.Location = new System.Drawing.Point(524, 814);
            this.green_slider.Name = "green_slider";
            this.green_slider.Size = new System.Drawing.Size(214, 45);
            this.green_slider.TabIndex = 8;
            this.green_slider.Scroll += new System.EventHandler(this.green_slider_Scroll);
            // 
            // red_label
            // 
            this.red_label.AutoSize = true;
            this.red_label.Location = new System.Drawing.Point(395, 796);
            this.red_label.Name = "red_label";
            this.red_label.Size = new System.Drawing.Size(52, 15);
            this.red_label.TabIndex = 9;
            this.red_label.Text = "Low Red";
            // 
            // blue_label
            // 
            this.blue_label.AutoSize = true;
            this.blue_label.Location = new System.Drawing.Point(827, 796);
            this.blue_label.Name = "blue_label";
            this.blue_label.Size = new System.Drawing.Size(55, 15);
            this.blue_label.TabIndex = 10;
            this.blue_label.Text = "Low Blue";
            this.blue_label.Click += new System.EventHandler(this.blue_label_Click);
            // 
            // green_label
            // 
            this.green_label.AutoSize = true;
            this.green_label.Location = new System.Drawing.Point(610, 796);
            this.green_label.Name = "green_label";
            this.green_label.Size = new System.Drawing.Size(63, 15);
            this.green_label.TabIndex = 11;
            this.green_label.Text = "Low Green";
            this.green_label.Click += new System.EventHandler(this.green_label_Click);
            // 
            // hred_slider
            // 
            this.hred_slider.Location = new System.Drawing.Point(304, 873);
            this.hred_slider.Name = "hred_slider";
            this.hred_slider.Size = new System.Drawing.Size(214, 45);
            this.hred_slider.TabIndex = 12;
            this.hred_slider.Scroll += new System.EventHandler(this.hred_slider_Scroll);
            // 
            // hred_label
            // 
            this.hred_label.AutoSize = true;
            this.hred_label.Location = new System.Drawing.Point(395, 855);
            this.hred_label.Name = "hred_label";
            this.hred_label.Size = new System.Drawing.Size(56, 15);
            this.hred_label.TabIndex = 13;
            this.hred_label.Text = "High Red";
            // 
            // hgreen_label
            // 
            this.hgreen_label.AutoSize = true;
            this.hgreen_label.Location = new System.Drawing.Point(610, 855);
            this.hgreen_label.Name = "hgreen_label";
            this.hgreen_label.Size = new System.Drawing.Size(67, 15);
            this.hgreen_label.TabIndex = 15;
            this.hgreen_label.Text = "High Green";
            // 
            // hgreen_slider
            // 
            this.hgreen_slider.Location = new System.Drawing.Point(524, 873);
            this.hgreen_slider.Name = "hgreen_slider";
            this.hgreen_slider.Size = new System.Drawing.Size(214, 45);
            this.hgreen_slider.TabIndex = 14;
            this.hgreen_slider.Scroll += new System.EventHandler(this.hgreen_slider_Scroll);
            // 
            // hblue_label
            // 
            this.hblue_label.AutoSize = true;
            this.hblue_label.Location = new System.Drawing.Point(827, 855);
            this.hblue_label.Name = "hblue_label";
            this.hblue_label.Size = new System.Drawing.Size(59, 15);
            this.hblue_label.TabIndex = 17;
            this.hblue_label.Text = "High Blue";
            // 
            // hblue_slider
            // 
            this.hblue_slider.Location = new System.Drawing.Point(744, 873);
            this.hblue_slider.Name = "hblue_slider";
            this.hblue_slider.Size = new System.Drawing.Size(214, 45);
            this.hblue_slider.TabIndex = 16;
            this.hblue_slider.Scroll += new System.EventHandler(this.hblue_slider_Scroll);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1264, 930);
            this.Controls.Add(this.hblue_label);
            this.Controls.Add(this.hblue_slider);
            this.Controls.Add(this.hgreen_label);
            this.Controls.Add(this.hgreen_slider);
            this.Controls.Add(this.hred_label);
            this.Controls.Add(this.hred_slider);
            this.Controls.Add(this.green_label);
            this.Controls.Add(this.blue_label);
            this.Controls.Add(this.red_label);
            this.Controls.Add(this.green_slider);
            this.Controls.Add(this.blue_slider);
            this.Controls.Add(this.red_slider);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.btn_pause);
            this.Controls.Add(this.bar_video);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.btn_file);
            this.Controls.Add(this.pictureBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "RipoGr20";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.red_slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.blue_slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.green_slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hred_slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hgreen_slider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hblue_slider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private OpenFileDialog openFileDialog1;
        private PictureBox pictureBox1;
        private Button btn_file;
        private Label timeLabel;
        private ProgressBarEx bar_video;
        private Button btn_pause;
        private CheckBox checkBox1;
        private TrackBar red_slider;
        private TrackBar blue_slider;
        private TrackBar green_slider;
        private Label red_label;
        private Label blue_label;
        private Label green_label;
        private TrackBar hred_slider;
        private Label hred_label;
        private Label hgreen_label;
        private TrackBar hgreen_slider;
        private Label hblue_label;
        private TrackBar hblue_slider;
    }
}