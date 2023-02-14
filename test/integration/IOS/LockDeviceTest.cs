using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Net.Integration.Tests.IOS
{
    internal class LockDeviceTest
    {
        private IOSDriver _driver;

        [SetUp]
        public void TestSetup()
        {
            var capabilities = Caps.GetIosCaps(Apps.Get("iosWebviewApp"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new IOSDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [TearDown]
        public void Cleanup()
        {
            if (_driver.IsLocked())
                _driver.Unlock();
            _driver?.Quit();
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test]
        public void IsLockedTest()
        {
            Assert.AreEqual(_driver.IsLocked(), false);
        }

        [Test]
        public void LockTest()
        {
            Assert.AreEqual(_driver.IsLocked(), false);
            _driver.Lock();
            Assert.AreEqual(_driver.IsLocked(), true);
        }

        [Test]
        public void LockTestWithSeconds()
        {
            Assert.AreEqual(_driver.IsLocked(), false);
            _driver.Lock(5);
            Assert.AreEqual(_driver.IsLocked(), false);
        }

        [Test]
        public void UnlockTest()
        {
            Assert.AreEqual(_driver.IsLocked(), false);
            _driver.Lock();
            Assert.AreEqual(_driver.IsLocked(), true);
            _driver.Unlock();
            Assert.AreEqual(_driver.IsLocked(), false);
        }
    }
}