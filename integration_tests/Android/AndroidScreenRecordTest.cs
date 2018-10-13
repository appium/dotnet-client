using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using Appium.Integration.Tests.Helpers;
using OpenQA.Selenium.Appium.Android;
using System.Threading;

namespace Appium.Integration.Tests.Android
{
    [TestFixture]
    public class AndroidScreenRecordTest
    {
        private AppiumDriver<IWebElement> driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            AppiumOptions capabilities = Env.isSauce()
                ? Caps.getAndroid501Caps(Apps.get("androidApiDemos"))
                : Caps.getAndroid19Caps(Apps.get("androidApiDemos"));
            if (Env.isSauce())
            {
                capabilities.AddAdditionalCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "android - complex");
                capabilities.AddAdditionalCapability("tags", new string[] {"sample"});
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIAndroid;
            driver = new AndroidDriver<IWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            driver.Manage().Timeouts().ImplicitWait = Env.IMPLICIT_TIMEOUT_SEC;
        }

        [OneTimeTearDown]
        public void AfterAll()
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

        [Test]
        public void TestScreenRecord()
        {
            driver.StartRecordingScreen();
            Thread.Sleep(5000);
            driver.StopRecordingScreen();
        }
    }
}