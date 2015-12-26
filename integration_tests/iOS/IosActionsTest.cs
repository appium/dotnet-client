using NUnit.Framework;
using System;
using Appium.Integration.Tests.Helpers;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using System.Threading;
using System.Drawing;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Integration.Tests.iOS
{
	[TestFixture ()]
	public class IosActionsTest
	{
		private AppiumDriver<IWebElement> driver;

        [TestFixtureSetUp]
        public void BeforeAll()
        { 
            DesiredCapabilities capabilities = Caps.getIos82Caps (Apps.get("iosTestApp")); 
			if (Env.isSauce ()) {
				capabilities.SetCapability("username", Env.getEnvVar("SAUCE_USERNAME")); 
				capabilities.SetCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
				capabilities.SetCapability("name", "ios - actions");
				capabilities.SetCapability("tags", new string[]{"sample"});
			}
			Uri serverUri = Env.isSauce () ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIForIOS;
			driver = new IOSDriver<IWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);	
			driver.Manage().Timeouts().ImplicitlyWait(Env.IMPLICIT_TIMEOUT_SEC);
            driver.CloseApp();
		}

        [SetUp]
        public void SetUp()
        {
            if (driver != null)
            {
                driver.LaunchApp();
            }
        }

        [TearDown]
        public void TearDowwn()
        {
            if (driver != null)
            {
                driver.CloseApp();
            }
        }

        [TestFixtureTearDown]
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

		[Test ()]
		public void SimpleActionTestCase ()
		{
			IWebElement el = driver.FindElementByAccessibilityId ("ComputeSumButton");
			ITouchAction action = new TouchAction(driver);
			action.Press(el, 10, 10).Release();
			action.Perform ();
		}

		[Test ()]
		public void MultiActionTestCase ()
		{
			IWebElement el = driver.FindElementByAccessibilityId ("ComputeSumButton");
			ITouchAction a1 = new TouchAction(driver);
			a1.Tap(el, 10, 10);
			ITouchAction a2 = new TouchAction(driver);
			a2.Tap(el);
			IMultiAction m = new MultiAction (driver);
			m.Add (a1).Add (a2);
			m.Perform ();
		}

		[Test ()]
		public void SwipeTestCase ()
		{
			driver.FindElementByName ("Test Gesture").Click ();
			Thread.Sleep (1000);

			driver.FindElementByName ("OK").Click ();
			Thread.Sleep (1000);

			Point loc = driver.FindElementByXPath ("//UIAMapView[1]").Location;
			ITouchAction swipe = Actions.Swipe (driver, loc.X, loc.Y, loc.X + 150, loc.Y, 800);
			swipe.Perform ();
		}
	}
}
