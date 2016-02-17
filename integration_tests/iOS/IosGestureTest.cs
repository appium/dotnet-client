using Appium.Integration.Tests.Helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Remote;
using System;
using System.Drawing;

namespace Appium.Integration.Tests.iOS
{
    [TestFixture()]
    class iOSGestureTest
    {
        private IOSDriver<IOSElement> driver;

        [TestFixtureSetUp]
        public void BeforeAll()
        {
            DesiredCapabilities capabilities = Caps.getIos82Caps(Apps.get("iosTestApp"));
            if (Env.isSauce())
            {
                capabilities.SetCapability("username", Env.getEnvVar("SAUCE_USERNAME"));
                capabilities.SetCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
                capabilities.SetCapability("name", "ios - complex");
                capabilities.SetCapability("tags", new string[] { "sample" });
            }

            Uri serverUri = Env.isSauce() ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIForIOS;
            driver = new IOSDriver<IOSElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);
            driver.Manage().Timeouts().ImplicitlyWait(Env.IMPLICIT_TIMEOUT_SEC);
        }

		[TestFixtureTearDown]
        public void AfterEach()
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

		[Test()]
		public void TapTest()
		{

			driver.FindElementById ("TextField1").SendKeys ("2");
			driver.FindElementById ("TextField2").SendKeys ("4");

			IOSElement e = driver.FindElementByAccessibilityId("ComputeSumButton");
			driver.Tap(2, e, 2000);
			const string str = "6";
			Assert.AreEqual (driver.FindElementByXPath ("//*[@name = \"Answer\"]").Text, str);
		}

		[Test()]
		public void PinchZoomTest()
		{
            driver.FindElementByName("Test Gesture").Click();
			driver.Zoom(200, 200, 300, 300, 2);
            driver.CloseApp();
            driver.LaunchApp();
		}

        [Test()]
        public void PinchTest()
        {
            driver.FindElementByName("Test Gesture").Click();
            driver.Pinch(200, 200, 100, 100, 2);
            driver.CloseApp();
            driver.LaunchApp();
        }

		[Test()]
		public void SwipeTest()
		{
			IOSElement slider = driver.FindElementByClassName ("UIASlider");
			Point location = slider.Location;
			Size size = slider.Size;

			driver.Swipe (location.X + size.Width / 2, location.Y + size.Height / 2, location.X - 1, location.Y + size.Height / 2, 3000);
			Assert.AreEqual ("0 %", slider.GetAttribute ("value"));
		}
    }
}
