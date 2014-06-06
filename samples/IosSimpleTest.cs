using NUnit.Framework;
using System;
using Appium.Samples.Helpers;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Threading;
using System.Drawing;

namespace Appium.Samples
{
	[TestFixture ()]
	public class IosSimpleTest
	{
		private AppiumDriver driver;
		private Random rnd = new Random();

		[TestFixtureSetUp]
		public void beforeAll(){
			DesiredCapabilities capabilities = Caps.buildIos71Caps (Apps.iosTestApp); 
			driver = new AppiumDriver(AppiumServers.localURI, capabilities);		
		}

		[TestFixtureTearDown]
		public void afterAll(){
			// shutdown
			driver.Quit();
		}

		private int Populate() {
			IList<string> fields = new List<string> ();
			fields.Add ("IntegerA");
			fields.Add ("IntegerB");
			int sum = 0;
			for (int i = 0; i < fields.Count; i++) {
				IWebElement el = driver.FindElementByName (fields[i]);
				int x = rnd.Next (1, 10);
				el.SendKeys("" + x);
				sum += x;
			}
			return sum;
		}

		[Test ()]
		public void computeSumTestCase ()
		{
			// fill form with random data
			int sumIn = Populate ();

			// compute and check the sum
			driver.FindElementByAccessibilityId ("ComputeSumButton").Click ();
			IWebElement sumEl = driver.FindElementByIosUIAutomation ("elements()[3]");
			int sumOut = Convert.ToInt32 (sumEl.Text);
			Assert.AreEqual (sumIn, sumOut);
			driver.FindElementByName ("Done").Click();
		}
		[Test ()]
		public void swipeTestCase ()
		{
			driver.FindElementByName ("Test Gesture").Click ();
			Thread.Sleep (1000);

			driver.FindElementByName ("OK").Click ();
			Thread.Sleep (1000);

			Point loc = driver.FindElementByXPath ("//UIAMapView[1]").Location;
			MyActions myActions = new MyActions (driver);
			myActions.Swipe (loc.X, loc.Y, loc.X + 150, loc.Y, 800);
		}

	}
}

