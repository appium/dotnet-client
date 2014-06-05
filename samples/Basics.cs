using System;

using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Appium;

namespace OpenQA.Selenium.Appium.Samples
{
	public class Basics
	{
		public Basics ()
		{
		}

		public void Run (string appPath) {
			// set up the remote web driver
			Console.WriteLine("Connecting to Appium server");
			DesiredCapabilities capabilities = new DesiredCapabilities();
			capabilities.SetCapability("appium-version", "1.0");
			capabilities.SetCapability("platformName", "iOS");
			capabilities.SetCapability("platformVersion", "7.1");
			capabilities.SetCapability("deviceName", "iPhone Simulator");
			capabilities.SetCapability("app", appPath);
			AppiumDriver driver = new AppiumDriver(new Uri("http://127.0.0.1:4723/wd/hub"), capabilities);		
			driver.Quit();
		}


	}
}

