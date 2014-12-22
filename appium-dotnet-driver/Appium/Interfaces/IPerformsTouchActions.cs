using OpenQA.Selenium.Appium.MultiTouch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.Interfaces
{
    public interface IPerformsTouchActions
    {
        /// <summary>
        /// Perform the multi action
        /// </summary>
        /// <param name="multiAction">multi action to perform</param>
        void PerformMultiAction(MultiAction multiAction);

        /// <summary>
        /// Perform the touch action
        /// </summary>
        /// <param name="touchAction">touch action to perform</param>
       void PerformTouchAction(TouchAction touchAction);

    }
}
