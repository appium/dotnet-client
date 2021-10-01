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
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

namespace Appium.Net.Integration.Tests.Windows
{
    public class ClickElementTest
    {
        private WindowsDriver<WebElement> _calculatorSession;
        protected static WebElement CalculatorResult;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var appCapabilities = new WindowsOptions();
            appCapabilities.AddAdditionalOption("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");

            var serverUri = new Uri("http://127.0.0.1:4723");
            //var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _calculatorSession = new WindowsDriver<WebElement>(serverUri, appCapabilities,
                Env.InitTimeoutSec);
            _calculatorSession.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;

            _calculatorSession.FindElementByName("Clear").Click();
            _calculatorSession.FindElementByName("Seven").Click();
            CalculatorResult = _calculatorSession.FindElementByName("Display is 7");
            Assert.IsNotNull(CalculatorResult);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            CalculatorResult = null;
            _calculatorSession.CloseApp();
            _calculatorSession.Dispose();
            _calculatorSession = null;
        }

        [SetUp]
        public void SetUp()
        {
            _calculatorSession.FindElementByName("Clear").Click();
            Assert.AreEqual("Display is 0", CalculatorResult.Text);
        }

        [Test]
        public void Addition()
        {
            _calculatorSession.FindElementByName("One").Click();
            _calculatorSession.FindElementByName("Plus").Click();
            _calculatorSession.FindElementByName("Seven").Click();
            _calculatorSession.FindElementByName("Equals").Click();
            Assert.AreEqual("Display is 8", CalculatorResult.Text);
        }

        [Test]
        public void Combination()
        {
            _calculatorSession.FindElementByName("Seven").Click();
            _calculatorSession.FindElementByName("Multiply by").Click();
            _calculatorSession.FindElementByName("Nine").Click();
            _calculatorSession.FindElementByName("Plus").Click();
            _calculatorSession.FindElementByName("One").Click();
            _calculatorSession.FindElementByName("Equals").Click();
            _calculatorSession.FindElementByName("Divide by").Click();
            _calculatorSession.FindElementByName("Eight").Click();
            _calculatorSession.FindElementByName("Equals").Click();
            Assert.AreEqual("Display is 8", CalculatorResult.Text);
        }

        [Test]
        public void Division()
        {
            _calculatorSession.FindElementByName("Eight").Click();
            _calculatorSession.FindElementByName("Eight").Click();
            _calculatorSession.FindElementByName("Divide by").Click();
            _calculatorSession.FindElementByName("One").Click();
            _calculatorSession.FindElementByName("One").Click();
            _calculatorSession.FindElementByName("Equals").Click();
            Assert.AreEqual("Display is 8", CalculatorResult.Text);
        }

        [Test]
        public void Multiplication()
        {
            _calculatorSession.FindElementByName("Nine").Click();
            _calculatorSession.FindElementByName("Multiply by").Click();
            _calculatorSession.FindElementByName("Nine").Click();
            _calculatorSession.FindElementByName("Equals").Click();
            Assert.AreEqual("Display is 81", CalculatorResult.Text);
        }

        [Test]
        public void Subtraction()
        {
            _calculatorSession.FindElementByName("Nine").Click();
            _calculatorSession.FindElementByName("Minus").Click();
            _calculatorSession.FindElementByName("One").Click();
            _calculatorSession.FindElementByName("Equals").Click();
            Assert.AreEqual("Display is 8", CalculatorResult.Text);
        }
    }
}