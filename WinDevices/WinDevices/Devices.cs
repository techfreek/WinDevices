using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management;
using System.Reflection;
using Microsoft.Win32;

namespace WinDevices
{
    public class Devices
    {
        private List<Device> deviceslList;
        /// <summary>
        /// Returns an instance which has a list of Plug and Play devices that can be queried
        /// </summary>
        public Devices()
        {
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
                deviceslList.Add(new Device(device));
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

        /// <summary>
        /// Allows selecting the field from a string
        /// </summary>
        /// <param name="field">string of the fieldname</param>
        /// <param name="value">Value you want to match. Only precise matches work.</param>
        /// <returns>First matching device or null</returns>
        public Device GetDeviceByField(string field, object value)
        {
            int i = 0;
            Device dev = null;
            Type DeviceType = typeof(Device);

            //Some reason, the name looks like this. Don't ask. I don't know why.
            field = '<' + field + ">k__BackingField";

            //find the field
            FieldInfo fInfo = DeviceType.GetField(field, BindingFlags.Public |
                                                        BindingFlags.Instance |
                                                        BindingFlags.NonPublic);
            if (fInfo == null) {
                return null;
            }

            for(; i < deviceslList.Count;i++)
            {
                dev = deviceslList[i];
                
                Object devValue = fInfo.GetValue(dev);

                if (value.Equals(devValue))
                {
                    return dev;
                }
            }
            return null;
        }

        /// <summary>
        /// Allows selecting the field from an enum in Device.DeviceProperties
        /// </summary>
        /// <param name="field">Enum from Device.DeviceProperties</param>
        /// <param name="value">Value you want to match. Only precise matches work.</param>
        /// <returns>First matching device or null</returns>
        public Device GetDeviceByField(int field, object value)
        {
            string _field;
            if (field >= 0 && field < Device.DevicePropertyNames.Length)
            {
                _field = Device.DevicePropertyNames[field];
                return GetDeviceByField(_field, value);
            }
            return null;
        }

        /// <summary>
        /// Checks to see if a device has a field value matching a supplied value and field
        /// </summary>
        /// <param name="field">string of the fieldname</param>
        /// <param name="value">Value you want to match. Only precise matches work.</param>
        /// <returns>Boolean indication if a device field is equivalent to the provided value</returns>
        public bool IsDeviceConnectedByField(string field, object value) {
            Device dev = null;

            dev = GetDeviceByField(field, value);

            if (dev == null)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks to see if a device has a field value matching a supplied value and field from an enum in Device.DeviceProperties
        /// </summary>
        /// <param name="field">Enum from Device.DeviceProperties</param>
        /// <param name="value">Value you want to match. Only precise matches work.</param>
        /// <returns>Boolean indication if a device field is equivalent to the provided value</returns>
        public bool IsDeviceConnectedByField(int field, object value) {
            string _field;
            if (field >= 0 && field < Device.DevicePropertyNames.Length) {
                _field = Device.DevicePropertyNames[field];
                return IsDeviceConnectedByField(_field, value);
            }
            return false;
        }
    }
}
