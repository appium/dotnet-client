//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//See the NOTICE file distributed with this work for additional
//information regarding copyright ownership.
//You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

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
using System.Reflection;

namespace OpenQA.Selenium.Appium.PageObjects
{
    public class ByFactory
    {
        private static By From(FindsByAttribute attribute)
        {
            Assembly assembly = Assembly.LoadFrom("WebDriver.Support.dll");
            Type seleniumByFactory = assembly.GetType("OpenQA.Selenium.Support.PageObjects.ByFactory");
            MethodInfo m = seleniumByFactory.GetMethod("From", new Type[] {typeof(FindsByAttribute)});
            return (By) m.Invoke(seleniumByFactory, new object[] {attribute});
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
                return null;
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

        private static ReadOnlyCollection<By> CreateNativeContextLocatorList(MemberInfo member, string platform,
            string automation)
        {
            if (platform == null)
                return null;

            string upperPlatform = platform.ToUpper();

            bool useSequence = false;
            bool useAll = false;

            MobileFindsBySequenceAttribute sequence =
                Attribute.GetCustomAttribute(member, typeof(MobileFindsBySequenceAttribute), true) as
                    MobileFindsBySequenceAttribute;
            MobileFindsByAllAttribute all =
                Attribute.GetCustomAttribute(member, typeof(MobileFindsByAllAttribute), true) as
                    MobileFindsByAllAttribute;

            string upperAutomation = automation?.ToUpper();

            if (!upperPlatform.Equals(MobilePlatform.Android.ToUpper()) &
                !upperPlatform.Equals(MobilePlatform.IOS.ToUpper()))
                return null;

            Attribute[] attributes = null;
            if (upperPlatform.Equals(MobilePlatform.Android.ToUpper()) & (upperAutomation != null &&
                                                                          upperAutomation.Equals(AutomationName
                                                                              .Selendroid.ToUpper())))
            {
                attributes = Attribute.GetCustomAttributes(member, typeof(FindsBySelendroidAttribute), true);
                if (sequence != null)
                    useSequence = sequence.Selendroid;
                if (all != null)
                    useAll = all.Selendroid;
            }


            if (upperPlatform.Equals(MobilePlatform.Android.ToUpper()) & (upperAutomation == null || !
                                                                              upperAutomation.Equals(AutomationName
                                                                                  .Selendroid.ToUpper())))
            {
                attributes = Attribute.GetCustomAttributes(member, typeof(FindsByAndroidUIAutomatorAttribute), true);
                if (sequence != null)
                    useSequence = sequence.Android;
                if (all != null)
                    useAll = all.Android;
            }

            if (upperPlatform.Equals(MobilePlatform.IOS.ToUpper()))
            {
                attributes = Attribute.GetCustomAttributes(member, typeof(FindsByIOSUIAutomationAttribute), true);
                if (sequence != null)
                    useSequence = sequence.IOS;
                if (all != null)
                    useAll = all.IOS;
            }

            if (useSequence && useAll)
            {
                throw new ArgumentException(
                    "Cannot specify MobileFindsBySequence(DesiredAutomation = true) and MobileFindsByAll(DesiredAutomation = true) " +
                    "on the same member and the same automation");
            }

            if (attributes == null || attributes.Length == 0)
                return null;

            List<By> bys = new List<By>();

            Array.Sort(attributes);
            foreach (var attribute in attributes)
            {
                var castedAttribute = (FindsByMobileAttribute) attribute;
                bys.Add(castedAttribute.By);
            }

            return GetListOfComplexBys(useSequence, useAll, bys).AsReadOnly();
        }


        public static IEnumerable<By> CreateBys(ISearchContext context, MemberInfo member)
        {
            string platform = GetPlatform(context);
            string automation = GetAutomation(context);

            IEnumerable<By> defaultBys = CreateDefaultLocatorList(member);
            IEnumerable<By> nativeBys = CreateNativeContextLocatorList(member, platform, automation);

            if (defaultBys == null && nativeBys == null)
            {
                List<By> defaultList = new List<By>();
                defaultList.Add(new ByIdOrName(member.Name));

                List<By> nativeList = new List<By>();
                nativeList.Add(By.Id(member.Name));

                defaultBys = defaultList;
                nativeBys = nativeList;
            }

            if (defaultBys == null)
            {
                List<By> defaultList = new List<By>();
                defaultList.Add(new ByIdOrName(member.Name));
                defaultBys = defaultList;
            }

            if (nativeBys == null)
            {
                nativeBys = defaultBys;
            }

            Dictionary<ContentTypes, IEnumerable<By>> map = new Dictionary<ContentTypes, IEnumerable<By>>()
                {[ContentTypes.HTML] = defaultBys, [ContentTypes.NATIVE] = nativeBys};
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

            if (GenericsUtility.MatchGenerics(typeof(AndroidDriver<>),
                AppiumPageObjectMemberDecorator.ListOfAvailableElementTypes, driverType))
                return MobilePlatform.Android;

            if (GenericsUtility.MatchGenerics(typeof(IOSDriver<>),
                AppiumPageObjectMemberDecorator.ListOfAvailableElementTypes, driverType))
                return MobilePlatform.IOS;

            if (typeof(IHasCapabilities).IsAssignableFrom(driverType))
            {
                IHasCapabilities hasCapabilities = (IHasCapabilities) driver;
                object platform = hasCapabilities.Capabilities.GetCapability(MobileCapabilityType.PlatformName);

                if (platform == null || string.IsNullOrEmpty(Convert.ToString(platform)))
                    platform = hasCapabilities.Capabilities.GetCapability(CapabilityType.Platform);

                string convertedPlatform = Convert.ToString(platform);
                if (platform != null && !string.IsNullOrEmpty(convertedPlatform))
                    return convertedPlatform;
            }
            return null;
        }


        private static string GetAutomation(ISearchContext context)
        {
            IWebDriver driver = WebDriverUnpackUtility.UnpackWebdriver(context);

            if (driver == null)
                return null;

            if (typeof(IHasCapabilities).IsAssignableFrom(driver.GetType()))
            {
                IHasCapabilities hasCapabilities = (IHasCapabilities) driver;
                object automation = hasCapabilities.Capabilities.GetCapability(MobileCapabilityType.AutomationName);

                if (automation == null || string.IsNullOrEmpty(Convert.ToString(automation)))
                    automation = hasCapabilities.Capabilities.GetCapability(CapabilityType.BrowserName);

                string convertedAutomation = Convert.ToString(automation);
                if (automation != null && !string.IsNullOrEmpty(convertedAutomation))
                    return convertedAutomation;
            }
            return null;
        }
    }
}