using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            
            foreach (var d in dev)
            {
                Debug.WriteLine("Friendly Name: " + d.FriendlyName + " Name: " + d.Name + " id: " + d.DeviceID + " mfg: " + d.Manufacturer);
            }

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
        public void DeviceNameConnected_NotExact_Crash()
        {
            Devices devs = new Devices();
            try {
                devs.IsDeviceNameConnected("e", false);
            } 
            catch (Exception e)
            {
                Assert.Fail("IsDeviceNameConnected_NotExact should not crash: " + e.ToString());
            }
        }

        [TestMethod]
        public void DeviceNameConnected_True() {
            Devices devs = new Devices();
            Assert.IsTrue(devs.IsDeviceNameConnected(TestHelpers.RandomFriendlyName()));
        }

        [TestMethod]
        public void DeviceNameConnected_NotExact_True() {
            Devices devs = new Devices();
            Assert.IsTrue(devs.IsDeviceNameConnected("e", false));
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
            //Sorry for this. This is the webcam on my windows laptop. If I find a better test case, I'll replace this
            Assert.IsTrue(devs.IsDeviceIDConnected(TestHelpers.RandomDeviceID()));
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
                Assert.Fail("GetDeviceNameConnected should not crash: " + e.ToString());
            }
        }

        [TestMethod]
        public void GetDeviceName_NotExact_Crash() {
            Devices devs = new Devices();
            try {
                devs.GetDeviceByName("e", false);
            } catch (Exception e) {
                Assert.Fail("GetDeviceNameConnected_NotExact should not crash: " + e.ToString());
            }
        }

        [TestMethod]
        public void GetDeviceName_True() {
            Devices devs = new Devices();
            Assert.IsNotNull(devs.GetDeviceByName(TestHelpers.RandomFriendlyName()));
        }

        [TestMethod]
        public void GetDeviceName_NotExact_True() {
            Devices devs = new Devices();
            Assert.IsNotNull(devs.GetDeviceByName("e", false));
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
            Assert.IsNotNull(devs.GetDeviceByID(TestHelpers.RandomDeviceID()));
        }

        [TestMethod]
        public void GetDeviceID_False() {
            Devices devs = new Devices();
            Assert.IsNull(devs.GetDeviceByID("This is a failing test"));
        }

        [TestCategory("GetDeviceByField_String")]
        [TestMethod]
        public void GetDeviceByField_String_Crash() {
            Devices devs = new Devices();
            Random rand = new Random();
            int i = rand.Next(TestHelpers.DevicePropertyNames.Length);
            try
            {

                devs.GetDeviceByField(TestHelpers.DevicePropertyNames[i], "i");
            } catch (Exception e) {
                Assert.Fail("GetFieldByField should not crash: " + e.ToString());
            }
        }

        [TestMethod]
        public void GetDeviceByField_Int_Crash() {
            Devices devs = new Devices();
            Random rand = new Random();
            int i = rand.Next(TestHelpers.DevicePropertyNames.Length);
            try {
                devs.GetDeviceByField(i, "i");
            } catch (Exception e) {
                Assert.Fail("GetFieldByField should not crash: " + e.ToString());
            }
        }

        [TestMethod]
        public void GetDeviceByField_Int_Success() {
            Devices devs = new Devices();
            Random rand = new Random();
            string devID = TestHelpers.RandomDeviceID();
            Assert.IsNotNull(devs.GetDeviceByField((int)Device.DeviceProperties.DEVICE_ID, devID));
        }

        [TestMethod]
        public void GetDeviceByField_Int_Fail() {
            Devices devs = new Devices();
            Random rand = new Random();
            string devID = TestHelpers.RandomDeviceID();

            //reverse the string
            char[] arr = devID.ToCharArray();
            Array.Reverse(arr);
            devID = new string(arr);

            Device dev = devs.GetDeviceByField((int) Device.DeviceProperties.DEVICE_ID, devID);
            Assert.IsNull(dev);
        }
    }
}
