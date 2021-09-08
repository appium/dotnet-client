﻿using System.Collections.Generic;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using SeleniumExtras.PageObjects;

namespace Appium.Net.Integration.Tests.PageObjects
{
    public class AndroidPageObjectChecksAttributeMixOnNativeApp1
    {
        /////////////////////////////////////////////////////////////////

        [FindsBy(How = How.Id, Using = "FakeHTMLid")]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/text1\")")]
        private IMobileElement<AndroidElement> _testMobileElement;

        [FindsBy(How = How.Id, Using = "FakeHTMLid")]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/text1\")")]
        private IList<AndroidElement> _testMobileElements;

        [FindsBy(How = How.Id, Using = "FakeHTMLid")]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/text1\")")]
        private IMobileElement<AndroidElement> TestMobileElement { set; get; }

        [FindsBy(How = How.Id, Using = "FakeHTMLid")]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/text1\")")]
        private IList<AndroidElement> TestMobileElements { set; get; }

        [FindsBy(How = How.Id, Using = "FakeHTMLid", Priority = 1)]
        [FindsBy(How = How.ClassName, Using = "FakeHTMLClass", Priority = 2)]
        [FindsBy(How = How.XPath, Using = ".//fakeTag", Priority = 3)]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/fakeId\")",
            Priority = 1)] [FindsByAndroidUIAutomator(ID = "FakeId", Priority = 2)]
        [FindsByAndroidUIAutomator(ClassName = "android.widget.TextView", Priority = 3)]
        private IMobileElement<AndroidElement> _testMultipleElement;

        [FindsBy(How = How.Id, Using = "FakeHTMLid", Priority = 1)]
        [FindsBy(How = How.ClassName, Using = "FakeHTMLClass", Priority = 2)]
        [FindsBy(How = How.XPath, Using = ".//fakeTag", Priority = 3)]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/fakeId\")",
            Priority = 1)] [FindsByAndroidUIAutomator(ID = "FakeId", Priority = 2)]
        [FindsByAndroidUIAutomator(ClassName = "android.widget.TextView", Priority = 3)]
        private IList<AndroidElement> _testMultipleElements;


        /////////////////////////////////////////////////////////////////

        [FindsBy(How = How.Id, Using = "FakeHTMLid", Priority = 1)]
        [FindsBy(How = How.ClassName, Using = "FakeHTMLClass", Priority = 2)]
        [FindsBy(How = How.XPath, Using = ".//fakeTag", Priority = 3)]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/fakeId\")",
            Priority = 1)]
        [FindsByAndroidUIAutomator(ID = "FakeId", Priority = 2)]
        [FindsByAndroidUIAutomator(ClassName = "android.widget.TextView", Priority = 3)]
        private IMobileElement<AndroidElement> TestMultipleFindByElementProperty { set; get; }

        [FindsBy(How = How.Id, Using = "FakeHTMLid", Priority = 1)]
        [FindsBy(How = How.ClassName, Using = "FakeHTMLClass", Priority = 2)]
        [FindsBy(How = How.XPath, Using = ".//fakeTag", Priority = 3)]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/fakeId\")",
            Priority = 1)]
        [FindsByAndroidUIAutomator(ID = "FakeId", Priority = 2)]
        [FindsByAndroidUIAutomator(ClassName = "android.widget.TextView", Priority = 3)]
        private IList<AndroidElement> MultipleFindByElementsProperty { set; get; }

        [FindsBySequence] [MobileFindsBySequence(Android = true)]
        [FindsBy(How = How.Id, Using = "FakeHTMLid", Priority = 1)]
        [FindsBy(How = How.ClassName, Using = "FakeHTMLClass", Priority = 2)]
        [FindsBy(How = How.XPath, Using = ".//fakeTag", Priority = 3)]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/content\")",
            Priority = 1)]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/list\")",
            Priority = 2)] [FindsByAndroidUIAutomator(ClassName = "android.widget.TextView", Priority = 3)]
        private IMobileElement<AndroidElement> _foundByChainedSearchElement;

        [FindsBySequence] [MobileFindsBySequence(Android = true)]
        [FindsBy(How = How.Id, Using = "FakeHTMLid", Priority = 1)]
        [FindsBy(How = How.ClassName, Using = "FakeHTMLClass", Priority = 2)]
        [FindsBy(How = How.XPath, Using = ".//fakeTag", Priority = 3)]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/content\")",
            Priority = 1)]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/list\")",
            Priority = 2)] [FindsByAndroidUIAutomator(ClassName = "android.widget.TextView", Priority = 3)]
        private IList<AndroidElement> _foundByChainedSearchElements;

        /////////////////////////////////////////////////////////////////

        [FindsBySequence]
        [MobileFindsBySequence(Android = true)]
        [FindsBy(How = How.Id, Using = "FakeHTMLid", Priority = 1)]
        [FindsBy(How = How.ClassName, Using = "FakeHTMLClass", Priority = 2)]
        [FindsBy(How = How.XPath, Using = ".//fakeTag", Priority = 3)]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/content\")",
            Priority = 1)]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/list\")",
            Priority = 2)]
        [FindsByAndroidUIAutomator(ClassName = "android.widget.TextView", Priority = 3)]
        private IMobileElement<AndroidElement> TestFoundByChainedSearchElementProperty { set; get; }

        [FindsBySequence]
        [MobileFindsBySequence(Android = true)]
        [FindsBy(How = How.Id, Using = "FakeHTMLid", Priority = 1)]
        [FindsBy(How = How.ClassName, Using = "FakeHTMLClass", Priority = 2)]
        [FindsBy(How = How.XPath, Using = ".//fakeTag", Priority = 3)]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/content\")",
            Priority = 1)]
        [FindsByAndroidUIAutomator(AndroidUIAutomator = "new UiSelector().resourceId(\"android:id/list\")",
            Priority = 2)]
        [FindsByAndroidUIAutomator(ClassName = "android.widget.TextView", Priority = 3)]
        private IList<AndroidElement> TestFoundByChainedSearchElementsProperty { set; get; }

        [FindsByAll] [MobileFindsByAll(Android = true)] [FindsBy(How = How.Id, Using = "FakeHTMLid", Priority = 1)]
        [FindsBy(How = How.ClassName, Using = "FakeHTMLClass", Priority = 2)]
        [FindsBy(How = How.XPath, Using = ".//fakeTag", Priority = 3)]
        [FindsByAndroidUIAutomator(ID = "android:id/text1", Priority = 1)]
        //[FindsByAndroidUIAutomator(ClassName = "android.widget.TextView", Priority = 2)]
        //Equals method of WebElement is not consistent for mobile apps
        //The second selector will be commented till the problem is worked out
        private IMobileElement<AndroidElement> _matchedToAllLocatorsElement;

        [FindsByAll] [MobileFindsByAll(Android = true)] [FindsBy(How = How.Id, Using = "FakeHTMLid", Priority = 1)]
        [FindsBy(How = How.ClassName, Using = "FakeHTMLClass", Priority = 2)]
        [FindsBy(How = How.XPath, Using = ".//fakeTag", Priority = 3)]
        [FindsByAndroidUIAutomator(ID = "android:id/text1", Priority = 1)]
        //[FindsByAndroidUIAutomator(ClassName = "android.widget.TextView", Priority = 2)]
        //Equals method of WebElement is not consistent for mobile apps
        //The second selector will be commented till the problem is worked out
        private IList<AndroidElement> _matchedToAllLocatorsElements;

        /////////////////////////////////////////////////////////////////
        [FindsByAll]
        [MobileFindsByAll(Android = true)]
        [FindsBy(How = How.Id, Using = "FakeHTMLid", Priority = 1)]
        [FindsBy(How = How.ClassName, Using = "FakeHTMLClass", Priority = 2)]
        [FindsBy(How = How.XPath, Using = ".//fakeTag", Priority = 3)]
        [FindsByAndroidUIAutomator(ID = "android:id/text1", Priority = 1)]
        //[FindsByAndroidUIAutomator(ClassName = "android.widget.TextView", Priority = 2)]
        //Equals method of WebElement is not consistent for mobile apps
        //The second selector will be commented till the problem is worked out
        private IMobileElement<AndroidElement> TestMatchedToAllLocatorsElementProperty { set; get; }

        [FindsByAll]
        [MobileFindsByAll(Android = true)]
        [FindsBy(How = How.Id, Using = "FakeHTMLid", Priority = 1)]
        [FindsBy(How = How.ClassName, Using = "FakeHTMLClass", Priority = 2)]
        [FindsBy(How = How.XPath, Using = ".//fakeTag", Priority = 3)]
        [FindsByAndroidUIAutomator(ID = "android:id/text1", Priority = 1)]
        //[FindsByAndroidUIAutomator(ClassName = "android.widget.TextView", Priority = 2)]
        //Equals method of WebElement is not consistent for mobile apps
        //The second selector will be commented till the problem is worked out
        private IList<AndroidElement> TestMatchedToAllLocatorsElementsProperty { set; get; }

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
        public string GetFoundByChainedSearchElementText()
        {
            return _foundByChainedSearchElement.Text;
        }

        public int GetFoundByChainedSearchElementSize()
        {
            return _foundByChainedSearchElements.Count;
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