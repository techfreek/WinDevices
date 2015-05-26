using System;
using System.Management;
using Microsoft.Win32;


namespace WinDevices {
    //Availability Return Values
    public enum AvailabilityStatus {
        OTHER=0x1, UNKNOWN, RUNNING, WARNING, IN_TEST, NA, OFF, OFFLINE, OFFDUTY, 
        DEGRADED, NOT_INSTALLED, INSTALLERROR, POWERSAVE_UNKOWN, POWERSAVELOW, 
        POWERSAVE_STANDBY, POWERCYCLE, POWERSAVE_WARN
    };

    //ConfigManagerErrorCodes
    public enum ConfigManagerErrorCodes {
        OK, BAD_CONFIG, BAD_DRIVER, CORRUPTED_DRIVER, DEVICE_NOT_WORKING, 
        WINDOWS_CANT_MANAGE, BOOT_CONFIG_CONFLICT, CANNOT_FILTER, MISSING_DRIVER, 
        FIRMWARE_NOT_REPORT_RESX, DEVICE_CANT_START, DEVICE_FAILED, NEED_MORE_RESOURCES, 
        WINDOWS_CANT_VERIFY_RESOURCES, PC_NEEDS_RESTART, DEVICE_NOT_WORKING_BAD_ENUM, 
        CANT_IDENTIFY_REQUESTED_RESX, WANTS_UNKOWN_RESOURCE, RESINTALL_DRIVERS, VD_LOADER_FAILURE, 
        POSSIBLE_REGISTRY_CORRUPTION, SYSTEM_FAILURE_REMOVING_DEV, DEVICE_DISABLED, SYSTEM_FAILURE, 
        DEVICE_NOT_PRESENT, SETTING_UP_DEVICE, STILL_SETTING_UP_DEVICE, DEVICE_LACKS_LOG_CONFIG, 
        MISSING_DEVICE_DRIVER, FIRMWARE_DIDNT_PROVIDE_RESX, IRQ_ALREADY_IN_USE, 
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

        public enum DeviceProperties {
            FRIENDLY_NAME, AVAILABILITY, CAPTION, CLASSGUID, CREATION_CLASS_NAME,
            CONFIG_MANAGER_ERROR_CODE, DESCRIPTION, DEVICE_ID, ERROR_CLEARED, 
            ERROR_DESCRIPTION, LAST_ERROR_CODE, MANUFACTURER, NAME, PNP_CLASS, PNP_DEVICE_ID,
            POWER_MANAGEMENT_SUPPORTED, PRESENT, SERVICE, STATUS, STATUS_INFO, SYSTEM_CREATION_CLASS_NAME,
            SYSTEM_NAME
        };

        internal static string[] DevicePropertyNames = new string[]
        {
            "FriendlyName", "Availability", "Caption", "ClassGuid", "CreationClassName", 
            "ConfigManagerErrorCode", "Description", "DeviceID", "ErrorCleared", 
            "ErrorDescription", "LastErrorCode", "Manufacturer", "Name", "PNPClass", "PNPDeviceID", 
            "PowerManagementSupported", "Present", "Service", "Status", "StatusInfo", "SystemCreationClassName", 
            "SystemName"
        };
        
        internal Device(ManagementBaseObject device, RegistryKey usbKey)
        {
            //save device, just in case
            //this.device = device;

            //extract properties
            this.Availability = (device.GetPropertyValue("Availability") == null)
                ? (UInt16) AvailabilityStatus.NA
                : (UInt16) device.GetPropertyValue("Availability");

            this.Caption = (device.GetPropertyValue("Caption") == null) 
                ? "" 
                : (string)device.GetPropertyValue("Caption");
            this.ClassGuid = (device.GetPropertyValue("ClassGuid") == null) 
                ? "" 
                : (string)device.GetPropertyValue("ClassGuid");
            this.CreationClassName = (device.GetPropertyValue("CreationClassName") == null) 
                ? "" 
                : (string)device.GetPropertyValue("CreationClassName");
            this.ConfigManagerErrorCode = (device.GetPropertyValue("ConfigManagerErrorCode") == null)
                ? (UInt32)ConfigManagerErrorCodes.DEVICE_NOT_PRESENT
                : (UInt32)device.GetPropertyValue("ConfigManagerErrorCode");

            this.Description = (device.GetPropertyValue("Description") == null) 
                ? "" 
                : (string)device.GetPropertyValue("Description");
            this.DeviceID = (device.GetPropertyValue("DeviceID") == null) 
                ? "" 
                : (string)device.GetPropertyValue("DeviceID");

            this.ErrorCleared = (device.GetPropertyValue("ErrorCleared") == null) 
                ? false 
                : (bool)device.GetPropertyValue("ErrorCleared");
            this.ErrorDescription = (device.GetPropertyValue("ErrorDescription") == null) 
                ? "" 
                : (string)device.GetPropertyValue("ErrorDescription");

            this.LastErrorCode = (device.GetPropertyValue("LastErrorCode") == null) 
                ? 0
                : (UInt32)device.GetPropertyValue("LastErrorCode");
            this.Manufacturer = (device.GetPropertyValue("Manufacturer") == null) 
                ? "" 
                : (string)device.GetPropertyValue("Manufacturer");
            this.Name = (device.GetPropertyValue("Name") == null) 
                ? "" 
                : (string)device.GetPropertyValue("Name");

            /*this.PNPClass = (device.GetPropertyValue("PNPClass") == null) 
                ? "" 
                : (string)device.GetPropertyValue("PNPClass");*/
            this.PNPDeviceID = (device.GetPropertyValue("PNPDeviceID") == null) 
                ? "" 
                : (string)device.GetPropertyValue("PNPDeviceID");
            this.PowerManagementSupported = (device.GetPropertyValue("PowerManagementSupported") == null) 
                ? false 
                : (bool)device.GetPropertyValue("PowerManagementSupported");
            /*this.Present = (device.GetPropertyValue("Present") == null) 
                ? false 
                : (bool)device.GetPropertyValue("Present");*/

            this.Service = (device.GetPropertyValue("Service") == null) 
                ? ""
                : (string)device.GetPropertyValue("Service");
            this.Status = (device.GetPropertyValue("Status") == null) 
                ? ""
                : (string)device.GetPropertyValue("Status");
            this.StatusInfo = (device.GetPropertyValue("StatusInfo") == null)
                ? (UInt16)StatusInfoValue.OTHER
                : (UInt16)device.GetPropertyValue("StatusInfo");
            this.SystemCreationClassName = (device.GetPropertyValue("SystemCreationClassName") == null) 
                ? ""
                : (string)device.GetPropertyValue("SystemCreationClassName");
            this.SystemName = (device.GetPropertyValue("SystemName") == null) 
                ? ""
                : (string)device.GetPropertyValue("SystemName");

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
