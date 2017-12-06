//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//See the NOTICE file distributed with this work for additional
//information regarding copyright ownership.
//You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using OpenQA.Selenium.Internal;

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

            if ((context as IWrapsElement) != null)
                return UnpackWebdriver(((IWrapsElement) context)
                    .WrappedElement);

            return null;
        }
    }
}