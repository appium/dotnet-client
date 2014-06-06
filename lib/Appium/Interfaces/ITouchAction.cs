using System;

namespace OpenQA.Selenium.Appium.Interfaces
{
	public interface ITouchAction
	{
		/// <summary>
		/// Press at the specified location in the element until the  context menu appears.
		/// </summary>
		/// <param name="element">The target element.</param>
		/// <param name="x">The x coordinate relative to the element.</param>
		/// <param name="y">The y coordinate relative to the element.</param>
		/// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
		ITouchAction LongPress (IWebElement el, double? x = null, double? y = null);

		/// <summary>
		/// Press at the specified location until the  context menu appears.
		/// </summary>
		/// <param name="element">The target element.</param>
		/// <param name="x">The x coordinate relative to the element.</param>
		/// <param name="y">The y coordinate relative to the element.</param>
		/// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
		ITouchAction LongPress (double x, double y);

		/// <summary>
		/// Move to the specified location in the element.
		/// </summary>
		/// <param name="element">The target element.</param>
		/// <param name="x">The x coordinate relative to the element.</param>
		/// <param name="y">The y coordinate relative to the element.</param>
		/// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
		ITouchAction MoveTo (IWebElement element, double? x = null, double? y = null);

		/// <summary>
		/// Move to the specified location.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
		ITouchAction MoveTo (double x, double y);

		/// <summary>
		/// Press at the specified location in the element.
		/// </summary>
		/// <param name="element">The target element.</param>
		/// <param name="x">The x coordinate relative to the element.</param>
		/// <param name="y">The y coordinate relative to the element.</param>
		/// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
		ITouchAction Press (IWebElement element, double? x = null, double? y = null);

		/// <summary>
		/// Press at the specified location.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
		ITouchAction Press (double x, double y);

		/// <summary>
		/// Release the pressure.
		/// </summary>
		/// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
		ITouchAction Release ();

		/// <summary>
		/// Tap at the specified location in the element.
		/// </summary>
		/// <param name="element">The target element.</param>
		/// <param name="x">The x coordinate relative to the element.</param>
		/// <param name="y">The y coordinate relative to the element.</param>
		/// <param name="count">The number of times to tap.</param>
		/// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
		ITouchAction Tap (IWebElement element, double? x = null, double? y = null, long? count = null);

		/// <summary>
		/// Tap at the specified location.
		/// </summary>
		/// <param name="x">The x coordinate.</param>
		/// <param name="y">The y coordinate.</param>
		/// <param name="count">The number of times to tap.</param>
		/// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
		ITouchAction Tap (double x, double y, long? count = null);

		/// <summary>
		/// Wait for the given duration.
		/// </summary>
		/// <param name="ms">The amount of time to wait in milliseconds.</param>
		/// <returns>A self-reference to this <see cref="ITouchAction"/>.</returns>
		ITouchAction Wait (long? ms = null);

		void Cancel();

		void Perform();
	}
}
