using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    class AndroidLocationTest
    {
        private AppiumDriver<AndroidElement> _driver;

        [SetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidCaps(Apps.Get("androidApiDemos"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver<AndroidElement>(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [TearDown]
        public void AfterEach()
        {
            _driver?.Quit();
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test]
        public void SetLocationTest()
        {
            var l = new Location();
            l.Altitude = 10;
            l.Longitude = 10;
            l.Latitude = 10;
            _driver.Location = l;
            //var l1 = driver.Location;
        }
    }
}