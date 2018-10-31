using System;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.PageObjects;
using SeleniumExtras.PageObjects;

namespace Appium.Net.Integration.Tests.PageObjects
{
    class AndroidJavaScriptTestPageObject
    {
        [FindsBy(How = How.XPath, Using = ".//*[@type=\"submit\"]")] private IWebElement _sendMeYourName;

        private readonly IWebDriver _driver;

        public AndroidJavaScriptTestPageObject(IWebDriver driver)
        {
            this._driver = driver;
            var timeSpan = new TimeOutDuration(new TimeSpan(0, 0, 0, 5, 0));
            PageFactory.InitElements(driver, this, new AppiumPageObjectMemberDecorator(timeSpan));
        }

        public void HighlightElement()
        {
            var executor = _driver as IJavaScriptExecutor;
            executor.ExecuteScript("arguments[0].style.border = '" + "4px solid rgb(0,255,0)" + "'", _sendMeYourName);
            Thread.Sleep(5000);
        }
    }
}