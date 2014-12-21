using System.Collections.ObjectModel;

namespace OpenQA.Selenium.Appium.Interfaces
{
    public interface IFindByAccessibilityId
    {
        /// <summary>
        /// Finds the first of elements that match the Accessibility Id selector supplied
        /// </summary>
        /// <param name="selector">an Accessibility Id selector</param>
        /// <returns>IWebElement object so that you can interact that object</returns>
        IWebElement FindElementByAccessibilityId(string selector);

        /// <summary>
        /// Finds a list of elements that match the Accessibility Id selector supplied
        /// </summary>
        /// <param name="selector">an Accessibility Id selector</param>
        /// <returns>IWebElement object so that you can interact that object</returns>
        ReadOnlyCollection<IWebElement> FindElementsByAccessibilityId(string selector);
    }
}
