using NUnit.Framework;
using System;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium.Test.Helpers;

namespace OpenQA.Selenium.Appium.Test.Specs
{
	[TestFixture]
	public class InitSessionTest
	{	
		public FakeAppium server;

		[TestFixtureSetUp]
		public void RunBeforeAll(){
			server = new FakeAppium (4723);			 
			server.Start ();
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
		public void InitSessionTestCase ()
		{
			server.respondToInit ();
			DesiredCapabilities capabilities = new DesiredCapabilities();
			new AppiumDriver (capabilities);
		}
	}

	[TestFixture]
	public class EndSessionTest
	{	
		public FakeAppium server;
		public AppiumDriver driver;

		[TestFixtureSetUp]
		public void RunBeforeAll(){
			server = new FakeAppium (4724);			 
			server.Start ();
		}

		[TestFixtureTearDown]
		public void RunAfterAll(){
			server.Stop ();
		}
			
		[SetUp]
		public void RunBeforeEach()
		{
			server.respondToInit ();
			DesiredCapabilities capabilities = new DesiredCapabilities();
			driver = new AppiumDriver (new Uri("http://127.0.0.1:4724/wd/hub"), capabilities);
			server.clear ();
		}


		[TearDown]
		public void RunAfterEach()
		{
			server.clear ();
		}

		[Test]
		public void CloseTestCase ()
		{
			server.respondTo ("DELETE", "/window", null);
			driver.Close ();
		}
			
		[Test]
		public void QuitTestCase ()
		{
			server.respondTo ("DELETE", "/", null);
			driver.Quit ();
		}

	}
}

