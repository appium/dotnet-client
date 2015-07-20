using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Appium.PageObjects.Attributes.Abstract;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpenQA.Selenium.Appium.PageObjects
{
    internal class ByFactory
    {
        private static By From(FindsByAttribute attribute)
        {
            var how = attribute.How;
            var usingValue = attribute.Using;
            switch (how)
            {
                case How.Id:
                    return By.Id(usingValue);
                case How.Name:
                    return By.Name(usingValue);
                case How.TagName:
                    return By.TagName(usingValue);
                case How.ClassName:
                    return By.ClassName(usingValue);
                case How.CssSelector:
                    return By.CssSelector(usingValue);
                case How.LinkText:
                    return By.LinkText(usingValue);
                case How.PartialLinkText:
                    return By.PartialLinkText(usingValue);
                case How.XPath:
                    return By.XPath(usingValue);
                case How.Custom:
                    if (attribute.CustomFinderType == null)
                    {
                        throw new ArgumentException("Cannot use How.Custom without supplying a custom finder type");
                    }

                    if (!attribute.CustomFinderType.IsSubclassOf(typeof(By)))
                    {
                        throw new ArgumentException("Custom finder type must be a descendent of the By class");
                    }

                    ConstructorInfo ctor = attribute.CustomFinderType.GetConstructor(new Type[] { typeof(string) });
                    if (ctor == null)
                    {
                        throw new ArgumentException("Custom finder type must expose a public constructor with a string argument");
                    }

                    By finder = ctor.Invoke(new object[] { usingValue }) as By;
                    return finder;
            }

            throw new ArgumentException(string.Format(CultureInfo.InvariantCulture, "Did not know how to construct How from how {0}, using {1}", how, usingValue));
        }

        private static ReadOnlyCollection<By> CreateDefaultLocatorList(MemberInfo member)
        {
            bool useSequence = (Attribute.GetCustomAttribute(member, typeof(FindsBySequenceAttribute), true) != null);
            bool useAll = (Attribute.GetCustomAttribute(member, typeof(FindsByAllAttribute), true) != null);

            if (useSequence && useAll)
            {
                throw new ArgumentException("Cannot specify FindsBySequence and FindsByAll on the same member");
            }

            List<By> bys = new List<By>();
            var attributes = Attribute.GetCustomAttributes(member, typeof(FindsByAttribute), true);

            if (attributes.Length == 0)
            {
                bys.Add(new ByIdOrName(member.Name));
                return bys.AsReadOnly();
            }

            Array.Sort(attributes);
            foreach (var attribute in attributes)
            {
                var castedAttribute = (FindsByAttribute) attribute;
                bys.Add(From(castedAttribute));
            }

            return GetListOfComplexBys(useSequence, useAll, bys).AsReadOnly();
        }

        private static List<By> GetListOfComplexBys(bool useSequence, bool useAll, List<By> bys)
        {
            if (useSequence)
            {
                ByChained chained = new ByChained(bys.ToArray());
                bys.Clear();
                bys.Add(chained);
            }

            if (useAll)
            {
                ByAll all = new ByAll(bys.ToArray());
                bys.Clear();
                bys.Add(all);
            }

            return bys;
        }

        private static ReadOnlyCollection<By> CreateNativeContextLocatorList(MemberInfo member, string platform, string automation)
        {
            if (platform == null)
                return null;

            string upperPlatform = platform.ToUpper();
            string upperAutomation = null;

            bool useSequence = false;
            bool useAll = false;

            MobileFindsBySequenceAttribute sequence = Attribute.GetCustomAttribute(member, typeof(MobileFindsBySequenceAttribute), true) as
                MobileFindsBySequenceAttribute;
            MobileFindsByAllAttribute all = Attribute.GetCustomAttribute(member, typeof(MobileFindsByAllAttribute), true) as
                MobileFindsByAllAttribute;

            if (automation != null)
                upperAutomation = automation.ToUpper();

            if (!upperPlatform.Equals(MobilePlatform.Android.ToUpper()) & ! upperPlatform.Equals(MobilePlatform.IOS.ToUpper()))
                return null;

            Attribute[] attributes = null;
            if (upperPlatform.Equals(MobilePlatform.Android.ToUpper()) & (upperAutomation != null && 
                upperAutomation.Equals(AutomationName.Selendroid.ToUpper()))){
                attributes = Attribute.GetCustomAttributes(member, typeof(FindsBySelendroidAttribute), true);
                if (sequence != null)
                    useSequence = sequence.Selendroid;
                if (all != null)
                    useAll = all.Selendroid;
            }



            if (upperPlatform.Equals(MobilePlatform.Android.ToUpper()) & (upperAutomation == null || !
                upperAutomation.Equals(AutomationName.Selendroid.ToUpper()))) {
                attributes = Attribute.GetCustomAttributes(member, typeof(FindsByAndroidUIAutomatorAttribute), true);
                if (sequence != null)
                    useSequence = sequence.Android;
                if (all != null)
                    useAll = all.Android;
            }

            if (upperPlatform.Equals(MobilePlatform.IOS.ToUpper())) {
                attributes = Attribute.GetCustomAttributes(member, typeof(FindsByIOSUIAutomationAttribute), true);
                if (sequence != null)
                    useSequence = sequence.IOS;
                if (all != null)
                    useAll = all.IOS;
            }

            if (useSequence && useAll)
            {
                throw new ArgumentException("Cannot specify MobileFindsBySequence(DesiredAutomation = true) and MobileFindsByAll(DesiredAutomation = true) " +
                    "on the same member and the same automation");
            }

            if (attributes == null || attributes.Length == 0)
                return null;

            List<By> bys = new List<By>();

            Array.Sort(attributes);
            foreach (var attribute in attributes)
            {
                var castedAttribute = (FindsByMobileAttribute) attribute;
                bys.Add( castedAttribute.By);
            }

            return GetListOfComplexBys(useSequence, useAll, bys).AsReadOnly();

        }


        internal static IEnumerable<By> CreateBys(ISearchContext context, MemberInfo member)
        {
            string platform = GetPlatform(context);
            string automation = GetAutomation(context);
                        
            IEnumerable<By> defaultBys = CreateDefaultLocatorList(member);
            ReadOnlyCollection<By> defaultByList = new List<By>(defaultBys).AsReadOnly();

            IEnumerable<By> nativeBys = CreateNativeContextLocatorList(member, platform, automation);
            IList<By> nativeByList = null;

            if (nativeBys == null)
                nativeByList = defaultByList;
            else
                nativeByList = new List<By>(nativeBys).AsReadOnly();

            Dictionary<ContentTypes, IEnumerable<By>> map = new Dictionary<ContentTypes, IEnumerable<By>>();
            map.Add(ContentTypes.HTML, defaultByList);
            map.Add(ContentTypes.NATIVE, nativeByList);
            ContentMappedBy by = new ContentMappedBy(map);
            List<By> bys = new List<By>();
            bys.Add(by);

            return bys.AsReadOnly();
        }

        private static string GetPlatform(ISearchContext context)
        {
            IWebDriver driver = WebDriverUnpackUtility.UnpackWebdriver(context);

            if (driver == null)
                return null;

            Type driverType = driver.GetType();

            if (GenericsUtility.MatchGenerics(typeof(AndroidDriver<>), AppiumPageObjectMemberDecorator.ListOfAvailableElementTypes, driverType))
                return MobilePlatform.Android;

            if (GenericsUtility.MatchGenerics(typeof(IOSDriver<>), AppiumPageObjectMemberDecorator.ListOfAvailableElementTypes, driverType))
                return MobilePlatform.IOS;

            if (typeof(IHasCapabilities).IsAssignableFrom(driverType))
            {
                IHasCapabilities hasCapabilities = (IHasCapabilities)driver;
                object platform = hasCapabilities.
                    Capabilities.GetCapability(MobileCapabilityType.PlatformName);

                if (platform == null || String.IsNullOrEmpty(Convert.ToString(platform)))
                    platform = hasCapabilities.Capabilities.GetCapability(CapabilityType.Platform);

                string convertedPlatform = Convert.ToString(platform);
                if (platform != null && !String.IsNullOrEmpty(convertedPlatform))
                    return convertedPlatform;
            }
            return null;
        }


        private static string GetAutomation(ISearchContext context)
        {
            IWebDriver driver = WebDriverUnpackUtility.
                 UnpackWebdriver(context);

            if (driver == null)
                return null;

            if (typeof(IHasCapabilities).IsAssignableFrom(driver.GetType()))
            {

                IHasCapabilities hasCapabilities = (IHasCapabilities)driver;
                object automation = hasCapabilities.
                    Capabilities.GetCapability(MobileCapabilityType.AutomationName);

                if (automation == null || String.IsNullOrEmpty(Convert.ToString(automation)))
                    automation = hasCapabilities.Capabilities.GetCapability(CapabilityType.BrowserName);

                string convertedAutomation = Convert.ToString(automation);
                if (automation != null && !String.IsNullOrEmpty(convertedAutomation))
                    return convertedAutomation;
            }
            return null;
        }
    }
}
