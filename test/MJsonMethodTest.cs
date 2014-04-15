using NUnit.Framework;
using System;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;

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

		[Test]
		public void RotateTestCase ()
		{
			server.respondTo ("POST", "/appium/device/rotate", null);
			Dictionary<string, int> parameters = new Dictionary<string, int> {{"x", 114}, 
				{"y", 198}, {"duration", 5}, {"radius", 3}, {"rotation", 220}, {"touchCount", 2}};
			driver.Rotate (parameters);
		}

		[Test]
		public void GetCurrentActivityTestCase ()
		{
			server.respondTo ("GET", "/appium/device/current_activity", ".activities.PeopleActivity");
			string activity = driver.GetCurrentActivity ();
			Assert.AreEqual (activity, ".activities.PeopleActivity");
		}

		[Test]
		public void InstallAppTestCase ()
		{
			server.respondTo ("POST", "/appium/device/install_app", null);
			driver.InstallApp ("/home/me/apps/superApp");
		}

		[Test]
		public void RemoveAppTestCase ()
		{
			server.respondTo ("POST", "/appium/device/remove_app", null);
			driver.RemoveApp ("rubbish");
		}

		[Test]
		public void IsAppInstalledTestCase ()
		{
			server.respondTo ("POST", "/appium/device/app_installed", true);
			driver.IsAppInstalled ("github");
		}

	}
}

