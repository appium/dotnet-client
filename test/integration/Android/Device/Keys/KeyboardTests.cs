using System.Collections.Generic;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android.Device.Keys
{
    class KeyboardTests
    {
        private AndroidDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            _driver?.Quit();
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test]
        public void HideKeyBoardTestCase()
        {
            StartCustomTitleActivity();
            var text_edit_btn = By.Id("io.appium.android.apis:id/left_text_edit");
            _driver.FindElement(text_edit_btn).Clear();
            _driver.FindElement(text_edit_btn).Click();
            _driver.HideKeyboard();
        }

        [Test]
        public void IsKeyBoardShownTestCase()
        {
            StartCustomTitleActivity();
            var text_edit_btn = By.Id("io.appium.android.apis:id/left_text_edit");
            _driver.FindElement(text_edit_btn).Clear();
            _driver.FindElement(text_edit_btn).Click();
            bool is_visible = _driver.IsKeyboardShown();
            Assert.That(is_visible);
        }

        [Test]
        public void HideKeyBoardWithKeyTestCase()
        {
            StartCustomTitleActivity();
            var text_edit_btn = By.Id("io.appium.android.apis:id/left_text_edit");
            _driver.FindElement(text_edit_btn).Clear();
            _driver.FindElement(text_edit_btn).Click();
            _driver.HideKeyboard("Enter");
            bool is_visible = _driver.IsKeyboardShown();
            Assert.That(!is_visible);
        }

        private void StartCustomTitleActivity() {
            _driver.ExecuteScript(
                "mobile:startActivity",
                new object[] {
                    new Dictionary<string, string>() {
                        ["intent"] = "io.appium.android.apis/.app.CustomTitle",
                    }
                }
            );
        }

    }
}