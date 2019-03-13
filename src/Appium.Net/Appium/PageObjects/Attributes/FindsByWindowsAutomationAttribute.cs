using System;
using OpenQA.Selenium.Appium.PageObjects.Attributes.Abstract;

namespace OpenQA.Selenium.Appium.PageObjects.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class, AllowMultiple = true)]
    public class FindsByWindowsAutomationAttribute : FindsByUIAutomatorsAttribute
    {
    }
}
