using System;
using System.Collections.Generic;
using System.Management;
using WinDevices;

namespace Test {
    static class TestHelpers
    {
        public static string[] DevicePropertyNames = new string[]
        {
            "FriendlyName", "Availability", "Caption", "ClassGuid", "CreationClassName", 
            "ConfigManagerErrorCode", "Description", "DeviceID", "ErrorCleared", 
            "ErrorDescription", "LastErrorCode", "Manufacturer", "Name", "PNPClass", "PNPDeviceID", 
            "PowerManagementSupported", "Present", "Service", "Status", "StatusInfo", "SystemCreationClassName", 
            "SystemName"
        };

        private static List<ManagementObject> devices = null;

        static private void GetDevices()
        {
            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_PnPEntity")) {
                collection = searcher.Get();
            }

            devices = new List<ManagementObject>();
            //Add each device to the list
            foreach (ManagementObject device in collection) {
                devices.Add(device);
            }
        }

        static private ManagementObject RandomDevice()
        {
            if (devices == null)
            {
                GetDevices();
            }

            Random rand = new Random();
            int i = rand.Next(devices.Count);
            return devices[i];
        }

        static public string RandomDeviceID()
        {
            return (string)RandomDevice().GetPropertyValue("DeviceID");
        }

        static public string RandomName()
        {
            return (string)RandomDevice().GetPropertyValue("Name");
        }

        static public string RandomFriendlyName()
        {
            string DevID = (string)RandomDevice().GetPropertyValue("DeviceID");
            return Device.FindFriendlyName(DevID);
        }

        static public string RandomClassGuid()
        {
            return (string)RandomDevice().GetPropertyValue("ClassGuid");
        }

        static public string RandomCreationClassName()
        {
            return (string)RandomDevice().GetPropertyValue("CreationClassName");
        }

        static public string RandomDescription()
        {
            return (string)RandomDevice().GetPropertyValue("Description");
        }

        static public string RandomManufacturer()
        {
            return (string)RandomDevice().GetPropertyValue("Manufacturer");
        }

        static public string RandomPNPClass()
        {
            return (string)RandomDevice().GetPropertyValue("PNPClass");
        }

        static public string RandomPNPDeviceID()
        {
            return (string)RandomDevice().GetPropertyValue("PNPDeviceID");
        }

        static public string RandomSystemCreationClassName()
        {
            return (string)RandomDevice().GetPropertyValue("SystemCreationClassName");
        }

        static public string RandomSystemName()
        {
            return (string)RandomDevice().GetPropertyValue("SystemName");
        }
    }
}
