using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Appium.PageObjects.Attributes.Abstract;
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

        private static ReadOnlyCollection<By> CreateDefaultLocatorList(MemberInfo member, bool useSequence, bool useAll)
        {
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

        private static ReadOnlyCollection<By> CreateNativeContextLocatorList(MemberInfo member, bool useSequence, bool useAll, string platform, string automation)
        {
            if (platform == null)
                return null;

            string upperPlatform = platform.ToUpper();
            string upperAutomation = null;

            if (automation != null)
                upperAutomation = automation.ToUpper();

            if (!upperPlatform.Equals(MobilePlatform.Android.ToUpper()) & ! upperPlatform.Equals(MobilePlatform.IOS.ToUpper()))
                return null;

            Attribute[] attributes = null;
            if (upperPlatform.Equals(MobilePlatform.Android.ToUpper()) & (upperAutomation != null && 
                upperAutomation.Equals(AutomationName.Selendroid.ToUpper()))){
                attributes = Attribute.GetCustomAttributes(member, typeof(FindsBySelendroidAttribute), true);
            }
                


            if (upperPlatform.Equals(MobilePlatform.Android.ToUpper()) & (upperAutomation == null || !
                upperAutomation.Equals(AutomationName.Selendroid.ToUpper())))
                attributes = Attribute.GetCustomAttributes(member, typeof(FindsByAndroidUIAutomatorAttribute), true);

            if (upperPlatform.Equals(MobilePlatform.IOS.ToUpper()))
                attributes = Attribute.GetCustomAttributes(member, typeof(FindsByIOSUIAutomationAttribute), true);

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


        internal static IEnumerable<By> CreateBys(string platform, string automation, MemberInfo member)
        {
            bool useSequence = (Attribute.GetCustomAttribute(member, typeof(FindsBySequenceAttribute), true) != null);
            bool useAll = (Attribute.GetCustomAttribute(member, typeof(FindsByAllAttribute), true) != null);

            if (useSequence && useAll)
            {
                throw new ArgumentException("Cannot specify FindsBySequence and FindsByAll on the same member");
            }

            IEnumerable<By> defaultBys = CreateDefaultLocatorList(member, useSequence, useAll);
            IEnumerable<By> nativeBys = CreateNativeContextLocatorList(member, useSequence, useAll, platform, automation);

            if (nativeBys == null)
                nativeBys = defaultBys;

            Dictionary<ContentTypes, IEnumerable<By>> map = new Dictionary<ContentTypes, IEnumerable<By>>();
            map.Add(ContentTypes.HTML, new List<By>(defaultBys).AsReadOnly());
            map.Add(ContentTypes.NATIVE, new List<By>(nativeBys).AsReadOnly());
            ContentMappedBy by = new ContentMappedBy(map);
            List<By> bys = new List<By>();
            bys.Add(by);

            return bys.AsReadOnly();
        }

    }
}
