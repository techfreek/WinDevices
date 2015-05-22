using System.Collections.Generic;
using System.Management;
using Microsoft.Win32;

namespace WinDevices
{
    public class Devices
    {
        private List<Device> deviceslList;

        private RegistryKey EnumKey;
        private RegistryKey usbKey;

        /// <summary>
        /// Returns an instance which has a list of Plug and Play devices that can be queried
        /// </summary>
        public Devices()
        {
            //navigate registry so we can find friendly names
            EnumKey = Registry.LocalMachine
                .OpenSubKey("SYSTEM")
                .OpenSubKey("CurrentControlSet")
                .OpenSubKey("Enum");
            usbKey = EnumKey.OpenSubKey("USB");
            

            // Find connected devices
            Discover();
        }

        private void Discover()
        {
            // get list of connected devices
            ManagementObjectCollection collection;
            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_PnPEntity")) {
                collection = searcher.Get();
            }

            deviceslList = new List<Device>();
            //Add each device to the list
            foreach (var device in collection) {
                deviceslList.Add(new Device(device, usbKey));
            }
        }

        /// <summary>
        /// Refreshes the list of connected devices
        /// </summary>
        public void Refresh()
        {
            Discover();
        }

        /// <summary>
        /// Gets a list of all devices connected
        /// </summary>
        /// <returns>Returns a list</returns>
        public List<Device> GetDevices()
        {
            return deviceslList;
        }

        /// <summary>
        /// Checks if a devices friendly name is currently connected
        /// </summary>
        /// <param name="name">FriendlyName of a device</param>
        /// <param name="exact">Indicates if the search should be exact. Default true.</param>
        /// <returns>boolean indicating if the device is connected</returns>
        public bool IsDeviceNameConnected(string name, bool exact = true)
        {
            bool connected = false;
            if(exact) {
                connected = deviceslList.Exists(x => x.FriendlyName == name);
            } else {
                connected = deviceslList.Exists(x => x.FriendlyName != null && x.FriendlyName.ToLower().Contains(name));
            }
            return connected;
        }

        /// <summary>
        /// Checks if a devices DeviceID is currently connected
        /// </summary>
        /// <param name="DeviceID">DeviceID of a device</param>
        /// <returns>boolean indicating if the device is connected</returns>
        public bool IsDeviceIDConnected(string DeviceID)
        {
            return deviceslList.Exists(x => x.DeviceID == DeviceID);
        }

        /// <summary>
        /// Attempts to get the device associated with the FriendlyName.
        /// </summary>
        /// <param name="name">FriendlyName of device</param>
        /// <param name="exact">Indicates if the search should be exact. Default true.</param>
        /// <returns>Instance of Device which provides more information</returns>
        public Device GetDeviceByName(string name, bool exact = true)
        {
            Device dev = null;
            
            if(exact) {
                dev = deviceslList.Find(x => x.FriendlyName == name);
            } else {
                dev = deviceslList.Find(x => x.FriendlyName != null &&  x.FriendlyName.ToLower().Contains(name));
            }

            return dev;
        }

        /// <summary>
        /// Attempts to get the device associated with the DeviceID.
        /// </summary>
        /// <param name="DeviceID">DeviceID of device</param>
        /// <returns>Instance of Device which provides more information</returns>
        public Device GetDeviceByID(string DeviceID)
        {
            Device dev;
            dev = deviceslList.Find(x => x.DeviceID == DeviceID);
            return dev;
        }
    }
}
