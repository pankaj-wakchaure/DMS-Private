using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace DMS.API
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        string GetData(int value);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        //[WebGet(UriTemplate = "ClientAccessPolicy.xml")]
        void SaveImage(string deviceId, string imageBas64);
        [OperationContract]
        Int64 SaveImageObj(ScreenshotDetails scrnObj);
        [OperationContract]
        void SaveDeviceIfNotExists(DevicesDetails devObj);
        [OperationContract]
        void UpdateImageObj(ScreenshotDetails scrnObj);

        [OperationContract]
        int GetInterval(string deviceId);

        [OperationContract]
        void UpdateDeviceIfNotExists(DevicesDetails devObj);

    }


    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
    }
    // Use a data contract as illustrated in the sample below to add composite types to service operations.
    [DataContract]
    public class ScreenshotDetails
    {
        long Id;
        string Fk_DeviceId;
        string Screenshot1;
        string Screenshot2;
        string Screenshot3;
        string Screenshot4;
        string Screenshot5;
        Nullable<System.DateTime> CreatedDate;
        Nullable<bool> IsDeleted;
        [DataMember]
        public Int64 id
        {
            get { return Id; }
            set { Id = value; }
        }

        [DataMember]
        public string fk_DeviceId
        {
            get { return Fk_DeviceId; }
            set { Fk_DeviceId = value; }
        }

        [DataMember]
        public string screenshot1
        {
            get { return Screenshot1; }
            set { Screenshot1 = value; }
        }

        [DataMember]
        public string screenshot2
        {
            get { return Screenshot2; }
            set { Screenshot2 = value; }
        }
        [DataMember]
        public string screenshot3
        {
            get { return Screenshot3; }
            set { Screenshot3 = value; }
        }
        [DataMember]
        public string screenshot4
        {
            get { return Screenshot4; }
            set { Screenshot4 = value; }
        }
        [DataMember]
        public string screenshot5
        {
            get { return Screenshot5; }
            set { Screenshot5 = value; }
        }
        [DataMember]
        public Nullable<System.DateTime> createdDate
        {
            get { return CreatedDate; }
            set { CreatedDate = value; }
        }

        [DataMember]
        public Nullable<bool> isDeleted
        {
            get { return IsDeleted; }
            set { IsDeleted = value; }
        }
    }

    [DataContract]
    public class DevicesDetails
    {
        long Id;
        string DeviceId;
        string DeviceName;
        Nullable<bool> DeviceStatus;
        string DeviceIp;
        Nullable<bool> IsDeleted;
        Nullable<System.DateTime> CreatedDate;
        Nullable<bool> IsShutdownDevice;
        int ScreenshotInterval;

        [DataMember]
        public Int64 id
        {
            get { return Id; }
            set { Id = value; }
        }

        [DataMember]
        public string deviceId
        {
            get { return DeviceId; }
            set { DeviceId = value; }
        }

        [DataMember]
        public string deviceName
        {
            get { return DeviceName; }
            set { DeviceName = value; }
        }

        [DataMember]
        public Nullable<bool> deviceStatus
        {
            get { return DeviceStatus; }
            set { DeviceStatus = value; }
        }
        [DataMember]
        public string deviceIp
        {
            get { return DeviceIp; }
            set { DeviceIp = value; }
        }
        [DataMember]
        public Nullable<bool> isDeleted
        {
            get { return IsDeleted; }
            set { IsDeleted = value; }
        }

        [DataMember]
        public Nullable<System.DateTime> createdDate
        {
            get { return CreatedDate; }
            set { CreatedDate = value; }
        }
        [DataMember]
        public Nullable<bool> isShutdownDevice
        {
            get { return IsShutdownDevice; }
            set { IsShutdownDevice = value; }
        }
        [DataMember]
        public Int32 screenshotInterval
        {
            get { return ScreenshotInterval; }
            set { ScreenshotInterval = value; }
        }

    }
}
