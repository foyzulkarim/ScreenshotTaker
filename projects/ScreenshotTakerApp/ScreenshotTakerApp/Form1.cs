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
    using System.Net.Http;
    using System.Net.Http.Headers;

    public partial class Form1 : Form
    {
        public string ApiBaseUrl { get; set; } = "http://localhost:50962";

        static string folder = "Screenshots";

        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        static int alarmCounter = 1;
        static bool exitFlag = false;


        public Form1()
        {
            InitializeComponent();
        }

        private void TimerEventProcessor(Object myObject,
                                                EventArgs myEventArgs)
        {           
            string image = AddImage();
            byte[] imageArray = System.IO.File.ReadAllBytes($@"{image}");
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);
            this.richTextBox1.Text = base64ImageRepresentation;
            Thread.Sleep(1000);
            this.richTextBox1.Clear();
            UploadImage(base64ImageRepresentation);
            File.Delete(image);            
        }

        private void UploadImage(string image)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                HttpContent stringContent = new StringContent(image, Encoding.UTF8, "application/text");
                string uri = $"{this.ApiBaseUrl}/api/values";
                var response = client.PostAsync(uri, stringContent).Result;
                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception();
                }

                this.richTextBox1.Text = response.Content.ReadAsStringAsync().Result;                
            }

        }



        private void Form1_Load(object sender, EventArgs e)
        {
            CheckFolder();
            myTimer.Tick += TimerEventProcessor;
            myTimer.Interval = 5000;
            myTimer.Start();
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
