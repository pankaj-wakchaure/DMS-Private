using DMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DMS.Service.Device
{
    public interface IDeviceService
    {
        List<Tbl_Devices> GetAllOnlineDevices();
        List<Tbl_ScreenshotDetails> GetScreensByDeviceId(string deviceId, string fromDate, string toDate);
        List<Tbl_Devices> GetAllDevices();
        Boolean SetScreenshotIntervalByDeviceId(string deviceId, int interval);
    }
}
