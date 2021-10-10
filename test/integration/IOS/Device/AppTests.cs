﻿using System;
using System.Collections.Generic;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Net.Integration.Tests.IOS.Device.App
{
    internal class AppTests
    {
        private IOSDriver<IWebElement> _driver;
        private AppiumOptions _iosOptions;
        private const string UiCatalogAppTestAppBundleId = "com.example.apple-samplecode.UICatalog";
        private const string IosTestAppBundleId = "io.appium.TestApp";
        private const string IosTestAppElement = "show alert";
        private const string UiCatalogTestAppElement = "Toolbars";
        private const string IosDockElement = "Dock";

        [OneTimeSetUp]
        public void SetUp()
        {
            _iosOptions = Caps.GetIosCaps(Apps.Get("iosUICatalogApp"));
            _driver = new IOSDriver<IWebElement>(
                Env.ServerIsLocal() ? AppiumServers.LocalServiceUri : AppiumServers.RemoteServerUri,
                _iosOptions);
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver.Dispose();
        }

        #region Activate App

        [Test]
        public void CanActivateAppTest()
        {
            //Activate an app to foreground
            Assert.DoesNotThrow(() => _driver.ActivateApp(IosTestAppBundleId));

            //Verify the expected app was activated
            Assert.DoesNotThrow(() => _driver.FindElementByAccessibilityId(IosTestAppElement));
        }

        [Test]
        public void CanActivateViaScriptAppTest()
        {
            //Activate an app to foreground
            Assert.DoesNotThrow(() => _driver.ExecuteScript("mobile: activateApp",
                new Dictionary<string, string> {{"bundleId", IosTestAppBundleId}}));

            //Verify the expected app was activated
            Assert.DoesNotThrow(() => _driver.FindElementByAccessibilityId(IosTestAppElement));
        }

        [Test]
        public void CanActivateAppFromBackgroundTest()
        {
            //Activate an app to foreground
            _driver.ActivateApp(IosTestAppBundleId);

            //Verify the expected app was activated
            Assert.DoesNotThrow(() => _driver.FindElementByAccessibilityId(IosTestAppElement));

            //Activates Test App to foreground from background
            Assert.DoesNotThrow(() => _driver.ActivateApp(UiCatalogAppTestAppBundleId));

            //Verify the expected app was activated
            Assert.DoesNotThrow(() => _driver.FindElementByAccessibilityId(UiCatalogTestAppElement));
        }

        #endregion

        #region Background App

        [Test]
        public void CanBackgroundApp()
        {
            Assert.DoesNotThrow(
                () => _driver.BackgroundApp());
            Assert.DoesNotThrow(() => _driver.FindElementByAccessibilityId(IosDockElement));
        }

        [Test]
        public void CanBackgroundAppForSeconds()
        {
            Assert.DoesNotThrow(
                () => _driver.BackgroundApp(5));
        }

        [Test]
        public void CanBackgroundAppForTimeSpan()
        {
            Assert.DoesNotThrow(
                () => _driver.BackgroundApp(TimeSpan.FromSeconds(10)));
        }

        [Test]
        public void CanBackgroundAppToDeactivationUsingNegativeSecond()
        {
            Assert.DoesNotThrow(
                () => _driver.BackgroundApp(-1));
            Assert.DoesNotThrow(() => _driver.FindElementByAccessibilityId(IosDockElement));
        }

        #endregion
    }
}