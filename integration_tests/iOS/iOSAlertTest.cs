using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using System.Threading;

namespace Appium.Integration.Tests.iOS
{
    public class iOSAlertTest
    {
        private AppiumDriver<IOSElement> driver;

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
            driver = new IOSDriver<IOSElement>(serverUri, capabilities, Env.InitTimeoutSec);
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
        public void AcceptAlertTest()
        {
            driver.FindElement(new ByIosUIAutomation(".elements().withName(\"show alert\")")).Click();
            Thread.Sleep(10000);
            driver.SwitchTo().Alert().Accept();
        }

        [Test]
        public void DismissAlertTest()
        {
            driver.FindElement(new ByIosUIAutomation(".elements().withName(\"show alert\")")).Click();
            Thread.Sleep(10000);
            driver.SwitchTo().Alert().Dismiss();
        }
    }
}