using System.Collections.ObjectModel;

namespace OpenQA.Selenium.Appium.Interfaces
{
    public interface IFindByAccessibilityId<W> where W : IWebElement
    {
        /// <summary>
        /// Finds the first of elements that match the Accessibility Id selector supplied
        /// </summary>
        /// <param name="selector">an Accessibility Id selector</param>
        /// <returns>IWebElement object so that you can interact that object</returns>
        W FindElementByAccessibilityId(string selector);

        /// <summary>
        /// Finds a list of elements that match the Accessibility Id selector supplied
        /// </summary>
        /// <param name="selector">an Accessibility Id selector</param>
        /// <returns>IWebElement object so that you can interact that object</returns>
        ReadOnlyCollection<W> FindElementsByAccessibilityId(string selector);
    }
}
