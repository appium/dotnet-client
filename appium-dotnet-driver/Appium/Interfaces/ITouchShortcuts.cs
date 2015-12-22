//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//See the NOTICE file distributed with this work for additional
//information regarding copyright ownership.
//You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.
namespace OpenQA.Selenium.Appium
{
	public interface ITouchShortcuts
	{
		/// <summary>
		/// Convenience method for tapping the center of an element on the screen
		/// </summary>
		/// <param name="fingers">number of fingers/appendages to tap with</param>
		/// <param name="element">element to tap</param>
		/// <param name="duration">how long between pressing down, and lifting fingers/appendages</param>
		void Tap(int fingers, IWebElement element, int duration);

		/// <summary>
		/// Convenience method for tapping a position on the screen
		/// </summary>
		/// <param name="fingers">number of fingers/appendages to tap with</param>
		/// <param name="x">x coordinate</param>
		/// <param name="y">y coordinate</param>
		/// <param name="duration">how long between pressing down, and lifting fingers/appendages</param>
		void Tap(int fingers, int x, int y, int duration);

		/// <summary>
		/// Convenience method for swiping across the screen
		/// </summary>
		/// <param name="startx">starting x coordinate</param>
		/// <param name="starty">starting y coordinate</param>
		/// <param name="endx">ending x coordinate</param>
		/// <param name="endy">ending y coordinate</param>
		/// <param name="duration">amount of time in milliseconds for the entire swipe action to take</param>
		void Swipe(int startx, int starty, int endx, int endy, int duration);

		/// <summary>
		/// Convenience method for pinching an element on the screen.
		/// "pinching" refers to the action of two appendages Pressing the screen and sliding towards each other.
		/// NOTE:
		/// driver convenience method places the initial touches around the element, if driver would happen to place one of them
		/// off the screen, appium with return an outOfBounds error. In driver case, revert to using the MultiAction api
		/// instead of driver method.
		/// </summary>
		/// <param name="el">The element to pinch</param>
		void Pinch(IWebElement el);

		/// <summary>
		/// Convenience method for pinching an element on the screen.
		/// "pinching" refers to the action of two appendages Pressing the screen and sliding towards each other.
		/// NOTE:
		/// driver convenience method places the initial touches around the element at a distance, if driver would happen to place
		/// one of them off the screen, appium will return an outOfBounds error. In driver case, revert to using the
		/// MultiAction api instead of driver method.
		/// </summary>
		/// <param name="x">x coordinate to terminate the pinch on</param>
		/// <param name="y">y coordinate to terminate the pinch on></param>
		void Pinch(int x, int y);

		/// <summary>
		/// Convenience method for "zooming in" on an element on the screen.
		/// "zooming in" refers to the action of two appendages Pressing the screen and sliding away from each other.
		/// NOTE:
		/// driver convenience method slides touches away from the element, if driver would happen to place one of them
		/// off the screen, appium will return an outOfBounds error. In driver case, revert to using the MultiAction api
		/// instead of driver method.
		/// <param name="x">x coordinate to terminate the zoom on</param>
		/// <param name="y">y coordinate to terminate the zoom on></param>
		/// </summary>
		void Zoom(int x, int y);

		/// <summary>
		/// Convenience method for "zooming in" on an element on the screen.
		/// "zooming in" refers to the action of two appendages Pressing the screen and sliding away from each other.
		/// NOTE:
		/// driver convenience method slides touches away from the element, if driver would happen to place one of them
		/// off the screen, appium will return an outOfBounds error. In driver case, revert to using the MultiAction api
		/// instead of driver method.
		/// <param name="el">The element to pinch</param>
		/// </summary>
		void Zoom(IWebElement el);

	}
}

