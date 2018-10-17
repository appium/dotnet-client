using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Appium.Integration.Tests.Android
{
    class FileInteractionTest
    {
        private AndroidDriver<IWebElement> driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            AppiumOptions capabilities = Env.ServerIsRemote()
                ? Caps.GetAndroidCaps(Apps.get("androidApiDemos"))
                : Caps.GetAndroidCaps(Apps.get("androidApiDemos"));
            if (Env.ServerIsRemote())
            {
                capabilities.AddAdditionalCapability("username", Env.GetEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.GetEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "android - complex");
                capabilities.AddAdditionalCapability("tags", new string[] {"sample"});
            }
            Uri serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            driver = new AndroidDriver<IWebElement>(serverUri, capabilities, Env.InitTimeoutSec);
            driver.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.ServerIsRemote())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test]
        public void PushStringTest()
        {
            string data =
                "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra";
            driver.PushFile("/data/local/tmp/remote.txt", data);
            byte[] returnDataBytes = driver.PullFile("/data/local/tmp/remote.txt");
            string returnedData = Encoding.UTF8.GetString(returnDataBytes);
            Assert.AreEqual(data, returnedData);
        }

        [Test]
        public void PushBytesTest()
        {
            string data =
                "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra";
            var bytes = Encoding.UTF8.GetBytes(data);
            var base64 = Convert.ToBase64String(bytes);

            driver.PushFile("/data/local/tmp/remote.txt", Convert.FromBase64String(base64));
            byte[] returnDataBytes = driver.PullFile("/data/local/tmp/remote.txt");
            string returnedData = Encoding.UTF8.GetString(returnDataBytes);
            Assert.AreEqual(data, returnedData);
        }

        [Test]
        public void PushFileTest()
        {
            string filePath = System.IO.Path.GetTempPath();
            var fileName = Guid.NewGuid().ToString();
            string fullPath = Path.Combine(filePath, fileName);

            File.WriteAllText(fullPath,
                "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra");

            try
            {
                FileInfo file = new FileInfo(fullPath);
                driver.PushFile("/data/local/tmp/remote.txt", file);
                byte[] returnDataBytes = driver.PullFile("/data/local/tmp/remote.txt");
                string returnedData = Encoding.UTF8.GetString(returnDataBytes);
                Assert.AreEqual(
                    "The eventual code is no more than the deposit of your understanding. ~E. W. Dijkstra",
                    returnedData);
            }
            finally
            {
                File.Delete(fullPath);
            }
        }
    }
}