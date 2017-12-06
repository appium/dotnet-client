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
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;

namespace OpenQA.Selenium.Appium.PageObjects.Interceptors
{
    internal abstract class SearchingInterceptor : IInterceptor
    {
        protected readonly IElementLocator locator;
        protected readonly TimeOutDuration waitingTimeSpan;
        protected readonly IEnumerable<By> bys;
        protected readonly DefaultWait<IElementLocator> waitingForElementList;
        protected static readonly TimeSpan zeroTimeSpan = new TimeSpan(0, 0, 0, 0, 0);
        protected readonly bool shouldCache;
        protected object cached;

        public SearchingInterceptor(IEnumerable<By> bys, IElementLocator locator, TimeOutDuration waitingTimeSpan,
            bool shouldCache)
        {
            this.locator = locator;
            this.waitingTimeSpan = waitingTimeSpan;
            this.shouldCache = shouldCache;
            this.bys = bys;
            waitingForElementList = new DefaultWait<IElementLocator>(locator);
            waitingForElementList.IgnoreExceptionTypes(new Type[] {typeof(StaleElementReferenceException)});
        }

        internal abstract object getTarget(IInvocation invocation);

        internal static Func<IElementLocator, ReadOnlyCollection<IWebElement>> ReturnWaitingFunction(
            IElementLocator locator,
            IEnumerable<By> bys)
        {
            return (IElementLocator) =>
            {
                List<IWebElement> result = new List<IWebElement>();

                foreach (var by in bys)
                {
                    try
                    {
                        List<By> listOfSingleBy = new List<By>();
                        listOfSingleBy.Add(by);
                        result.AddRange(locator.LocateElements(listOfSingleBy));
                    }
                    catch (NoSuchElementException e)
                    {
                        continue;
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
                if (result.Count > 0)
                    return result.AsReadOnly();

                return null;
            };
        }

        public virtual void Intercept(IInvocation invocation)
        {
            MethodInfo method = invocation.Method;
            object[] args = invocation.Arguments;
            object result = method.Invoke(getTarget(invocation), args);
            invocation.ReturnValue = result;
        }
    }
}