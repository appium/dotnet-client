using NUnit.Framework;
using System;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Test.Helpers;
using OpenQA.Selenium.Appium.Android;

namespace OpenQA.Selenium.Appium.Test.Specs
{
    [TestFixture]
    public class InitSessionTest
    {
        public FakeAppium server;
        public readonly Uri defaultUri = new Uri("http://127.0.0.1:4733/wd/hub");
        public readonly DesiredCapabilities capabilities = new DesiredCapabilities();

        [TestFixtureSetUp]
        public void RunBeforeAll()
        {
            server = new FakeAppium(4723);
            server.Start();
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

        [Test]
        public void InitSessionTestCase()
        {
            server.respondToInit();
            DesiredCapabilities capabilities = new DesiredCapabilities();
            new AndroidDriver<IWebElement>(capabilities);
        }
    }

    [TestFixture]
    public class EndSessionTest
    {
        public FakeAppium server;
        public readonly Uri defaultUri = new Uri("http://127.0.0.1:4724/wd/hub");
        public readonly DesiredCapabilities capabilities = new DesiredCapabilities();

        [TestFixtureSetUp]
        public void RunBeforeAll()
        {
            server = new FakeAppium(4724);
            server.Start();
        }

        [TestFixtureTearDown]
        public void RunAfterAll()
        {
            server.Stop();
        }

        [SetUp]
        public void RunBeforeEach()
        {
            server.respondToInit();
            DesiredCapabilities capabilities = new DesiredCapabilities();
            AndroidDriver<IWebElement> driver = new AndroidDriver<IWebElement>(defaultUri, capabilities);
            server.clear();
        }


        [TearDown]
        public void RunAfterEach()
        {
            server.clear();
        }

        [Test]
        public void CloseTestCase()
        {
            AndroidDriver<IWebElement> driver = new AndroidDriver<IWebElement>(defaultUri, capabilities);
            server.respondTo("DELETE", "/window", null);
            driver.Close();
        }

        [Test]
        public void QuitTestCase()
        {
            AndroidDriver<IWebElement> driver = new AndroidDriver<IWebElement>(defaultUri, capabilities);
            server.respondTo("DELETE", "/", null);
            driver.Quit();
        }

    }
}

