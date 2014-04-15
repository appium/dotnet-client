using NUnit.Framework;
using System;
using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium
{
	[TestFixture ()]
	public class SessionTest
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
		public void TeardownTest()
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
}

