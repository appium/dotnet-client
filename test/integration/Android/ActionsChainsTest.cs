//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//See the NOTICE file distributed with this work for additional
//information regarding copyright ownership.
//You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using System;
using System.Drawing;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interactions;
// Define an alias to OpenQA.Selenium.Appium.Interactions.PointerInputDevice to hide
// inherited OpenQA.Selenium.Interactions.PointerInputDevice that causes ambiguity.
// In the future, all functions of OpenQA.Selenium.Appium.Interactions should be moved
// up to OpenQA.Selenium.Interactions and this alias can simply be removed.
using PointerInputDevice = OpenQA.Selenium.Appium.Interactions.PointerInputDevice;

namespace Appium.Net.Integration.Tests.Android
{
    [TestFixture]
    [Category("Drawing")]
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

        private IList<AppiumElement> WaitForTextViewElements(int minimumCount)
        {
            var previousImplicitWait = _driver.Manage().Timeouts().ImplicitWait;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, previousImplicitWait.TotalSeconds > 0 ? previousImplicitWait : TimeSpan.FromSeconds(10));
                return wait.Until(d =>
                {
                    var els = ((AndroidDriver)d).FindElements(MobileBy.ClassName("android.widget.TextView"));
                    return els.Count >= minimumCount ? els : null;
                });
            }
            finally
            {
                _driver.Manage().Timeouts().ImplicitWait = previousImplicitWait;
            }
        }

        // ActivateApp can return while the ApiDemos list is still being redrawn, so elements found right
        // after it may go stale before the actions run. Find them again and retry when that happens.
        private int PerformOnTextViews(int minimumCount, Func<IList<AppiumElement>, IList<ActionSequence>> buildActions)
        {
            const int maxAttempts = 3;
            for (var attempt = 1; ; attempt++)
            {
                var els = WaitForTextViewElements(minimumCount);
                try
                {
                    _driver.PerformActions(buildActions(els));
                    return els.Count;
                }
                catch (StaleElementReferenceException) when (attempt < maxAttempts)
                {
                }
            }
        }

        private IList<AppiumElement> WaitForTextViewCountToChange(int previousCount)
        {
            var previousImplicitWait = _driver.Manage().Timeouts().ImplicitWait;
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.Zero;
            try
            {
                WebDriverWait wait = new WebDriverWait(_driver, previousImplicitWait.TotalSeconds > 0 ? previousImplicitWait : TimeSpan.FromSeconds(10));
                return wait.Until(d =>
                {
                    var currentEls = ((AndroidDriver)d).FindElements(MobileBy.ClassName("android.widget.TextView"));
                    return currentEls.Count != previousCount ? currentEls : null;
                });
            }
            finally
            {
                _driver.Manage().Timeouts().ImplicitWait = previousImplicitWait;
            }
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
            var number1 = PerformOnTextViews(3, els =>
            {
                var elementToTouch = els[2];

                var touch = new PointerInputDevice(PointerKind.Touch, "finger");
                var sequence = new ActionSequence(touch);

                var move = touch.CreatePointerMove(elementToTouch, 0, 0, TimeSpan.FromSeconds(1));
                var actionPress = touch.CreatePointerDown(PointerButton.TouchContact);
                var pause = touch.CreatePause(TimeSpan.FromMilliseconds(250));
                var actionRelease = touch.CreatePointerUp(PointerButton.TouchContact);

                sequence.AddAction(move);
                sequence.AddAction(actionPress);
                sequence.AddAction(pause);
                sequence.AddAction(actionRelease);

                return new List<ActionSequence>
                {
                    sequence
                };
            });

            var els = WaitForTextViewCountToChange(number1);

            Assert.That(els, Has.Count.Not.EqualTo(number1));
        }

        [Test]
        public void TouchByCoordinatesTestCase()
        {
            var number1 = PerformOnTextViews(3, els =>
            {
                var elementToTouch = els[2];

                var touch = new PointerInputDevice(PointerKind.Touch, "finger");
                var sequence = new ActionSequence(touch);

                Point point = new()
                {
                    X = (elementToTouch.Rect.X+elementToTouch.Rect.Width)/2,
                    Y = elementToTouch.Rect.Y
                };

                Interaction move = touch.CreatePointerMove(CoordinateOrigin.Viewport, point.X, point.Y, TimeSpan.Zero);
                Interaction actionPress = touch.CreatePointerDown(PointerButton.TouchContact);
                Interaction actionRelease = touch.CreatePointerUp(PointerButton.TouchContact);

                sequence.AddAction(move);
                sequence.AddAction(actionPress);
                sequence.AddAction(actionRelease);

                return new List<ActionSequence>
                {
                    sequence
                };
            });

            var els = WaitForTextViewCountToChange(number1);

            Assert.That(els, Has.Count.Not.EqualTo(number1));
        }

        [Test]
        public void ScrollActionTestCase()
        {
            if (Env.IsCiEnvironment())
            {
                Assert.Ignore("Skipping ScrollActionTestCase test in CI environment");
            }
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

            IList<AppiumElement> els = WaitForTextViewElements(8);
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

            using (Assert.EnterMultipleScope())
            {
                Assert.That(origin.Location.Y, Is.Not.EqualTo(loc1.Y));
                Assert.That(target.Location.Y, Is.Not.EqualTo(loc2.Y));
            }

        }

        [Test]
        public void ScrollUsingAddActionsTestCase()
        {
            if (Env.IsCiEnvironment())
            {
                Assert.Ignore("Skipping ScrollUsingAddActionsTestCase test in CI environment");
            }
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

            IList<AppiumElement> els = WaitForTextViewElements(8);
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

            using (Assert.EnterMultipleScope())
            {
                Assert.That(origin.Location.Y, Is.Not.EqualTo(loc1.Y));
                Assert.That(target.Location.Y, Is.Not.EqualTo(loc2.Y));
            }

        }
    }
}
