using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.PageObjects.Attributes.Abstract
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = true)]
    public abstract class FindsByUIAutomatorsAttribute : FindsByMobileAttribute
    {
        /// <summary>
        /// Sets the target accessibility
        /// </summary>
        public String Accessibility
        {
            set
            {
                byList.Add(new ByAccessibilityId(value));
            }
            get
            {
                return null;
            }
        }
    }
}
