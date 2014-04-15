using NUnit.Framework;
using System;
using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium
{
	public static class Env{
		public static FakeAppium server;
		public static AppiumDriver driver;
	}

	[SetUpFixture]
	public class MySetUpClass
	{
		[SetUp]
		public void Setup()
		{
			Env.server = new FakeAppium ();
			Env.server.Start ();
			Env.server.respondToInit ();
			DesiredCapabilities capabilities = new DesiredCapabilities();
			Env.driver = new AppiumDriver (capabilities);
			Env.server.clear ();

		}

		[TearDown]
		public void Teardown()
		{
			Env.server.Stop ();
		}
	}

	[TestFixture ()]
	public class AppiumDriverTest
	{	
		[TearDown]
		public void TeardownTest()
		{
			Env.server.clear ();
		}

		[Test]
		public void ShakeDeviceTestCase ()
		{
			Env.server.respondTo ("POST", "/appium/device/shake", null);
			Env.driver.ShakeDevice ();
		}

		[Test]
		public void LockDeviceTestCase ()
		{
			Env.server.respondTo ("POST", "/appium/device/lock", null);
			Env.driver.LockDevice (3);
		}

		[Test]
		public void ToggleAirplaneModeTestCase ()
		{
			Env.server.respondTo ("POST", "/appium/device/toggle_airplane_mode", null);
			Env.driver.ToggleAirplaneMode ();
		}

	}
}

