using System;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium;
using NUnit.Framework;
using OpenQA.Selenium.Remote;
using Appium.Integration.Tests.Helpers;
using OpenQA.Selenium.Appium.Android;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Appium.Integration.Tests.Android
{
    [TestFixture]
    public class AndroidScreenRecordTest
    {
        private AppiumDriver<IWebElement> driver;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            AppiumOptions capabilities = Env.isSauce()
                ? Caps.getAndroid81Caps(Apps.get("androidApiDemos"))
                : Caps.getAndroid81Caps(Apps.get("androidApiDemos"));
            if (Env.isSauce())
            {
                capabilities.AddAdditionalCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.AddAdditionalCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.AddAdditionalCapability("name", "android - complex");
                capabilities.AddAdditionalCapability("tags", new string[] {"sample"});
            }
            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIAndroid;
            driver = new AndroidDriver<IWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            driver.Manage().Timeouts().ImplicitWait = Env.IMPLICIT_TIMEOUT_SEC;
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.isSauce())
            {
                AppiumServers.StopLocalService();
            }
        }

        [Test]
        public void TestScreenRecord()
        {
            var obj = new AndroidScreenRecordOptions();
            driver.StartRecordingScreen(obj);
            Thread.Sleep(1000);
            String Base64ResponseString = driver.StopRecordingScreen();
            Assert.IsNotEmpty(Base64ResponseString);
            Assert.IsTrue(Validations.IsBase64String(Base64ResponseString), "Response Must be a base64 string");
        }


        [Test]
        public void TestScreenRecordWithOptions()
        {
            var obj = new AndroidScreenRecordOptions()
            {
                VideoSize = "640x480",
                BugReport = "true",
                BitRate = "1"

            };
            driver.StartRecordingScreen(obj);
            Thread.Sleep(1000);
            String Base64ResponseString = driver.StopRecordingScreen();
            Assert.IsNotEmpty(Base64ResponseString);
            Assert.IsTrue(Validations.IsBase64String(Base64ResponseString), "Response Must be a base64 string");
        }

        [Test]
        public void TestScreenRecordOutputToFile()
        {
            var obj = new AndroidScreenRecordOptions();
            driver.StartRecordingScreen(obj);
            Thread.Sleep(2000);
            String Base64ResponseString = driver.StopRecordingScreen();
            byte[] data = Convert.FromBase64String(Base64ResponseString);
            string filePath = Path.GetTempPath();
            var fileName = "TestScreenRecordOutput.mp4";
            string fullPath = Path.Combine(filePath, fileName);
            Console.WriteLine(fullPath);
            try
            {
                File.WriteAllBytes(fullPath, data);
                Assert.IsTrue(File.Exists(fullPath));
                FileInfo outputFileInfo = new FileInfo(fullPath);
                Assert.IsTrue(outputFileInfo.Length > 10000);
            }
            finally
            {
                File.Delete(fullPath);
            }
        }
    }
}