﻿using System.Collections.Generic;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Appium;

namespace Appium.Net.Integration.Tests.PageObjects
{
    public class IosPageObjectChecksAttributesForNativeIosApp
    {
        /////////////////////////////////////////////////////////////////

        [FindsByIOSUIAutomation(IosUIAutomation = ".elements()[0]")]
        private IMobileElement _testMobileElement;

        [FindsByIOSUIAutomation(IosUIAutomation = ".elements()[0]")] private IList<AppiumWebElement> _testMobileElements;

        [FindsByIOSUIAutomation(IosUIAutomation = ".elements()[0]")]
        private IMobileElement TestMobileElement { set; get; }

        [FindsByIOSUIAutomation(IosUIAutomation = ".elements()[0]")]
        private IList<AppiumWebElement> TestMobileElements { set; get; }

        [FindsByIOSUIAutomation(ID = "FakeID", Priority = 1)]
        [FindsByIOSUIAutomation(ClassName = "UIAUAIFakeClass", Priority = 2)]
        [FindsByIOSUIAutomation(ClassName = "UIAButton", Priority = 3)]
        private IMobileElement _testMultipleElement;

        [FindsByIOSUIAutomation(ID = "FakeID", Priority = 1)]
        [FindsByIOSUIAutomation(ClassName = "UIAUAIFakeClass", Priority = 2)]
        [FindsByIOSUIAutomation(ClassName = "UIAButton", Priority = 3)] private IList<AppiumWebElement> _testMultipleElements;


        /////////////////////////////////////////////////////////////////

        [FindsByIOSUIAutomation(ID = "FakeID", Priority = 1)]
        [FindsByIOSUIAutomation(ClassName = "UIAUAIFakeClass", Priority = 2)]
        [FindsByIOSUIAutomation(ClassName = "UIAButton", Priority = 3)]
        private IMobileElement TestMultipleFindByElementProperty { set; get; }

        [FindsByIOSUIAutomation(ID = "FakeID", Priority = 1)]
        [FindsByIOSUIAutomation(ClassName = "UIAUAIFakeClass", Priority = 2)]
        [FindsByIOSUIAutomation(ClassName = "UIAButton", Priority = 3)]
        private IList<AppiumWebElement> MultipleFindByElementsProperty { set; get; }

        [MobileFindsByAll(IOS = true)] [FindsByIOSUIAutomation(ClassName = "UIAButton", Priority = 1)]
        //Equals method of WebElement is not consistent for mobile apps
        //The second selector won't be added till the problem is worked out
        private IMobileElement _matchedToAllLocatorsElement;

        [MobileFindsByAll(IOS = true)] [FindsByIOSUIAutomation(ClassName = "UIAButton", Priority = 1)]
        //Equals method of WebElement is not consistent for mobile apps
        //The second selector won't be added till the problem is worked out
        private IList<AppiumWebElement> _matchedToAllLocatorsElements;

        /////////////////////////////////////////////////////////////////

        [MobileFindsByAll(IOS = true)]
        [FindsByIOSUIAutomation(ClassName = "UIAButton", Priority = 1)]
        //Equals method of WebElement is not consistent for mobile apps
        //The second selector won't be added till the problem is worked out
        private IMobileElement TestMatchedToAllLocatorsElementProperty { set; get; }

        [MobileFindsByAll(IOS = true)]
        [FindsByIOSUIAutomation(ClassName = "UIAButton", Priority = 1)]
        //Equals method of WebElement is not consistent for mobile apps
        //The second selector won't be added till the problem is worked out
        private IList<AppiumWebElement> TestMatchedToAllLocatorsElementsProperty { set; get; }

        //////////////////////////////////////////////////////////////////////////
        public string GetMobileElementText()
        {
            return _testMobileElement.Text;
        }

        public int GetMobileElementSize()
        {
            return _testMobileElements.Count;
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
            return _testMultipleElement.Text;
        }

        public int GetMultipleFindByElementSize()
        {
            return _testMultipleElements.Count;
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
        public string GetMatchedToAllLocatorsElementText()
        {
            return _matchedToAllLocatorsElement.Text;
        }

        public int GetMatchedToAllLocatorsElementSize()
        {
            return _matchedToAllLocatorsElements.Count;
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