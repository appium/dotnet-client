using System;
using System.Collections.Generic;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Interfaces;

namespace Appium.Samples
{
	public class MyActions
	{
		private AppiumDriver driver;

		public MyActions (AppiumDriver driver) {
			this.driver = driver;
		}

		public void Swipe(int startX, int startY, int endX, int endY, 
				int duration) {
			ITouchAction touchAction = new TouchAction(driver) 
				.Press (startX, startY)
				.Wait (duration)
				.MoveTo (endX, endY)
				.Release ();

			touchAction.Perform();
		}
	}
}

