using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;

namespace Appium.Integration.Tests.iOS
{
    [TestFixture()]
    class iOSLocationTest
    {
        private AppiumDriver<IOSElement> driver;

        [OneTimeSetUp]
        public void beforeAll()
        {
            AppiumOptions capabilities = Caps.GetIOSCaps(Apps.get("iosTestApp"));
            if (Env.ServerIsRemote())
            {
                capabilities.AddAdditionalCapability("username", Env.GetEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.GetEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "ios - complex");
                capabilities.AddAdditionalCapability("tags", new string[] {"sample"});
            }
            Uri serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            driver = new IOSDriver<IOSElement>(serverUri, capabilities, Env.InitTimeoutSec);
            driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [OneTimeTearDown]
        public void afterAll()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test()]
        public void SetLocationTest()
        {
            var l = new Location();
            l.Altitude = 10;
            l.Longitude = 10;
            l.Latitude = 10;
            driver.Location = l;
            //var l1 = driver.Location;
        }
    }
}