using NUnit.Framework;
using System;
using Appium.Integration.Tests.Helpers;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Integration.Tests.Android
{
	[TestFixture ()]
	public class AndroidSimpleTest
	{
		private AndroidDriver<AndroidElement> driver;

		[TestFixtureSetUp]
		public void BeforeAll(){
			DesiredCapabilities capabilities = Env.isSauce () ? 
				Caps.getAndroid18Caps (Apps.get ("androidApiDemos")) :
				Caps.getAndroid19Caps (Apps.get ("androidApiDemos"));
			if (Env.isSauce ()) {
				capabilities.SetCapability("username", Env.getEnvVar("SAUCE_USERNAME")); 
				capabilities.SetCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
				capabilities.SetCapability("name", "android - simple");
				capabilities.SetCapability("tags", new string[]{"sample"});
			}
			Uri serverUri = Env.isSauce () ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIAndroid;
            driver = new AndroidDriver<AndroidElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);	
			driver.Manage().Timeouts().ImplicitlyWait(Env.IMPLICIT_TIMEOUT_SEC);
		}

		[TestFixtureTearDown]
		public void AfterAll(){
            if (driver != null)
            {
                driver.Quit();
            }
            if (!Env.isSauce())
            {
                AppiumServers.StopLocalService();
            }
        }

		[Test ()]
		public void FindElementTestCase ()
		{
            By byAccessibilityId = new ByAccessibilityId("Graphics");
            Assert.AreNotEqual(driver.FindElement(byAccessibilityId).Text, null);
            Assert.GreaterOrEqual(driver.FindElements(byAccessibilityId).Count, 1);
			
            driver.FindElementByAccessibilityId ("Graphics").Click ();
			Assert.IsNotNull (driver.FindElementByAccessibilityId ("Arcs"));
			driver.Navigate ().Back ();

            Assert.IsNotNull(driver.FindElementByName("App"));
            
            Assert.IsNotNull(driver.FindElement(new ByAndroidUIAutomator("new UiSelector().clickable(true)")).Text);
			var els = driver.FindElementsByAndroidUIAutomator ("new UiSelector().clickable(true)");
            Assert.GreaterOrEqual(els.Count, 12); 

            var els2 = driver.FindElements(new ByAndroidUIAutomator("new UiSelector().enabled(true)"));
            Assert.GreaterOrEqual(els2.Count, 12);

            els = driver.FindElementsByAndroidUIAutomator("new UiSelector().enabled(true)");
			Assert.GreaterOrEqual (els.Count, 12);
			Assert.IsNotNull (driver.FindElementByXPath ("//android.widget.TextView[@text='API Demos']"));
		}
	}
}

