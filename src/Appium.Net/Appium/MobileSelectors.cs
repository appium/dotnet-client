using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenQA.Selenium.Appium
{
    public static class MobileSelectors
    {
        public static IWebElement FindElementByAccessibilityId(this IWebElement searchContext, string selector)
        {
            return (searchContext as WebDriver).FindElement(MobileSelector.Accessibility, selector);
        }

        public static ReadOnlyCollection<IWebElement> FindElementsByAccessibilityId(this IWebElement searchContext, string selector)
        {
            return (searchContext as WebDriver).FindElements(MobileSelector.Accessibility, selector);
        }

        public static IWebElement FindElementByName(this IWebElement searchContext, string selector)
        {
            return (searchContext as WebDriver).FindElement(MobileSelector.Name, selector);
        }

        public static ReadOnlyCollection<IWebElement> FindElementsByName(this IWebElement searchContext, string selector)
        {
            return (searchContext as WebDriver).FindElements(MobileSelector.Name, selector);
        }

        #region IFindByIosUIAutomation Members

        public static IWebElement FindElementByIosUIAutomation(this IWebElement searchContext, string selector) =>
            (searchContext as WebDriver).FindElement(MobileSelector.iOSAutomatoion, selector);

        public static IReadOnlyCollection<IWebElement> FindElementsByIosUIAutomation(this IWebElement searchContext, string selector) =>
            (searchContext as WebDriver).FindElements(MobileSelector.iOSAutomatoion, selector);

        #endregion IFindByIosUIAutomation Members

        #region IFindByAndroidUIAutomator Members

        public static IWebElement FindElementByAndroidUIAutomator(this IWebElement searchContext, string selector) =>
            (searchContext as WebDriver).FindElement(MobileSelector.AndroidUIAutomator, selector);

        public static IWebElement FindElementByAndroidUIAutomator(this IWebElement searchContext, IUiAutomatorStatementBuilder selector) =>
            (searchContext as WebDriver).FindElement(MobileSelector.AndroidUIAutomator, selector.Build());

        public static IReadOnlyCollection<IWebElement> FindElementsByAndroidUIAutomator(this IWebElement searchContext, string selector) =>
            (searchContext as WebDriver).FindElements(MobileSelector.AndroidUIAutomator, selector);

        public static IReadOnlyCollection<IWebElement> FindElementsByAndroidUIAutomator(this IWebElement searchContext, IUiAutomatorStatementBuilder selector) =>
            (searchContext as WebDriver).FindElements(MobileSelector.AndroidUIAutomator, selector.Build());

        #endregion IFindByAndroidUIAutomator Members

        #region IFindByAndroidDataMatcher Members

        public static IWebElement FindElementByAndroidDataMatcher(this IWebElement searchContext, string selector) =>
            (searchContext as WebDriver).FindElement(MobileSelector.AndroidDataMatcher, selector);

        public static IReadOnlyCollection<IWebElement> FindElementsByAndroidDataMatcher(this IWebElement searchContext, string selector) =>
            (searchContext as WebDriver).FindElements(MobileSelector.AndroidDataMatcher, selector);

        #endregion IFindByAndroidDataMatcher Members

        #region IFindByAndroidViewMatcher Members

        public static IWebElement FindElementByAndroidViewMatcher(this IWebElement searchContext, string selector) =>
            (searchContext as WebDriver).FindElement(MobileSelector.AndroidViewMatcher, selector);

        public static IReadOnlyCollection<IWebElement> FindElementsByAndroidViewMatcher(this IWebElement searchContext, string selector) =>
            (searchContext as WebDriver).FindElements(MobileSelector.AndroidViewMatcher, selector);

        #endregion IFindByAndroidViewMatcher Members

        #region IFindByTizenUIAutomation Members

        public static IWebElement FindElementByTizenUIAutomation(this IWebElement searchContext, string selector) =>
            (searchContext as WebDriver).FindElement(MobileSelector.TizenUIAutomation, selector);

        public static IReadOnlyCollection<IWebElement> FindElementsByTizenUIAutomation(this IWebElement searchContext, string selector) =>
            (searchContext as WebDriver).FindElements(MobileSelector.TizenUIAutomation, selector);

        #endregion IFindByTizenUIAutomation Members

        #region IFindByWindowsUIAutomation Members

        public static IWebElement FindElementByWindowsUIAutomation(this IWebElement searchContext, string selector) =>
            (searchContext as WebDriver).FindElement(MobileSelector.WindowsUIAutomation, selector);

        public static IReadOnlyCollection<IWebElement> FindElementsByWindowsUIAutomation(this IWebElement searchContext, string selector) =>
            (searchContext as WebDriver).FindElements(MobileSelector.WindowsUIAutomation, selector);

        #endregion IFindByWindowsUIAutomation Members
    }
}
