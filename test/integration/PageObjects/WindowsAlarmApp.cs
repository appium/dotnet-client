using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using OpenQA.Selenium.Appium.MultiTouch;

namespace Appium.Net.Integration.Tests.PageObjects
{
    public class WindowsAlarmApp
    {
        [FindsByWindowsAutomation(Accessibility = "AlarmButton")]
        private IMobileElement<AppiumWebElement> _alarmTab;

        [FindsByWindowsAutomation(Accessibility = "ClockButton")]
        private IMobileElement<AppiumWebElement> _clockTab;

        [FindsByWindowsAutomation(Accessibility = "WorldClockItemGrid")]
        private IMobileElement<AppiumWebElement> _worldClock;

        [FindsByWindowsAutomation(Accessibility = "AddAlarmButton")]
        private IMobileElement<AppiumWebElement> _addAlarmButton;

        [FindsByWindowsAutomation(Accessibility = "AlarmNameTextBox")]
        private IMobileElement<AppiumWebElement> _alarmNameTextBox;

        [FindsByWindowsAutomation(Accessibility = "PeriodLoopingSelector")]
        private IMobileElement<AppiumWebElement> _periodSelector;

        [FindsByWindowsAutomation(Accessibility = "HourLoopingSelector")]
        private IMobileElement<AppiumWebElement> _hourSelector;

        [FindsByWindowsAutomation(Accessibility = "MinuteLoopingSelector")]
        private IMobileElement<AppiumWebElement> _minuteSelector;

        [FindsByWindowsAutomation(Accessibility = "AlarmSaveButton")]
        private IMobileElement<AppiumWebElement> _saveButton;

        private AppiumDriver<AppiumWebElement> _driver;

        public WindowsAlarmApp(AppiumDriver<AppiumWebElement> driver, TimeOutDuration timeout)
        {
            _driver = driver;

            PageFactory.InitElements(driver, this, new AppiumPageObjectMemberDecorator(timeout));
        }

        public void SwitchToAlarmTab()
        {
            _alarmTab.Click();
        }

        public void SwitchToClockTab()
        {
            _clockTab.Click();
        }

        public string LocalTime
        {
            get
            {
                var localTimeText = _worldClock.Text;
                var timeStrings = localTimeText.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var timeString in timeStrings)
                {
                    // Get the time. E.g. "11:32 AM" from "Local time, Monday, February 22, 2016, 11:32 AM, "
                    if (timeString.Contains(":"))
                    {
                        localTimeText = new string(timeString.Trim().Where(c => c < 128).ToArray()); // Remove 8206 character, see https://stackoverflow.com/questions/18298208/strange-error-when-parsing-string-to-date
                        break;
                    }
                }

                return localTimeText;
            }
        }

        public void AddAlarmOffsetByOneMinute(string localTimeText, string alarmName)
        {
            // Create a test alarm 1 minute after the read local time
            var fi = CultureInfo.CurrentUICulture.DateTimeFormat;

            var alarmTime = DateTime.Parse(localTimeText, fi);
            alarmTime = alarmTime.AddMinutes(1.0);

            var hourString = alarmTime.ToString("%h", fi);
            var minuteString = alarmTime.ToString("mm", fi);
            var period = alarmTime.ToString("tt", fi);

            _addAlarmButton.Click();
            _alarmNameTextBox.Clear();
            _alarmNameTextBox.SendKeys(alarmName);

            try
            {
                _periodSelector.FindElementByName(period).Click();
            }
            catch (NoSuchElementException)
            {
                hourString = alarmTime.ToString("HH", fi);
            }

            _hourSelector.FindElementByName(hourString).Click();
            _minuteSelector.FindElementByName(minuteString).Click();

            _saveButton.Click();
        }

        public bool HasAlarmWithName(string alarmName)
        {
            return _driver.FindElementByName(alarmName).Displayed;
        }

        public void DeleteAlarmWithName(string alarmName)
        {
            var alarmEntry = _driver.FindElementByName(alarmName);

            new TouchAction(_driver)
                .Press(
                    x: alarmEntry.Coordinates.LocationInViewport.X,
                    y: alarmEntry.Coordinates.LocationInViewport.X)
                .Release();

            _driver.FindElementByName("Delete").Click();
        }
    }
}
