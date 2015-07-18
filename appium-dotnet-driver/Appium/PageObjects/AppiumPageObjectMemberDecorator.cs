using Castle.DynamicProxy;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.PageFactory.Interceptors;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Appium.PageObjects.Interceptors;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpenQA.Selenium.Appium.PageObjects
{
    public class AppiumPageObjectMemberDecorator : IPageObjectMemberDecorator
    {
        private static readonly TimeSpan defaultTimeSpan = new TimeSpan(0, 0, 1);
        private readonly TimeSpan timeForWaitingForElements;

        private static List<Type> listAvailableElementTypes;



        private static List<Type> ListAvailableElementTypes
        {
            get
            {
                if (listAvailableElementTypes == null)
                {
                    listAvailableElementTypes = new List<Type>();
                    listAvailableElementTypes.Add(typeof(IWebElement));
                    listAvailableElementTypes.Add(typeof(RemoteWebElement));
                    listAvailableElementTypes.Add(typeof(AppiumWebElement));
                    listAvailableElementTypes.Add(typeof(AndroidElement));
                    listAvailableElementTypes.Add(typeof(IOSElement));
                }

                return listAvailableElementTypes;
            }
        }


        private static bool MatchGenerics(Type generalType, List<Type> possibleParameters, Type targetType)
        {
            foreach (var type in possibleParameters)
            {
                Type fullType = generalType.MakeGenericType(type);
                if (fullType.Equals(targetType))
                    return true;
            }

            return false;
        }


        private static string GetPlatform(ISearchContext context)
        {
            IWebDriver driver = WebDriverUnpackUtility.UnpackWebdriver(context);

            if (driver == null)
                return null;

            Type driverType = driver.GetType();

            if (MatchGenerics(typeof(AndroidDriver<>), ListAvailableElementTypes, driverType))
                return MobilePlatform.Android;

            if (MatchGenerics(typeof(IOSDriver<>), ListAvailableElementTypes, driverType))
                return MobilePlatform.IOS;

            if (typeof(IHasCapabilities).IsAssignableFrom(driverType))
            {
                IHasCapabilities hasCapabilities = (IHasCapabilities) driver;
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

            if (typeof(IHasCapabilities).IsAssignableFrom(driver.GetType())) {

                IHasCapabilities hasCapabilities = (IHasCapabilities) driver;
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

        public AppiumPageObjectMemberDecorator()
            :this(defaultTimeSpan)
        {}

        public AppiumPageObjectMemberDecorator(TimeSpan timeForWaitingForElements)
        {
            this.timeForWaitingForElements = timeForWaitingForElements;
        }


        private static Type GetTypeOfASingleElement(Type targetType, MemberInfo member)
        {
            if (typeof(IWebElement).Equals(targetType))
                return targetType;
            else
                if (MatchGenerics(typeof(IMobileElement<>), ListAvailableElementTypes, targetType))
                    return targetType;

            return null;
        }

        private TimeSpan GetTimeWaitingForElements(MemberInfo member)
        {
            WithTimeSpanAttribute customTimeSpan = (WithTimeSpanAttribute) Attribute.
                GetCustomAttribute(member, typeof(WithTimeSpanAttribute), true);
            if (customTimeSpan != null)
                try
                {
                    return new TimeSpan(0, customTimeSpan.Hours, customTimeSpan.Minutes, 
                        customTimeSpan.Seconds, customTimeSpan.Milliseconds);
                }
                catch (Exception e) 
                {
                    throw new Exception("Exception was thrown while it was tryig to get time of the waiting for elements. Please check the "+
                        member.MemberType.ToString() + " " + member.Name + ".", e);
                }

            return timeForWaitingForElements;               
        }


        public object Decorate(MemberInfo member, IElementLocator locator)
        {
            FieldInfo field = member as FieldInfo;
            PropertyInfo property = member as PropertyInfo;

            Type targetType = null;
            if (field != null)
            {
                targetType = field.FieldType;
            }

            bool hasPropertySet = false;
            if (property != null)
            {
                hasPropertySet = property.CanWrite;
                targetType = property.PropertyType;
            }

            if (field == null & (property == null || !hasPropertySet))
                return null;

            if (field != null && (field.IsStatic || field.IsInitOnly))
                return null;

            Type aSingleElementType = GetTypeOfASingleElement(targetType, member);

            Type aListOfElementsType = null;
            if (MatchGenerics(typeof(IList<>), ListAvailableElementTypes, targetType))
                aListOfElementsType = targetType;

            if (aSingleElementType == null & aListOfElementsType == null)
                return null;

            ISearchContext context = locator.SearchContext;
            string platform = GetPlatform(context);
            string automation = GetAutomation(context);
            
            IEnumerable<By> bys = ByFactory.CreateBys(platform, automation, member);

            ProxyGenerator generator = new ProxyGenerator();
            TimeSpan span = GetTimeWaitingForElements(member);
            bool shouldCache = (Attribute.
                GetCustomAttribute(member, typeof(CacheLookupAttribute), true) != null);

            if (aSingleElementType != null)
                return generator.CreateInterfaceProxyWithoutTarget(aSingleElementType, new Type[] { typeof(IWrapsDriver), typeof(IWrapsElement) },
                    ProxyGenerationOptions.Default, new ElementInterceptor(bys, locator, span, shouldCache));

            return generator.CreateInterfaceProxyWithoutTarget(aListOfElementsType, ProxyGenerationOptions.Default, 
                    new ElementListInterceptor(bys, locator, span, shouldCache));
        }

    }
}
