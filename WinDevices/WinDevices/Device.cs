using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;


namespace WinDevices {
    //Accessibility Return Values
    public enum AccessibilityStatus {
        OTHER=0x1, UNKNOWN, RUNNING, WARNING, IN_TEST, NA, OFF, OFFLINE, OFFDUTY, 
        DEGRADED, NOT_INSTALLED, INSTALLERROR, POWERSAVE_UNKOWN, POWERSAVELOW, 
        POWERSAVE_STANDBY, POWERCYCLE, POWERSAVE_WARN
    };

    //ConfigManagerErrorCodes
    public enum ConfigManagerErrorCodes {
        OK, BAD_CONFIG, BAD_DRIVER, CORRUPTED_DRIVER, DEVICE_NOT_WORKING, WINDOWS_CANT_MANAGE, 
        BOOT_CONFIG_CONFLICT, CANNOT_FILTER, MISSING_DRIVER, DEVICE_FAILED, NEED_MORE_RESOURCES, 
        WINDOWS_CANT_VERIFY_RESOURCES, NEEDS_RESTART, DEVICE_NOT_WORKING_BAD_ENUM,
        WANTS_UNKOWN_RESOURCE, RESINTALL_DRIVERS, VD_LOADER_FAILURE, POSSIBLE_REGISTRY_CORRUPTION,
        DEVICE_DISABLED, SYSTEM_FAILURE, DEVICE_NOT_PRESENT, SETTING_UP_DEVICE, STILL_SETTING_UP_DEVICE, 
        DEVICE_LACKS_LOG_CONFIG, MISSING_DEVICE_DRIVER, FIRMWARE_DIDNT_PROVIDE_RESX, IRQ_ALREADY_IN_USE,
        DEVICE_NOTWORKING_PROPERLY
    };


    //StatusInfo Return Values
    public enum StatusInfoValue {
        OTHER=0x1, UNKNOWN, ENABLED, DISABLED, NA
    };

    public class Device {
        //Instance of object
        //private ManagementBaseObject device;

        public string FriendlyName { get; private set; }
        
        public UInt16 Availability { get; private set; }
        
        public string Caption { get; private set; }
        public string ClassGuid { get; private set; }
        public string CreationClassName { get; private set; }
        public UInt32 ConfigManagerErrorCode { get; private set; }

        public string Description { get; private set; }
        public string DeviceID { get; private set; }
        
        public bool ErrorCleared { get; private set; }
        public string ErrorDescription { get; private set; }
        
        public UInt32 LastErrorCode { get; private set; }
        public string Manufacturer { get; private set; }
        public string Name { get; private set; }
        
        public string PNPClass { get; private set; }
        public string PNPDeviceID { get; private set; }
        public bool PowerManagementSupported { get; private set; }
        public bool Present { get; private set; }
        
        public string Service { get; private set; }
        public string Status { get; private set; }
        public UInt16 StatusInfo { get; private set; }
        public string SystemCreationClassName { get; private set; }
        public string SystemName { get; private set; }
        
        internal Device(ManagementBaseObject device, RegistryKey usbKey)
        {
            //save device, just in case
            //this.device = device;

            //extract properties
            this.Availability = (UInt16)device.GetPropertyValue("Availability");
            
            this.Caption = (string)device.GetPropertyValue("Caption");
            this.ClassGuid = (string)device.GetPropertyValue("ClassGuid");
            this.CreationClassName = (string)device.GetPropertyValue("CreationClassName");
            this.ConfigManagerErrorCode = (UInt32)device.GetPropertyValue("ConfigManagerErrorCode");

            this.Description = (string)device.GetPropertyValue("Description");
            this.DeviceID = (string)device.GetPropertyValue("DeviceID");
            
            this.ErrorCleared = (bool)device.GetPropertyValue("ErrorCleared");
            this.ErrorDescription = (string)device.GetPropertyValue("ErrorDescription");
            
            this.LastErrorCode = (UInt32)device.GetPropertyValue("LastErrorCode");
            this.Manufacturer = (string)device.GetPropertyValue("Manufacturer");
            this.Name = (string)device.GetPropertyValue("Name");
            
            this.PNPClass = (string)device.GetPropertyValue("PNPClass");
            this.PNPDeviceID = (string)device.GetPropertyValue("PNPDeviceID");
            this.PowerManagementSupported = (bool)device.GetPropertyValue("PowerManagementSupported");
            this.Present = (bool)device.GetPropertyValue("Present");

            this.Service = (string)device.GetPropertyValue("Service");
            this.Status = (string)device.GetPropertyValue("Status");
            this.StatusInfo = (UInt16)device.GetPropertyValue("StatusInfo");
            this.SystemCreationClassName = (string)device.GetPropertyValue("SystemCreationClassName");
            this.SystemName = (string)device.GetPropertyValue("SystemName");

            FindFriendlyName(usbKey);
        }

        private void FindFriendlyName(RegistryKey usbKey)
        {
            string id = this.DeviceID;
            string[] parts = this.DeviceID.Split('\\');
            //parts[0]: ignore
            //parts[1]: folder to check
            //parts[2]: subfolder/id

            //I should look up what these names actually mean
            RegistryKey folder = usbKey.OpenSubKey(parts[1]);
            RegistryKey subFolder;

            if (folder != null)
            {
                subFolder = folder.OpenSubKey(parts[2]);
                if (subFolder != null)
                {
                    //Get name. If it does not exist, provide a default
                    this.FriendlyName = (string)subFolder.GetValue("FriendlyName", "");
                }
            }
        }
    }
}
