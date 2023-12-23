using System;
using System.IO;
using System.Text;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android
{
    class FileInteractionTest
    {
        private AndroidDriver _driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"))
                : Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _driver = new AndroidDriver(serverUri, capabilities, Env.InitTimeoutSec);
            _driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            _driver?.Quit();
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test]
        public void PushStringTest()
        {
            var data =
                "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra";
            _driver.PushFile("/data/local/tmp/remote.txt", data);
            var returnDataBytes = _driver.PullFile("/data/local/tmp/remote.txt");
            var returnedData = Encoding.UTF8.GetString(returnDataBytes);
            Assert.That(returnedData, Is.EqualTo(data));
        }

        [Test]
        public void PushBytesTest()
        {
            var data =
                "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra";
            var bytes = Encoding.UTF8.GetBytes(data);
            var base64 = Convert.ToBase64String(bytes);

            _driver.PushFile("/data/local/tmp/remote.txt", Convert.FromBase64String(base64));
            var returnDataBytes = _driver.PullFile("/data/local/tmp/remote.txt");
            var returnedData = Encoding.UTF8.GetString(returnDataBytes);
            Assert.That(returnedData, Is.EqualTo(data));
        }

        [Test]
        public void PushFileTest()
        {
            var filePath = System.IO.Path.GetTempPath();
            var fileName = Guid.NewGuid().ToString();
            var fullPath = Path.Combine(filePath, fileName);

            File.WriteAllText(fullPath,
                "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra");

            try
            {
                var file = new FileInfo(fullPath);
                _driver.PushFile("/data/local/tmp/remote.txt", file);
                var returnDataBytes = _driver.PullFile("/data/local/tmp/remote.txt");
                var returnedData = Encoding.UTF8.GetString(returnDataBytes);
                Assert.That(
                    returnedData, Is.EqualTo("The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra"));
            }
            finally
            {
                File.Delete(fullPath);
            }
        }
    }
}