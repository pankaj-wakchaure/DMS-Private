using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Timers;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO.Compression;
using ETestApp.Dservice;
using ETestApp.SystemTray;

namespace ETestApp
{
    class Program
    {
        #region Global Declaration
       public static string DeviceName = "Default Device";
        #endregion
       static void Main(string[] args)
       {
           //temp();
           Device d = new Device();
           DeviceName = Environment.MachineName;
           //get interval
           int interval = 5;
           //interval=d.GetInterval("DeviceName");//after udate service it will get
           Timer(interval);
           d.SaveDeviceIfNotExists(DeviceName);
           System.Threading.Thread guiThread = new System.Threading.Thread(startGui);
           guiThread.Start();
           //Application.Run(new Form1());
       }
        public static void Timer(int interval)
        {
                System.Timers.Timer timer;
                timer = new System.Timers.Timer(60 * 1000 * interval);
                timer.Elapsed += new ElapsedEventHandler(timer_Elapsed);
                timer.Enabled = true;
                timer.Start();
        }

        public static void timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            string folderPath = Application.StartupPath;
            Device d = new Device();
            d.DeleteFiles(folderPath);
            d.captureMonitorscreen(folderPath + "\\test" + System.DateTime.Now.TimeOfDay.Ticks + ".jpg");
        }
        static void startGui()
        {
            using (IdsProcessIcon pIcon = new IdsProcessIcon())
            {
                pIcon.Display();
                Application.Run();
            }
        }
        public static void temp()
        {
            string folderPath = Application.StartupPath;
            Device d = new Device();
            d.DeleteFiles(folderPath);
            d.captureMonitorscreen(folderPath + "\\test" + System.DateTime.Now.TimeOfDay.Ticks + ".jpg");
        }
    }
}
