using DMS.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DMS.API
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {

        #region Global Declaration
        MonitoringSystemEntities entity = null;
        #endregion         
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public void SaveImage(string deviceId, string imageBas64)
        {
            entity = new MonitoringSystemEntities();
            Tbl_ScreenshotDetails screenObj = new Tbl_ScreenshotDetails();
            screenObj.CreatedDate = System.DateTime.Now;
            screenObj.Screenshot1 = imageBas64;
            screenObj.Fk_DeviceId = deviceId;
            screenObj.IsDeleted = false;

            entity.Tbl_ScreenshotDetails.Add(screenObj);
            entity.SaveChanges();
        }

        public Int64 SaveImageObj(ScreenshotDetails scrnObj)
        {
            try
            {
                entity = new MonitoringSystemEntities();

                Tbl_ScreenshotDetails screenObj = new Tbl_ScreenshotDetails();
                screenObj.CreatedDate = System.DateTime.Now;
                screenObj.Screenshot1 = scrnObj.screenshot1;
                screenObj.Screenshot2 = scrnObj.screenshot2;
                screenObj.Screenshot3 = scrnObj.screenshot3;
                screenObj.Screenshot4 = scrnObj.screenshot4;
                screenObj.Screenshot5 = scrnObj.screenshot5;
                screenObj.Fk_DeviceId = scrnObj.fk_DeviceId;
                screenObj.IsDeleted = scrnObj.isDeleted;
                entity.Tbl_ScreenshotDetails.Add((screenObj));
                entity.SaveChanges();
                return screenObj.Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void UpdateImageObj(ScreenshotDetails scrnObj)
        {
            try
            {
                entity = new MonitoringSystemEntities();
                var screenObj = entity.Tbl_ScreenshotDetails.Where(p => p.Fk_DeviceId.Equals(scrnObj.fk_DeviceId) && p.Id == scrnObj.id && p.IsDeleted == false).FirstOrDefault();
                if (screenObj == null)
                    return;

                screenObj.Screenshot4 = scrnObj.screenshot4;
                screenObj.Screenshot5 = scrnObj.screenshot5;
                entity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        
        public void SaveDeviceIfNotExists(DevicesDetails devObj)
        {
            try
            {
                entity = new MonitoringSystemEntities();
                var v = entity.Tbl_Devices.Where(p => p.DeviceId.Equals(devObj.deviceId)).FirstOrDefault();
                if (v != null)
                    return;

                entity.Tbl_Devices.Add(PrepareDeviceDataModel(devObj));
                entity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public Tbl_Devices PrepareDeviceDataModel(DevicesDetails devObj)
        {
            Tbl_Devices dev = new Tbl_Devices();
            dev.CreatedDate = System.DateTime.Now;
            dev.DeviceId = devObj.deviceId;
            dev.DeviceIp = devObj.deviceIp;
            dev.DeviceName = devObj.deviceName;
            dev.DeviceStatus = devObj.deviceStatus;
            dev.IsDeleted = devObj.isDeleted;
            dev.IsShutdownDevice = devObj.isShutdownDevice;
            return dev;
        }
        /// <summary>
        /// Reason To get Interval
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public int GetInterval(string deviceId)
        {
            try
            {
                entity = new MonitoringSystemEntities();
                var v = entity.Tbl_Devices.Where(p => p.DeviceId == deviceId && p.IsDeleted == false).FirstOrDefault();
                if (v == null)
                    return 5;//default interval
                else
                    return v.ScreenshotInterval != null ? Convert.ToInt32(v.ScreenshotInterval) : 5;
            }
            catch (Exception)
            {                
                throw;
            }
        }

        public void UpdateDeviceIfNotExists(DevicesDetails devObj)
        {
            try
            {
                entity = new MonitoringSystemEntities();
                var dev = entity.Tbl_Devices.Where(p => p.DeviceId.Equals(devObj.deviceId) && p.IsDeleted==false).FirstOrDefault();
                if (dev == null)
                    return;

                dev.DeviceIp = devObj.deviceId;
                dev.ScreenshotInterval = devObj.screenshotInterval;
                entity.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
