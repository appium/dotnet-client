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

using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace OpenQA.Selenium.Appium
{
    public abstract class MobileBy : By
    {
        protected readonly string selector = string.Empty;
        private readonly string SearchingCriteriaName;


        internal MobileBy(string selector, string searchingCriteriaName) : base()
        {
            if (string.IsNullOrEmpty(selector))
            {
                throw new ArgumentException("Selector identifier cannot be null or the empty string", nameof(selector));
            }

            this.selector = selector;
            SearchingCriteriaName = searchingCriteriaName;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var finder = context as IFindsByFluentSelector<IWebElement>;
            if (finder != null)
                return finder.FindElement(SearchingCriteriaName, selector);
            throw new InvalidCastException($"Unable to cast {context.GetType().FullName} " +
                $"to {nameof(IFindsByFluentSelector<IWebElement>)}");
        }

        /// <summary>
        /// Finds many elements
        /// </summary>
        /// <param name="context">Context used to find the element.</param>
        /// <returns>A readonly collection of elements that match.</returns>
        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var finder = context as IFindsByFluentSelector<IWebElement>;
            if (finder != null)
                return finder.FindElements(SearchingCriteriaName, selector).ToList().AsReadOnly();
            throw new InvalidCastException($"Unable to cast {context.GetType().FullName} " +
                $"to {nameof(IFindsByFluentSelector<IWebElement>)}");
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

        public static By WindowsAutomation(string selector) => new ByWindowsAutomation(selector);

        public static By TizenAutomation(string selector) => new ByTizenAutomation(selector);

        public static By IosNSPredicate(string selector) => new ByIosNSPredicate(selector);

        public static By IosClassChain(string selector) => new ByIosClassChain(selector);
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
        public ByAccessibilityId(string selector) : base(selector, MobileSelector.Accessibility)
        {
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var finder = context as IFindByAccessibilityId<IWebElement>;
            if (finder != null)
                return finder.FindElementByAccessibilityId(selector);
            return base.FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var finder = context as IFindByAccessibilityId<IWebElement>;
            if (finder != null)
                return finder.FindElementsByAccessibilityId(selector).ToList().AsReadOnly();
            return base.FindElements(context);
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
        public ByAndroidUIAutomator(string selector) : base(selector, MobileSelector.AndroidUIAutomator)
        {
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var finder = context as IFindByAndroidUIAutomator<IWebElement>;
            if (finder != null)
                return finder.FindElementByAndroidUIAutomator(selector);
            return base.FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var finder = context as IFindByAndroidUIAutomator<IWebElement>;
            if (finder != null)
                return finder.FindElementsByAndroidUIAutomator(selector).ToList().AsReadOnly();
            return base.FindElements(context);
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
        public ByIosUIAutomation(string selector) : base(selector, MobileSelector.iOSAutomatoion)
        {
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var finder = context as IFindByIosUIAutomation<IWebElement>;
            if (finder != null)
                return finder.FindElementByIosUIAutomation(selector);
            return base.FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var finder = context as IFindByIosUIAutomation<IWebElement>;
            if (finder != null)
                return finder.FindElementsByIosUIAutomation(selector).ToList().AsReadOnly();
            return base.FindElements(context);
        }

        public override string ToString() =>
            $"ByIosUIAutomation({selector})";
    }

    public class ByWindowsAutomation : MobileBy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ByWindowsAutomation"/> class.
        /// </summary>
        /// <param name="selector">The selector to use in finding the element.</param>
        public ByWindowsAutomation(string selector) : base(selector, MobileSelector.WindowsUIAutomation)
        {
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var finder = context as IFindByWindowsUIAutomation<IWebElement>;
            if (finder != null)
                return finder.FindElementByWindowsUIAutomation(selector);
            return base.FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var finder = context as IFindByWindowsUIAutomation<IWebElement>;
            if (finder != null)
                return finder.FindElementsByWindowsUIAutomation(selector).ToList().AsReadOnly();
            return base.FindElements(context);
        }

        public override string ToString() =>
            $"ByWindowsAutomation({selector})";
    }

    public class ByTizenAutomation : MobileBy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ByTizenAutomation"/> class.
        /// </summary>
        /// <param name="selector">The selector to use in finding the element.</param>
        public ByTizenAutomation(string selector) : base(selector, MobileSelector.TizenUIAutomation)
        {
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var finder = context as IFindByTizenUIAutomation<IWebElement>;
            if (finder != null)
                return finder.FindElementByTizenUIAutomation(selector);
            return base.FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var finder = context as IFindByTizenUIAutomation<IWebElement>;
            if (finder != null)
                return finder.FindElementsByTizenUIAutomation(selector).ToList().AsReadOnly();
            return base.FindElements(context);
        }

        public override string ToString() =>
            $"ByTizenAutomation({selector})";
    }

    public class ByIosNSPredicate : MobileBy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ByIosNSPredicate"/> class.
        /// </summary>
        /// <param name="selector">The selector to use in finding the element.</param>
        public ByIosNSPredicate(string selector) : base(selector, MobileSelector.iOSPredicateString)
        {
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var finder = context as IFindsByIosNSPredicate<IWebElement>;
            if (finder != null)
                return finder.FindElementByIosNsPredicate(selector);
            return base.FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var finder = context as IFindsByIosNSPredicate<IWebElement>;
            if (finder != null)
                return finder.FindElementsByIosNsPredicate(selector).ToList().AsReadOnly();
            return base.FindElements(context);
        }

        public override string ToString() =>
            $"ByIosNSPredicate({selector})";
    }

    public class ByIosClassChain : MobileBy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ByIosClassChain"/> class.
        /// </summary>
        /// <param name="selector">The selector to use in finding the element.</param>
        public ByIosClassChain(string selector) : base(selector, MobileSelector.iOSClassChain)
        {
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var finder = context as IFindsByIosClassChain<IWebElement>;
            if (finder != null)
                return finder.FindElementByIosClassChain(selector);
            return base.FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var finder = context as IFindsByIosClassChain<IWebElement>;
            if (finder != null)
                return finder.FindElementsByIosClassChain(selector).ToList().AsReadOnly();
            return base.FindElements(context);
        }

        public override string ToString() =>
            $"ByIosClassChain({selector})";
    }
}