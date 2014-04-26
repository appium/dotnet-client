using NUnit.Framework;
using System;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Appium.MultiTouch;

namespace OpenQA.Selenium.Appium
{
	[TestFixture ()]
	public class TouchTest
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
		public void TouchActionCase ()
		{
//			server.respondTo ("POST", "/element", new Dictionary<string, object>  {
//				{"ELEMENT", '5'}
//			});
//			IWebElement element = driver.FindElementByIosUIAutomation (".elements()");
//			server.respondTo ("POST", "/touch/perform", new Dictionary<string, object>  {
//				{"not sure", '0'}
//			});	
//			//server.respondTo ("POST", "/appium/device/shake", null);
//			Actions a1 = new TouchActions (driver).SingleTap (element);
//			a1.Perform ();
		}
	}
}

