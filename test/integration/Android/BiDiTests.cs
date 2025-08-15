using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.BiDi;
using System.Threading.Tasks;

namespace Appium.Net.Integration.Tests.Android
{
    public class BiDiTests
    {
        private AndroidDriver _driver;

        private BiDi _bidi;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Caps.GetAndroidUIAutomatorCaps();
            capabilities.UseWebSocketUrl = true;
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
        }

        [Test]
        public async Task WebDriverWaitElementNotFoundTestCase()
        {
            _bidi = await _driver.AsBiDiAsync();
            await _bidi.StatusAsync();
            // Subscribe to log.entryAdded events
            // _bidi.Session.Subscribe(new[] { "log.entryAdded" });

            // Listen for log events (example: using an event handler)
            // _bidi.Log.EntryAdded += (sender, e) =>
            // {
            //     Console.WriteLine($"Log: {e.Text}");
            // };

            // Trigger a log event in the browser (for demonstration)
            // _driver.Navigate().GoToUrl("https://example.com");
            // _driver.ExecuteScript("console.log('Hello from BiDi!');");

            // Wait for events to be received
            System.Threading.Thread.Sleep(1000);
        }

        [OneTimeTearDown]
        public async Task AfterAll()
        {
            if (_bidi != null)
            {
                await _bidi.DisposeAsync();
            }
            if (_driver != null)
                {
                    _driver?.Quit();
                }
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }
    }
}
