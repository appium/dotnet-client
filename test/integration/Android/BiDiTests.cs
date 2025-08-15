using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.BiDi;
using OpenQA.Selenium.BiDi.Log;
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

            var context = (await _bidi.BrowsingContext.GetTreeAsync())[0].Context;

            TaskCompletionSource<LogEntry> tcs = new();
            await using var subscription = await context.Log.OnEntryAddedAsync(tcs.SetResult);

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
