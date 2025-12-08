using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.BiDi;
using System;
using System.Threading.Tasks;

namespace Appium.Net.Integration.Tests.Android
{
    public class BiDiTests
    {
        private AndroidDriver _driver;

        private BiDi _bidi;

        [OneTimeSetUp]
        public async Task BeforeAll()
        {
            var capabilities = Caps.GetAndroidUIAutomatorCaps();
            capabilities.UseWebSocketUrl = true;
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _bidi = await _driver.AsBiDiAsync();
        }

        [Test]
        public async Task RunBiDiScript()
        {
            await _bidi.StatusAsync();
        }

        [Test]
        public async Task ListenLogMessages()
        {
            TaskCompletionSource<bool> tcs = new();

            await _bidi.Log.OnEntryAddedAsync(e => tcs.SetResult(true), new() { Contexts = [new(_bidi, "NATIVE_APP")] });

            Assert.That(() => tcs.Task.Wait(TimeSpan.FromSeconds(3)), Throws.Nothing);
        }

        [OneTimeTearDown]
        public async Task AfterAll()
        {
            if (_bidi != null)
            {
                try
                {
                    await _bidi.DisposeAsync();
                }
                catch { }
                ;
            }
            _driver?.Quit();
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }
    }
}
