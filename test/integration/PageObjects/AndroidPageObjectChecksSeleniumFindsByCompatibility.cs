using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using SeleniumExtras.PageObjects;

namespace Appium.Net.Integration.Tests.PageObjects
{
    public class AndroidPageObjectChecksSeleniumFindsByCompatibility
    {
        [FindsBy(How = How.ClassName, Using = "android.widget.TextView")] private IWebElement _testElement;

        /////////////////////////////////////////////////////////////////

        [FindsBy(How = How.ClassName, Using = "android.widget.TextView")]
        private IWebElement TestElement { set; get; }

        [FindsBy(How = How.ClassName, Using = "android.widget.TextView")] private IList<IWebElement> _testElements;

        [FindsBy(How = How.ClassName, Using = "android.widget.TextView")]
        private IList<IWebElement> TestElements { set; get; }

        /////////////////////////////////////////////////////////////////

        [FindsBy(How = How.ClassName, Using = "android.widget.TextView")]
        private IMobileElement<WebElement> _testMobileElement;

        [FindsBy(How = How.ClassName, Using = "android.widget.TextView")]
        private IList<WebElement> _testMobileElements;

        [FindsBy(How = How.ClassName, Using = "android.widget.TextView")]
        private IMobileElement<WebElement> TestMobileElement { set; get; }

        [FindsBy(How = How.ClassName, Using = "android.widget.TextView")]
        private IList<WebElement> TestMobileElements { set; get; }

        [FindsBy(How = How.Name, Using = "FakeName", Priority = 1)]
        [FindsBy(How = How.Id, Using = "FakeId", Priority = 2)]
        [FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 3)]
        private IMobileElement<WebElement> _testMultipleElement;

        [FindsBy(How = How.Name, Using = "FakeName", Priority = 1)]
        [FindsBy(How = How.Id, Using = "FakeId", Priority = 2)]
        [FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 3)]
        private IList<WebElement> _testMultipleElements;


        /////////////////////////////////////////////////////////////////

        [FindsBy(How = How.Name, Using = "FakeName", Priority = 1)]
        [FindsBy(How = How.Id, Using = "FakeId", Priority = 2)]
        [FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 3)]
        private IMobileElement<WebElement> TestMultipleFindByElementProperty { set; get; }

        [FindsBy(How = How.Name, Using = "FakeName", Priority = 1)]
        [FindsBy(How = How.Id, Using = "FakeId", Priority = 2)]
        [FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 3)]
        private IList<WebElement> MultipleFindByElementsProperty { set; get; }

        [FindsBySequence] [FindsBy(How = How.Id, Using = "android:id/content", Priority = 1)]
        [FindsBy(How = How.Id, Using = "android:id/list", Priority = 2)]
        [FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 3)]
        private IMobileElement<WebElement> _foundByChainedSearchElement;

        [FindsBySequence] [FindsBy(How = How.Id, Using = "android:id/content", Priority = 1)]
        [FindsBy(How = How.Id, Using = "android:id/list", Priority = 2)]
        [FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 3)]
        private IList<WebElement> _foundByChainedSearchElements;

        /////////////////////////////////////////////////////////////////

        [FindsBySequence]
        [FindsBy(How = How.Id, Using = "android:id/content", Priority = 1)]
        [FindsBy(How = How.Id, Using = "android:id/list", Priority = 2)]
        [FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 3)]
        private IMobileElement<WebElement> TestFoundByChainedSearchElementProperty { set; get; }

        [FindsBySequence]
        [FindsBy(How = How.Id, Using = "android:id/content", Priority = 1)]
        [FindsBy(How = How.Id, Using = "android:id/list", Priority = 2)]
        [FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 3)]
        private IList<WebElement> TestFoundByChainedSearchElementsProperty { set; get; }

        [FindsByAll] [FindsBy(How = How.Id, Using = "android:id/text1", Priority = 1)]
        //[FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 2)]
        //Equals method of WebElement is not consistent for mobile apps
        //The second selector will be commented till the problem is worked out
        private IMobileElement<WebElement> _matchedToAllLocatorsElement;

        [FindsByAll] [FindsBy(How = How.Id, Using = "android:id/text1", Priority = 1)]
        //[FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 2)]
        //Equals method of WebElement is not consistent for mobile apps
        //The second selector will be commented till the problem is worked out
        private IList<WebElement> _matchedToAllLocatorsElements;

        /////////////////////////////////////////////////////////////////
        [FindsByAll]
        [FindsBy(How = How.Id, Using = "android:id/text1", Priority = 1)]
        //[FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 2)]
        //Equals method of WebElement is not consistent for mobile apps
        //The second selector will be commented till the problem is worked out
        private IMobileElement<WebElement> TestMatchedToAllLocatorsElementProperty { set; get; }

        [FindsByAll]
        [FindsBy(How = How.Id, Using = "android:id/text1", Priority = 1)]
        //[FindsBy(How = How.ClassName, Using = "android.widget.TextView", Priority = 2)]
        //Equals method of WebElement is not consistent for mobile apps
        //The second selector will be commented till the problem is worked out
        private IList<WebElement> TestMatchedToAllLocatorsElementsProperty { set; get; }

        public string GetElementText()
        {
            return _testElement.Text;
        }

        public int GetElementSize()
        {
            return _testElements.Count;
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