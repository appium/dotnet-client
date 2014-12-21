using System.Collections.ObjectModel;

namespace OpenQA.Selenium.Appium.Interfaces
{
    public interface IFindByAndroidUIAutomator
    {
        /// <summary>
        /// Finds the first element in the page that matches the Android UIAutomator selector supplied
        /// </summary>
        /// <param name="selector">Selector for the element.</param>
        /// <returns>IWebElement object so that you can interact that object</returns>
        /// <example>
        /// <code>
        /// IWebDriver driver = new RemoteWebDriver(DesiredCapabilities.Firefox());
        /// IWebElement elem = driver.FindElementByAndroidUIAutomator('elements()'))
        /// </code>
        /// </example>
        IWebElement FindElementByAndroidUIAutomator(string selector);

        /// <summary>
        /// Finds a list of elements that match the Android UIAutomator selector supplied
        /// </summary>
        /// <param name="selector">Selector for the elements.</param>
        /// <returns>ReadOnlyCollection of IWebElement object so that you can interact with those objects</returns>
        /// <example>
        /// <code>
        /// IWebDriver driver = new RemoteWebDriver(DesiredCapabilities.Firefox());
        /// ReadOnlyCollection<![CDATA[<IWebElement>]]> elem = driver.FindElementsByAndroidUIAutomator(elements())
        /// </code>
        /// </example>
        ReadOnlyCollection<IWebElement> FindElementsByAndroidUIAutomator(string selector);
    }
}
