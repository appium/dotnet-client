using System;
using System.Collections.Generic;
using System.Threading;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.MultiTouch;

namespace Appium.Net.Integration.Tests.Android
{
    [Obsolete("Touch Actions are deprecated")]
    //TODO: remove this test once we deprecate touch actions
    [TestFixture]
    public class TouchActionTest
    {
        private AndroidDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
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
            IList<AppiumElement> els = _driver.FindElements(MobileBy.ClassName("android.widget.TextView"));

            var number1 = els.Count;

            var tap = new TouchAction(_driver);
            tap.Tap(els[4], 8, 5, 2);
            tap.Perform();

            els = _driver.FindElements(MobileBy.ClassName("android.widget.TextView"));

            Assert.That(els.Count, Is.Not.EqualTo(number1));
        }

        [Test]
        public void ComplexTouchActionTestCase()
        {
            AppiumElement ViewsElem = _driver.FindElement(MobileBy.AccessibilityId("Views"));
            var tap = new TouchAction(_driver);
            tap.Tap(ViewsElem).Wait(200);
            tap.Perform();
            IList<AppiumElement> els = _driver.FindElements(MobileBy.ClassName("android.widget.TextView"));
            var loc1 = els[7].Location;
            var target = els[1];
            var loc2 = target.Location;           
            var touchAction = new TouchAction(_driver);
            touchAction.Press(loc1.X, loc1.Y).Wait(800)
                .MoveTo(loc2.X, loc2.Y).Release().Perform();
            Assert.That(target.Location.Y, Is.Not.EqualTo(loc2.Y));
        }

        [Test]
        public void SingleMultiActionTestCase()
        {
            AppiumElement ViewsElem = _driver.FindElement(MobileBy.AccessibilityId("Views"));
            var tap = new TouchAction(_driver);
            tap.Tap(ViewsElem).Wait(200);
            tap.Perform();
            IList<AppiumElement> els = _driver.FindElements(MobileBy.ClassName("android.widget.TextView"));
            var loc1 = els[8].Location;
            var target = els[1];
            var loc2 = target.Location;

            var swipe = new TouchAction(_driver);

            swipe.Press(loc1.X, loc1.Y).Wait(1000)
                .MoveTo(loc2.X, loc2.Y).Release();

            var multiAction = new MultiAction(_driver);
            multiAction.Add(swipe).Perform();
            Assert.That(target.Location.Y, Is.Not.EqualTo(loc2.Y));
        }

        [Test]
        public void SequentalMultiActionTestCase()
        {
            var originalActivity = _driver.CurrentActivity;
            IList<AppiumElement> els = _driver.FindElements(MobileBy.ClassName("android.widget.TextView"));
            var multiTouch = new MultiAction(_driver);

            var tap1 = new TouchAction(_driver);
            tap1.Press(els[6]).Wait(1500).Release();

            multiTouch.Add(tap1).Add(tap1).Perform();

            Thread.Sleep(2500);
            els = _driver.FindElements(MobileBy.ClassName("android.widget.TextView"));

            var tap2 = new TouchAction(_driver);
            tap2.Press(els[1]).Wait(1500).Release();

            var multiTouch2 = new MultiAction(_driver);
            multiTouch2.Add(tap2).Add(tap2).Perform();

            Thread.Sleep(2500);
            Assert.That(_driver.CurrentActivity, Is.Not.EqualTo(originalActivity));
        }
    }
}