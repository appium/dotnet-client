using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace Appium.Integration.Tests.Windows
{
    public class WindowsClickElementTest
    {
        private WindowsDriver<WindowsElement> CalculatorSession;
        protected const string WindowsApplicationDriverUrl = "http://localhost:4723/wd/hub";
        protected static RemoteWebElement CalculatorResult;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            DesiredCapabilities appCapabilities = new DesiredCapabilities();
            appCapabilities.SetCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");

            // TODO: What about supporting AppiumServers.sauceURI ???
            CalculatorSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appCapabilities, Env.INIT_TIMEOUT_SEC);
            CalculatorSession.Manage().Timeouts().ImplicitlyWait(Env.IMPLICIT_TIMEOUT_SEC);

            CalculatorSession.FindElementByName("Clear").Click();
            CalculatorSession.FindElementByName("Seven").Click();
            CalculatorResult = CalculatorSession.FindElementByName("Display is  7 ") as RemoteWebElement;
            Assert.IsNotNull(CalculatorResult);
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            CalculatorResult = null;
            CalculatorSession.CloseApp();
            CalculatorSession.Dispose();
            CalculatorSession = null;
        }

        [SetUp]
        public void SetUp()
        {
            CalculatorSession.FindElementByName("Clear").Click();
            Assert.AreEqual("Display is  0 ", CalculatorResult.Text);
        }

        [Test()]
        public void Addition()
        {
            CalculatorSession.FindElementByName("One").Click();
            CalculatorSession.FindElementByName("Plus").Click();
            CalculatorSession.FindElementByName("Seven").Click();
            CalculatorSession.FindElementByName("Equals").Click();
            Assert.AreEqual("Display is  8 ", CalculatorResult.Text);
        }

        [Test()]
        public void Combination()
        {
            CalculatorSession.FindElementByName("Seven").Click();
            CalculatorSession.FindElementByName("Multiply by").Click();
            CalculatorSession.FindElementByName("Nine").Click();
            CalculatorSession.FindElementByName("Plus").Click();
            CalculatorSession.FindElementByName("One").Click();
            CalculatorSession.FindElementByName("Equals").Click();
            CalculatorSession.FindElementByName("Divide by").Click();
            CalculatorSession.FindElementByName("Eight").Click();
            CalculatorSession.FindElementByName("Equals").Click();
            Assert.AreEqual("Display is  8 ", CalculatorResult.Text);
        }

        [Test]
        public void Division()
        {
            CalculatorSession.FindElementByName("Eight").Click();
            CalculatorSession.FindElementByName("Eight").Click();
            CalculatorSession.FindElementByName("Divide by").Click();
            CalculatorSession.FindElementByName("One").Click();
            CalculatorSession.FindElementByName("One").Click();
            CalculatorSession.FindElementByName("Equals").Click();
            Assert.AreEqual("Display is  8 ", CalculatorResult.Text);
        }

        [Test]
        public void Multiplication()
        {
            CalculatorSession.FindElementByName("Nine").Click();
            CalculatorSession.FindElementByName("Multiply by").Click();
            CalculatorSession.FindElementByName("Nine").Click();
            CalculatorSession.FindElementByName("Equals").Click();
            Assert.AreEqual("Display is  81 ", CalculatorResult.Text);
        }

        [Test]
        public void Subtraction()
        {
            CalculatorSession.FindElementByName("Nine").Click();
            CalculatorSession.FindElementByName("Minus").Click();
            CalculatorSession.FindElementByName("One").Click();
            CalculatorSession.FindElementByName("Equals").Click();
            Assert.AreEqual("Display is  8 ", CalculatorResult.Text);
        }
    }
}
