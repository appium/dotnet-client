using System.Threading;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Net.Integration.Tests.iOS
{
    public class IOsAlertTest
    {
        private AppiumDriver<IOSElement> _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetIosCaps(Apps.Get("iosTestApp"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new IOSDriver<IOSElement>(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [OneTimeTearDown]
        public void AfterEach()
        {
            _driver?.Quit();
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test]
        public void AcceptAlertTest()
        {
            _driver.FindElement(new ByIosUIAutomation(".elements().withName(\"show alert\")")).Click();
            Thread.Sleep(10000);
            _driver.SwitchTo().Alert().Accept();
        }

        [Test]
        public void DismissAlertTest()
        {
            _driver.FindElement(new ByIosUIAutomation(".elements().withName(\"show alert\")")).Click();
            Thread.Sleep(10000);
            _driver.SwitchTo().Alert().Dismiss();
        }
    }
}