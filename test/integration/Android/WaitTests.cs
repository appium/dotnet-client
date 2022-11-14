using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;

namespace Appium.Net.Integration.Tests.Android
{
    public class WaitTests
    {
        private AndroidDriver _driver;
        private WebDriverWait _waitDriver;
        private TimeSpan _driverTimeOut = TimeSpan.FromSeconds(5);

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
        }

        [SetUp]
        public void SetUp()
        {
            _driver.StartActivity("io.appium.android.apis", ".ApiDemos");
            _waitDriver = new WebDriverWait(_driver, _driverTimeOut);
            _waitDriver.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }

        [Test]
        public void WebDriverWaitElementNotFoundTestCase()
        {       
            try
            {
                var text = _waitDriver.Until(drv =>
                {
                    return drv.FindElement(MobileBy.Id("Storage"));
                });
            }
            catch (Exception wx)
            {
                var excpetionType =  wx.GetType();
                Assert.AreEqual(excpetionType, typeof(WebDriverTimeoutException));
            }    
        }

        [Test]
        public void WebDriverWaitIsWaitingTestCase()
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            try
            {   
                var text = _waitDriver.Until(drv =>
                {
                    return drv.FindElement(MobileBy.Id("Storage"));
                });
            }
            catch (Exception)
            {
                stopWatch.Stop();
                TimeSpan ts = stopWatch.Elapsed;
                Assert.AreEqual(ts.Seconds, _driverTimeOut.Seconds);
            }
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            if (_driver != null)
            {
                _driver.CloseApp();
                _driver?.Quit();
            }   
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }
    }
}
