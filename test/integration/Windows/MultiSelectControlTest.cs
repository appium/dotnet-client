//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//See the NOTICE file distributed with this work for additional
//information regarding copyright ownership.
//You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace Appium.Net.Integration.Tests.Windows
{
    public class MultiSelectControlTest
    {
        protected static WindowsDriver AlarmClockSession;
        protected static WindowsDriver DesktopSession;

        [OneTimeSetUp]
        public void Setup()
        {
            // Launch the AlarmClock app
            var appCapabilities = new AppiumOptions();
            appCapabilities.App = "Microsoft.WindowsAlarms_8wekyb3d8bbwe!App";
            appCapabilities.PlatformName ="Windows";
            appCapabilities.DeviceName = "WindowsPC";
            appCapabilities.AddAdditionalAppiumOption("ms:experimental-webdriver", true);

            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;

            AlarmClockSession =
                new WindowsDriver(serverUri, appCapabilities);

            Assert.IsNotNull(AlarmClockSession);
            AlarmClockSession.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            // Create a session for Desktop
            var desktopCapabilities = new AppiumOptions();
            desktopCapabilities.App = "Root";
            desktopCapabilities.DeviceName = "WindowsPC";

            DesktopSession =
                new WindowsDriver(serverUri, desktopCapabilities);
            Assert.IsNotNull(DesktopSession);

        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            SwitchToAlarmTab();

            // Delete all created alarms
            var alarmEntries = AlarmClockSession.FindElements(MobileBy.Name("Windows Application Driver Test Alarm"));
            foreach (var alarmEntry in alarmEntries)
            {
                /////// TODO - Implement for Appium
                //// AlarmClockSession.Mouse.ContextClick(alarmEntry.Coordinates);
                /// AlarmClockSession.FindElement(MobileBy.AccessibilityId("ContextMenuDelete")).Click();
                /// For now Click and delete
                alarmEntry.Click();
                AlarmClockSession.FindElement(MobileBy.AccessibilityId("DeleteButton")).Click();
            }

            AlarmClockSession.Dispose();
            AlarmClockSession = null;
        }

        [Test]
        public void FullMultiSelectScenario()
        {
            // Read the current local time
            SwitchToWorldClockTab();
            var localTimeText = ReadLocalTime();
            Assert.IsTrue(localTimeText.Length > 0);

            // Add an alarm at 1 minute after local time
            SwitchToAlarmTab();
            AddAlarm(localTimeText);
            Thread.Sleep(300);
            var alarmEntries = AlarmClockSession.FindElements(MobileBy.Name("Windows Application Driver Test Alarm"));
            Assert.IsTrue(alarmEntries.Count > 0);
        }

        public static void SwitchToAlarmTab()
        {
            AlarmClockSession.FindElement(MobileBy.AccessibilityId("AlarmButton")).Click();
        }

        public void SwitchToWorldClockTab()
        {
            AlarmClockSession.FindElement(MobileBy.AccessibilityId("ClockButton")).Click();
        }

        public string ReadLocalTime()
        {
            var localTimeText = "";
            AppiumElement worldClockPivotItem =
                AlarmClockSession.FindElement(MobileBy.AccessibilityId("ClockButton"));
            if (worldClockPivotItem != null)
            {
                localTimeText = AlarmClockSession.FindElement(MobileBy.AccessibilityId("TimeBlock")).GetDomAttribute("Name");
                var timeStrings = localTimeText.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries);

                foreach (var timeString in timeStrings)
                {
                    // Get the time. E.g. "11:32 AM" from "Local time, Monday, February 22, 2016, 11:32 AM, "
                    if (timeString.Contains(":"))
                    {
                        localTimeText = new string(timeString.Trim().Where(c => c < 128).ToArray()); // Remove 8206 character, see https://stackoverflow.com/questions/18298208/strange-error-when-parsing-string-to-date
                        break;
                    }
                }
            }

            return localTimeText;
        }

        public void AddAlarm(string timeText)
        {
            if (timeText.Length > 0)
            {
                // Create a test alarm 1 minute after the read local time
                var fi = CultureInfo.CurrentUICulture.DateTimeFormat;
                var alarmTime = DateTime.Parse(timeText, fi);
                alarmTime = alarmTime.AddMinutes(1.0);
                var hourString = alarmTime.ToString("%h", fi);
                var minuteString = alarmTime.ToString("mm", fi);
                var period = alarmTime.ToString("tt", fi);

                AlarmClockSession.FindElement(MobileBy.AccessibilityId("AddAlarmButton")).Click();
                var alarmNameBox = AlarmClockSession.FindElement(MobileBy.Name("Alarm name"));
                alarmNameBox.Clear();
                alarmNameBox.SendKeys("Windows Application Driver Test Alarm");
                AppiumElement periodSelector = null;
                try
                {
                    periodSelector = AlarmClockSession.FindElement(MobileBy.AccessibilityId("PeriodLoopingSelector"));
                }
                catch (NoSuchElementException)
                {
                    hourString = alarmTime.ToString("HH", fi);
                }
                periodSelector?.FindElement(MobileBy.Name(period)).Click();
                AlarmClockSession.FindElement(MobileBy.AccessibilityId("HourPicker")).SendKeys(hourString);
                AlarmClockSession.FindElement(MobileBy.AccessibilityId("MinutePicker")).Click();
                AlarmClockSession.FindElement(MobileBy.AccessibilityId("MinutePicker")).SendKeys(minuteString);
                Thread.Sleep(500);
                AlarmClockSession.FindElement(MobileBy.AccessibilityId("PrimaryButton")).Click();
            }
        }

        public void DismissNotification()
        {
            try
            {
                AppiumElement newNotification = DesktopSession.FindElement(MobileBy.Name("New notification"));
                Assert.IsTrue(newNotification.FindElement(MobileBy.AccessibilityId("MessageText")).Text
                    .Contains("Windows Application Driver Test Alarm"));
                newNotification.FindElement(MobileBy.Name("Dismiss")).Click();
            }
            catch
            {
            }
        }
    }
}