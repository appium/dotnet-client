using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appium.Samples.PageObjects
{
    public class AndroidPageObjectChecksSeleniumFindsByCompatibility
    {
        [FindsBy(How = How.ClassName, Using = "android.widget.TextView")]
        private IWebElement testElement;

        /////////////////////////////////////////////////////////////////
        private object propertyElement;
        private object propertyElements;
        /////////////////////////////////////////////////////////////////

        [FindsBy(How = How.ClassName, Using = "android.widget.TextView")]
        private IWebElement TestElement
        {
            set
            {
                propertyElement = value;
            }
            get
            {
                return (IWebElement) propertyElement;
            }
        }

        [FindsBy(How = How.ClassName, Using = "android.widget.TextView")]
        private IList<IWebElement> testElements;

        [FindsBy(How = How.ClassName, Using = "android.widget.TextView")]
        private IList<IWebElement> TestElements
        {
            set
            {
                propertyElements = value;
            }
            get
            {
                return (IList< IWebElement >) propertyElements;
            }
        }

        /////////////////////////////////////////////////////////////////
        private object testMobileElementProperty;
        private object testMobileElementsProperty;
        /////////////////////////////////////////////////////////////////

        [FindsBy(How = How.ClassName, Using = "android.widget.TextView")]
        private IMobileElement<AndroidElement> testMobileElement;

        [FindsBy(How = How.ClassName, Using = "android.widget.TextView")]
        private IList<AndroidElement> testMobileElements;

        [FindsBy(How = How.ClassName, Using = "android.widget.TextView")]
        private IMobileElement<AndroidElement> TestMobileElement
        {
            set
            {
                testMobileElementProperty = value;
            }
            get
            {
                return (IMobileElement<AndroidElement>) testMobileElementProperty;
            }
        }

        [FindsBy(How = How.ClassName, Using = "android.widget.TextView")]
        private IList<AndroidElement> TestMobileElements
        {
            set
            {
                testMobileElementsProperty = value;
            }
            get
            {
                return (IList<AndroidElement>) testMobileElementsProperty;
            }
        }

        [FindsBy(How = How.Name, Using = "FakeName", Priority = 1)]
        [FindsBy(How = How.Id, Using = "FakeId", Priority = 2)]
        [FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 3)]
        private IMobileElement<AndroidElement> testMultipleElement;

        [FindsBy(How = How.Name, Using = "FakeName", Priority = 1)]
        [FindsBy(How = How.Id, Using = "FakeId", Priority = 2)]
        [FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 3)]
        private IList<AndroidElement> testMultipleElements;


        /////////////////////////////////////////////////////////////////
        private object testMultipleFindByElementProperty;
        private object testMultipleFindByElementsProperty;
        /////////////////////////////////////////////////////////////////

        [FindsBy(How = How.Name, Using = "FakeName", Priority = 1)]
        [FindsBy(How = How.Id, Using = "FakeId", Priority = 2)]
        [FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 3)]
        private IMobileElement<AndroidElement> TestMultipleFindByElementProperty
        {
            set
            {
                testMultipleFindByElementProperty = value;
            }
            get
            {
                return (IMobileElement<AndroidElement>) testMultipleFindByElementProperty;
            }
        }

        [FindsBy(How = How.Name, Using = "FakeName", Priority = 1)]
        [FindsBy(How = How.Id, Using = "FakeId", Priority = 2)]
        [FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 3)]
        private IList<AndroidElement> MultipleFindByElementsProperty
        {
            set
            {
                testMultipleFindByElementsProperty = value;
            }
            get
            {
                return (IList<AndroidElement>) testMultipleFindByElementsProperty;
            }
        }

        [FindsBySequence]
        [FindsBy(How = How.Id, Using = "android:id/content", Priority = 1)]
        [FindsBy(How = How.Id, Using = "android:id/list", Priority = 2)]
        [FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 3)]
        private IMobileElement<AndroidElement> foundByChainedSearchElement;

        [FindsBySequence]
        [FindsBy(How = How.Id, Using = "android:id/content", Priority = 1)]
        [FindsBy(How = How.Id, Using = "android:id/list", Priority = 2)]
        [FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 3)]
        private IList<AndroidElement> foundByChainedSearchElements;

        /////////////////////////////////////////////////////////////////
        private object foundByChainedSearchElementProperty;
        private object foundByChainedSearchElementsProperty;
        /////////////////////////////////////////////////////////////////

        [FindsBySequence]
        [FindsBy(How = How.Id, Using = "android:id/content", Priority = 1)]
        [FindsBy(How = How.Id, Using = "android:id/list", Priority = 2)]
        [FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 3)]
        private IMobileElement<AndroidElement> TestFoundByChainedSearchElementProperty
        {
            set
            {
                foundByChainedSearchElementProperty = value;
            }
            get
            {
                return (IMobileElement<AndroidElement>) foundByChainedSearchElementProperty;
            }
        }

        [FindsBySequence]
        [FindsBy(How = How.Id, Using = "android:id/content", Priority = 1)]
        [FindsBy(How = How.Id, Using = "android:id/list", Priority = 2)]
        [FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 3)]
        private IList<AndroidElement> TestFoundByChainedSearchElementsProperty
        {
            set
            {
                foundByChainedSearchElementsProperty = value;
            }
            get
            {
                return (IList<AndroidElement>) foundByChainedSearchElementsProperty;
            }
        }

        [FindsByAll]
        [FindsBy(How = How.Id, Using = "android:id/text1", Priority = 1)]
        //[FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 2)]
        //Equals method of RemoteWebElement is not consistent for mobile apps
        //The second selector will be commented till the problem is worked out
        private IMobileElement<AndroidElement> matchedToAllLocatorsElement;

        [FindsByAll]
        [FindsBy(How = How.Id, Using = "android:id/text1", Priority = 1)]
        //[FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 2)]
        //Equals method of RemoteWebElement is not consistent for mobile apps
        //The second selector will be commented till the problem is worked out
        private IList<AndroidElement> matchedToAllLocatorsElements;

        /////////////////////////////////////////////////////////////////
        private object matchedToAllLocatorsElementProperty;
        private object matchedToAllLocatorsElementsProperty;
        /////////////////////////////////////////////////////////////////

        [FindsByAll]
        [FindsBy(How = How.Id, Using = "android:id/text1", Priority = 1)]
        //[FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 2)]
        //Equals method of RemoteWebElement is not consistent for mobile apps
        //The second selector will be commented till the problem is worked out
        private IMobileElement<AndroidElement> TestMatchedToAllLocatorsElementProperty
        {
            set
            {
                matchedToAllLocatorsElementProperty = value;
            }
            get
            {
                return (IMobileElement<AndroidElement>) matchedToAllLocatorsElementProperty;
            }
        }

        [FindsByAll]
        [FindsBy(How = How.Id, Using = "android:id/text1", Priority = 1)]
        //[FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 2)]
        //Equals method of RemoteWebElement is not consistent for mobile apps
        //The second selector will be commented till the problem is worked out
        private IList<AndroidElement> TestMatchedToAllLocatorsElementsProperty
        {
            set
            {
                matchedToAllLocatorsElementsProperty = value;
            }
            get
            {
                return (IList<AndroidElement>) matchedToAllLocatorsElementsProperty;
            }
        }

        public string GetElementText()
        {
            return testElement.Text;
        }

        public int GetElementSize()
        {
            return testElements.Count;
        }

        public string GetElementPropertyText()
        {
            return TestElement.Text;
        }

        public int GetElementPropertySize()
        {
            return TestElements.Count;
        }

        //////////////////////////////////////////////////////////////////////////
        public string GetMobileElementText()
        {
            return testMobileElement.Text;
        }

        public int GetMobileElementSize()
        {
            return testMobileElements.Count;
        }

        public string GetMobileElementPropertyText()
        {
            return TestMobileElement.Text;
        }

        public int GetMobileElementPropertySize()
        {
            return TestMobileElements.Count;
        }

        //////////////////////////////////////////////////////////////////////////
        public string GetMultipleFindByElementText()
        {
            return testMultipleElement.Text;
        }

        public int GetMultipleFindByElementSize()
        {
            return testMultipleElements.Count;
        }

        public string GetMultipleFindByElementPropertyText()
        {
            return TestMultipleFindByElementProperty.Text;
        }

        public int GetMultipleFindByElementPropertySize()
        {
            return MultipleFindByElementsProperty.Count;
        }

        //////////////////////////////////////////////////////////////////////////
        public string GetFoundByChainedSearchElementText()
        {
            return foundByChainedSearchElement.Text;
        }

        public int GetFoundByChainedSearchElementSize()
        {
            return foundByChainedSearchElements.Count;
        }

        public string GetFoundByChainedSearchElementPropertyText()
        {
            return TestFoundByChainedSearchElementProperty.Text;
        }

        public int GetFoundByChainedSearchElementPropertySize()
        {
            return TestFoundByChainedSearchElementsProperty.Count;
        }

        //////////////////////////////////////////////////////////////////////////
        public string GetMatchedToAllLocatorsElementText()
        {
            return matchedToAllLocatorsElement.Text;
        }

        public int GetMatchedToAllLocatorsElementSize()
        {
            return matchedToAllLocatorsElements.Count;
        }

        public string GetMatchedToAllLocatorsElementPropertyText()
        {
            return TestMatchedToAllLocatorsElementProperty.Text;
        }

        public int GetMatchedToAllLocatorsElementPropertySize()
        {
            return TestMatchedToAllLocatorsElementsProperty.Count;
        }

    }
}
