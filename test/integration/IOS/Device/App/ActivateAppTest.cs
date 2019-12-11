using System;
using System.Collections.Generic;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;

namespace Appium.Net.Integration.Tests.IOS.Device.App
{
    internal class ActivateAppTest
    {
        private IOSDriver<IWebElement> _driver;
        private AppiumOptions _iosOptions;
        private const string UiCatalogAppTestAppBundleId = "com.example.apple-samplecode.UICatalog";
        private const string IosTestAppBundleId = "io.appium.TestApp";
        private const string _iosTestAppElement = "show alert";
        private const string _uiCatalogTestAppElement = "Toolbars";


        [OneTimeSetUp]
        public void SetUp()
        {
            _iosOptions = Caps.GetIosCaps(Apps.Get("iosUICatalogApp"));
            _iosOptions.AddAdditionalCapability("newCommandTimeout", 400000);
            _driver = new IOSDriver<IWebElement>(
                Env.ServerIsLocal() ? AppiumServers.LocalServiceUri : AppiumServers.RemoteServerUri,
                _iosOptions, TimeSpan.FromSeconds(120));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Dispose();
        }

        [Test]
        public void CanActivateAppTest()
        {
            //Activate an app to foreground
            Assert.DoesNotThrow(() => _driver.ActivateApp(IosTestAppBundleId));

            //Verify the expected app was activated
            Assert.DoesNotThrow(() => _driver.FindElementByAccessibilityId(_iosTestAppElement));
        }

        [Test]
        public void CanActivateViaScriptAppTest()
        {
            //Activate an app to foreground
            Assert.DoesNotThrow(() => _driver.ExecuteScript("mobile: activateApp",
                new Dictionary<string, string> {{"bundleId", IosTestAppBundleId}}));

            //Verify the expected app was activated
            Assert.DoesNotThrow(() => _driver.FindElementByAccessibilityId(_iosTestAppElement));
        }

        [Test]
        public void CanActivateAppFromBackgroundTest()
        {
            //Activate an app to foreground
            _driver.ActivateApp(IosTestAppBundleId);

            //Verify the expected app was activated
            Assert.DoesNotThrow(() => _driver.FindElementByAccessibilityId(_iosTestAppElement));

            //Activates Test App to foreground from background
            Assert.DoesNotThrow(() => _driver.ActivateApp(UiCatalogAppTestAppBundleId));

            //Verify the expected app was activated
            Assert.DoesNotThrow(() => _driver.FindElementByAccessibilityId(_uiCatalogTestAppElement));
        }
    }
}