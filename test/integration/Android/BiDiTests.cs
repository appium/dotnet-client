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
        public async Task RunBiDiScript()
        {
            _bidi = await _driver.AsBiDiAsync();
            await _bidi.StatusAsync();
        }

        [OneTimeTearDown]
        public async Task AfterAll()
        {
            if (_bidi != null)
            {
                await _bidi.DisposeAsync();
            }
            _driver?.Quit();
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }
    }
}
