using NUnit.Framework;
using System;
using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium
{
	[TestFixture ()]
	public class MJsonMethodTest
	{	
		public FakeAppium server;
		public AppiumDriver driver;

		[TestFixtureSetUp]
		public void RunBeforeAll(){
			server = new FakeAppium (4733);			 
			server.Start ();
			server.respondToInit ();
			DesiredCapabilities capabilities = new DesiredCapabilities();
			driver = new AppiumDriver (new Uri("http://127.0.0.1:4733/wd/hub"), capabilities);
			server.clear ();	
		}

		[TestFixtureTearDown]
		public void RunAfterAll(){
			server.Stop ();
		}
			
		[TearDown]
		public void RunAfterEach()
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


		[Test]
		public void SetContextTestCase ()
		{
			server.respondTo ("POST", "/context", null);
			driver.SetContext ("1234");
		}

		[Test]
		public void GetContextTestCase ()
		{
			server.respondTo ("GET", "/context", "1234");
			Assert.AreEqual( driver.GetContext (), "1234");
		}

		[Test]
		public void GetContextsTestCase ()
		{
			server.respondTo ("GET", "/contexts", new string[] {"ab", "cde", "123"});
			Assert.AreEqual( driver.GetContexts (), new string[] {"ab", "cde", "123"});
		}

		[Test]
		public void KeyEventTestCase ()
		{
			server.respondTo ("POST", "/appium/device/keyevent", null);
			driver.KeyEvent ("5");
		}

	}
}

