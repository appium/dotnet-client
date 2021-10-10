﻿using System;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android.Device.App
{
    internal class AppTests
    {
        private AppiumDriver<IWebElement> _driver;
        private AppiumOptions _androidOptions;
        private const string IntentAppPackageName = "com.prgguru.android";
        private const string ApiDemosPackageName = "io.appium.android.apis";
        private const string IntentAppElement = "com.prgguru.android:id/button1";
        private const string ApiDemoElement = "Accessibility";

        [OneTimeSetUp]
        public void SetUp()
        {
            _androidOptions = Caps.GetAndroidUIAutomatorCaps(Apps.Get(Apps.androidApiDemos));
            _driver = new AndroidDriver<IWebElement>(
                Env.ServerIsLocal() ? AppiumServers.LocalServiceUri : AppiumServers.RemoteServerUri,
                _androidOptions);
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
            Assert.DoesNotThrow(() => _driver.ActivateApp(IntentAppPackageName));

            //Verify the expected app was activated
            Assert.DoesNotThrow(() => _driver.FindElementById(IntentAppElement));
        }

        [Test]
        public void CanActivateAppFromBackgroundTest()
        {
            //Activate an app to foreground
            _driver.ActivateApp(IntentAppPackageName);

            //Verify the expected app was activated
            Assert.DoesNotThrow(() => _driver.FindElementById(IntentAppElement));

            //Activates Test App to foreground from background
            Assert.DoesNotThrow(() => _driver.ActivateApp(ApiDemosPackageName));

            //Verify the expected app was activated
            Assert.DoesNotThrow(() => _driver.FindElementByAccessibilityId(ApiDemoElement));
        }

        #endregion

        #region Background App

        [Test]
        public void CanBackgroundApp()
        {
            Assert.DoesNotThrow(
                () => _driver.BackgroundApp());
        }

        [Test]
        public void CanBackgroundAppForSeconds()
        {
            Assert.DoesNotThrow(
                () => _driver.BackgroundApp(5));
        }

        [Test]
        public void CanBackgroundAppToDeactivationUsingNegativeSecond()
        {
            Assert.DoesNotThrow(
                () => _driver.BackgroundApp(-1));
        }

        #endregion
    }
}