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
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace OpenQA.Selenium.Appium.PageObjects.Interceptors
{
    class ElementListInterceptor : SearchingInterceptor
    {
        public ElementListInterceptor(IEnumerable<By> bys, IElementLocator locator, TimeOutDuration waitingTimeSpan,
            bool shouldCache)
            : base(bys, locator, waitingTimeSpan, shouldCache)
        {
        }

        private IList convert(ReadOnlyCollection<IWebElement> original, Type genericParameter)
        {
            Type generic = typeof(List<>).MakeGenericType(genericParameter);
            object result = GenericsUtility.CraeteInstanceOfSomeGeneric(typeof(List<>), genericParameter,
                new Type[] { },
                new object[] { });
            IList list = result as IList;

            foreach (var elem in original)
            {
                list.Add(elem);
            }

            return list;
        }

        internal override object getTarget(IInvocation invocation)
        {
            if (cached != null)
                return cached;

            Type genericParameter = invocation.Method.DeclaringType.GetGenericArguments()[0];
            IList result = GenericsUtility.CraeteInstanceOfSomeGeneric(typeof(List<>), genericParameter, new Type[] { },
                new object[] { }) as IList;

            ITimeouts timeOuts = WebDriverUnpackUtility.UnpackWebdriver(locator.SearchContext).Manage().Timeouts();
            try
            {
                timeOuts.ImplicitWait = zeroTimeSpan;
                waitingForElementList.Timeout = waitingTimeSpan.WaitingDuration;
                ReadOnlyCollection<IWebElement>
                    found = waitingForElementList.Until(ReturnWaitingFunction(locator, bys));
                result = convert(found, genericParameter);
            }
            catch (WebDriverTimeoutException ignored)
            {
            }
            finally
            {
                timeOuts.ImplicitWait = waitingTimeSpan.WaitingDuration;
            }

            if (shouldCache && cached == null)
                cached = result;

            return result;
        }
    }
}