using System.IO;
using System;
using System.Drawing;
using System.IO;
using System.Security;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Collections;
using System.Diagnostics;
using Emgu.CV.Util;
using Emgu.CV.Cuda;
using System.Media;

namespace RIPO; 

public partial class Form1 : Form
{
    CancellationTokenSource _cts = new();
    private Queue<Mat> _frames;
    private bool _capturing { get; set; }
    private int _currentFrame { get; set; }
    private bool _pause { get; set; }
    private double _fps { get; set; }
    private int _fpsI { get; set; }
    private int _frameCount { get; set; }
    private bool _preprocessing { get; set; }
    private Mat _normalFrame { get; set; }
    private Mat _processedFrame { get; set; }
    private double _redVal { get; set; }
    private double _blueVal { get; set; }
    private double _greenVal { get; set; }
    private double _hRedVal { get; set; }
    private double _hBlueVal { get; set; }
    private double _hGreenVal { get; set; }
    private Rectangle _boundingRect { get; set; }
    private TimeSpan _timeSpan { get; set; }
    private CascadeClassifier _cascadeClassifier { get; set; }
    public Form1()
    {
        InitializeComponent();
        openFileDialog1 = new OpenFileDialog();
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        this.StartPosition = FormStartPosition.CenterScreen;
        //this.red_slider.Hide();
        //this.red_label.Hide();
        //this.green_slider.Hide();
        //this.green_label.Hide();
        //this.blue_slider.Hide();
        //this.blue_label.Hide();

        _frames = new Queue<Mat>();
        _capturing = false;
        _currentFrame = 0;
        _pause = false;
        _fps = 0;
        _fpsI = 0;
        _frameCount = 0;
        _preprocessing = false;
        _normalFrame = new();
        _processedFrame = new();
        _redVal = 0;
        _blueVal = 0;
        _greenVal = 0;
        _hRedVal = 0;
        _hGreenVal = 0;
        _hBlueVal = 0;
        red_slider.Maximum = 255;
        red_slider.Value = Convert.ToInt32(_redVal);
        green_slider.Maximum = 255;
        green_slider.Value = Convert.ToInt32(_greenVal);
        blue_slider.Maximum = 255;
        blue_slider.Value = Convert.ToInt32(_blueVal);
        hred_slider.Maximum = 255;
        hred_slider.Value = Convert.ToInt32(_hRedVal);
        hgreen_slider.Maximum = 255;
        hgreen_slider.Value = Convert.ToInt32(_hGreenVal);
        hblue_slider.Maximum = 255;
        hblue_slider.Value = Convert.ToInt32(_hBlueVal);
        _boundingRect = new();
        _timeSpan = new();
        _cascadeClassifier = new(@"C:\Users\lgszu\source\repos\RIPO\RIPO\stop_data.xml");
    }

    private void pictureBox1_Click(object sender, EventArgs e)
    {
        return;
    }
    private async void btn_file_Click(object sender, EventArgs e)
    {
        string path = Directory.GetCurrentDirectory();
        openFileDialog1.InitialDirectory = path;
        openFileDialog1.DefaultExt = ".mp4";
        openFileDialog1.Filter = "Video Files|*.mp4;*.mkv;...";
        pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        var dialogResult = openFileDialog1.ShowDialog();

        if(dialogResult != DialogResult.OK)
        {
            return;
        }

        if (_capturing)
        {
            _cts.Cancel();
            await Task.Delay(200);
            _cts.Dispose();
            _cts = new CancellationTokenSource();
            _currentFrame = 0;
            _pause = false;
            this.btn_pause.Text = "Pause";
        }

        string fileExt = Path.GetFullPath(openFileDialog1.FileName);
        VideoCapture _capture = new(fileExt);
        _fps = _capture.Get(CapProp.Fps);
        _fpsI = Convert.ToInt32(_fps);
        _frameCount = (int)Math.Floor(_capture.Get(CapProp.FrameCount));
        _timeSpan = TimeSpan.FromSeconds(Convert.ToInt32(_frameCount / _fps));
        _capturing = false;
        _capture.Read(_normalFrame);
        _frames.Enqueue(_normalFrame);
        this.pictureBox1.Image = _normalFrame.ToBitmap();
        this.bar_video.Maximum = _frameCount;
        int waitTime = Convert.ToInt32(1000 / _fps);

        if (_capture == null)
        {
            return;
        }

        _capturing = true;
        Stopwatch sw = new();
        Task processFrame = Task.Delay(1);

        for (int i = 0; _currentFrame < _frameCount;)
        {
            sw.Start();

            if (_cts.Token.IsCancellationRequested)
            {
                sw.Stop();
                _capture.Dispose();
                break;
            }

            if (_pause)
            {
                if (i != _currentFrame)
                {
                    _capture.Set(CapProp.PosFrames, _currentFrame);
                    i = _currentFrame;
                    SetTime();
                    _boundingRect = new();
                    _capture.Read(_normalFrame);
                    await GetStopSignCords();
                    PutBoxAndLabel();
                    this.pictureBox1.Image = _normalFrame.ToBitmap();
                    this.bar_video.Value = _currentFrame;
                }
                sw.Stop();
                await Task.Delay(waitTime);
                continue;
            }

            if (i != _currentFrame)
            {
                _capture.Set(CapProp.PosFrames, _currentFrame);
                i = _currentFrame;
                SetTime();
            }

            this.bar_video.Value = _currentFrame;

            if (_currentFrame % _fpsI == 0)
            {
                SetTime();
            }

            _capture.Read(_normalFrame);

            if (_normalFrame.IsEmpty)
            {
                sw.Stop();
                _capture.Dispose();
                break;
            }

            if (_preprocessing && processFrame.IsCompleted)
            {
                processFrame = Task.Run(() => GetStopSignCords());
            }

            PutBoxAndLabel();
            this.pictureBox1.Image = _normalFrame.ToBitmap();
            _currentFrame++;
            i++;
            sw.Stop();
            int proccesTime = Convert.ToInt32(sw.ElapsedMilliseconds) + 2;

            if (proccesTime < waitTime)
            {
                await Task.Delay(waitTime - proccesTime);
            }

            sw.Reset();

        }
        sw.Stop();
        _capture.Dispose();
        await Task.Delay(100);
        //pictureBox1.Image = _frames.Dequeue().ToBitmap();
        timeLabel.Text = _timeSpan + " / " + _timeSpan;
        btn_pause.Text = "Play";
        _pause = true;
    }

    async Task GetStopSignCords()
    {
        var modes = new (int, int)[]
        {
            (130,70),(125,65),(125,60),(120,30),(125,35),(130,30),(130,40),(140,120),(140,130),(130,130)
        };
        List<Task<(Rectangle, Mat)>> tasks = new();
        foreach (var mode in modes)
        {
            tasks.Add(Task.Run(() => ProcessFrame(_normalFrame, mode.Item1, mode.Item2)));
        }

        await Task.WhenAll(tasks);
        List<Rectangle> boxes = new();
        foreach(var task in tasks)
        {
            if(task.Result.Item1.X == 0 && task.Result.Item1.Y == 0)
            {
                continue;
            }
            boxes.Add(task.Result.Item1);
        }
        _boundingRect = new();
        if (boxes.Count() == 0)
        {
            _processedFrame = tasks[0].Result.Item2;
            return;
        }
        Debug.WriteLine("New Sign");
        List<Tuple<int,Rectangle>> ints = new();
        foreach(var box in boxes)
        {
            Debug.WriteLine($"X: {box.X} Y: {box.Y}");
            ints.Add(new(box.X + box.Y,box));
        }
        ints = ints.OrderByDescending(x => x.Item1).ToList();
        int average = 0;
        foreach(var item in ints)
        {
            average += item.Item1;
        }
        average /= ints.Count();
        int finalX = 0;
        int finalY = 0;
        int finalW = 0;
        int finalH = 0;
        int iterator = 0;
        foreach (var item in ints)
        {
            if(Math.Abs(item.Item1 - average) <= 150 )
            {
                finalX += item.Item2.X;
                finalY += item.Item2.Y;
                finalW += item.Item2.Width;
                finalH += item.Item2.Height;

                iterator++;
            }
        }
        finalX /= iterator;
        finalY /= iterator;
        finalW /= iterator;
        finalH /= iterator;
        _boundingRect = new(finalX,finalY,finalW,finalH);
        _processedFrame = tasks[0].Result.Item2;
    }

    private (Rectangle,Mat) ProcessWithClassifier(Mat frame)
    {
        Mat processedFrame = new();
        frame.CopyTo(processedFrame);
        List<Rectangle> rects = new();

        rects = _cascadeClassifier.DetectMultiScale(processedFrame).ToList();

        if (rects.Count() == 0)
            return (new(), processedFrame);

        var largestRect = rects.Aggregate((r1, r2) =>
            (r1.Height * r1.Width) > (r2.Height * r2.Width) ? r1 : r2);

        return (largestRect, processedFrame);
    }

    private (Rectangle,Mat) ProcessFrame(Mat frame,double green, double blue)
    {
        Mat processedFrame = new();
        frame.CopyTo(processedFrame);
        //return ProcessWithClassifier(processedFrame);
        CvInvoke.CvtColor(processedFrame, processedFrame, ColorConversion.Bgr2Hsv);
        //Crop image
        CvInvoke.PyrDown(processedFrame, processedFrame);

        CvInvoke.GaussianBlur(processedFrame, processedFrame, new Size(5, 5), 1.5, 1.5);



        double lRed = 0;
        double lGreen = _greenVal != 0 ? _greenVal : green;
        double lBlue = _blueVal != 0 ? _blueVal : blue;

        double hRedVal = 255;
        double hGreen = 255;
        double hBlue = 255; 

        ScalarArray lower = new(new MCvScalar(lRed, lGreen, lBlue)); //0, 130, 70
        ScalarArray upper = new(new MCvScalar(hRedVal, hGreen, hBlue)); //255, 220, 220

        Mat hier = new();
        VectorOfVectorOfPoint contours = new();
        CvInvoke.InRange(processedFrame, lower, upper, processedFrame);

        CvInvoke.Erode(processedFrame, processedFrame, null, new Point(1, 1), 1, BorderType.Default, CvInvoke.MorphologyDefaultBorderValue);
        CvInvoke.Dilate(processedFrame, processedFrame, null, new Point(1, 1), 1, BorderType.Default, CvInvoke.MorphologyDefaultBorderValue);
        CvInvoke.Canny(processedFrame, processedFrame, 30, 100, 3, true);

        CvInvoke.FindContours(processedFrame, contours, hier, RetrType.Tree, ChainApproxMethod.ChainApproxSimple);

        List<Rectangle> boxes = new();
        Parallel.For(0, contours.Size, i =>
        {
            VectorOfPoint approx = new();
            double arcLen = CvInvoke.ArcLength(contours[i], true);
            CvInvoke.ApproxPolyDP(contours[i], approx, 0.1 * arcLen, true);
            double contourArea = CvInvoke.ContourArea(approx, false);
            if (contourArea > 5000)
            {
                if (approx.Length % 8 == 0)
                {
                    var moment = CvInvoke.Moments(approx);
                    var box = CvInvoke.BoundingRectangle(approx);
                    boxes.Add(box);
                    //sprawdzanie czy wzglednie kwadratowe
                }
            }
        });

        if(boxes.Count() != 0)
        {
            return (boxes[0], processedFrame);
        }

        return (new(), processedFrame);
    }

    void SetTime()
    {
        int seconds = Convert.ToInt32(_currentFrame / _fps);
        TimeSpan elapsedTime = TimeSpan.FromSeconds(seconds);
        this.timeLabel.Text = elapsedTime + " / " + _timeSpan;
    }

    void PutBoxAndLabel()
    {
        if (_boundingRect.X == 0 && _boundingRect.Y == 0)
        {
            return;
        }
        CvInvoke.Rectangle(_normalFrame, _boundingRect, new MCvScalar(0, 255, 0));
        CvInvoke.PutText(_normalFrame, "Znak stopu B20", new Point(_boundingRect.X, _boundingRect.Y - 5), FontFace.HersheyComplexSmall, 1, new MCvScalar(0, 255, 0));
    }

    private void Form1_Load(object sender, EventArgs e)
    {

    }

    private void label1_Click(object sender, EventArgs e)
    {
        return;
    }

    private void bar_video_Click(object sender, EventArgs e)
    {
        if(!_capturing)
        {
            return;
        }
        float absoluteMouse = (PointToClient(MousePosition).X - this.bar_video.Bounds.X);
        float calcFactor = this.bar_video.Width / (float)this.bar_video.Maximum;
        float relativeMouse = absoluteMouse / calcFactor;
        _currentFrame = Convert.ToInt32(relativeMouse);
    }

    private void btn_pause_Click(object sender, EventArgs e)
    {
        if (!_capturing)
        {
            return;
        }
        if(_pause)
        {
            this.btn_pause.Text = "Pause";
            _pause = false;

            return;
        }
        _pause = true;
        this.btn_pause.Text = "Play";
    }

    private void checkBox1_CheckedChanged(object sender, EventArgs e)
    {
        if (!_capturing)
        {
            checkBox1.Checked = false;
            return;
        }
        if (_preprocessing)
        {
            _preprocessing = false;
            if (_pause)
            {
                this.pictureBox1.Image = _normalFrame.ToBitmap();
            }
            return;
        }
        _preprocessing = true;
        if(_pause)
        {
            _boundingRect = new();
            GetStopSignCords();
            this.pictureBox1.Image = _processedFrame.ToBitmap();
        }
    }
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
        if (!msg.HWnd.Equals(Handle) &&
            (keyData == Keys.Left || keyData == Keys.Right ||
             keyData == Keys.Up || keyData == Keys.Down))
        {
            if(_capturing)
            {
                switch (keyData)
                {
                    case Keys.Left:
                        {
                            if(_currentFrame <= _fpsI * 5)
                            {
                                _currentFrame = 0;
                            }
                            else
                            {
                                _currentFrame -= _fpsI * 5;
                            }
                        }
                        break;
                    case Keys.Right:
                        {
                            _currentFrame += _fpsI * 5;
                        }
                        break;
                    case Keys.Up:
                        {
                            
                        }
                        break;
                    case Keys.Down:
                        {
                           
                        }
                        break;

                }
            }

            return true;
        }

        return base.ProcessCmdKey(ref msg, keyData);
    }

    private void Form1_KeyPress(object sender, KeyEventArgs e)
    {

    }
    
    private bool renderOnPause(Label label, TrackBar slider, string sliderName)
    {
        if(!_capturing)
        {
            slider.Value = 0;
        }
        if (_pause && _preprocessing)
        {;
            label.Text = sliderName + ": " + slider.Value.ToString();
            switch(sliderName)
            {
                case "Red" :
                    _redVal = slider.Value;
                    break;
                case "Green":
                    _greenVal = slider.Value;
                    break;
                case "Blue":
                    _blueVal = slider.Value;
                    break;
                case "hRed":
                    _hRedVal = slider.Value;
                    break;
                case "hGreen":
                    _hGreenVal = slider.Value;
                    break;
                case "hBlue":
                    _hBlueVal = slider.Value;
                    break;
            }
            _boundingRect = new();
            GetStopSignCords();
            this.pictureBox1.Image = _processedFrame.ToBitmap();
            return true;
        }

        return false;
    }

    private void red_slider_Scroll(object sender, EventArgs e)
    {
        renderOnPause(red_label, red_slider, "Red");
    }

    private void green_slider_Scroll(object sender, EventArgs e)
    {
        renderOnPause(green_label, green_slider, "Green");
    }

    private void blue_slider_Scroll(object sender, EventArgs e)
    {
        renderOnPause(blue_label, blue_slider, "Blue");
    }

    private void blue_label_Click(object sender, EventArgs e)
    {

    }

    private void green_label_Click(object sender, EventArgs e)
    {

    }

    private void hred_slider_Scroll(object sender, EventArgs e)
    {
        renderOnPause(hred_label, hred_slider, "hRed");
    }

    private void hgreen_slider_Scroll(object sender, EventArgs e)
    {
        renderOnPause(hgreen_label, hgreen_slider, "hGreen");
    }

    private void hblue_slider_Scroll(object sender, EventArgs e)
    {
        renderOnPause(hblue_label, hblue_slider, "hBlue");
    }
}
