﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Face_Recognition___Attendance_System
{
    public partial class Form1 : Form
    {
        HaarCascade faceDetected;
        MCvFont font = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_TRIPLEX, 0.6d, 0.6d);
        Image<Bgr, Byte> Frame;
        Capture camera;
        Image<Gray, byte> result;
        Image<Gray, byte> TrainedFace = null;
        Image<Gray, byte> grayface = null;
        List<Image<Gray, byte>> trainingImages = new List<Image<Gray, byte>>();
        List<string> labels = new List<string>();
        List<string> Users = new List<string>();
        int Count, NumLabels, t;
        string name, names = null;

        public Form1()
        {
            InitializeComponent();
            faceDetected = new HaarCascade("haarcascade_frontalface_default.xml");
            try
            {
                string Labelsinf = File.ReadAllText(Application.StartupPath + "/Faces/Faces.txt");
                string [] Labels = Labelsinf.Split(',');
                NumLabels = Convert.ToInt32(Labels[0]);
                Count = NumLabels;
                string FacesLoad;
                for(int i = 1; i < NumLabels + 1; i++)
                {
                    FacesLoad = "face" + i + ".bmp";
                    trainingImages.Add(new Image<Gray, byte>(Application.StartupPath + "/Faces/face" + i + ".bmp"));
                    labels.Add(Labels[i]);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Nothing in The Files" + ex);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            camera = new Capture();
            camera.QueryFrame();
            Application.Idle += new EventHandler(FrameProcedure);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Count = Count + 1;
            grayface = camera.QueryGrayFrame().Resize(567, 490, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
            MCvAvgComp[][] facedetected = grayface.DetectHaarCascade(faceDetected, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));
            foreach (MCvAvgComp f in facedetected[0])
            {
                TrainedFace = Frame.Copy(f.rect).Convert<Gray, byte>();
                break;
            }
            TrainedFace = result.Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
            trainingImages.Add(TrainedFace);
            labels.Add(textBox1.Text);
            File.WriteAllText(Application.StartupPath + "/Faces/Faces.txt", trainingImages.ToArray().Length.ToString() + ",");
            for (int i = 1; i < trainingImages.ToArray().Length + 1; i++)
            {
                trainingImages.ToArray()[i - 1].Save(Application.StartupPath + "/Faces/face" + i + ".bmp");
                File.AppendAllText(Application.StartupPath + "/Faces/Faces.txt", labels.ToArray()[i - 1] + ",");
            }
            MessageBox.Show(textBox1.Text + " Added Sucessfully");
        }

        private void FrameProcedure(object sender, EventArgs e)
        {
            Users.Add("");
            Frame = camera.QueryFrame().Resize(567, 490, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
            grayface = Frame.Convert<Gray, Byte>();
            MCvAvgComp[][] facesnow = grayface.DetectHaarCascade(faceDetected, 1.2, 10, Emgu.CV.CvEnum.HAAR_DETECTION_TYPE.DO_CANNY_PRUNING, new Size(20, 20));
            foreach (MCvAvgComp f in facesnow[0])
            {
                result = Frame.Copy(f.rect).Convert<Gray, Byte>().Resize(100, 100, Emgu.CV.CvEnum.INTER.CV_INTER_CUBIC);
                Frame.Draw(f.rect, new Bgr(Color.Green), 3);
                if(trainingImages.ToArray().Length != 0)
                {
                    MCvTermCriteria term = new MCvTermCriteria(Count, 0.001);
                    EigenObjectRecognizer recognizer = new EigenObjectRecognizer(trainingImages.ToArray(), labels.ToArray(), 1500, ref term);
                    name = recognizer.Recognize(result);
                    Frame.Draw(name, ref font, new Point(f.rect.X - 2, f.rect.Y - 2), new Bgr(Color.Red));
                }
                Users.Add("");
            }
            Camerabox.Image = Frame;
            names = "";
            Users.Clear();
        }
    }
}
