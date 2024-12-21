using System.Threading;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Net.Integration.Tests.IOS
{
    public class AlertTests
    {
        private AppiumDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetIosCaps(Apps.Get("iosTestApp"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new IOSDriver(serverUri, capabilities, Env.InitTimeoutSec);
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
            _driver.FindElement(MobileBy.IosNSPredicate("label == 'show alert'")).Click();
            Thread.Sleep(5000);
            _driver.SwitchTo().Alert().Accept();
        }

        [Test]
        public void DismissAlertTest()
        {
            _driver.FindElement(MobileBy.IosNSPredicate("label == 'show alert'")).Click();
            Thread.Sleep(5000);
            _driver.SwitchTo().Alert().Dismiss();
        }

        [Test]
        public void TextAlertTest()
        {
            _driver.FindElement(MobileBy.IosNSPredicate("label == 'show alert'")).Click();
            Thread.Sleep(500);
            string alertText = _driver.SwitchTo().Alert().Text;
            Assert.That(alertText, Is.EqualTo("Cool title\nthis alert is so cool."));
        }
    }
}