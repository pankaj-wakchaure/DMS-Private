using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.Models
{
    public class DeviceModel
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        public string DeviceName { get; set; }
        public Nullable<bool> DeviceStatus { get; set; }
        public string DeviceIp { get; set; }
        public Nullable<bool> IsDeleted { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsShutdownDevice { get; set; }
        public Nullable<int> ScreenshotInterval { get; set; }
    }
}