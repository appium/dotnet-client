using OpenQA.Selenium;
using OpenQA.Selenium.Appium.PageObjects;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Threading;

namespace Appium.Samples.PageObjects
{
    class AndroidJavaScriptTestPageObject
    {
        [FindsBy(How = How.XPath, Using = ".//*[@type=\"submit\"]")]
        private IWebElement sendMeYourName;

        private readonly IWebDriver driver;

        public AndroidJavaScriptTestPageObject(IWebDriver driver)
        {
            this.driver = driver;
            TimeOutDuration timeSpan = new TimeOutDuration(new TimeSpan(0, 0, 0, 5, 0));
            PageFactory.InitElements(driver, this, new AppiumPageObjectMemberDecorator(timeSpan));
        }

        public void HighlightElement()
        {
            IJavaScriptExecutor executor = driver as IJavaScriptExecutor;
            executor.ExecuteScript("arguments[0].style.border = '" + "4px solid rgb(0,255,0)" + "'", sendMeYourName);
            Thread.Sleep(5000);
        }

    }
}
