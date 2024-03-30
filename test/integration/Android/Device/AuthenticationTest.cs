using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;

namespace Appium.Net.Integration.Tests.Android.Device
{
    public class AuthenticationTest
    {
        private AndroidDriver _driver;
        private readonly string appID = Apps.androidApiDemos;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            
            var capabilities = Caps.GetAndroidUIAutomatorCaps(Apps.Get(appID));
            capabilities.AddAdditionalAppiumOption(MobileCapabilityType.FullReset, true);
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [SetUp]
        public void SetUp()
        {
            _driver?.ActivateApp(Apps.GetId(appID));
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            _ = (_driver?.TerminateApp(Apps.GetId(appID)));
            _driver?.Quit();
        }

        [Test]
        public void TestSendFingerprint()
        {
            // There's no way to verify sending fingerprint had an effect,
            // so just test that it's successfully called without an exception
            _driver.FingerPrint(1);
        }
    }
}
