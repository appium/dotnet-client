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

namespace Appium.Net.Integration.Tests.Windows
{
    public class ClickElementTest
    {
        private WindowsDriver _calculatorSession;
        protected static WebElement CalculatorResult;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.App = "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App";
            appCapabilities.DeviceName = "WindowsPC";
            appCapabilities.PlatformName = "Windows";

            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _calculatorSession = new WindowsDriver(serverUri, appCapabilities,
                Env.InitTimeoutSec);
            _calculatorSession.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;

            _calculatorSession.FindElement(MobileBy.Name("Clear")).Click();
            _calculatorSession.FindElement(MobileBy.Name("Seven")).Click();
            CalculatorResult = _calculatorSession.FindElement(MobileBy.Name("Display is 7")) as WebElement;
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
            _calculatorSession.FindElement(MobileBy.Name("Clear")).Click();
            Assert.AreEqual("Display is 0", CalculatorResult.Text);
        }

        [Test]
        public void Addition()
        {
            _calculatorSession.FindElement(MobileBy.Name("One")).Click();
            _calculatorSession.FindElement(MobileBy.Name("Plus")).Click();
            _calculatorSession.FindElement(MobileBy.Name("Seven")).Click();
            _calculatorSession.FindElement(MobileBy.Name("Equals")).Click();
            Assert.AreEqual("Display is 8", CalculatorResult.Text);
        }

        [Test]
        public void Combination()
        {
            _calculatorSession.FindElement(MobileBy.Name("Seven")).Click();
            _calculatorSession.FindElement(MobileBy.Name("Multiply by")).Click();
            _calculatorSession.FindElement(MobileBy.Name("Nine")).Click();
            _calculatorSession.FindElement(MobileBy.Name("Plus")).Click();
            _calculatorSession.FindElement(MobileBy.Name("One")).Click();
            _calculatorSession.FindElement(MobileBy.Name("Equals")).Click();
            _calculatorSession.FindElement(MobileBy.Name("Divide by")).Click();
            _calculatorSession.FindElement(MobileBy.Name("Eight")).Click();
            _calculatorSession.FindElement(MobileBy.Name("Equals")).Click();
            Assert.AreEqual("Display is 8", CalculatorResult.Text);
        }

        [Test]
        public void Division()
        {
            _calculatorSession.FindElement(MobileBy.Name("Eight")).Click();
            _calculatorSession.FindElement(MobileBy.Name("Eight")).Click();
            _calculatorSession.FindElement(MobileBy.Name("Divide by")).Click();
            _calculatorSession.FindElement(MobileBy.Name("One")).Click();
            _calculatorSession.FindElement(MobileBy.Name("One")).Click();
            _calculatorSession.FindElement(MobileBy.Name("Equals")).Click();
            Assert.AreEqual("Display is 8", CalculatorResult.Text);
        }

        [Test]
        public void Multiplication()
        {
            _calculatorSession.FindElement(MobileBy.Name("Nine")).Click();
            _calculatorSession.FindElement(MobileBy.Name("Multiply by")).Click();
            _calculatorSession.FindElement(MobileBy.Name("Nine")).Click();
            _calculatorSession.FindElement(MobileBy.Name("Equals")).Click();
            Assert.AreEqual("Display is 81", CalculatorResult.Text);
        }

        [Test]
        public void Subtraction()
        {
            _calculatorSession.FindElement(MobileBy.Name("Nine")).Click();
            _calculatorSession.FindElement(MobileBy.Name("Minus")).Click();
            _calculatorSession.FindElement(MobileBy.Name("One")).Click();
            _calculatorSession.FindElement(MobileBy.Name("Equals")).Click();
            Assert.AreEqual("Display is 8", CalculatorResult.Text);
        }
    }
}