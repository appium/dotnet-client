using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android.Enums;

namespace Appium.Net.Integration.Tests.Android
{
    public class AndroidKeyCodeTest
    {
#pragma warning disable CS0618 // Type or member is obsolete
        [TestCase(nameof(AndroidKeyCode.Keycode_MAX),
            "This constant was deprecated in API level 3. There are now more than MAX_KEYCODE keycodes. Use getMaxKeyCode() instead.")]
        [TestCase(nameof(AndroidKeyCode.FlagWokeHere), "This constant was deprecated in API level 20.")]
#pragma warning restore CS0618
        public void DeprecatedConstant_IsMarkedAsObsolete(string fieldName, string expectedMessage)
        {
            var fieldInfo = typeof(AndroidKeyCode).GetField(fieldName);
            Assert.That(fieldInfo, Is.Not.Null);

            var obsoleteAttribute = Attribute.GetCustomAttribute(fieldInfo, typeof(ObsoleteAttribute)) as ObsoleteAttribute;
            Assert.That(obsoleteAttribute, Is.Not.Null);
            Assert.That(obsoleteAttribute.Message, Is.EqualTo(expectedMessage));
        }
    }
}
