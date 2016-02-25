using DMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Data.ModelClasses
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
        public List<Models.Tbl_ScreenshotDetails> GetScreensByDeviceId(string deviceId , string fromDate,string toDate)
        {
            entity = new MonitoringSystemEntities();
            DateTime dtTime = System.DateTime.Now;

            //subtract 2 minutes from  current time to check screnn shots from last 2 minutes for device
            dtTime = dtTime.AddMinutes(-2);
            try
            {
                if (string.IsNullOrEmpty(fromDate) || string.IsNullOrEmpty(toDate))
                {
                    var v = (from t in entity.Tbl_Devices
                             join t1 in entity.Tbl_ScreenshotDetails on t.DeviceId equals t1.Fk_DeviceId
                             where t.DeviceId == deviceId //&& t1.CreatedDate >= dtTime
                             select t1).OrderByDescending(p => p.CreatedDate).Take(15);
                    return v.ToList();
                }
                else
                {
                    DateTime fromDatedate = Convert.ToDateTime(fromDate);
                    DateTime toDatedate = Convert.ToDateTime(toDate);
                    
                    var v = (from t in entity.Tbl_Devices
                             join t1 in entity.Tbl_ScreenshotDetails on t.DeviceId equals t1.Fk_DeviceId
                             where t.DeviceId == deviceId && t1.CreatedDate >= fromDatedate && t1.CreatedDate<=toDatedate
                             select t1).OrderByDescending(p => p.CreatedDate);
                    return v.ToList();
                  }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
