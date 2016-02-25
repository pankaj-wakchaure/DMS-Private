using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.IO;
using Data.ModelClasses;
//using Data.ModelClasses;

namespace ETestApp
{
    public partial class Form1 : Form
    {
        private DateTime dtBootTime = new DateTime();
        public Form1()
        {
            InitializeComponent();
        }

        #region take screenshot
        private  void button9_Click(object sender, System.EventArgs e)
        {
            string folderPath = @"E:\Pankaj Project\testImage\test"+System.DateTime.Now.TimeOfDay.Milliseconds+".jpg";
            //captureMonitorscreen(folderPath);
            Device d = new Device();
            d.captureMonitorscreen(folderPath);
        }

        public void captureMonitorscreen(string folderPath)
        {
            Rectangle bounds = Screen.PrimaryScreen.Bounds;
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                { 
                    g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size); 
                }
                bitmap.Save(@folderPath, ImageFormat.Jpeg);
                
                //getBase64ScreenShot(folderPath);
                string folderPathn = @"E:\Pankaj Project\testImage\test" + System.DateTime.Now.TimeOfDay.Milliseconds + ".jpg";
                
                Image image = Image.FromFile(@folderPath);
                float scaleFactor = 0.5f;
                var newWidth = (int)(bounds.Width * scaleFactor);
                var newHeight = (int)(bounds.Height * scaleFactor);
                var thumbnailImg = new Bitmap(newWidth, newHeight);
                var thumbGraph = Graphics.FromImage(thumbnailImg);
                thumbGraph.CompositingQuality = CompositingQuality.HighQuality;
                thumbGraph.SmoothingMode = SmoothingMode.HighQuality;
                thumbGraph.InterpolationMode = InterpolationMode.HighQualityBicubic;
                var imageRectangle = new Rectangle(0, 0, newWidth, newHeight);
                thumbGraph.DrawImage(image, imageRectangle);
                thumbnailImg.Save(@folderPathn, image.RawFormat);
                thumbnailImg.Dispose();
                thumbGraph.Dispose();
                Image imageCom = Image.FromFile(@folderPathn);
                getBase64ScreenShot(@folderPathn);
             //   SaveJPGWithCompressionSetting(imageCom, @folderPathn, 25L);
             
            }
        }
        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }
        private static void SaveJPGWithCompressionSetting(Image image, string szFileName, long lCompression)
        {
            EncoderParameters eps = new EncoderParameters(1);
            eps.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, lCompression);
            ImageCodecInfo ici = GetEncoderInfo("image/jpeg");
            image.Save(szFileName, ici, eps);
        }
#endregion

        #region base64
        public string getBase64ScreenShot(string folderPath)
        {
            string filName = null;
            filName = folderPath;
            if (string.IsNullOrEmpty(filName))
                return null;

            Image image = Image.FromFile(filName);
            string returnstring = ImageToBase64(image, ImageFormat.Jpeg);
            Tbl_ScreenshotDetails sdetail = new Tbl_ScreenshotDetails();
            sdetail.SaveImageToDb("pkjDevice", returnstring);
            return returnstring;
        }

        /// <summary>
        /// MEthod to convert image to base64 string
        /// </summary>
        public string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }        
        #endregion
    }
}
