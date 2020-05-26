using System.Collections.Generic;
using System.Threading;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.MultiTouch;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    public class TouchActionTest
    {
        private AndroidDriver<AppiumWebElement> _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver<AppiumWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
            _driver.CloseApp();
        }

        [SetUp]
        public void SetUp()
        {
            _driver?.LaunchApp();
        }

        [TearDown]
        public void TearDowwn()
        {
            _driver?.CloseApp();
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

        [Test]
        public void SimpleTouchActionTestCase()
        {
            IList<AppiumWebElement> els = _driver.FindElementsByClassName("android.widget.TextView");

            var number1 = els.Count;

            var tap = new TouchAction(_driver);
            tap.Tap(els[4], 10, 5, 2).Perform();

            els = _driver.FindElementsByClassName("android.widget.TextView");

            Assert.AreNotEqual(number1, els.Count);
        }

        [Test]
        public void ComplexTouchActionTestCase()
        {
            IList<AppiumWebElement> els = _driver.FindElementsByClassName("android.widget.TextView");
            var loc1 = els[7].Location;
            var target = els[1];
            var loc2 = target.Location;           
            var touchAction = new TouchAction(_driver);
            touchAction.Press(loc1.X, loc1.Y).Wait(800)
                .MoveTo(loc2.X, loc2.Y).Release().Perform();
            Assert.AreNotEqual(loc2.Y, target.Location.Y);
        }

        [Test]
        public void SingleMultiActionTestCase()
        {
            IList<AppiumWebElement> els = _driver.FindElementsByClassName("android.widget.TextView");
            var loc1 = els[7].Location;
            var target = els[1];
            var loc2 = target.Location;

            var swipe = new TouchAction(_driver);

            swipe.Press(loc1.X, loc1.Y).Wait(1000)
                .MoveTo(loc2.X, loc2.Y).Release();

            var multiAction = new MultiAction(_driver);
            multiAction.Add(swipe).Perform();
            Assert.AreNotEqual(loc2.Y, target.Location.Y);
        }

        [Test]
        public void SequentalMultiActionTestCase()
        {
            var originalActivity = _driver.CurrentActivity;
            IList<AppiumWebElement> els = _driver.FindElementsByClassName("android.widget.TextView");
            var multiTouch = new MultiAction(_driver);

            var tap1 = new TouchAction(_driver);
            tap1.Press(els[5]).Wait(1500).Release();

            multiTouch.Add(tap1).Add(tap1).Perform();

            Thread.Sleep(2500);
            els = _driver.FindElementsByClassName("android.widget.TextView");

            var tap2 = new TouchAction(_driver);
            tap2.Press(els[1]).Wait(1500).Release();

            var multiTouch2 = new MultiAction(_driver);
            multiTouch2.Add(tap2).Add(tap2).Perform();

            Thread.Sleep(2500);
            Assert.AreNotEqual(originalActivity, _driver.CurrentActivity);
        }
    }
}