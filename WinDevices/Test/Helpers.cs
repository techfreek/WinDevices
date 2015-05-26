using System;
using System.Collections.Generic;
using System.Management;

namespace Test {
    static class TestHelpers {
        static private List<ManagementObject> GetDevices()
        {
            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_PnPEntity")) {
                collection = searcher.Get();
            }

            List<ManagementObject> deviceslList = new List<ManagementObject>();
            //Add each device to the list
            foreach (ManagementObject device in collection) {
                deviceslList.Add(device);
            }
            return deviceslList;
        }

        static public string RandomDeviceID()
        {
            List<ManagementObject> devices = GetDevices();
            Random rand = new Random();
            int i =  rand.Next(devices.Count);

            return (string)devices[i].GetPropertyValue("DeviceID");

        }

    }
}
