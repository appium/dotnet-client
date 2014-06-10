using System;
using System.Collections.Generic;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Interfaces;

namespace Appium.Samples
{
	public class Env
	{
		private static bool isTrue(string val) {
			return (val == "true") || (val == "1");
		}

		static public bool isSauce() {
			return isTrue( Environment.GetEnvironmentVariable ("SAUCE") );
		}

		static public bool isDev() {
			return isTrue( Environment.GetEnvironmentVariable ("DEV") );
		}

	}
}

