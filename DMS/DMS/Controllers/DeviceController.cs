using DMS.Data.Models;
using DMS.Models;
using DMS.Service.Device;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DMS.Controllers
{
    public class DeviceController : Controller
    {
        #region Global diclaration
        IDeviceService deviceObj = new DeviceService();
        #endregion
        //
        // GET: /Device/
        public ActionResult GetAllOnlineDevices()
        {
            return View();
        }
        /// <summary>
        /// Auther : Get latest 5 screen shots
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult GetScreensByDeviceId(string deviceId, string fromDate,string toDate)
        {
            try
            {
                // screen shot data in base 64 format
                var screenObj = prepareViewModel(deviceObj.GetScreensByDeviceId(deviceId,fromDate,toDate));
                return Json(new { Rresult = screenObj, Message = "Screen list" });
            }
            catch (Exception)
            {
                return Json(new { Message = "Getting error in Screen list" });
                throw;
            }
        }

        public ActionResult Index()
        {
            List<Tbl_Devices> deviceList = new List<Tbl_Devices>();
            DeviceViewModel deviceobj = new DeviceViewModel();
            try
            {
                 deviceList = deviceObj.GetAllOnlineDevices();
               // return Json(new{Result=deviceList,Message="Online Device List"},JsonRequestBehavior.AllowGet);
                 //deviceobj.Tbl_Device = deviceList;
                 return View("Index", deviceList);
            }
            catch (Exception)
            {
                return Json(new { Message = "Error Occoured in getting device list" });
                throw;
            }
        }

        public List<DeviceViewModel> prepareViewModel(List<Tbl_ScreenshotDetails> listObj)
         {
            List<DeviceViewModel> devList = new List<DeviceViewModel>();
            foreach(var x in listObj)
            {
                DeviceViewModel devObj = new DeviceViewModel();
                var base64Image = x.Screenshot1 + (!string.IsNullOrEmpty(x.Screenshot2) ? x.Screenshot2 : "")
                    + (!string.IsNullOrEmpty(x.Screenshot3) ? x.Screenshot3 : "")
                    + (!string.IsNullOrEmpty(x.Screenshot4) ? x.Screenshot4 : "")
                    + (!string.IsNullOrEmpty(x.Screenshot5) ? x.Screenshot5 : "");

                devObj.Id = x.Id;
                devObj.Fk_DeviceId = x.Fk_DeviceId;
                devObj.CreatedDate = x.CreatedDate;
                devObj.Screenshot = string.Format("data:image/gif;base64,{0}", base64Image);
                devList.Add(devObj);
            }
            return devList;
        }
        
        public ActionResult DeviceEdit()
        {
            List<Tbl_Devices> deviceList = new List<Tbl_Devices>();
            DeviceViewModel deviceobj = new DeviceViewModel();
            try
            {
                
                deviceList = deviceObj.GetAllDevices();
                return View("DeviceEdit", deviceList);
            }
            catch (Exception)
            {
                return Json(new { Message = "Error Occoured in getting device list" });
                throw;
            }
        }

        [HttpPost]
        public JsonResult SetScreenshotIntervalByDeviceId(string deviceId, int interval)
        {
            try
            {
                // screen shot data in base 64 format
                var screenObj = deviceObj.SetScreenshotIntervalByDeviceId(deviceId, interval);
                return Json(new { Rresult = screenObj, Message = "Screen Interval save successfully" });
            }
            catch (Exception)
            {
                return Json(new { Message = "Getting error in Screen list" });
                throw;
            }
        }
	}
}