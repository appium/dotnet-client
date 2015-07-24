using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.PageObjects
{
    internal class WebDriverUnpackUtility
    {
        /// <summary>
        /// This method returns a wrapped IWebDriver instance 
        /// </summary>
        /// <returns></returns>
        internal static IWebDriver UnpackWebdriver(ISearchContext context)
        {
            IWebDriver driver = context as IWebDriver;
            if (driver != null)
                return driver;

            // Search context it is not only Webdriver. Webelement is search context
            // too.
            // RemoteWebElement and AppiumWebElement implement IWrapsDriver
            if ((context as IWrapsDriver) != null)
                return UnpackWebdriver(((IWrapsDriver) context)
                    .WrappedDriver);

            if ((context as  IWrapsElement) != null)
                return UnpackWebdriver(((IWrapsElement) context)
					.WrappedElement);

            return null;
        }
    }
}
