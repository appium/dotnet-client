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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Interfaces;

namespace Appium.Net.Integration.Tests.Common
{
    /// <summary>
    /// These are exercised via the underlying selector classes (e.g. <see cref="ByAndroidDataMatcher"/>)
    /// in the Android/Espresso integration tests, but the <see cref="MobileBy"/> static factory
    /// methods themselves are never called from anywhere else, so they get no coverage there.
    /// </summary>
    public class MobileByFactoryTest
    {
        [TestCase("androidDataMatcher", typeof(ByAndroidDataMatcher), "ByAndroidDataMatcher(androidDataMatcher)")]
        [TestCase("androidViewMatcher", typeof(ByAndroidViewMatcher), "ByAndroidViewMatcher(androidViewMatcher)")]
        public void AndroidFactoryMethodsBuildExpectedSelector(string selector, Type expectedType, string expectedToString)
        {
            By by = selector == "androidDataMatcher"
                ? MobileBy.AndroidDataMatcher(selector)
                : MobileBy.AndroidViewMatcher(selector);

            Assert.That(by, Is.InstanceOf(expectedType));
            Assert.That(by.ToString(), Is.EqualTo(expectedToString));
        }

        [Test]
        public void WindowsAutomationFactoryMethodBuildsExpectedSelector()
        {
            By by = MobileBy.WindowsAutomation("//Button");
            Assert.That(by, Is.InstanceOf<ByWindowsAutomation>());
            Assert.That(by.ToString(), Is.EqualTo("ByWindowsAutomation(//Button)"));
        }

        [Test]
        public void TizenAutomationFactoryMethodBuildsExpectedSelector()
        {
            By by = MobileBy.TizenAutomation("selector");
            Assert.That(by, Is.InstanceOf<ByTizenAutomation>());
            Assert.That(by.ToString(), Is.EqualTo("ByTizenAutomation(selector)"));
        }

        [Test]
        public void AndroidDataMatcherDelegatesToMatchingFinder()
        {
            var element = new FakeWebElement();
            var finder = new FakeFinder(elementToReturn: element);

            var found = MobileBy.AndroidDataMatcher("{'name':'hasEntry'}").FindElement(finder);

            Assert.That(found, Is.SameAs(element));
            Assert.That(finder.LastCalledMethod, Is.EqualTo(nameof(IFindByAndroidDataMatcher<IWebElement>.FindElementByAndroidDataMatcher)));
            Assert.That(finder.LastSelector, Is.EqualTo("{'name':'hasEntry'}"));
        }

        [Test]
        public void AndroidViewMatcherDelegatesToMatchingFinder()
        {
            var elements = new ReadOnlyCollection<IWebElement>(new List<IWebElement> { new FakeWebElement() });
            var finder = new FakeFinder(elementsToReturn: elements);

            var found = MobileBy.AndroidViewMatcher("{'name':'withText'}").FindElements(finder);

            Assert.That(found, Is.SameAs(elements));
            Assert.That(finder.LastCalledMethod, Is.EqualTo(nameof(IFindByAndroidViewMatcher<IWebElement>.FindElementsByAndroidViewMatcher)));
        }

        [Test]
        public void FindElementThrowsWhenContextDoesNotSupportSelector()
        {
            var by = MobileBy.AndroidDataMatcher("selector");
            var unsupportedContext = new UnsupportedSearchContext();

            Assert.Throws<InvalidCastException>((Action)(() => by.FindElement(unsupportedContext)));
        }

        private class UnsupportedSearchContext : ISearchContext
        {
            public IWebElement FindElement(By by) => throw new NotSupportedException();
            public ReadOnlyCollection<IWebElement> FindElements(By by) => throw new NotSupportedException();
        }

        private class FakeFinder : ISearchContext,
            IFindByAndroidDataMatcher<IWebElement>,
            IFindByAndroidViewMatcher<IWebElement>,
            IFindByWindowsUIAutomation<IWebElement>,
            IFindByTizenUIAutomation<IWebElement>
        {
            private readonly IWebElement _elementToReturn;
            private readonly IReadOnlyCollection<IWebElement> _elementsToReturn;

            public string LastCalledMethod { get; private set; }
            public string LastSelector { get; private set; }

            public FakeFinder(IWebElement elementToReturn = null, IReadOnlyCollection<IWebElement> elementsToReturn = null)
            {
                _elementToReturn = elementToReturn;
                _elementsToReturn = elementsToReturn;
            }

            public IWebElement FindElement(By by) => throw new NotSupportedException();
            public ReadOnlyCollection<IWebElement> FindElements(By by) => throw new NotSupportedException();

            public IWebElement FindElement(string by, string value) => throw new NotSupportedException();
            public IReadOnlyCollection<IWebElement> FindElements(string selector, string value) => throw new NotSupportedException();

            public IWebElement FindElementByAndroidDataMatcher(string selector)
            {
                LastCalledMethod = nameof(FindElementByAndroidDataMatcher);
                LastSelector = selector;
                return _elementToReturn;
            }

            public IReadOnlyCollection<IWebElement> FindElementsByAndroidDataMatcher(string selector)
            {
                LastCalledMethod = nameof(FindElementsByAndroidDataMatcher);
                LastSelector = selector;
                return _elementsToReturn;
            }

            public IWebElement FindElementByAndroidViewMatcher(string selector)
            {
                LastCalledMethod = nameof(FindElementByAndroidViewMatcher);
                LastSelector = selector;
                return _elementToReturn;
            }

            public IReadOnlyCollection<IWebElement> FindElementsByAndroidViewMatcher(string selector)
            {
                LastCalledMethod = nameof(FindElementsByAndroidViewMatcher);
                LastSelector = selector;
                return _elementsToReturn;
            }

            public IWebElement FindElementByWindowsUIAutomation(string selector)
            {
                LastCalledMethod = nameof(FindElementByWindowsUIAutomation);
                LastSelector = selector;
                return _elementToReturn;
            }

            public IReadOnlyCollection<IWebElement> FindElementsByWindowsUIAutomation(string selector)
            {
                LastCalledMethod = nameof(FindElementsByWindowsUIAutomation);
                LastSelector = selector;
                return _elementsToReturn;
            }

            public IWebElement FindElementByTizenUIAutomation(string selector)
            {
                LastCalledMethod = nameof(FindElementByTizenUIAutomation);
                LastSelector = selector;
                return _elementToReturn;
            }

            public IReadOnlyCollection<IWebElement> FindElementsByTizenUIAutomation(string selector)
            {
                LastCalledMethod = nameof(FindElementsByTizenUIAutomation);
                LastSelector = selector;
                return _elementsToReturn;
            }
        }

        private class FakeWebElement : IWebElement
        {
            public string TagName => throw new NotSupportedException();
            public string Text => throw new NotSupportedException();
            public bool Enabled => throw new NotSupportedException();
            public bool Selected => throw new NotSupportedException();
            public Point Location => throw new NotSupportedException();
            public Size Size => throw new NotSupportedException();
            public bool Displayed => throw new NotSupportedException();

            public void Clear() => throw new NotSupportedException();
            public void SendKeys(string text) => throw new NotSupportedException();
            public void Submit() => throw new NotSupportedException();
            public void Click() => throw new NotSupportedException();
            public string GetAttribute(string attributeName) => throw new NotSupportedException();
            public string GetDomAttribute(string attributeName) => throw new NotSupportedException();
            public string GetDomProperty(string propertyName) => throw new NotSupportedException();
            public string GetCssValue(string propertyName) => throw new NotSupportedException();
            public IWebElement FindElement(By by) => throw new NotSupportedException();
            public ReadOnlyCollection<IWebElement> FindElements(By by) => throw new NotSupportedException();
            public ISearchContext GetShadowRoot() => throw new NotSupportedException();
        }
    }
}
