//using Data.Models;
using ETestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelClasses
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
       public List<ETestApp.Models.Tbl_Devices> GetAllOnlineDevices()
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
    }
}
