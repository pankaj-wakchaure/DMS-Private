using DMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Data.ModelClasses
{
   public class Tbl_Devices
   {
       #region Global declartion
       MonitoringSystemEntities entity = null;
       #endregion
       
       /// <summary>
       /// Auther : Pankaj W.
       /// Date Get all online devices which are having screen shot in last 2 minutes
       /// </summary>
       /// <returns></returns>
       public List<Models.Tbl_Devices> GetAllOnlineDevices()
       {
           entity = new MonitoringSystemEntities();
           DateTime dtTime = System.DateTime.Now;
           int val= 312222;
           dtTime = dtTime.AddMinutes(-val);
           try
           {
               var v = (from t in entity.Tbl_Devices
                        join t1 in entity.Tbl_ScreenshotDetails on t.DeviceId equals t1.Fk_DeviceId
                        where t1.CreatedDate >= dtTime
                        select t).Distinct();
               return v.ToList();

           }
           catch (Exception)
           {
               throw;
           }
       }
       /// <summary>
       /// Reason To get all devices
       /// </summary>
       /// <returns></returns>
       public List<Models.Tbl_Devices> GetAllDevices()
       {
           entity = new MonitoringSystemEntities();
           try
           {
               var v = (entity.Tbl_Devices.Where(p => p.IsDeleted == false)).Distinct();
               return v.ToList();
           }
           catch (Exception)
           {
               throw;
           }
       }
       public Boolean setScreenshotInterval(string deviceId, int interval)
       {
           entity = new MonitoringSystemEntities();
           try
           {
               var v = (entity.Tbl_Devices.Where(p =>p.DeviceId==deviceId &&  p.IsDeleted == false)).FirstOrDefault();
               if (v == null)
                   return false;
               v.ScreenshotInterval = interval;
               entity.SaveChanges();
               return true;
           }
           catch (Exception)
           {
               throw;
           }
       }
    }
}
