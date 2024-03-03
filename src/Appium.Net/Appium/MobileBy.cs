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
        protected readonly string selector;
        private readonly string _searchingCriteriaName;

        internal MobileBy(string selector, string searchingCriteriaName)
        {
            if (string.IsNullOrEmpty(selector))
            {
                throw new ArgumentException("Selector identifier cannot be null or the empty string", nameof(selector));
            }

            this.selector = selector;
            _searchingCriteriaName = searchingCriteriaName;
        }

        /// <summary>
        /// Find a single element.
        /// </summary>
        /// <param name="context">Context used to find the element.</param>
        /// <returns>The element that matches</returns>
        public override IWebElement FindElement(ISearchContext context)
        {
            if (context is IFindsByFluentSelector<IWebElement> finder)
                return finder.FindElement(_searchingCriteriaName, selector);
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
            if (context is IFindsByFluentSelector<IWebElement> finder)
                return finder.FindElements(_searchingCriteriaName, selector).ToList().AsReadOnly();
            throw new InvalidCastException($"Unable to cast {context.GetType().FullName} " +
                                           $"to {nameof(IFindsByFluentSelector<IWebElement>)}");
        }

        /// <summary>
        /// This method creates a <see cref="By"/> strategy 
        /// that searches for elements by accessibility id
        /// About Android accessibility
        /// <see href="https://developer.android.com/intl/ru/training/accessibility/accessible-app.html"/>
        /// About iOS accessibility
        /// <see href="https://developer.apple.com/library/ios/documentation/UIKit/Reference/UIAccessibilityIdentification_Protocol/index.html"/>
        /// </summary>
        /// <param name="selector">The selector to use in finding the element.</param>
        /// <returns></returns>
        public static By AccessibilityId(string selector) => new ByAccessibilityId(selector);

        /// <summary>
        /// This method creates a <see cref="By"/> strategy 
        /// that searches for elements using Android UI automation framework.
        /// <see href="http://developer.android.com/intl/ru/tools/testing-support-library/index.html#uia-apis"/>
        /// </summary>
        /// <param name="selector">The selector to use in finding the element.</param>
        /// <returns></returns>
        public static By AndroidUIAutomator(string selector) => new ByAndroidUIAutomator(selector);

        /// <summary>
        /// This method creates a <see cref="By"/> strategy 
        /// that searches for elements using Android UI automation framework.
        /// <see href="http://developer.android.com/intl/ru/tools/testing-support-library/index.html#uia-apis"/>
        /// </summary>
        /// <param name="selector">The selector to use in finding the element.</param>
        /// <returns></returns>
        public static By AndroidUIAutomator(IUiAutomatorStatementBuilder selector) =>
            new ByAndroidUIAutomator(selector);

        /// <summary>
        /// This method creates a <see cref="By"/> strategy
        /// that searches for elements using Espresso's Data Matcher.
        /// <see href="http://appium.io/docs/en/writing-running-appium/android/espresso-datamatcher-selector"/>
        /// </summary>
        /// <param name="selector">The selector to use in finding the element.</param>
        /// <returns></returns>
        public static By AndroidDataMatcher(string selector) => new ByAndroidDataMatcher(selector);

        /// <summary>
        /// This method creates a <see cref="By"/> strategy
        /// that searches for elements using Espresso's View Matcher.
        /// <see href="https://developer.android.com/training/testing/espresso/basics#finding-view"/>
        /// </summary>
        /// <param name="selector">The selector to use in finding the element.</param>
        /// <returns></returns>
        public static By AndroidViewMatcher(string selector) => new ByAndroidViewMatcher(selector);

        /// <summary>
        /// This method creates a <see cref="By"/> strategy 
        /// that searches for elements using iOS UI automation.
        /// <see href="https://developer.apple.com/library/tvos/documentation/DeveloperTools/Conceptual/InstrumentsUserGuide/UIAutomation.html"/>
        /// </summary>
        /// <param name="selector">The selector to use in finding the element.</param>
        /// <returns></returns>
        public static By IosUIAutomation(string selector) => new ByIosUIAutomation(selector);

        public static By WindowsAutomation(string selector) => new ByWindowsAutomation(selector);

        public static By TizenAutomation(string selector) => new ByTizenAutomation(selector);

        public static By IosNSPredicate(string selector) => new ByIosNSPredicate(selector);

        public static By IosClassChain(string selector) => new ByIosClassChain(selector);

        public static By Image(string selector) => new ByImage(selector);

        public static new By Name(string selector) => new ByName(selector);

        public static new By Id(string selector) => new ById(selector);

        public static new By ClassName(string selector) => new ByClassName(selector);

        public static new By TagName(string selector) => new ByTagName(selector);
    }

    /// <summary>
    /// Finds element when the Accessibility Id selector has the specified value.
    /// About Android accessibility 
    /// <see href="https://developer.android.com/intl/ru/training/accessibility/accessible-app.html"/>
    /// About iOS accessibility
    /// <see href="https://developer.apple.com/library/ios/documentation/UIKit/Reference/UIAccessibilityIdentification_Protocol/index.html"/>
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
            if (context is IFindByAccessibilityId<IWebElement> finder)
                return finder.FindElementByAccessibilityId(selector);
            return base.FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            if (context is IFindByAccessibilityId<IWebElement> finder)
                return finder.FindElementsByAccessibilityId(selector).ToList().AsReadOnly();
            return base.FindElements(context);
        }

        public override string ToString() =>
            $"ByAccessibilityId({selector})";
    }

    /// <summary>
    /// Finds element when the Android UIAutomator selector has the specified value.
    /// <see href="http://developer.android.com/intl/ru/tools/testing-support-library/index.html#uia-apis"/>
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ByAndroidUIAutomator"/> class.
        /// </summary>
        /// <param name="selector">The selector to use in finding the element.</param>
        public ByAndroidUIAutomator(IUiAutomatorStatementBuilder selector) : base(selector.Build(),
            MobileSelector.AndroidUIAutomator)
        {
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            if (context is IFindByAndroidUIAutomator<IWebElement> finder)
                return finder.FindElementByAndroidUIAutomator(selector);
            return base.FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            if (context is IFindByAndroidUIAutomator<IWebElement> finder)
                return finder.FindElementsByAndroidUIAutomator(selector).ToList().AsReadOnly();
            return base.FindElements(context);
        }

        public override string ToString() =>
            $"ByAndroidUIAutomator({selector})";
    }

    /// <summary>
    /// Finds element when the Espresso's Data Matcher selector has the specified value.
    /// <see href="http://appium.io/docs/en/writing-running-appium/android/espresso-datamatcher-selector"/>
    /// </summary>
    public class ByAndroidDataMatcher : MobileBy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ByAndroidDataMatcher"/> class.
        /// </summary>
        /// <param name="selector">The selector to use in finding the element.</param>
        public ByAndroidDataMatcher(string selector) : base(selector, MobileSelector.AndroidDataMatcher)
        {
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            if (context is IFindByAndroidDataMatcher<IWebElement> finder)
                return finder.FindElementByAndroidDataMatcher(selector);
            return base.FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            if (context is IFindByAndroidDataMatcher<IWebElement> finder)
                return finder.FindElementsByAndroidDataMatcher(selector).ToList().AsReadOnly();
            return base.FindElements(context);
        }

        public override string ToString() =>
            $"ByAndroidDataMatcher({selector})";
    }

    /// <summary>
    /// Finds element when the Espresso's View Matcher selector has the specified value.
    /// <see href="https://developer.android.com/training/testing/espresso/basics#finding-view"/>
    /// </summary>
    public class ByAndroidViewMatcher : MobileBy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ByAndroidViewMatcher"/> class.
        /// </summary>
        /// <param name="selector">The selector to use in finding the element.</param>
        public ByAndroidViewMatcher(string selector) : base(selector, MobileSelector.AndroidViewMatcher)
        {
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            if (context is IFindByAndroidViewMatcher<IWebElement> finder)
                return finder.FindElementByAndroidViewMatcher(selector);
            return base.FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            if (context is IFindByAndroidViewMatcher<IWebElement> finder)
                return finder.FindElementsByAndroidViewMatcher(selector).ToList().AsReadOnly();
            return base.FindElements(context);
        }

        public override string ToString() =>
            $"ByAndroidViewMatcher({selector})";
    }

    /// <summary>
    /// Finds element when the Ios UIAutomation selector has the specified value.
    /// <see href="https://developer.apple.com/library/tvos/documentation/DeveloperTools/Conceptual/InstrumentsUserGuide/UIAutomation.html"/>
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
            if (context is IFindByIosUIAutomation<IWebElement> finder)
                return finder.FindElementByIosUIAutomation(selector);
            return base.FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            if (context is IFindByIosUIAutomation<IWebElement> finder)
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
            if (context is IFindByWindowsUIAutomation<IWebElement> finder)
                return finder.FindElementByWindowsUIAutomation(selector);
            return base.FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            if (context is IFindByWindowsUIAutomation<IWebElement> finder)
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
            if (context is IFindByTizenUIAutomation<IWebElement> finder)
                return finder.FindElementByTizenUIAutomation(selector);
            return base.FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            if (context is IFindByTizenUIAutomation<IWebElement> finder)
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
            if (context is IFindsByIosNSPredicate<IWebElement> finder)
                return finder.FindElementByIosNsPredicate(selector);
            return base.FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            if (context is IFindsByIosNSPredicate<IWebElement> finder)
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
            if (context is IFindsByIosClassChain<IWebElement> finder)
                return finder.FindElementByIosClassChain(selector);
            return base.FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            if (context is IFindsByIosClassChain<IWebElement> finder)
                return finder.FindElementsByIosClassChain(selector).ToList().AsReadOnly();
            return base.FindElements(context);
        }

        public override string ToString() =>
            $"ByIosClassChain({selector})";
    }

    public class ByImage : MobileBy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ByImage"/> class.
        /// </summary>
        /// <param name="selector">Image selector.</param>
        public ByImage(string selector) : base(selector, MobileSelector.Image)
        {
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            if (context is IFindsByImage<IWebElement> finder)
                return finder.FindElementByImage(selector);
            return base.FindElement(context);
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            if (context is IFindsByImage<IWebElement> finder)
                return finder.FindElementsByImage(selector).ToList().AsReadOnly();
            return base.FindElements(context);
        }

        public override string ToString() =>
            $"ByImage({selector})";
    }

    public class ByName : MobileBy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ByName"/> class.
        /// </summary>
        /// <param name="selector">Name selector.</param>
        public ByName(string selector) : base(selector, MobileSelector.Name)
        {
        }
        public override string ToString() =>
            $"ByName({selector})";
    }

    public class ById : MobileBy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ById"/> class.
        /// </summary>
        /// <param name="selector">Id selector.</param>
        public ById(string selector) : base(selector, MobileSelector.Id)
        {
        }
        public override string ToString() =>
            $"ById({selector})";
    }

    public class ByTagName : MobileBy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ByTagName"/> class.
        /// </summary>
        /// <param name="selector">Tag name selector.</param>
        public ByTagName(string selector) : base(selector, MobileSelector.TagName)
        {
        }
        public override string ToString() =>
            $"ByTagName({selector})";
    }

    public class ByClassName : MobileBy
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ByClassName"/> class.
        /// </summary>
        /// <param name="selector">Class name selector.</param>
        public ByClassName(string selector) : base(selector, MobileSelector.ClassName)
        {
        }
        public override string ToString() =>
            $"ByClassName({selector})";
    }
}