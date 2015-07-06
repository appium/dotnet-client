using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.Appium.Interfaces
{
    public interface IScrollsTo<W>
        where W : IWebElement
    {
        /// <summary>
        /// Scroll to an element which contains the given text.
        /// </summary>
        W ScrollTo(String text);

        /// <summary>
        /// Scroll to an element with the given text.
        /// </summary>
        W ScrollToExact(String text);
    }
}
