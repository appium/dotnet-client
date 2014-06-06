using System;

namespace OpenQA.Selenium.Appium.Interfaces
{
	public interface IMultiAction
	{
		IMultiAction Add(ITouchAction touchAction);
		void Cancel();
		void Perform();
	}
}
