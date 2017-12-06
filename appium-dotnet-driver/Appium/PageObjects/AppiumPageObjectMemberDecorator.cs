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

using Castle.DynamicProxy;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.iOS;
using OpenQA.Selenium.Appium.PageObjects.Interceptors;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace OpenQA.Selenium.Appium.PageObjects
{
    public class AppiumPageObjectMemberDecorator : IPageObjectMemberDecorator
    {
        private static readonly TimeSpan defaultTimeSpan = new TimeSpan(0, 0, 1);
        private readonly TimeOutDuration timeForWaitingForElements;

        private static List<Type> listAvailableElementTypes;


        internal static List<Type> ListOfAvailableElementTypes
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

        public AppiumPageObjectMemberDecorator()
            : this(new TimeOutDuration(defaultTimeSpan))
        {
        }

        public AppiumPageObjectMemberDecorator(TimeOutDuration timeForWaitingForElements)
        {
            this.timeForWaitingForElements = timeForWaitingForElements;
        }


        private static Type GetTypeOfASingleElement(Type targetType, MemberInfo member)
        {
            if (typeof(IWebElement).Equals(targetType))
                return targetType;
            else if (GenericsUtility.MatchGenerics(typeof(IMobileElement<>), ListOfAvailableElementTypes, targetType))
                return targetType;

            return null;
        }

        private TimeOutDuration GetTimeWaitingForElements(MemberInfo member)
        {
            WithTimeSpanAttribute customTimeSpan =
                (WithTimeSpanAttribute) Attribute.GetCustomAttribute(member, typeof(WithTimeSpanAttribute), true);
            if (customTimeSpan != null)
                try
                {
                    return new TimeOutDuration(new TimeSpan(0, customTimeSpan.Hours, customTimeSpan.Minutes,
                        customTimeSpan.Seconds, customTimeSpan.Milliseconds));
                }
                catch (Exception e)
                {
                    throw new Exception(
                        "Exception was thrown while it was tryig to get time of the waiting for elements. Please check the " +
                        member.MemberType.ToString() + " " + member.Name + ".", e);
                }

            return timeForWaitingForElements;
        }


        public object Decorate(MemberInfo member, IElementLocator locator)
        {
            FieldInfo field = member as FieldInfo;
            PropertyInfo property = member as PropertyInfo;

            Type targetType = null;
            targetType = field?.FieldType;

            bool hasPropertySet = false;
            if (property != null)
            {
                hasPropertySet = property.CanWrite;
                targetType = property?.PropertyType;
            }

            if (field == null & (property == null || !hasPropertySet))
                return null;

            if (field != null && (field.IsStatic || field.IsInitOnly))
                return null;

            Type aSingleElementType = GetTypeOfASingleElement(targetType, member);

            Type aListOfElementsType = null;
            if (GenericsUtility.MatchGenerics(typeof(IList<>), ListOfAvailableElementTypes, targetType))
                aListOfElementsType = targetType;

            if (aSingleElementType == null & aListOfElementsType == null)
                return null;

            ISearchContext context = locator.SearchContext;
            IEnumerable<By> bys = ByFactory.CreateBys(context, member);

            ProxyGenerator generator = new ProxyGenerator();
            TimeOutDuration span = GetTimeWaitingForElements(member);
            bool shouldCache = (Attribute.GetCustomAttribute(member, typeof(CacheLookupAttribute), true) != null);

            if (aSingleElementType != null)
                return generator.CreateInterfaceProxyWithoutTarget(aSingleElementType,
                    new Type[] {typeof(IWrapsDriver), typeof(IWrapsElement)},
                    ProxyGenerationOptions.Default, new ElementInterceptor(bys, locator, span, shouldCache));

            return generator.CreateInterfaceProxyWithoutTarget(aListOfElementsType, ProxyGenerationOptions.Default,
                new ElementListInterceptor(bys, locator, span, shouldCache));
        }
    }
}