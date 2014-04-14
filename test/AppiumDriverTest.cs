using NUnit.Framework;
using System;
using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium
{
	[TestFixture ()]
	public class AppiumDriverTest
	{	
		FakeAppium server = new FakeAppium ();
		AppiumDriver driver = null;
		[SetUp]
		public void SetupTest()
		{
			server.Start ();
			server.respondToInit ();
			DesiredCapabilities capabilities = new DesiredCapabilities();
			driver = new AppiumDriver (capabilities);
			server.clear ();
		}

		[TearDown]
		public void TeardownTest()
		{
			server.Stop ();
		}

		[Test]
		public void TestCase ()
		{
			server.respondTo ("POST", "/appium/device/shake", null);
			driver.ShakeDevice ();
		}
	}
}

