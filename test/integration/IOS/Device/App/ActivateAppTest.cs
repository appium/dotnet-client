using System.Collections.Generic;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Net.Integration.Tests.IOS.Device.App
{
    internal class ActivateAppTest
    {
        private IOSDriver<IWebElement> _driver;
        private AppiumOptions _iosOptions;
        private const string IMessageBundleId = "com.apple.MobileSMS";
        private const string TestAppBundleId = "";// TODO find out bundleId for UICatalog

        [OneTimeSetUp]
        public void SetUp()
        {
            _iosOptions = Caps.GetIosCaps(Apps.Get("iosUICatalogApp"));
            _driver = new IOSDriver<IWebElement>(
                Env.ServerIsLocal() ? AppiumServers.LocalServiceUri : AppiumServers.RemoteServerUri,
                _iosOptions);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Dispose();
        }

        [Test]
        public void CanActivateAppTest()
        {
            //Activate iMessage app
            Assert.DoesNotThrow(() => _driver.ActivateApp(IMessageBundleId));

            //Verify the expected iMessage app was activated
            Assert.DoesNotThrow(() => _driver.FindElementByAccessibilityId(""));
        }

        [Test]
        public void CanActivateViaScriptAppTest()
        {
            Assert.DoesNotThrow(() => _driver.ExecuteScript("mobile: activateApp",
                new Dictionary<string, string> {{"bundleId: ", IMessageBundleId } }));

            //Verify the expected (iMessage) app was activated
            Assert.DoesNotThrow(() => _driver.FindElementByAccessibilityId(""));
        }

        [Test]
        public void CanActivateAppFromBackgroundTest()
        {
            //Activate Messages App after test app has launched
            _driver.ActivateApp("com.apple.MobileSMS");

            //Verify the expected (iMessage) app was activated
            Assert.DoesNotThrow(() => _driver.FindElementByAccessibilityId(""));

            //Activates Test App to foreground from background
            Assert.DoesNotThrow(() => _driver.ActivateApp(TestAppBundleId));

            //Verify the expected app was activated
            Assert.DoesNotThrow(() => _driver.FindElementByAccessibilityId(""));
        }
    }
}
