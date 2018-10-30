using System;
using System.Collections.Generic;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.PageObjects;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Internal;
using SeleniumExtras.PageObjects;

namespace Appium.Net.Integration.Tests.PageObjectTests.DesktopBrowserCompatibility
{
    [TestFixture]
    public class DesktopBrowserCompatibilityTest
    {
        private IWebDriver _driver;
        private bool _allPassed = true;
        private FoundLinks _links;

        [FindsBy(How = How.Name, Using = "q")]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/someId\")")]
        [FindsByIOSUIAutomation(IosUIAutomation = ".elements()[0]")] private IWebElement _searchTextField;

        [FindsByAndroidUIAutomator(ClassName = "someClass")] [FindsByIOSUIAutomation(IosUIAutomation = "//selector[1]")]
        [FindsBy(How = How.Name, Using = "btnG")] private IWebElement _searchButton;

        [CacheLookup] //this element will be found once
        private IWebElement _ires; //Should be found by ID or Name equal "ires"

        private IList<IWebElement> _btnG; //these elements are found by name="btnG"

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var timeSpan = new TimeOutDuration(new TimeSpan(0, 0, 0, 5, 0));
            _driver = new FirefoxDriver();
            _driver.Url = "https://www.google.com";
            var decorator = new AppiumPageObjectMemberDecorator(timeSpan);
            PageFactory.InitElements(_driver, this, decorator);
            _links = new FoundLinks();
            PageFactory.InitElements(_ires, _links, decorator);
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            try
            {
                if (Env.ServerIsRemote())
                    ((IJavaScriptExecutor) _driver).ExecuteScript(
                        "sauce:job-result=" + (_allPassed ? "passed" : "failed"));
            }
            finally
            {
                _driver.Quit();
            }
        }

        [Test]
        public void GoogleSearching()
        {
            _searchTextField.SendKeys("Hello Appium!");
            Assert.GreaterOrEqual(1, _btnG.Count);
            _searchButton.Click();
            Assert.GreaterOrEqual(10, _links.Links.Count);
            Assert.AreNotEqual(_ires, null);
            Assert.AreNotEqual(((IWrapsElement) _ires).WrappedElement, null);

            //this checking notices that element is found once and cached
            Assert.AreEqual(((IWrapsElement) _ires).WrappedElement.GetHashCode(),
                ((IWrapsElement) _ires).WrappedElement.GetHashCode());

            //this checking notices that element are found once and cached
            var cachedList = _links.Links;
            Assert.GreaterOrEqual(10, cachedList.Count);
            Assert.AreEqual(_links.Links.Count, cachedList.Count);

            var i = 0;
            foreach (var element in cachedList)
            {
                Assert.AreEqual(element.GetHashCode(), _links.Links[i].GetHashCode());
                i++;
            }
        }
    }


    class FoundLinks
    {
        [CacheLookup]
        [FindsBySequence]
        [FindsBy(How = How.ClassName, Using = "r", Priority = 1)]
        [FindsBy(How = How.TagName, Using = "a", Priority = 2)]
        public IList<IWebElement> Links { set; get; }
    }
}