using System.Collections.Generic;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace Appium.Net.Integration.Tests.PageObjectTests.DesktopBrowserCompatibility
{
    class FoundLinks
    {
        [CacheLookup]
        [FindsBySequence]
        [FindsBy(How = How.ClassName, Using = "r", Priority = 1)]
        [FindsBy(How = How.TagName, Using = "a", Priority = 2)]
        public IList<IWebElement> Links { set; get; }
    }
}