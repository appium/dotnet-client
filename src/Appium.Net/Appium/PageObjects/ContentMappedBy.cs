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

using OpenQA.Selenium.Appium.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenQA.Selenium.Appium.PageObjects
{
    internal class ContentMappedBy : By
    {
        private readonly Dictionary<ContentTypes, IEnumerable<By>> map;
        private readonly static string NATIVE_APP_PATTERN = "NATIVE_APP";

        internal ContentMappedBy(Dictionary<ContentTypes, IEnumerable<By>> map)
        {
            this.map = map;
        }

        private IEnumerable<By> GetReturnRelevantBys(ISearchContext context)
        {
            IWebDriver driver = WebDriverUnpackUtility.UnpackWebdriver(context);
            if (!typeof(IContextAware).IsAssignableFrom(driver.GetType())) //it is desktop browser 
                return map[ContentTypes.HTML];

            IContextAware contextAware = driver as IContextAware;
            string currentContext = contextAware.Context;
            if (currentContext.Contains(NATIVE_APP_PATTERN))
                return map[ContentTypes.NATIVE];

            return map[ContentTypes.HTML];
        }

        private static bool IsInvalidSelectorRootCause(Exception e)
        {
            string invalid_selector_pattern = "Invalid locator strategy:";
            if (e == null)
                return false;

            string message = e.Message;

            if (!string.IsNullOrWhiteSpace(message) &&
                (typeof(InvalidSelectorException).IsAssignableFrom(e.GetType()) ||
                 message.Contains(invalid_selector_pattern)))
            {
                return true;
            }

            return IsInvalidSelectorRootCause(e.InnerException);
        }


        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            List<IWebElement> result = new List<IWebElement>();
            IEnumerable<By> bys = GetReturnRelevantBys(context);

            foreach (var by in bys)
            {
                try
                {
                    ReadOnlyCollection<IWebElement> list = context.FindElements(by);
                    result.AddRange(list);
                }
                catch (Exception e)
                {
                    if (IsInvalidSelectorRootCause(e))
                    {
                        continue;
                    }
                    throw e;
                }
            }

            return result.AsReadOnly();
        }

        private static string ReturnByString(IEnumerable<By> bys)
        {
            string result = "";
            foreach (var by in bys)
            {
                result = result + by.ToString() + "; ";
            }
            return result;
        }

        public override string ToString()
        {
            IEnumerable<By> defaultBy = map[ContentTypes.HTML];
            IEnumerable<By> nativeBy = map[ContentTypes.NATIVE];

            if (defaultBy.Equals(nativeBy))
                return ReturnByString(defaultBy);

            return "Locator map: " + "\n" +
                   "- native content: \"" + ReturnByString(nativeBy) + "\" \n" +
                   "- html content: \"" + ReturnByString(defaultBy) + "\"";
        }
    }
}