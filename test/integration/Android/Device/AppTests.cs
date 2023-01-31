using System;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android.Device.App
{
    internal class AppTests
    {
        private AppiumDriver _driver;
        private AppiumOptions _androidOptions;
        private const string ApiDemosPackageName = "io.appium.android.apis";
        private const string ApiDemoElement = "Accessibility";

        [OneTimeSetUp]
        public void SetUp()
        {
            _androidOptions = Caps.GetAndroidUIAutomatorCaps(Apps.Get(Apps.androidApiDemos));
            _driver = new AndroidDriver(
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
            Assert.DoesNotThrow(() => _driver.ActivateApp(ApiDemosPackageName));

            //Verify the expected app was activated
            Assert.DoesNotThrow(() => _driver.FindElement(MobileBy.AccessibilityId(ApiDemoElement)));
        }
        
        [Test]
        public void CanActivateAppWithTimeoutTest()
        {
            //Activate an app to foreground
            Assert.DoesNotThrow(() => _driver.ActivateApp(ApiDemosPackageName, TimeSpan.FromSeconds(20)));

            //Verify the expected app was activated
            Assert.DoesNotThrow(() => _driver.FindElement(MobileBy.AccessibilityId(ApiDemoElement)));
        }
        [Test]
        public void CanActivateAppFromBackgroundTest()
        {
            //Activate an app to foreground
            _driver.ActivateApp(ApiDemosPackageName);

            //Verify the expected app was activated
            Assert.DoesNotThrow(() => _driver.FindElement(MobileBy.AccessibilityId(ApiDemoElement)));

            Assert.DoesNotThrow(() => _driver.BackgroundApp());

            //Activates Test App to foreground from background
            Assert.DoesNotThrow(() => _driver.ActivateApp(ApiDemosPackageName));

            //Verify the expected app was activated
            Assert.DoesNotThrow(() => _driver.FindElement(MobileBy.AccessibilityId(ApiDemoElement)));
            Assert.DoesNotThrow(() => _driver.FindElement(MobileBy.AccessibilityId(ApiDemoElement)));
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
                () => _driver.BackgroundApp(TimeSpan.FromSeconds(5)));
        }

        [Test]
        public void CanBackgroundAppToDeactivationUsingNegativeSecond()
        {
            Assert.DoesNotThrow(
                () => _driver.BackgroundApp(-TimeSpan.FromSeconds(-1)));
        }

        #endregion
    }
}