using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScreenshotTakerApp
{
    using System.Drawing.Imaging;
    using System.IO;
    using System.Threading;
    using System.IO.Compression;


    public partial class Form1 : Form
    {
        static string folder = "Screenshots";

        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        static int alarmCounter = 1;
        static bool exitFlag = false;


        public Form1()
        {
            InitializeComponent();
        }

        private static void TimerEventProcessor(Object myObject,
                                                EventArgs myEventArgs)
        {
            AddImage();            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            CheckFolder();
            myTimer.Tick += TimerEventProcessor;
            myTimer.Interval = 5000;
            myTimer.Start();
        }

        private void startButton_Click(object sender, EventArgs e)
        {
            string fileName = AddImage();            
        }

        private static string AddImage()
        {
            Rectangle bounds = Screen.GetBounds(Point.Empty);
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }

                long ticks = DateTime.Now.Ticks;
                string filename = $@"{folder}\{ticks}.jpeg";
                bitmap.Save(filename, ImageFormat.Jpeg);
                return filename;
            }
        }

        private void CheckFolder()
        {
            bool exists = Directory.Exists(folder);
            if (!exists)
            {
                var directoryInfo = Directory.CreateDirectory(folder);
            }
        }
    }
}
