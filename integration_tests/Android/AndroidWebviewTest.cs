using NUnit.Framework;
using System;
using Appium.Integration.Tests.Helpers;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium;
using System.Threading;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;

namespace Appium.Integration.Tests.Android
{
	[TestFixture ()]
	public class AndroidWebviewTest
	{
		private IWebDriver driver;

		[TestFixtureSetUp]
		public void BeforeAll(){
			DesiredCapabilities capabilities = Env.isSauce () ? 
				Caps.getAndroid18Caps (Apps.get ("selendroidTestApp")) :
				Caps.getAndroid19Caps (Apps.get ("selendroidTestApp"));
			if (Env.isSauce ()) {
				capabilities.SetCapability("username", Env.getEnvVar("SAUCE_USERNAME")); 
				capabilities.SetCapability("accessKey", Env.getEnvVar("SAUCE_ACCESS_KEY"));
				capabilities.SetCapability("name", "android - webview");
				capabilities.SetCapability("tags", new string[]{"sample"});
			}
			Uri serverUri = Env.isSauce () ? AppiumServers.sauceURI : AppiumServers.LocalServiceURIAndroid;
            driver = new AndroidDriver<IWebElement>(serverUri, capabilities, Env.INIT_TIMEOUT_SEC);	
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
		public void WebViewTestCase ()
		{
			driver.FindElement(By.Name("buttonStartWebviewCD")).Click ();
			Thread.Sleep (5000);
			if (!Env.isSauce ()) {
				// Contexts don't work in android 4.3.3
				var contexts = ((IContextAware) driver).Contexts;
				string webviewContext = null;
				for (int i = 0; i < contexts.Count; i++) {
					Console.WriteLine (contexts [i]);
					if (contexts [i].Contains ("WEBVIEW")) {
						webviewContext = contexts [i]; 
					}
				}
				Assert.IsNotNull (webviewContext);
                ((IContextAware) driver).Context = webviewContext;
				Assert.IsTrue (driver.PageSource.Contains ("Hello, can you please tell me your name?"));
			}
		}
	}
}

