using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.PageObjects.Attributes
{
    /// <summary>
    /// Marks elements to indicate that found elements should match the criteria of
    /// all <see cref="FindsByAndroidUIAutomatorAttribute"/> or <see cref="FindsByIOSUIAutomationAttribute"/>
    /// or <see cref="FindsBySelendroidAttribute"/> on the field or property.
    /// </summary>
    /// <remarks>
    /// <para>
    /// When used with a set of <see cref="MobileFindsByAllAttribute"/>, all criteria must be
    /// matched to be returned. The criteria are used in sequence according to the
    /// Priority property. Note that the behavior when setting multiple
    /// <see cref="FindsByAndroidUIAutomatorAttribute"/> or 
    /// <see cref="FindsByIOSUIAutomationAttribute"/> or <see cref="FindsBySelendroidAttribute"/> Priority properties to the same value, or not
    /// specifying a Priority value, is undefined.
    /// </para>
    /// <para>
    /// <code>
    /// // Will find the element with the class "SomeClass" that also has an ID
    /// // attribute matching "elementId".
    /// [MobileFindsByAll(Android = true)]
    /// [FindsByAndroidUIAutomator(Class = "SomeClass", Priority = 0)]
    /// [FindsByAndroidUIAutomator(Id = "elementId", Priority = 1)]
    /// public IWebElement thisElement;
    /// </code>
    /// </para>
    /// </remarks>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = false)]
    public class MobileFindsByAllAttribute : Attribute
    {
        /// <summary>
        /// If this property is "true" then target elements should match the criteria of
        /// all <see cref="FindsByAndroidUIAutomatorAttribute"/>  on the field or property.
        /// </summary>
        [DefaultValue(false)]
        public bool Android { get; set; }

        /// <summary>
        /// If this property is "true" then target elements should match the criteria of
        /// all <see cref="FindsByIOSUIAutomationAttribute"/>  on the field or property.
        /// </summary>
        [DefaultValue(false)]
        public bool IOS { get; set; }

        /// <summary>
        /// If this property is "true" then target elements should match the criteria of
        /// all <see cref="FindsBySelendroidAttribute"/>  on the field or property.
        /// </summary>
        [DefaultValue(false)]
        public bool Selendroid { get; set; }
    }
}
