using NUnit.Framework;
using System;
using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium
{
	[SetUpFixture]
	public class MJsonMethodTestSetUp
	{
		[SetUp]
		public void SetUpTest()
		{
			MJsonMethodTest.server = new FakeAppium ();			 
			MJsonMethodTest.server.Start ();
			MJsonMethodTest.server.respondToInit ();
			DesiredCapabilities capabilities = new DesiredCapabilities();
			MJsonMethodTest.driver = new AppiumDriver (capabilities);
			MJsonMethodTest.server.clear ();

		}

		[TearDown]
		public void TeardownTest()
		{
			MJsonMethodTest.server.Stop ();
		}
	}

	[TestFixture ()]
	public class MJsonMethodTest
	{	
		public static FakeAppium server;
		public static AppiumDriver driver;

		[TearDown]
		public void TeardownTest()
		{
			server.clear ();
		}

		[Test]
		public void ShakeDeviceTestCase ()
		{
			server.respondTo ("POST", "/appium/device/shake", null);
			driver.ShakeDevice ();
		}

		[Test]
		public void LockDeviceTestCase ()
		{
			server.respondTo ("POST", "/appium/device/lock", null);
			driver.LockDevice (3);
		}

		[Test]
		public void ToggleAirplaneModeTestCase ()
		{
			server.respondTo ("POST", "/appium/device/toggle_airplane_mode", null);
			driver.ToggleAirplaneMode ();
		}

	}
}

