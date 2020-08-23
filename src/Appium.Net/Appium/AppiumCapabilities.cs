using System.Collections.Generic;
using System.Reflection;
using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium
{
    /// <summary>
    /// Appium capabilities
    /// </summary>
    public class AppiumCapabilities : DesiredCapabilities
    {
        /// <summary>
        /// Get the capabilities back as a dictionary
        ///
        /// This method uses Reflection and should be removed once
        /// AppiumOptions class is avalaible for each driver
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> ToDictionary()
        {
            var bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            FieldInfo capsField = typeof(DesiredCapabilities)
                    .GetField("capabilities", bindingFlags);

            return capsField?.GetValue(this) as Dictionary<string, object>;
        }
    }
}