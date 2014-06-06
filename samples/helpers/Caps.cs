using System;
using OpenQA.Selenium.Remote;

namespace Appium.Samples.Helpers
{
	public class Caps
	{
		public static DesiredCapabilities buildIos71Caps (string app) {
			DesiredCapabilities capabilities = new DesiredCapabilities();
			capabilities.SetCapability("appium-version", "1.0");
			capabilities.SetCapability("platformName", "iOS");
			capabilities.SetCapability("platformVersion", "7.1");
			capabilities.SetCapability("deviceName", "iPhone Simulator");
			capabilities.SetCapability("app", app);
			return capabilities;
		}
	}
}

