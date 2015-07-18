using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Appium.Samples.PageObjects
{
    public class AndroidPageObjectChecksSelendroidModeOnNativeApp
    {
        /////////////////////////////////////////////////////////////////
        private object testMobileElementProperty;
        private object testMobileElementsProperty;
        /////////////////////////////////////////////////////////////////

        [FindsBySelendroid(ID = "my_text_field")]
        private IWebElement testMobileElement;

        [FindsBySelendroid(ID = "my_text_field")]
        private IList<IWebElement> testMobileElements;

        [FindsBySelendroid(ID = "my_text_field")]
        private IWebElement TestMobileElement
        {
            set
            {
                testMobileElementProperty = value;
            }
            get
            {
                return (IWebElement) testMobileElementProperty;
            }
        }

        [FindsBySelendroid(ID = "my_text_field")]
        private IList<IWebElement> TestMobileElements
        {
            set
            {
                testMobileElementsProperty = value;
            }
            get
            {
                return (IList<IWebElement>) testMobileElementsProperty;
            }
        }

        [FindsBySelendroid(ID = "fake_content", Priority = 1)]
        [FindsBySelendroid(ClassName = "android.widget.FakeSelendroidClass", Priority = 2)]
        [FindsBySelendroid(ID = "waitingButtonTest", Priority = 3)]

        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/fakeId\")", Priority = 1)]
        [FindsByAndroidUIAutomator(ID = "FakeId", Priority = 2)]
        [FindsByAndroidUIAutomator(ClassName = "FakeClass", Priority = 3)]
        private IWebElement testMultipleElement;

        [FindsBySelendroid(ID = "fake_content", Priority = 1)]
        [FindsBySelendroid(ClassName = "fakeSelendroidClass", Priority = 2)]
        [FindsBySelendroid(LinkText = "Press to throw unhandled exception", Priority = 3)]

        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/fakeId\")", Priority = 1)]
        [FindsByAndroidUIAutomator(ID = "FakeId", Priority = 2)]
        [FindsByAndroidUIAutomator(ClassName = "FakeClass", Priority = 3)]
        private IList<IWebElement> testMultipleElements;


        /////////////////////////////////////////////////////////////////
        private object testMultipleFindByElementProperty;
        private object testMultipleFindByElementsProperty;
        /////////////////////////////////////////////////////////////////

        [FindsBySelendroid(ID = "fake_content", Priority = 1)]
        [FindsBySelendroid(ClassName = "fakeSelendroidClass", Priority = 2)]
        [FindsBySelendroid(LinkText = "Press to throw unhandled exception", Priority = 3)]
        private IWebElement TestMultipleFindByElementProperty
        {
            set
            {
                testMultipleFindByElementProperty = value;
            }
            get
            {
                return (IWebElement) testMultipleFindByElementProperty;
            }
        }

        [FindsBySelendroid(ID = "fake_content", Priority = 1)]
        [FindsBySelendroid(ClassName = "fakeSelendroidClass", Priority = 2)]
        [FindsBySelendroid(LinkText = "Press to throw unhandled exception", Priority = 3)]
        private IList<IWebElement> MultipleFindByElementsProperty
        {
            set
            {
                testMultipleFindByElementsProperty = value;
            }
            get
            {
                return (IList<IWebElement>)testMultipleFindByElementsProperty;
            }
        }

        [FindsBySequence]
        [FindsBySelendroid(ID = "content", Priority = 1)]
        [FindsBySelendroid(ClassName = "android.widget.FrameLayout", Priority = 2)]
        [FindsBySelendroid(LinkText = "Press to throw unhandled exception", Priority = 3)]

        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/content\")", Priority = 1)]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/list\")", Priority = 2)]
        [FindsByAndroidUIAutomator(ClassName = "android.widget.TextView", Priority = 3)]
        private IWebElement foundByChainedSearchElement;

        [FindsBySequence]
        [FindsBySelendroid(ID = "content", Priority = 1)]
        [FindsBySelendroid(ClassName = "android.widget.FrameLayout", Priority = 2)]
        [FindsBySelendroid(LinkText = "Press to throw unhandled exception", Priority = 3)]

        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/content\")", Priority = 1)]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/list\")", Priority = 2)]
        [FindsByAndroidUIAutomator(ClassName = "android.widget.TextView", Priority = 3)]
        private IList<IWebElement> foundByChainedSearchElements;

        /////////////////////////////////////////////////////////////////
        private object foundByChainedSearchElementProperty;
        private object foundByChainedSearchElementsProperty;
        /////////////////////////////////////////////////////////////////

        [FindsBySequence]
        [FindsBySelendroid(ID = "content", Priority = 1)]
        [FindsBySelendroid(ClassName = "android.widget.FrameLayout", Priority = 2)]
        [FindsBySelendroid(PartialLinkText = "Press to throw unhandled exception", Priority = 3)]

        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/content\")", Priority = 1)]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/list\")", Priority = 2)]
        [FindsByAndroidUIAutomator(ClassName = "android.widget.TextView", Priority = 3)]
        private IWebElement TestFoundByChainedSearchElementProperty
        {
            set
            {
                foundByChainedSearchElementProperty = value;
            }
            get
            {
                return (IWebElement)foundByChainedSearchElementProperty;
            }
        }

        [FindsBySequence]
        [FindsBySelendroid(ID = "content", Priority = 1)]
        [FindsBySelendroid(ClassName = "android.widget.FrameLayout", Priority = 2)]
        [FindsBySelendroid(PartialLinkText = "Press to throw unhandled exception", Priority = 3)]

        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/content\")", Priority = 1)]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/list\")", Priority = 2)]
        [FindsByAndroidUIAutomator(ClassName = "android.widget.TextView", Priority = 3)]
        private IList<IWebElement> TestFoundByChainedSearchElementsProperty
        {
            set
            {
                foundByChainedSearchElementsProperty = value;
            }
            get
            {
                return (IList<IWebElement>)foundByChainedSearchElementsProperty;
            }
        }

        [FindsByAll]
        [FindsBySelendroid(LinkText = "Press to throw unhandled exception", Priority = 1)]
        //[FindsByAndroidUIAutomator(ID = "waitingButtonTest", Priority = 2)]
        //Equals method of RemoteWebElement is not consistent for mobile apps
        //The second selector will be commented till the problem is worked out
        private IMobileElement<IWebElement> matchedToAllLocatorsElement;

        [FindsByAll]
        [FindsBySelendroid(LinkText = "Press to throw unhandled exception", Priority = 1)]
        //[FindsByAndroidUIAutomator(ID = "waitingButtonTest", Priority = 2)]
        //Equals method of RemoteWebElement is not consistent for mobile apps
        //The second selector will be commented till the problem is worked out
        private IList<IWebElement> matchedToAllLocatorsElements;

        /////////////////////////////////////////////////////////////////
        private object matchedToAllLocatorsElementProperty;
        private object matchedToAllLocatorsElementsProperty;
        /////////////////////////////////////////////////////////////////

        [FindsByAll]
        [FindsBySelendroid(LinkText = "Press to throw unhandled exception", Priority = 1)]
        //[FindsByAndroidUIAutomator(ID = "waitingButtonTest", Priority = 2)]
        //Equals method of RemoteWebElement is not consistent for mobile apps
        //The second selector will be commented till the problem is worked out
        private IWebElement TestMatchedToAllLocatorsElementProperty
        {
            set
            {
                matchedToAllLocatorsElementProperty = value;
            }
            get
            {
                return (IWebElement) matchedToAllLocatorsElementProperty;
            }
        }

        [FindsByAll]
        [FindsBySelendroid(LinkText = "Press to throw unhandled exception", Priority = 1)]
        //[FindsByAndroidUIAutomator(ID = "waitingButtonTest", Priority = 2)]
        //Equals method of RemoteWebElement is not consistent for mobile apps
        //The second selector will be commented till the problem is worked out
        private IList<IWebElement> TestMatchedToAllLocatorsElementsProperty
        {
            set
            {
                matchedToAllLocatorsElementsProperty = value;
            }
            get
            {
                return (IList<IWebElement>)matchedToAllLocatorsElementsProperty;
            }
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
