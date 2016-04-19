using System.Collections.ObjectModel;

namespace OpenQA.Selenium.Appium.Interfaces
{
    public interface IFindByWindowsUIAutomation<W> where W : IWebElement
    {
        /// <summary>
        /// Finds the first of elements that match the Windows UIAutomation selector supplied
        /// </summary>
        /// <param name="selector">a Windows UIAutomation selector</param>
        /// <returns>IWebElement object so that you can interact that object</returns>
        W FindElementByWindowsUIAutomation(string selector);

        /// <summary>
        /// Finds a list of elements that match the Windows UIAutomation selector supplied
        /// </summary>
        /// <param name="selector">a Windows UIAutomation selector</param>
        /// <returns>IWebElement object so that you can interact that object</returns>
        ReadOnlyCollection<W> FindElementsByWindowsUIAutomation(string selector);
    }
}
