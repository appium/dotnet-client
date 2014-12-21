using System.Collections.ObjectModel;

namespace OpenQA.Selenium.Appium.src.Appium.Interfaces
{
    public interface IFindByIosUIAutomation
    {
        /// <summary>
        /// Finds the first of elements that match the Ios UIAutomation selector supplied
        /// </summary>
        /// <param name="selector">an Ios UIAutomation selector</param>
        /// <returns>IWebElement object so that you can interact that object</returns>
        IWebElement FindElementByIosUIAutomation(string selector);

        /// <summary>
        /// Finds a list of elements that match the Ios UIAutomation selector supplied
        /// </summary>
        /// <param name="selector">an Ios UIAutomation selector</param>
        /// <returns>IWebElement object so that you can interact that object</returns>
        ReadOnlyCollection<IWebElement> FindElementsByIosUIAutomation(string selector);
    }
}
