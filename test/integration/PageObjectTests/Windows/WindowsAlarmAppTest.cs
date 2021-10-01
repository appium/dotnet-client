using Appium.Net.Integration.Tests.helpers;
using Appium.Net.Integration.Tests.PageObjects;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.PageObjects;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace Appium.Net.Integration.Tests.PageObjectTests.Windows
{
    public class WindowsAlarmAppTest
    {
        private AppiumDriver<AppiumWebElement> _driver;

        [SetUp]
        public void Setup()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalOption("app", "Microsoft.WindowsAlarms_8wekyb3d8bbwe!App");

            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;

            _driver = new WindowsDriver<AppiumWebElement>(serverUri, appCapabilities);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.CloseApp();
        }

        [Test]
        public void AddAlarm()
        { 
            var alarmApp = new WindowsAlarmApp(_driver, new TimeOutDuration(TimeSpan.FromSeconds(2)));

            alarmApp.SwitchToClockTab();
            var localTimeText = alarmApp.LocalTime;

            alarmApp.SwitchToAlarmTab();

            var alarmName = "Windows Page Object Alarm";
            alarmApp.AddAlarmOffsetByOneMinute(localTimeText, alarmName);

            Assert.IsTrue(alarmApp.HasAlarmWithName(alarmName));

            alarmApp.DeleteAlarmWithName(alarmName);
        }
    }
}
