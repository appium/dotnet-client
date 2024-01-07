using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using System;
using System.Drawing;
using OpenQA.Selenium.Appium.Interactions;
// Define an alias to OpenQA.Selenium.Appium.Interactions.PointerInputDevice to hide
// inherited OpenQA.Selenium.Interactions.PointerInputDevice that causes ambiguity.
// In the future, all functions of OpenQA.Selenium.Appium.Interactions should be moved
// up to OpenQA.Selenium.Interactions and this alias can simply be removed.
using PointerInputDevice = OpenQA.Selenium.Appium.Interactions.PointerInputDevice;
using System.Threading;

namespace Appium.Net.Integration.Tests.Android
{
    internal class ActionsChainsTest
    {
        private AndroidDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get(Apps.androidApiDemos))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get(Apps.androidApiDemos));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [SetUp]
        public void SetUp()
        {
            _driver?.ActivateApp(Apps.GetId(Apps.androidApiDemos));
        }

        [TearDown]
        public void TearDowwn()
        {
            _ = _driver?.TerminateApp(Apps.GetId(Apps.androidApiDemos));
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
            var elementToTouch = els[2];

            var touch = new PointerInputDevice(PointerKind.Touch, "finger");
            var sequence = new ActionSequence(touch);

            var move = touch.CreatePointerMove(elementToTouch, elementToTouch.Location.X, elementToTouch.Location.Y,TimeSpan.FromSeconds(1));
            var actionPress = touch.CreatePointerDown(PointerButton.TouchContact);
            var pause = touch.CreatePause(TimeSpan.FromMilliseconds(250));
            var actionRelease = touch.CreatePointerUp(PointerButton.TouchContact);

            sequence.AddAction(move);
            sequence.AddAction(actionPress);
            sequence.AddAction(pause);
            sequence.AddAction(actionRelease);

            var actions_seq = new List<ActionSequence>
            {
                sequence
            };

            _driver.PerformActions(actions_seq);

            els = _driver.FindElements(MobileBy.ClassName("android.widget.TextView"));

            Assert.That(els, Has.Count.Not.EqualTo(number1));
        }

        [Test]
        public void TouchByCoordinatesTestCase()
        {
            IList<AppiumElement> els = _driver.FindElements(MobileBy.ClassName("android.widget.TextView"));
            var number1 = els.Count;

            var touch = new PointerInputDevice(PointerKind.Touch, "finger");
            var sequence = new ActionSequence(touch);

            Point point = new()
            {
                X = 148,
                Y = 400
            };

            Interaction move = touch.CreatePointerMove(CoordinateOrigin.Viewport, point.X, point.Y, TimeSpan.Zero);
            Interaction actionPress = touch.CreatePointerDown(PointerButton.TouchContact);
            Interaction actionRelease = touch.CreatePointerUp(PointerButton.TouchContact);

            sequence.AddAction(move);
            sequence.AddAction(actionPress);
            sequence.AddAction(actionRelease);

            var actions_seq = new List<ActionSequence>
            {
                sequence
            };

            _driver.PerformActions(actions_seq);
            Thread.Sleep(1000);
            els = _driver.FindElements(MobileBy.ClassName("android.widget.TextView"));

            Assert.That(els, Has.Count.Not.EqualTo(number1));
        }

        [Test]
        public void ScrollActionTestCase()
        {
            AppiumElement ViewsElem = _driver.FindElement(MobileBy.AccessibilityId("Views"));

            ActionBuilder actionBuilder = new ActionBuilder();

            var touch = new PointerInputDevice(PointerKind.Touch, "finger");

            var moveAction = touch.CreatePointerMove(ViewsElem, 0, 0, TimeSpan.FromMilliseconds(0));
            actionBuilder.AddAction(moveAction);
            var tapAction = touch.CreatePointerDown(PointerButton.TouchContact);
            actionBuilder.AddAction(tapAction);
            var tapActionPause= touch.CreatePause(TimeSpan.FromMilliseconds(200));
            actionBuilder.AddAction(tapActionPause);
            var tapActionUp = touch.CreatePointerUp(PointerButton.TouchContact);
            actionBuilder.AddAction(tapActionUp);

            var sequence = actionBuilder.ToActionSequenceList();

            _driver.PerformActions(sequence);

            IList<AppiumElement> els = _driver.FindElements(MobileBy.ClassName("android.widget.TextView"));
            var origin = els[7];
            var loc1 = origin.Location;
            var target = els[1];
            var loc2 = target.Location;

            actionBuilder.ClearSequences();

            actionBuilder.AddAction(touch.CreatePointerMove(origin, 0,0, TimeSpan.FromMilliseconds(800)));
            actionBuilder.AddAction(touch.CreatePointerDown(PointerButton.TouchContact));
            actionBuilder.AddAction(touch.CreatePause(TimeSpan.FromMilliseconds(800)));
            actionBuilder.AddAction(touch.CreatePointerMove(target, 0, 0, TimeSpan.FromMilliseconds(800)));
            actionBuilder.AddAction(touch.CreatePointerUp(PointerButton.TouchContact));

            var sequenceActions = actionBuilder.ToActionSequenceList();

            _driver.PerformActions(sequenceActions);

            Assert.Multiple(() =>
            {
                Assert.That(origin.Location.Y, Is.Not.EqualTo(loc1.Y));
                Assert.That(target.Location.Y, Is.Not.EqualTo(loc2.Y));
            });

        }

        [Test]
        public void ScrollUsingAddActionsTestCase()
        {
            AppiumElement ViewsElem = _driver.FindElement(MobileBy.AccessibilityId("Views"));

            ActionBuilder actionBuilder = new ActionBuilder();

            var touch = new PointerInputDevice(PointerKind.Touch, "finger");

            var moveAction = touch.CreatePointerMove(ViewsElem, 0, 0, TimeSpan.FromMilliseconds(0));
            var tapAction = touch.CreatePointerDown(PointerButton.TouchContact);
            var tapActionPause = touch.CreatePause(TimeSpan.FromMilliseconds(400));
            var tapActionUp = touch.CreatePointerUp(PointerButton.TouchContact);

            Interaction[] Tapinteractions = new Interaction[]
            {
                moveAction,
                tapAction,
                tapActionPause,
                tapActionUp
            };

            actionBuilder.AddActions(Tapinteractions);

            var sequence = actionBuilder.ToActionSequenceList();
            _driver.PerformActions(sequence);

            IList<AppiumElement> els = _driver.FindElements(MobileBy.ClassName("android.widget.TextView"));
            var origin = els[7];
            var loc1 = origin.Location;
            var target = els[1];
            var loc2 = target.Location;

            actionBuilder.ClearSequences();;

            Interaction[] interactions = new Interaction[]
            {
                touch.CreatePointerMove(origin, 0, 0, TimeSpan.FromMilliseconds(800)),
                touch.CreatePointerDown(MouseButton.Touch),
                touch.CreatePause(TimeSpan.FromMilliseconds(800)),
                touch.CreatePointerMove(target, 0, 0, TimeSpan.FromMilliseconds(800)),
                touch.CreatePointerUp(MouseButton.Touch)
            };

            actionBuilder.AddActions(interactions);

            var sequenceActions = actionBuilder.ToActionSequenceList();
            _driver.PerformActions(sequenceActions);

            Assert.Multiple(() =>
            {
                Assert.That(origin.Location.Y, Is.Not.EqualTo(loc1.Y));
                Assert.That(target.Location.Y, Is.Not.EqualTo(loc2.Y));
            });

        }
    }
}
