using NUnit.Framework;
using System;
using OpenQA.Selenium.Remote;
using System.Collections.Generic;

namespace OpenQA.Selenium.Appium
{
	[TestFixture ()]
	public class ByIosUIAutomationTest
	{	
		public FakeAppium server;
		public AppiumDriver driver;

		[TestFixtureSetUp]
		public void RunBeforeAll(){
			server = new FakeAppium (4743);			 
			server.Start ();
			server.respondToInit ();
			DesiredCapabilities capabilities = new DesiredCapabilities();
			driver = new AppiumDriver (new Uri("http://127.0.0.1:4743/wd/hub"), capabilities);
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
		public void FindElementByIosUIAutomation ()
		{			 
			server.respondTo ("POST", "/element", new Dictionary<string, object>  {
				{"ELEMENT", '5'}
			});
			IWebElement element = driver.FindElementByIosUIAutomation (".elements()");
			server.clear ();
			server.respondTo ("GET", "/element/5/attribute/id", "1234");
			element.GetAttribute ("id");
		}
			
		[Test]
		public void FindElementsByIosUIAutomation ()
		{		
			List<object> results = new List<object>();
			results.Add (new Dictionary<string, object> {{"ELEMENT", "4"}});
			results.Add (new Dictionary<string, object> {{"ELEMENT", "6"}});
			results.Add (new Dictionary<string, object> {{"ELEMENT", "8"}});
			server.respondTo ("POST", "/elements", results);
			ICollection<IWebElement> elements = driver.FindElementsByIosUIAutomation (".elements()");
			Assert.AreEqual (elements.Count, 3); 
		}

		[Test]
		public void ByIosUIAutomation ()
		{			 
			server.respondTo ("POST", "/element", new Dictionary<string, object>  {
				{"ELEMENT", '5'}
			});
			driver.FindElement(new ByIosUIAutomation(".elements()"));
			(new ByIosUIAutomation(".elements()")).FindElement(driver);
			server.clear ();
			List<object> results = new List<object>();
			results.Add (new Dictionary<string, object> {{"ELEMENT", "4"}});
			server.respondTo ("POST", "/elements", results);
			driver.FindElements(new ByIosUIAutomation(".elements()"));
			(new ByIosUIAutomation(".elements()")).FindElements(driver);
		}

	}

	[TestFixture ()]
	public class ByAndroidUIAutomatorTest
	{	
		public FakeAppium server;
		public AppiumDriver driver;

		[TestFixtureSetUp]
		public void RunBeforeAll(){
			server = new FakeAppium (4744);			 
			server.Start ();
			server.respondToInit ();
			DesiredCapabilities capabilities = new DesiredCapabilities();
			driver = new AppiumDriver (new Uri("http://127.0.0.1:4744/wd/hub"), capabilities);
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
		public void FindElementByAndroidUIAutomator ()
		{			 
			server.respondTo ("POST", "/element", new Dictionary<string, object>  {
				{"ELEMENT", '5'}
			});
			IWebElement element = driver.FindElementByAndroidUIAutomator (".elements()");
			server.clear ();
			server.respondTo ("GET", "/element/5/attribute/id", "1234");
			element.GetAttribute ("id");
		}

		[Test]
		public void FindElementsByAndroidUIAutomator ()
		{		
			List<object> results = new List<object>();
			results.Add (new Dictionary<string, object> {{"ELEMENT", "4"}});
			results.Add (new Dictionary<string, object> {{"ELEMENT", "6"}});
			results.Add (new Dictionary<string, object> {{"ELEMENT", "8"}});
			server.respondTo ("POST", "/elements", results);
			ICollection<IWebElement> elements = driver.FindElementsByAndroidUIAutomator (".elements()");
			Assert.AreEqual (elements.Count, 3); 
		}

		[Test]
		public void ByAndroidUIAutomator ()
		{			 
			server.respondTo ("POST", "/element", new Dictionary<string, object>  {
				{"ELEMENT", '5'}
			});
			driver.FindElement(new ByAndroidUIAutomator(".elements()"));
			(new ByAndroidUIAutomator(".elements()")).FindElement(driver);
			server.clear ();
			List<object> results = new List<object>();
			results.Add (new Dictionary<string, object> {{"ELEMENT", "4"}});
			server.respondTo ("POST", "/elements", results);
			driver.FindElements(new ByAndroidUIAutomator(".elements()"));
			(new ByAndroidUIAutomator(".elements()")).FindElements(driver);
		}

	}

}

