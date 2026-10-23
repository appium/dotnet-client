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

using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium;

namespace Appium.Net.Integration.Tests.Common
{
    public class GuardClausesTest
    {
        [Test]
        public void RequireNotNullReturnsValueWhenNotNull()
        {
            var value = "hello";
            Assert.That(value.RequireNotNull(nameof(value)), Is.SameAs(value));
        }

        [Test]
        public void RequireNotNullThrowsWhenNull()
        {
            string value = null;
            var ex = Assert.Throws<ArgumentNullException>((Action)(() => value.RequireNotNull("value")));
            Assert.That(ex.ParamName, Is.EqualTo("value"));
        }

        [Test]
        public void RequireIsPositiveReturnsValueWhenZeroOrGreater()
        {
            Assert.That(0.RequireIsPositive("value"), Is.EqualTo(0));
            Assert.That(5.RequireIsPositive("value"), Is.EqualTo(5));
        }

        [Test]
        public void RequireIsPositiveThrowsWhenNegative()
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>((Action)(() => (-1).RequireIsPositive("value")));
            Assert.That(ex.ParamName, Is.EqualTo("value"));
        }

        [TestCase(0.1)]
        [TestCase(0.5)]
        [TestCase(0.999)]
        public void RequirePercentageReturnsValueWhenBetweenZeroAndOne(double input)
        {
            Assert.That(input.RequirePercentage("value"), Is.EqualTo(input));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(-0.1)]
        [TestCase(1.1)]
        public void RequirePercentageThrowsWhenOutOfRange(double input)
        {
            var ex = Assert.Throws<ArgumentOutOfRangeException>((Action)(() => input.RequirePercentage("value")));
            Assert.That(ex.ParamName, Is.EqualTo("value"));
        }
    }
}
