using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using WinDevices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Test {
    [TestClass]
    public class TestDevices {
        //------------------------------------------------------------------------------
        [TestCategory("Constructor")]
        [TestMethod]
        public void Constructor_Crash() {
            try
            {
                Devices devs = new Devices();
            }
            catch (Exception e)
            {
                Assert.Fail("Constructor should not crash: " + e.ToString());
            }
        }

        //------------------------------------------------------------------------------
        [TestCategory("Refresh")]
        [TestMethod]
        public void Refresh_Crash()
        {
            Devices devs = new Devices();
            try {
                devs.Refresh();
            } 
            catch (Exception e)
            {
                Assert.Fail("Refresh should not crash: " + e.ToString());
            }
        }

        //------------------------------------------------------------------------------
        [TestCategory("GetDevices")]
        [TestMethod]
        public void GetDevices_Crash()
        {
            Devices devs = new Devices();
            try
            {
                devs.GetDevices();
            }
            catch (Exception e)
            {
                Assert.Fail("GetDevices should not crash: " + e.ToString());
            }
        }

        [TestMethod]
        public void GetDevicesShouldNotBeEmpty() {
            Devices devs = new Devices();
            List<Device> dev = devs.GetDevices();
            Assert.AreNotEqual(dev.Count, 0);
        }

        //------------------------------------------------------------------------------
        [TestCategory("IsDeviceNameConnected")]
        [TestMethod]
        public void DeviceNameConnected_Crash()
        {
            Devices devs = new Devices();
            try {
                devs.IsDeviceNameConnected("test");
            } 
            catch (Exception e)
            {
                Assert.Fail("IsDeviceNameConnected should not crash: " + e.ToString());
            }
        }

        [TestMethod]
        public void DeviceNameConnected_True() {
            Devices devs = new Devices();
            Assert.IsTrue(devs.IsDeviceNameConnected("lenovo"));
        }

        [TestMethod]
        public void DeviceNameConnected_False() {
            Devices devs = new Devices();
            Assert.IsFalse(devs.IsDeviceNameConnected("This is a failing test"));
        }

        //------------------------------------------------------------------------------
        [TestCategory("IsDeviceIDConnected")]
        [TestMethod]
        public void DeviceIDConnected_Crash() {
            Devices devs = new Devices();
            try {
                devs.IsDeviceIDConnected("test");
            } catch (Exception e) {
                Assert.Fail("IsDeviceNameConnected should not crash: " + e.ToString());
            }
        }

        [TestMethod]
        public void DeviceIDConnected_True() {
            Devices devs = new Devices();
            ///TODO: Lookup actual device id
            Assert.IsTrue(devs.IsDeviceIDConnected("lenovo"));
        }

        [TestMethod]
        public void DeviceIDConnected_False() {
            Devices devs = new Devices();
            Assert.IsFalse(devs.IsDeviceIDConnected("This is a failing test"));
        }

        //------------------------------------------------------------------------------
        [TestCategory("GetDeviceByName")]
        [TestMethod]
        public void GetDeviceName_Crash() {
            Devices devs = new Devices();
            try {
                devs.GetDeviceByName("test");
            } catch (Exception e) {
                Assert.Fail("IsDeviceNameConnected should not crash: " + e.ToString());
            }
        }

        [TestMethod]
        public void GetDeviceName_True() {
            Devices devs = new Devices();
            Assert.IsNotNull(devs.GetDeviceByName("lenovo"));
        }

        [TestMethod]
        public void GetDeviceName_False() {
            Devices devs = new Devices();
            Assert.IsNull(devs.GetDeviceByName("This is a failing test"));
        }

        //------------------------------------------------------------------------------
        [TestCategory("GetDeviceByID")]
        [TestMethod]
        public void GetDeviceID_Crash() {
            Devices devs = new Devices();
            try {
                devs.GetDeviceByID("test");
            } catch (Exception e) {
                Assert.Fail("IsDeviceNameConnected should not crash: " + e.ToString());
            }
        }

        [TestMethod]
        public void GetDeviceID_True() {
            Devices devs = new Devices();
            ///TODO: Lookup actual device id
            Assert.IsNotNull(devs.GetDeviceByID("lenovo"));
        }

        [TestMethod]
        public void GetDeviceID_False() {
            Devices devs = new Devices();
            Assert.IsNull(devs.GetDeviceByID("This is a failing test"));
        }
    }
}
