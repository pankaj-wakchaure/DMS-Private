using Data.ModelClasses;
using ETestApp.Dservice;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ETestApp
{
   public class Device
    {
        #region take screenshot
        private void button9_Click(object sender, System.EventArgs e)
        {
            string folderPath = Application.StartupPath;//@"E:\Pankaj Project\testImage" ;
            DeleteFiles(folderPath);
            captureMonitorscreen(folderPath + "\test"+System.DateTime.Now.TimeOfDay.Milliseconds + ".jpg");
        }
       public void DeleteFiles(string path)
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                foreach(FileInfo fl in dir.GetFiles())
                {
                    fl.Delete();
                }
            }
            catch (Exception)
            { 
            }
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

                //getBase64ScreenShot(folderPath);//@"E:\Pankaj Project\testImage
                string folderPathn = Application.StartupPath+"\\test" + System.DateTime.Now.TimeOfDay.Milliseconds + ".jpg";

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
                //  SaveJPGWithCompressionSetting(imageCom, @folderPathn, 25L);
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
            SendImage(Program.DeviceName, returnstring);
            return returnstring;
        }

        /// <summary>
        /// Method to convert image to base64 string
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
        #region SaveImage        
        public static void SendImage(string deviceId, string imageBas64)
        {
            try
            {
                Service1Client d = new Service1Client();
                ScreenshotDetails screenObj = new ScreenshotDetails();
                int length = !string.IsNullOrEmpty(imageBas64) ? imageBas64.Length : 0;

                screenObj.createdDate = System.DateTime.Now;
                screenObj.fk_DeviceId = deviceId;
                screenObj.isDeleted = false;

                if (length > 20000)
                {
                    screenObj.screenshot1 = imageBas64.Substring(0, 20000);
                }
                if (length < 20000)
                {
                    screenObj.screenshot1 = imageBas64.Substring(0, length);
                }

                if (length < 40000 && length > 20000)
                {
                    screenObj.screenshot2 = imageBas64.Substring(20000, (length - 20000));
                }

                if (length > 40000)
                {
                    screenObj.screenshot2 = imageBas64.Substring(20000, 20000);
                }

                if (length > 40000 && length < 60000)
                {
                    screenObj.screenshot3 = imageBas64.Substring(40000, (length - 40000));
                }

                if (length > 60000)
                {
                    screenObj.screenshot3 = imageBas64.Substring(40000, 20000);
                }
                //save half string

                screenObj.id = d.SaveImageObj(screenObj);

                if (length > 60000 && length < 80000)
                {
                    screenObj.screenshot4 = imageBas64.Substring(60000, (length - 60000));
                }

                if (length > 80000)
                {
                    screenObj.screenshot4 = imageBas64.Substring(60000, 20000);
                }
                if (length > 100000)
                {
                    screenObj.screenshot5 = imageBas64.Substring(80000, (length - 80000));
                }
                if (length > 60000)
                {
                    screenObj.screenshot1 = "";
                    screenObj.screenshot2 = "";
                    screenObj.screenshot3 = "";
                    d.UpdateImageObj(screenObj);
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion
        public void SaveDeviceIfNotExists(string deviceName)
        {
            Service1Client d = new Service1Client();
            try
            {
                DevicesDetails devDetail = new DevicesDetails();
                devDetail.createdDate = System.DateTime.Now;
                devDetail.deviceId = deviceName;
                devDetail.deviceName = deviceName;
                devDetail.deviceStatus = true;
                devDetail.isDeleted = false;
                devDetail.isShutdownDevice = false;
                d.SaveDeviceIfNotExists(devDetail);
            }
            catch (Exception)
            {
            }
        }
    }
}
