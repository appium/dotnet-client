using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Appium.Interfaces;

namespace Appium.Samples.Helpers
{
	public class Actions
	{
		public static ITouchAction Swipe(IPerformsTouchActions driver, int startX, int startY, int endX, int endY, 
				int duration) {
			ITouchAction touchAction = new TouchAction(driver) 
				.Press (startX, startY)
				.Wait (duration)
				.MoveTo (endX, endY)
				.Release ();
			return touchAction;
		}
	}
}

