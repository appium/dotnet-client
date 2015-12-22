using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;

namespace Appium.Integration.Tests.iOS
{
    [TestFixture()]
    class IosGestureTest
    {
        private AppiumDriver<IOSElement> driver;

        [SetUp]
        public void BeforeAll()
        {
            DesiredCapabilities capabilities = Caps.getIos82Caps(Apps.get("iosUICatalogApp"));
            if (Env.isSauce())
            {
                capabilities.SetCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.SetCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.SetCapability("name", "ios - complex");
                capabilities.SetCapability("tags", new string[] { "sample" });
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIForIOS;
            driver = new IOSDriver<IOSElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            driver.Manage().Timeouts().ImplicitlyWait(Env.IMPLICIT_TIMEOUT_SEC);
        }

        [TearDown]
        public void AfterEach()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.isSauce())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test()]
        public void GestureTestCase()
        {
            IOSElement e = driver.FindElementByName("TextField1");
            driver.Tap(1, e, 2000);
            driver.Zoom(e);
            driver.Pinch(e);
        }
    }
}
