using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.PageObjects.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false)]
    public class WithTimeSpanAttribute : Attribute
    {
        [DefaultValue(0)]
        public int Hours { get; set; }

        [DefaultValue(0)]
        public int Minutes { get; set; }

        public int Seconds { get; set; }

        [DefaultValue(0)]
        public int Milliseconds { get; set; }
    }
}
