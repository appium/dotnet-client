﻿//******************************************************************************
//
// Copyright (c) 2018 Microsoft Corporation. All rights reserved.
//
// This code is licensed under the MIT License (MIT).
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//******************************************************************************

using System;
using System.Drawing;
using System.Threading;
using System.Collections.Generic;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Appium.Interactions;
using OpenQA.Selenium.Appium.Windows;

// Define an alias to OpenQA.Selenium.Appium.Interactions.PointerInputDevice to hide
// inherited OpenQA.Selenium.Interactions.PointerInputDevice that causes ambiguity.
// In the future, all functions of OpenQA.Selenium.Appium.Interactions should be moved
// up to OpenQA.Selenium.Interactions and this alias can simply be removed.
using PointerInputDevice = OpenQA.Selenium.Appium.Interactions.PointerInputDevice;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using Appium.Net.Integration.Tests.helpers;

namespace Appium.Net.Integration.Tests.Windows
{
    public class PenTest : StickyNotesTest
    {
        private WindowsDriver newStickyNoteSession;
        private AppiumElement inkCanvas;

        [Test]
        public void DrawBasicSquare()
        {
            Point canvasCoordinate = inkCanvas.Coordinates.LocationInViewport;
            Size squareSize = new Size(inkCanvas.Size.Width * 3 / 5, inkCanvas.Size.Height * 3 / 5);
            Point A = new Point(canvasCoordinate.X + inkCanvas.Size.Width / 5, canvasCoordinate.Y + inkCanvas.Size.Height / 5);

            // A        B
            //  ┌──────┐   Draw a basic ABCD square using Pen through the Actions API
            //  │      │   in viewport(default) origin mode:
            //  │      │   - X is absolute horizontal position in the session window
            //  └──────┘   - Y is absolute vertical position in the session window
            // D        C
            PointerInputDevice penDevice = new PointerInputDevice(PointerKind.Pen);
            ActionSequence sequence = new ActionSequence(penDevice, 0);

            // Draw line AB from point A to B
            sequence.AddAction(penDevice.CreatePointerMove(CoordinateOrigin.Viewport, A.X, A.Y, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerDown(PointerButton.PenContact));
            sequence.AddAction(penDevice.CreatePointerMove(CoordinateOrigin.Viewport, A.X + squareSize.Width, A.Y, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerUp(PointerButton.PenContact));

            // Draw line BC from point B to C
            sequence.AddAction(penDevice.CreatePointerDown(PointerButton.PenContact));
            sequence.AddAction(penDevice.CreatePointerMove(CoordinateOrigin.Viewport, A.X + squareSize.Width, A.Y + squareSize.Height, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerUp(PointerButton.PenContact));

            // Draw line CD from point C to D
            sequence.AddAction(penDevice.CreatePointerDown(PointerButton.PenContact));
            sequence.AddAction(penDevice.CreatePointerMove(CoordinateOrigin.Viewport, A.X, A.Y + squareSize.Height, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerUp(PointerButton.PenContact));

            // Draw line DA from point D to A
            sequence.AddAction(penDevice.CreatePointerDown(PointerButton.PenContact));
            sequence.AddAction(penDevice.CreatePointerMove(CoordinateOrigin.Viewport, A.X, A.Y, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerUp(PointerButton.PenContact));

            newStickyNoteSession.PerformActions(new List<ActionSequence> { sequence });

            try
            {
                var result = newStickyNoteSession.FindElement(MobileBy.AccessibilityId("RichEditBox"));
                Assert.Fail("RichEditBox should not be defined anymore after a pen input is successfully performed.");
            }
            catch { }
        }

        [Test]
        public void DrawBasicSquareWithExtraAttributes()
        {
            Point canvasCoordinate = inkCanvas.Coordinates.LocationInViewport;
            Size squareSize = new Size(inkCanvas.Size.Width * 3 / 5, inkCanvas.Size.Height * 3 / 5);
            Point A = new Point(canvasCoordinate.X + inkCanvas.Size.Width / 5, canvasCoordinate.Y + inkCanvas.Size.Height / 5);

            // A        B
            //  ┌──────┐   Draw a basic ABCD square using Pen through the Actions API
            //  │      │   in pointer origin mode:
            //  │      │   - X is relative to the previous X position in this session
            //  └──────┘   - Y is relative to the previous Y position in this session
            // D        C
            PointerInputDevice penDevice = new PointerInputDevice(PointerKind.Pen);
            ActionSequence sequence = new ActionSequence(penDevice, 0);
            PenInfo penExtraAttributes = new PenInfo { TiltX = 45, TiltY = 45, Twist = 45 };

            // Draw line AB from point A to B with attributes defined in penExtraAttributes
            sequence.AddAction(penDevice.CreatePointerMove(CoordinateOrigin.Pointer, A.X, A.Y, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerDown(PointerButton.PenContact, penExtraAttributes));
            sequence.AddAction(penDevice.CreatePointerMove(CoordinateOrigin.Pointer, squareSize.Width, 0, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerUp(PointerButton.PenContact));

            // Draw line BC from point B to C and apply maximum (0.9f) pressure as the pen draw between the points
            sequence.AddAction(penDevice.CreatePointerDown(PointerButton.PenContact));
            sequence.AddAction(penDevice.CreatePointerMove(CoordinateOrigin.Pointer, 0, squareSize.Height, TimeSpan.Zero, new PenInfo { Pressure = 0.9f }));
            sequence.AddAction(penDevice.CreatePointerUp(PointerButton.PenContact));

            // Draw line CD from point C to D and keep the maximum pressure by not changing the pressure attribute
            sequence.AddAction(penDevice.CreatePointerDown(PointerButton.PenContact));
            sequence.AddAction(penDevice.CreatePointerMove(CoordinateOrigin.Pointer, -squareSize.Width, 0, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerUp(PointerButton.PenContact));

            // Draw line DA from point D to A and reduce the pressure to minimum (0.1f) as the pen draw between the points
            sequence.AddAction(penDevice.CreatePointerDown(PointerButton.PenContact));
            sequence.AddAction(penDevice.CreatePointerMove(CoordinateOrigin.Pointer, 0, -squareSize.Height, TimeSpan.Zero, new PenInfo { Pressure = 0.1f }));
            sequence.AddAction(penDevice.CreatePointerUp(PointerButton.PenContact));

            newStickyNoteSession.PerformActions(new List<ActionSequence> { sequence });

            try
            {
                var result = newStickyNoteSession.FindElement(MobileBy.AccessibilityId("RichEditBox"));
                Assert.Fail("RichEditBox should not be defined anymore after a pen input is successfully performed.");
            }
            catch { }
        }

        [Test]
        public void DrawSquareAndDeleteStrokes()
        {
            int offset = 400;
            // A        B
            //  ┌──────┐   Draw a basic ABCD square using Pen through the Actions API
            //  │      │   in pointer element mode:
            //  │      │   - X is relative to the horizontal element center point
            //  └──────┘   - Y is relative to the vertical element center point
            // D        C
            PointerInputDevice penDevice = new PointerInputDevice(PointerKind.Pen);
            ActionSequence sequence = new ActionSequence(penDevice, 0);

            // Draw line CD from point C to D and apply 30% pen pressure
            sequence.AddAction(penDevice.CreatePointerMove(inkCanvas, offset, offset, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerDown(PointerButton.PenContact, new PenInfo { Pressure = 0.3f }));
            sequence.AddAction(penDevice.CreatePointerMove(inkCanvas, -offset, offset, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerUp(PointerButton.PenContact));

            // Draw line DA from point D to A
            sequence.AddAction(penDevice.CreatePointerDown(PointerButton.PenContact));
            sequence.AddAction(penDevice.CreatePointerMove(inkCanvas, -offset, -offset, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerUp(PointerButton.PenContact));

            // Draw line AB from point A to B
            sequence.AddAction(penDevice.CreatePointerDown(PointerButton.PenContact));
            sequence.AddAction(penDevice.CreatePointerMove(inkCanvas, offset, -offset, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerUp(PointerButton.PenContact));

            // Draw line BC from point B to C
            sequence.AddAction(penDevice.CreatePointerDown(PointerButton.PenContact));
            sequence.AddAction(penDevice.CreatePointerMove(inkCanvas, offset, offset, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerUp(PointerButton.PenContact));

            // Draw diagonal line CA from point C straight to A
            sequence.AddAction(penDevice.CreatePointerDown(PointerButton.PenContact));
            sequence.AddAction(penDevice.CreatePointerMove(inkCanvas, -offset, -offset, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerUp(PointerButton.PenContact));

            // Draw diagonal line DB from point D straight to B
            sequence.AddAction(penDevice.CreatePointerMove(inkCanvas, -offset, offset, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerDown(PointerButton.PenContact));
            sequence.AddAction(penDevice.CreatePointerMove(inkCanvas, offset, -offset, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerUp(PointerButton.PenContact));

            // Erase line AB by pressing PenEraser (Pen tail end/eraser button) on the line AB from right to left
            sequence.AddAction(penDevice.CreatePointerMove(inkCanvas, offset / 2, -offset, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerDown(PointerButton.PenEraser));
            sequence.AddAction(penDevice.CreatePointerMove(inkCanvas, -offset / 2, -offset, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerUp(PointerButton.PenEraser));

            // Erase line CD by pressing PenEraser (Pen tail end/eraser button) on the line CD from left to right
            sequence.AddAction(penDevice.CreatePointerMove(inkCanvas, -offset / 2, offset, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerDown(PointerButton.PenEraser));
            sequence.AddAction(penDevice.CreatePointerMove(inkCanvas, offset / 2, offset, TimeSpan.Zero));
            sequence.AddAction(penDevice.CreatePointerUp(PointerButton.PenEraser));

            newStickyNoteSession.PerformActions(new List<ActionSequence> { sequence });

            try
            {
                var result = newStickyNoteSession.FindElement(MobileBy.AccessibilityId("RichEditBox"));
                Assert.Fail("RichEditBox should not be defined anymore after a pen input is successfully performed.");
            }
            catch { }
        }

        [SetUp]
        public void CreateNewStickyNote()
        {
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;

            const int stickyNoteWidth = 1000;
            const int stickyNoteHeight = 1000;
            const int stickyNotePositionX = 100;
            const int stickyNotePositionY = 100;

            // Preserve existing or previously opened Sticky Notes by keeping track of them on initialize
            var openedStickyNotesWindowsBefore = session.FindElements(MobileBy.ClassName("ApplicationFrameWindow"));
            Assert.That(openedStickyNotesWindowsBefore, Is.Not.Null);
            Assert.That(openedStickyNotesWindowsBefore.Count > 0, Is.True);

            // Create a new Sticky Note by pressing Ctrl + N
            openedStickyNotesWindowsBefore[0].SendKeys(Keys.Control + "n" + Keys.Control);
            Thread.Sleep(TimeSpan.FromSeconds(2));

            var openedStickyNotesWindowsAfter = session.FindElements(MobileBy.ClassName("ApplicationFrameWindow"));
            Assert.That(openedStickyNotesWindowsAfter, Is.Not.Null);
            Assert.That(openedStickyNotesWindowsAfter.Count, Is.EqualTo(openedStickyNotesWindowsBefore.Count + 1));

            // Identify the newly opened Sticky Note by removing the previously opened ones from the list
            List<AppiumElement> openedStickyNotes = new List<AppiumElement>(openedStickyNotesWindowsAfter);
            foreach (var preExistingStickyNote in openedStickyNotesWindowsBefore)
            {
                openedStickyNotes.Remove(preExistingStickyNote);
            }
            Assert.That(openedStickyNotes.Count, Is.EqualTo(1));

            // Create a new session based from the newly opened Sticky Notes window
            var newStickyNoteWindowHandle = openedStickyNotes[0].GetAttribute("NativeWindowHandle");
            newStickyNoteWindowHandle = (int.Parse(newStickyNoteWindowHandle)).ToString("x"); // Convert to Hex
            AppiumOptions appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalAppiumOption("appTopLevelWindow", newStickyNoteWindowHandle);
            appCapabilities.DeviceName = "WindowsPC";
            newStickyNoteSession = new WindowsDriver(serverUri, appCapabilities);
            Assert.That(newStickyNoteSession, Is.Not.Null);

            // Resize and re-position the Sticky Notes window we are working with
            newStickyNoteSession.Manage().Window.Size = new Size(stickyNoteWidth, stickyNoteHeight);
            newStickyNoteSession.Manage().Window.Position = new Point(stickyNotePositionX, stickyNotePositionY);

            // Verify that this Sticky Notes is indeed new. Newly created Sticky Notes has both
            // RichEditBox and InkCanvas in the UI tree. Once modified by a pen or key input,
            // it will only contain one or the other.
            Assert.That(newStickyNoteSession.FindElement(MobileBy.AccessibilityId("RichEditBox")), Is.Not.Null);
            inkCanvas = newStickyNoteSession.FindElement(MobileBy.AccessibilityId("InkCanvas"));
            Assert.That(inkCanvas, Is.Not.Null);
        }

        [TearDown]
        public void DeleteStickyNote()
        {
            if (newStickyNoteSession != null)
            {
                // Create a new Sticky Note by pressing Ctrl + N
                /////// TODO - Implement for Appium
                //// newStickyNoteSession.Keyboard.SendKeys(Keys.Control + "d" + Keys.Control);
                Thread.Sleep(TimeSpan.FromSeconds(2));

                try
                {
                    // If a delete confirmation dialog is displayed, press Delete to proceed
                    newStickyNoteSession.FindElement(MobileBy.AccessibilityId("YesButton")).Click();
                }
                catch { }
            }

            inkCanvas = null;
            newStickyNoteSession?.Quit();
        }
    }
}
