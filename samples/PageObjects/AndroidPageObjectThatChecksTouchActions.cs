using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.PageObjects.Attributes;

namespace Appium.Samples.PageObjects
{
    class AndroidPageObjectThatChecksTouchActions
    {
        [FindsByAndroidUIAutomator(Accessibility = "Accessibility")]
        private IWebElement accessibility;

        [FindsByAndroidUIAutomator(Accessibility = "Custom View")]
        private IWebElement customView;

        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().clickable(true)")]
        private IWebElement clickable;

        public void CheckTap(IPerformsTouchActions performer)
        {
            TouchAction t = new TouchAction(performer);
            t.Tap(accessibility);
            t.Perform();

            MultiAction m = new MultiAction(performer);
            m.Add(new TouchAction(performer).Tap(customView));
            m.Add(new TouchAction(performer).Tap(clickable));
            m.Perform();
        }
    }
}
