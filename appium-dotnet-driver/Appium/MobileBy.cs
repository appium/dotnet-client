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

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;

namespace OpenQA.Selenium.Appium
{
    public abstract class MobileBy: By
    {
        protected readonly string selector = string.Empty;
        private readonly string InterfaceNameRegExp;
        private readonly string SingleSearchMethodName;
        private readonly string ListSearchMethodName;

        internal MobileBy(string selector, string interfaceNameRegExp, string singleSearchMethodName,
            string listSearchMethodName)
            :base()
        {
            if (string.IsNullOrEmpty(selector))
            {
                throw new ArgumentException("selector identifier cannot be null or the empty string", "selector");
            }

            this.selector = selector;
            InterfaceNameRegExp = interfaceNameRegExp;
            SingleSearchMethodName = singleSearchMethodName;
            ListSearchMethodName = listSearchMethodName;
        }

        /// <summary>
        /// Find a single element.
        /// </summary>
        /// <param name="context">Context used to find the element.</param>
        /// <returns>The element that matches</returns>
        public override IWebElement FindElement(ISearchContext context)
        {
            Type contextType = context.GetType();
            Type findByAccessibilityId = contextType.GetInterface(InterfaceNameRegExp + "`1", false);
            if (null == findByAccessibilityId)
            {
                throw new InvalidCastException("Unable to cast " + contextType.ToString() + " to " + InterfaceNameRegExp);
            }
            MethodInfo m = findByAccessibilityId.GetMethod(SingleSearchMethodName, new Type[] { typeof(string) });
            return (IWebElement)m.Invoke(context, new object[] { selector });
        }

        /// <summary>
        /// Finds many elements
        /// </summary>
        /// <param name="context">Context used to find the element.</param>
        /// <returns>A readonly collection of elements that match.</returns>
        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            Type contextType = context.GetType();
            Type findByAccessibilityId = contextType.GetInterface(InterfaceNameRegExp + "`1", false);
            if (null == findByAccessibilityId)
            {
                throw new InvalidCastException("Unable to cast " + contextType.ToString() + " to " + InterfaceNameRegExp);
            }
            MethodInfo m = findByAccessibilityId.GetMethod(ListSearchMethodName, new Type[] { typeof(string) });
            return CollectionConverterUnility.
                            ConvertToExtendedWebElementCollection<IWebElement>((IList)m.Invoke(context, new object[] { selector }));
        }

        /// <summary>
        /// This method creates a <see cref="OpenQA.Selenium.By"/> strategy 
        /// that searches for elements by accessibility id
        /// About Android accessibility
        /// <see cref="https://developer.android.com/intl/ru/training/accessibility/accessible-app.html"/>
        /// About iOS accessibility
        /// <see cref="https://developer.apple.com/library/ios/documentation/UIKit/Reference/UIAccessibilityIdentification_Protocol/index.html"/>
        /// </summary>
        /// <param name="selector">The selector to use in finding the element.</param>
        /// <returns></returns>
        public static By AccessibilityId(string selector) => new ByAccessibilityId(selector);

        /// <summary>
        /// This method creates a <see cref="OpenQA.Selenium.By"/> strategy 
        /// that searches for elements using Android UI automation framework.
        /// <see cref="http://developer.android.com/intl/ru/tools/testing-support-library/index.html#uia-apis"/>
        /// </summary>
        /// <param name="selector">The selector to use in finding the element.</param>
        /// <returns></returns>
        public static By AndroidUIAutomator(string selector) => new ByAndroidUIAutomator(selector);

        /// <summary>
        /// This method creates a <see cref="OpenQA.Selenium.By"/> strategy 
        /// that searches for elements using iOS UI automation.
        /// <see cref="https://developer.apple.com/library/tvos/documentation/DeveloperTools/Conceptual/InstrumentsUserGuide/UIAutomation.html"/>
        /// </summary>
        /// <param name="selector">The selector to use in finding the element.</param>
        /// <returns></returns>
        public static By IosUIAutomation(string selector) => new ByIosUIAutomation(selector);
    }

    /// <summary>
    /// Finds element when the Accessibility Id selector has the specified value.
    /// About Android accessibility 
    /// <see cref="https://developer.android.com/intl/ru/training/accessibility/accessible-app.html"/>
    /// About iOS accessibility
    /// <see cref="https://developer.apple.com/library/ios/documentation/UIKit/Reference/UIAccessibilityIdentification_Protocol/index.html"/>
    /// </summary>
    public class ByAccessibilityId : MobileBy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ByAccessibilityId"/> class.
        /// </summary>
        /// <param name="selector">The selector to use in finding the element.</param>
        public ByAccessibilityId(string selector)
            :base(selector, "IFindByAccessibilityId", "FindElementByAccessibilityId", "FindElementsByAccessibilityId")
        {
        }

        public override string ToString() =>
            $"ByAccessibilityId({selector})";
    }

    /// <summary>
    /// Finds element when the Android UIAutomator selector has the specified value.
    /// <see cref="http://developer.android.com/intl/ru/tools/testing-support-library/index.html#uia-apis"/>
    /// </summary>
    public class ByAndroidUIAutomator : MobileBy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ByAndroidUIAutomator"/> class.
        /// </summary>
        /// <param name="selector">The selector to use in finding the element.</param>
        public ByAndroidUIAutomator(string selector)
            : base(selector, "IFindByAndroidUIAutomator", "FindElementByAndroidUIAutomator", "FindElementsByAndroidUIAutomator")
        {
        }

        public override string ToString() =>
            $"ByAndroidUIAutomator({selector})";
    }

    /// <summary>
    /// Finds element when the Ios UIAutomation selector has the specified value.
    /// <see cref="https://developer.apple.com/library/tvos/documentation/DeveloperTools/Conceptual/InstrumentsUserGuide/UIAutomation.html"/>
    /// </summary>
    public class ByIosUIAutomation : MobileBy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ByIosUIAutomation"/> class.
        /// </summary>
        /// <param name="selector">The selector to use in finding the element.</param>
        public ByIosUIAutomation(string selector)
            :base(selector, "IFindByIosUIAutomation", "FindElementByIosUIAutomation", "FindElementsByIosUIAutomation")
        {
        }

        public override string ToString() =>
            $"ByIosUIAutomation({selector})";
    }
}
