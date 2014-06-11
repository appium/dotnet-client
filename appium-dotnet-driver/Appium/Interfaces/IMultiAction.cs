using System;

namespace OpenQA.Selenium.Appium.Interfaces
{
	public interface IMultiAction
	{
		/// <summary>
		/// Add touch actions to be performed
		/// </summary>
		/// <param name="touchAction"></param>
		IMultiAction Add(ITouchAction touchAction);
		/// <summary>
		/// Cancels the Multi Action
		/// </summary>
		void Cancel();
		/// <summary>
		/// Performs the Multi Action
		/// </summary>
		void Perform();
	}
}
