using NUnit.Framework;
using System;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using OpenQA.Selenium.Appium.Test.Helpers;

namespace OpenQA.Selenium.Appium.Test.Specs
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
			driver.KeyEvent(5);
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
		public void ElementRotateTestCase ()
		{
			server.respondTo ("POST", "/element", new Dictionary<string, object>  {
				{"ELEMENT", '5'}
			});
			AppiumWebElement element = (AppiumWebElement) driver.FindElementByIosUIAutomation (".elements()");
			server.clear ();
			server.respondTo ("POST", "/appium/device/rotate", null);
			Dictionary<string, int> parameters = new Dictionary<string, int> {{"x", 114}, 
				{"y", 198}, {"duration", 5}, {"radius", 3}, {"rotation", 220}, {"touchCount", 2}};
			element.Rotate (parameters);
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

		[Test]
		public void PushFileTestCase ()
		{
			server.respondTo ("POST", "/appium/device/push_file", null);
			driver.PushFile ("/pictures/me.jpg", "abde433qsawe3242");
		}

		[Test]
		public void PullFileTestCase ()
		{
			var data = "wqewdsf232wewqqw443";
			server.respondTo ("POST", "/appium/device/pull_file", data);
			string result = driver.PullFile ("/pictures/me.jpg");
			Assert.AreEqual (result, data);
		}

		[Test]
		public void ToggleWifiTestCase ()
		{
			server.respondTo ("POST", "/appium/device/toggle_wifi", null);
			driver.ToggleWifi ();
		}

		[Test]
		public void ToggleLocationServicesTestCase ()
		{
			server.respondTo ("POST", "/appium/device/toggle_location_services", null);
			driver.ToggleLocationServices ();
		}

		[Test]
		public void LaunchAppTestCase ()
		{
			server.respondTo ("POST", "/appium/app/launch", null);
			driver.LaunchApp ();
		}

		[Test]
		public void CloseAppTestCase ()
		{
			server.respondTo ("POST", "/appium/app/close", null);
			driver.CloseApp ();
		}

		[Test]
		public void ResetAppTestCase ()
		{
			server.respondTo ("POST", "/appium/app/reset", null);
			driver.ResetApp ();
		}

		[Test]
		public void BackgroundAppTestCase ()
		{
			server.respondTo ("POST", "/appium/app/background", null);
			driver.BackgroundApp (5);
		}

		[Test]
		public void EndTestCoverageTestCase ()
		{
			var data = "21343n2312j3jw";
			server.respondTo ("POST", "/appium/app/end_test_coverage", data);
			var result = driver.EndTestCoverage ("android.intent.action.BOOT_COMPLETED", "/random/path");
			Assert.AreEqual (result, data);
		}

		[Test]
		public void GetAppStringsTestCase ()
		{
			var data = "21343n2312j3jw";
			server.respondTo ("POST", "/appium/app/strings", data);
			var result = driver.GetAppStrings ();
			Assert.AreEqual (result, data);
		}

		[Test]
		public void SetImmediateValueTestCase ()
		{
			server.respondTo ("POST", "/element", new Dictionary<string, object>  {
				{"ELEMENT", '5'}
			});
			AppiumWebElement element = (AppiumWebElement) driver.FindElementByIosUIAutomation (".elements()");
			server.clear ();
			server.respondTo ("POST", "/appium/element/5/value", null);
			element.SetImmediateValue ("123");
		}

		[Test]
		public void HideKeyboardTestCase ()
		{
			{
				server.clear ();
				RequestProcessor re = server.respondTo ("POST", "/appium/device/hide_keyboard", null);
				driver.HideKeyboard (key: "Done");
				Assert.AreEqual (re.inputData, "{\"keyName\":\"Done\"}");
			}
			{
				server.clear ();
				RequestProcessor re = server.respondTo ("POST", "/appium/device/hide_keyboard", null);
				driver.HideKeyboard ("pressKey", "Done");
				Assert.AreEqual (re.inputData, "{\"strategy\":\"pressKey\",\"keyName\":\"Done\"}");
			}
			{
				server.clear ();
				RequestProcessor re = server.respondTo ("POST", "/appium/device/hide_keyboard", null);
				driver.HideKeyboard ("tapOutside");
				Assert.AreEqual (re.inputData, "{\"strategy\":\"tapOutside\"}");
			}
		}

		[Test]
		public void SettingsTestCase ()
		{
			{
				var data = "{\"setting\": true}";
				Dictionary<String, Object> simpleDict = new Dictionary<string, object> ();
				simpleDict.Add ("setting", true);
				server.respondTo ("GET", "/appium/settings", data);
				var result = driver.GetSettings ();
				Assert.AreEqual (result, simpleDict);
			}
			{
				RequestProcessor re = server.respondTo ("POST", "/appium/settings", null);
				driver.IgnoreUnimportantViews (true);
				Assert.AreEqual (re.inputData, "{\"settings\":{\"ignoreUnimportantViews\":true}}");
			}
		}

	}
}

