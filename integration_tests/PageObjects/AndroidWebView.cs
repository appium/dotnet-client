using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;

namespace Appium.Net.Integration.Tests.PageObjects
{
    public class AndroidWebView
    {
        [FindsBy(How = How.Id, Using = "name_input")]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/fakeID\")")]
        private IWebElement _name;

        [FindsBy(How = How.Name, Using = "car")]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/fakeID\")")]
        [FindsByIOSUIAutomation(IosUIAutomation = ".elements()[0]")] private IWebElement _carSelect;

        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/fakeID\")")]
        [FindsByIOSUIAutomation(IosUIAutomation = ".elements()[0]")]
        [FindsBy(How = How.XPath, Using = ".//*[@type=\"submit\"]")] private IWebElement _sendMeYourName;

        public void SetName(string name)
        {
            this._name.Clear();
            this._name.SendKeys(name);
        }

        public void SelectCar(String car)
        {
            var select = new SelectElement(_carSelect);
            select.SelectByValue(car);
        }

        public void SendMeYourName()
        {
            _sendMeYourName.Submit();
        }
    }
}