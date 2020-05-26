using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android
{
    public class ElementTestEspresso
    {
        private AndroidDriver<AndroidElement> _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetAndroidEspressoCaps(Apps.Get("androidApiDemos"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver<AndroidElement>(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [SetUp]
        public void SetUp()
        {
            _driver.StartActivity("io.appium.android.apis", ".ApiDemos");
        }

        [Test]
        public void FindByAndroidDataMatcherTest()
        {
            const string selectorData = @"{
                'name':'hasEntry',
                'args':['title','Graphics']
                }";

            By byAndroidDataMatcher = new ByAndroidDataMatcher(selectorData);

            Assert.AreNotEqual(
                _driver.FindElementById("android:id/list").FindElement(byAndroidDataMatcher).Text,
                null);
            Assert.GreaterOrEqual(
                _driver.FindElementById("android:id/list").FindElements(byAndroidDataMatcher).Count,
                1);
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            _driver?.Quit();
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }
    }
}