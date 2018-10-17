using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using Appium.Integration.Tests.Helpers;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Integration.Tests.iOS
{
    public class iOSAppStringsTest
    {
        private AppiumDriver<IWebElement> driver;

        [OneTimeSetUp]
        public void BeforeAll()
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
            driver = new IOSDriver<IWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
            driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [OneTimeTearDown]
        public void AfterEach()
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

        [Test]
        public void GetAppStrings()
        {
            Assert.AreNotSame(0, driver.GetAppStringDictionary().Count);
        }

        [Test]
        public void GetAppStringsUsingLang()
        {
            Assert.AreNotSame(0, driver.GetAppStringDictionary("en").Count);
        }

        [Test]
        public void GetAppStringsUsingLangAndFileStrings()
        {
            Assert.AreNotSame(0, driver.GetAppStringDictionary("en", "Localizable.strings").Count);
        }
    }
}