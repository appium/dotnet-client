using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.PageObjects
{
    /// <summary>
    /// This class stores the time of the waiting for elements.
    /// It also allows to change this duration in runtime.
    /// </summary>
    public class TimeOutDuration
    {
        
        public TimeOutDuration(TimeSpan span)
        {
            WaitingDuration = span;
        }

        public TimeSpan WaitingDuration
        {
            set; get;
        }
    }
}
