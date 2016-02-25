using DMS.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DMS.Models
{
    public class DeviceViewModel
    {
        public long Id { get; set; }
        public string Fk_DeviceId { get; set; }
        public string Screenshot { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<bool> IsDeleted { get; set; }


    }
}