using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.PageObjects.Attributes
{
    /// <summary>
    /// Marks elements to indicate that each <see cref="FindsByAndroidUIAutomatorAttribute"/> or 
    /// <see cref="FindsByIOSUIAutomationAttribute"/> or <see cref="FindsBySelendroidAttribute"/> on the field or
    /// property should be used in sequence to find the appropriate element.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When used with a set of <see cref="FindsByAndroidUIAutomatorAttribute"/> or 
    /// <see cref="FindsByIOSUIAutomationAttribute"/> or <see cref="FindsBySelendroidAttribute"/>, the criteria are used
    /// in sequence according to the Priority property to find child elements. Note that
    /// the behavior when setting multiple <see cref="FindsByAttribute"/> Priority
    /// properties to the same value, or not specifying a Priority value, is undefined.
    /// </para>
    /// <para>
    /// <code>
    /// // Will find the element with the ID attribute matching "elementId", then will find
    /// // a child element with the ID attribute matching "childElementId".
    /// [MobileFindsBySequence(Android = true)]
    /// [FindsByAndroidUIAutomator(Id = "elementId", Priority = 0)]
    /// [FindsByAndroidUIAutomator(Id = "childElementId", Priority = 1)]
    /// public IWebElement targetElement;
    /// </code>
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false)]
    public class MobileFindsBySequenceAttribute : Attribute
    {
        /// <summary>
        /// If this property is "true" then each <see cref="FindsByAndroidUIAutomatorAttribute"/>  on the field or
        /// property will be used in sequence to find the appropriate element.
        /// </summary>
        [DefaultValue(false)]
        public bool Android { get; set; }

        /// <summary>
        /// If this property is "true" then each <see cref="FindsByIOSUIAutomationAttribute"/>  on the field or
        /// property will be used in sequence to find the appropriate element.
        /// </summary>
        [DefaultValue(false)]
        public bool IOS { get; set; }

        /// <summary>
        /// If this property is "true" then each <see cref="FindsBySelendroidAttribute"/>  on the field or
        /// property will be used in sequence to find the appropriate element.
        /// </summary>
        [DefaultValue(false)]
        public bool Selendroid { get; set; }
    }
}
