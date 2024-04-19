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
        private readonly TimeSpan _driverTimeOut = TimeSpan.FromSeconds(5);
        private readonly string _appKey = "androidApiDemos";

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get(_appKey))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get(_appKey));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
        }

        [SetUp]
        public void SetUp()
        {
            _driver.StartActivity(Apps.GetId(_appKey), ".ApiDemos");
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
                Assert.That(typeof(WebDriverTimeoutException), Is.EqualTo(excpetionType));
            }    
        }

        [Test]
        public void WebDriverWaitIsWaitingTestCase()
        {
            Stopwatch stopWatch = new();
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
                Assert.That(_driverTimeOut.Seconds, Is.EqualTo(ts.Seconds));
            }
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            if (_driver != null)
            {
                _ = _driver.TerminateApp(Apps.GetId(_appKey));
                _driver?.Quit();
            }   
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }
    }
}
