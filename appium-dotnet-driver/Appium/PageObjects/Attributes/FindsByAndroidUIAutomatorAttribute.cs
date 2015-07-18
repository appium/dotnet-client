using OpenQA.Selenium.Appium.PageObjects.Attributes.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.PageObjects.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = true)]
    public class FindsByAndroidUIAutomatorAttribute : FindsByUIAutomatorsAttribute
    {
        /// <summary>
        /// Sets the target UI automator locator
        /// </summary>
        public String AndroidUIAutomator
        {
            set
            {
                byList.Add(new ByAndroidUIAutomator(value));
            }
            get
            {
                return null;
            }
        }
    }
}
