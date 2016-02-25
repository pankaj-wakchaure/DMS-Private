using ETestApp.Models;
//using Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelClasses
{
   public class Tbl_ScreenshotDetails
    {
        #region Global declartion
        MonitoringSystemEntities entity = null;
        #endregion
        /// <summary>
        /// Auther: Pankaj W.
        /// Date :17-08-15
        /// Reason : get lates max 5 screen shots based on id
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns> list Tbl_ScreenshotDetails</returns>
        public List<ETestApp.Models.Tbl_ScreenshotDetails> GetScreensByDeviceId(string deviceId)
        {
            entity = new MonitoringSystemEntities();
            DateTime dtTime = System.DateTime.Now;
            //subtract 2 minutes from  current time to check screnn shots from last 2 minutes for device
            dtTime = dtTime.AddMinutes(-2);
            try
            {
                var v = (from t in entity.Tbl_Devices
                         join t1 in entity.Tbl_ScreenshotDetails on t.DeviceId equals t1.Fk_DeviceId
                         where t.DeviceId == deviceId //&& t1.CreatedDate >= dtTime
                         select t1).OrderByDescending(p=>p.CreatedDate).Take(15);
                return v.ToList();

            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveImageToDb(string deviceId, string imageBas64)
        {
            int length = !string.IsNullOrEmpty(imageBas64) ? imageBas64.Length : 0;
            entity = new MonitoringSystemEntities();
            ETestApp.Models.Tbl_ScreenshotDetails screenObj = new ETestApp.Models.Tbl_ScreenshotDetails();
            screenObj.CreatedDate = System.DateTime.Now;
            
            if (length > 20000)
            {
                screenObj.Screenshot1 = imageBas64.Substring(0,20000);
            }
            if (length < 20000)
            {
                screenObj.Screenshot1 = imageBas64.Substring(0, length);
            }
            
            if(length< 40000 && length > 20000)
            {
                screenObj.Screenshot2 = imageBas64.Substring(20000, (length-20000));
            }
            
            if (length > 40000)
            {
                screenObj.Screenshot2 = imageBas64.Substring(20000, 20000);
            }

            if (length > 40000 && length < 60000)
            {
                screenObj.Screenshot3 = imageBas64.Substring(40000, (length-40000));
            }

            if (length > 60000)
            {
                screenObj.Screenshot3 = imageBas64.Substring(40000, 20000);
            }

            if (length > 60000 && length < 80000)
            {
                screenObj.Screenshot4 = imageBas64.Substring(60000, (length-60000));
            }

            if (length > 80000)
            {
                screenObj.Screenshot4 = imageBas64.Substring(60000, 20000);
            }
            if (length > 100000)
            {
                screenObj.Screenshot5 = imageBas64.Substring(80000, (length-80000));
            }

            screenObj.Fk_DeviceId = deviceId;
            screenObj.IsDeleted = false;
            entity.Tbl_ScreenshotDetails.Add(screenObj);
            entity.SaveChanges();

        }

    }
}
