using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android
{
    public class ElementTestEspresso
    {
        private AndroidDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetAndroidEspressoCaps(Apps.Get("androidApiDemos"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
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

            Assert.Multiple(() =>
            {
                Assert.That(
                            Is.Not.EqualTo(_driver.FindElement(MobileBy.Id("android:id/list")).FindElement(byAndroidDataMatcher).Text), null);
                Assert.That(
                    _driver.FindElement(MobileBy.Id("android:id/list")).FindElements(byAndroidDataMatcher), Is.Not.Empty);
            });
        }

        [Test]
        public void FindByAndroidViewMatcherTest()
        {
            const string selectorData = @"{
                'name':'withText',
                'args':[{
                    'name':'containsString',
                    'args':['Preference']
                    }
                }]";

            By byAndroidViewMatcher = new ByAndroidViewMatcher(selectorData);

            Assert.Multiple(() =>
            {
                Assert.That(
                            Is.Not.EqualTo(_driver.FindElement(MobileBy.Id("android:id/list")).FindElement(byAndroidViewMatcher).Text), null);
                Assert.That(
                    _driver.FindElement(MobileBy.Id("android:id/list")).FindElements(byAndroidViewMatcher), Is.Not.Empty);
            });
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