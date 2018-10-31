using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.PageObjects.Attributes;

namespace Appium.Net.Integration.Tests.PageObjects
{
    class AndroidPageObjectThatChecksTouchActions
    {
        [FindsByAndroidUIAutomator(Accessibility = "Accessibility")] private IWebElement _accessibility;

        [FindsByAndroidUIAutomator(Accessibility = "Custom View")] private IWebElement _customView;

        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().clickable(true)")]
        private IWebElement _clickable;

        public void CheckTap(IPerformsTouchActions performer)
        {
            var t = new TouchAction(performer);
            t.Tap(_accessibility);
            t.Perform();

            var m = new MultiAction(performer);
            m.Add(new TouchAction(performer).Tap(_customView));
            m.Add(new TouchAction(performer).Tap(_clickable));
            m.Perform();
        }
    }
}