using System.Collections.Generic;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;

namespace Appium.Net.Integration.Tests.Driver
{
    [TestFixture]
    public class AppiumDriverLocationTests
    {
        private class TestAppiumDriver : AppiumDriver
        {
            public TestAppiumDriver(ICommandExecutor executor)
                : base(executor, new AppiumOptions().ToCapabilities())
            {
            }
        }

        private class MockCommandExecutor : ICommandExecutor
        {
            public Response ResponseToReturn { get; set; }

            public Response Execute(Command commandToExecute)
            {
                if (commandToExecute.Name == DriverCommand.NewSession)
                {
                    return new Response(
                        "test-session",
                        new Dictionary<string, object>
                        {
                            { "capabilities", new Dictionary<string, object>() }
                        },
                        WebDriverResult.Success);
                }

                return ResponseToReturn;
            }

            public Task<Response> ExecuteAsync(Command commandToExecute)
            {
                return Task.FromResult(Execute(commandToExecute));
            }

            public bool TryAddCommand(string commandName, CommandInfo commandInfo)
            {
                return true;
            }

            public void Dispose()
            {
            }
        }

        private MockCommandExecutor _executor;
        private TestAppiumDriver _driver;

        [SetUp]
        public void SetUp()
        {
            _executor = new MockCommandExecutor();
            _driver = new TestAppiumDriver(_executor);
        }

        [TearDown]
        public void TearDown()
        {
            _driver?.Dispose();
            _executor?.Dispose();
        }

        [Test]
        public void Location_WithValidNumericValues_ReturnsExpectedCoordinates()
        {
            _executor.ResponseToReturn = new Response(
                "session-id",
                new Dictionary<string, object>
                {
                    { "altitude", 100.5 },
                    { "latitude", 45.123 },
                    { "longitude", -93.456 }
                },
                WebDriverResult.Success);

            var location = _driver.Location;

            Assert.That(location.Altitude, Is.EqualTo(100.5));
            Assert.That(location.Latitude, Is.EqualTo(45.123));
            Assert.That(location.Longitude, Is.EqualTo(-93.456));
        }

        [Test]
        public void Location_WithIntegerAndStringCoordinates_ConvertsSuccessfully()
        {
            _executor.ResponseToReturn = new Response(
                "session-id",
                new Dictionary<string, object>
                {
                    { "altitude", 100 },
                    { "latitude", "45.5" },
                    { "longitude", "-93.5" }
                },
                WebDriverResult.Success);

            var location = _driver.Location;

            Assert.That(location.Altitude, Is.EqualTo(100.0));
            Assert.That(location.Latitude, Is.EqualTo(45.5));
            Assert.That(location.Longitude, Is.EqualTo(-93.5));
        }

        [Test]
        public void Location_WithMissingKeys_DefaultsMissingToZero()
        {
            _executor.ResponseToReturn = new Response(
                "session-id",
                new Dictionary<string, object>
                {
                    { "latitude", 45.123 }
                },
                WebDriverResult.Success);

            var location = _driver.Location;

            Assert.That(location.Altitude, Is.EqualTo(0.0));
            Assert.That(location.Latitude, Is.EqualTo(45.123));
            Assert.That(location.Longitude, Is.EqualTo(0.0));
        }

        [Test]
        public void Location_WithNullValues_DoesNotThrowAndDefaultsToZero()
        {
            _executor.ResponseToReturn = new Response(
                "session-id",
                new Dictionary<string, object>
                {
                    { "altitude", null },
                    { "latitude", null },
                    { "longitude", null }
                },
                WebDriverResult.Success);

            var location = _driver.Location;

            Assert.That(location.Altitude, Is.EqualTo(0.0));
            Assert.That(location.Latitude, Is.EqualTo(0.0));
            Assert.That(location.Longitude, Is.EqualTo(0.0));
        }

        [Test]
        public void Location_WithNonNumericStrings_DoesNotThrowAndDefaultsToZero()
        {
            _executor.ResponseToReturn = new Response(
                "session-id",
                new Dictionary<string, object>
                {
                    { "altitude", "invalid" },
                    { "latitude", "not-a-number" },
                    { "longitude", "" }
                },
                WebDriverResult.Success);

            var location = _driver.Location;

            Assert.That(location.Altitude, Is.EqualTo(0.0));
            Assert.That(location.Latitude, Is.EqualTo(0.0));
            Assert.That(location.Longitude, Is.EqualTo(0.0));
        }

        [Test]
        public void Location_WithNonConvertibleTypes_DoesNotThrowAndDefaultsToZero()
        {
            _executor.ResponseToReturn = new Response(
                "session-id",
                new Dictionary<string, object>
                {
                    { "altitude", new object() },
                    { "latitude", new List<int>() },
                    { "longitude", new Dictionary<string, object>() }
                },
                WebDriverResult.Success);

            var location = _driver.Location;

            Assert.That(location.Altitude, Is.EqualTo(0.0));
            Assert.That(location.Latitude, Is.EqualTo(0.0));
            Assert.That(location.Longitude, Is.EqualTo(0.0));
        }

        [Test]
        public void Location_WithNonDictionaryResponse_DoesNotThrowAndReturnsDefaultLocation()
        {
            _executor.ResponseToReturn = new Response(
                "session-id",
                "unexpected string payload",
                WebDriverResult.Success);

            var location = _driver.Location;

            Assert.That(location.Altitude, Is.EqualTo(0.0));
            Assert.That(location.Latitude, Is.EqualTo(0.0));
            Assert.That(location.Longitude, Is.EqualTo(0.0));
        }

        [Test]
        public void Location_WithNullResponseValue_DoesNotThrowAndReturnsDefaultLocation()
        {
            _executor.ResponseToReturn = new Response(
                "session-id",
                null,
                WebDriverResult.Success);

            var location = _driver.Location;

            Assert.That(location.Altitude, Is.EqualTo(0.0));
            Assert.That(location.Latitude, Is.EqualTo(0.0));
            Assert.That(location.Longitude, Is.EqualTo(0.0));
        }
    }
}
