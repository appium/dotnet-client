using System.Collections.ObjectModel;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium.Windows
{
    public class WindowsElement : AppiumWebElement, IFindByWindowsUIAutomation<AppiumWebElement>
    {
        public WindowsElement(RemoteWebDriver parent, string id)
            : base(parent, id)
        {
        }

        #region IFindByWindowsUIAutomation Members

        public AppiumWebElement FindElementByWindowsUIAutomation(string selector)
        {
            return (AppiumWebElement)this.FindElement("-windows uiautomation", selector);
        }

        public ReadOnlyCollection<AppiumWebElement> FindElementsByWindowsUIAutomation(string selector)
        {
            return CollectionConverterUnility.ConvertToExtendedWebElementCollection<AppiumWebElement>(
                this.FindElements("-windows uiautomation", selector));
        }
        #endregion IFindByWindowsUIAutomation Members
    }
}