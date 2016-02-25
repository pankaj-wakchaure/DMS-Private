using DMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Service.Device
{
    public class DeviceService:IDeviceService
    {
        /// <summary>
        /// Reason : Get all onlone devices
        /// </summary>
        /// <returns></returns>
        public List<Tbl_Devices> GetAllOnlineDevices()
        {
           DMS.Data.ModelClasses.Tbl_Devices tblObj = new Data.ModelClasses.Tbl_Devices();
           return tblObj.GetAllOnlineDevices();
        }

        /// <summary>
        /// Reason : Get all onlone devices
        /// </summary>
        /// <returns></returns>
        public List<Tbl_ScreenshotDetails> GetScreensByDeviceId(string deviceId,string fromDate,string toDate)
        {
            DMS.Data.ModelClasses.Tbl_ScreenshotDetails tblObj = new Data.ModelClasses.Tbl_ScreenshotDetails();
            return tblObj.GetScreensByDeviceId(deviceId, fromDate, toDate);
        }
        /// <summary>
        /// Reason : Get all devices
        /// 
        /// </summary>
        /// <returns></returns>
        public List<Tbl_Devices> GetAllDevices()
        {
            DMS.Data.ModelClasses.Tbl_Devices tblObj = new Data.ModelClasses.Tbl_Devices();
            return tblObj.GetAllDevices();
        }

        public Boolean SetScreenshotIntervalByDeviceId(string deviceId, int interval)
        {
            DMS.Data.ModelClasses.Tbl_Devices tblObj = new Data.ModelClasses.Tbl_Devices();
          return  tblObj.setScreenshotInterval(deviceId,interval);        
        }
    }
}
