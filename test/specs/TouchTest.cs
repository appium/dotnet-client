using NUnit.Framework;
using System;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Test.Helpers;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.iOS;

namespace OpenQA.Selenium.Appium.Test.Specs
{
    [TestFixture()]
    public class TouchTest
    {
        public FakeAppium server;
        public readonly Uri defaultUri = new Uri("http://127.0.0.1:4753/wd/hub");
        public readonly DesiredCapabilities capabilities = new DesiredCapabilities();

        [TestFixtureSetUp]
        public void RunBeforeAll()
        {
            server = new FakeAppium(4753);
            server.Start();
            server.respondToInit();
            server.clear();
        }

        [TestFixtureTearDown]
        public void RunAfterAll()
        {
            server.Stop();
        }

        [TearDown]
        public void RunAfterEach()
        {
            server.clear();
        }

        private RequestProcessor setupTouchAction()
        {
            server.respondTo("POST", "/element", new Dictionary<string, object>  { 				{"ELEMENT", '5'} 			});
            return server.respondTo("POST", "/touch/perform", null);
        }

        [Test]
        public void LongPressTestCase()
        {
            IOSDriver<IWebElement> driver = new IOSDriver<IWebElement>(defaultUri, capabilities);
            RequestProcessor re = setupTouchAction();
            IWebElement element = driver.FindElementByIosUIAutomation(".elements()");

            ITouchAction a;

            a = new TouchAction(driver)
                .LongPress(element, 50, 75);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"longpress\",\"options\":{\"element\":\"5\",\"x\":50,\"y\":75}}]}");

            a = new TouchAction(driver)
                .LongPress(element, 0.5, 75);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"longpress\",\"options\":{\"element\":\"5\",\"x\":0.5,\"y\":75}}]}");

            a = new TouchAction(driver)
                .LongPress(0.5, 75);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"longpress\",\"options\":{\"x\":0.5,\"y\":75}}]}");

            a = new TouchAction(driver)
                .LongPress(element);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"longpress\",\"options\":{\"element\":\"5\"}}]}");
        }

        [Test]
        public void MoveToTestCase()
        {
            IOSDriver<IWebElement> driver = new IOSDriver<IWebElement>(defaultUri, capabilities);
            RequestProcessor re = setupTouchAction();
            IWebElement element = driver.FindElementByIosUIAutomation(".elements()");

            ITouchAction a;

            a = new TouchAction(driver)
                .MoveTo(element, 50, 75);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"moveTo\",\"options\":{\"element\":\"5\",\"x\":50,\"y\":75}}]}");

            a = new TouchAction(driver)
                .MoveTo(element, 0.5, 75);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"moveTo\",\"options\":{\"element\":\"5\",\"x\":0.5,\"y\":75}}]}");

            a = new TouchAction(driver)
                .MoveTo(0.5, 75);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"moveTo\",\"options\":{\"x\":0.5,\"y\":75}}]}");

            a = new TouchAction(driver)
                .MoveTo(element);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"moveTo\",\"options\":{\"element\":\"5\"}}]}");
        }

        [Test]
        public void PressTestCase()
        {
            IOSDriver<IWebElement> driver = new IOSDriver<IWebElement>(defaultUri, capabilities);
            RequestProcessor re = setupTouchAction();
            IWebElement element = driver.FindElementByIosUIAutomation(".elements()");

            ITouchAction a;

            a = new TouchAction(driver)
                .Press(element, 50, 75);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"press\",\"options\":{\"element\":\"5\",\"x\":50,\"y\":75}}]}");

            a = new TouchAction(driver)
                .Press(element, 0.5, 75);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"press\",\"options\":{\"element\":\"5\",\"x\":0.5,\"y\":75}}]}");

            a = new TouchAction(driver)
                .Press(0.5, 75);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"press\",\"options\":{\"x\":0.5,\"y\":75}}]}");

            a = new TouchAction(driver)
                .Press(element);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"press\",\"options\":{\"element\":\"5\"}}]}");
        }

        [Test]
        public void ReleaseTestCase()
        {
            IOSDriver<IWebElement> driver = new IOSDriver<IWebElement>(defaultUri, capabilities);
            RequestProcessor re = setupTouchAction();
            ITouchAction a;

            a = new TouchAction(driver)
                .Release();
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"release\"}]}");
        }

        [Test]
        public void TapTestCase()
        {
            IOSDriver<IWebElement> driver = new IOSDriver<IWebElement>(defaultUri, capabilities);
            RequestProcessor re = setupTouchAction();
            IWebElement element = driver.FindElementByIosUIAutomation(".elements()");

            ITouchAction a;

            a = new TouchAction(driver)
                .Tap(element, 50, 75);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"tap\",\"options\":{\"element\":\"5\",\"x\":50,\"y\":75}}]}");

            a = new TouchAction(driver)
                .Tap(element, 50, 75, 10);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"tap\",\"options\":{\"element\":\"5\",\"x\":50,\"y\":75,\"count\":10}}]}");

            a = new TouchAction(driver)
                .Tap(element, 0.5, 75);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"tap\",\"options\":{\"element\":\"5\",\"x\":0.5,\"y\":75}}]}");

            a = new TouchAction(driver)
                .Tap(0.5, 75);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"tap\",\"options\":{\"x\":0.5,\"y\":75}}]}");

            a = new TouchAction(driver)
                .Tap(0.5, 75, 10);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"tap\",\"options\":{\"x\":0.5,\"y\":75,\"count\":10}}]}");

            a = new TouchAction(driver)
                .Tap(element);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"tap\",\"options\":{\"element\":\"5\"}}]}");

            a = new TouchAction(driver)
                .Tap(element, count: 10);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"tap\",\"options\":{\"element\":\"5\",\"count\":10}}]}");

        }

        [Test]
        public void WaitTestCase()
        {
            IOSDriver<IWebElement> driver = new IOSDriver<IWebElement>(defaultUri, capabilities);
            RequestProcessor re = setupTouchAction();
            ITouchAction a;

            a = new TouchAction(driver)
                .Wait(1000);
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"wait\",\"options\":{\"ms\":1000}}]}");

            a = new TouchAction(driver)
                .Wait();
            a.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[{\"action\":\"wait\"}]}");
        }

        private RequestProcessor setupMultiAction()
        {
            server.respondTo("POST", "/element", new Dictionary<string, object>  { 				{"ELEMENT", '5'} 			});
            return server.respondTo("POST", "/touch/multi/perform", null);
        }

        [Test]
        public void MultiActionTestCase()
        {
            IOSDriver<IWebElement> driver = new IOSDriver<IWebElement>(defaultUri, capabilities);
            RequestProcessor re = setupMultiAction();
            IWebElement element = driver.FindElementByIosUIAutomation(".elements()");

            MultiAction m = new MultiAction(driver);
            m.Perform();
            Assert.AreEqual(re.inputData, "");

            TouchAction a1 = new TouchAction(driver);
            a1
                .Press(element, 100, 100)
                .Wait(1000)
                .Release();
            m.Add(a1);
            m.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[[{\"action\":\"press\",\"options\":{\"element\":\"5\",\"x\":100,\"y\":100}},{\"action\":\"wait\",\"options\":{\"ms\":1000}},{\"action\":\"release\"}]]}");

            TouchAction a2 = new TouchAction(driver);
            a2
                .Tap(100, 100)
                .MoveTo(element);
            m.Add(a2);
            m.Perform();
            Assert.AreEqual(re.inputData, "{\"actions\":[[{\"action\":\"press\",\"options\":{\"element\":\"5\",\"x\":100,\"y\":100}},{\"action\":\"wait\",\"options\":{\"ms\":1000}},{\"action\":\"release\"}],[{\"action\":\"tap\",\"options\":{\"x\":100,\"y\":100}},{\"action\":\"moveTo\",\"options\":{\"element\":\"5\"}}]]}");
        }
    }
}

