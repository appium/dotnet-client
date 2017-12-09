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
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace OpenQA.Selenium.Appium.PageObjects.Interceptors
{
    internal class ElementInterceptor : SearchingInterceptor
    {
        private readonly IWebDriver driver;

        public ElementInterceptor(IEnumerable<By> bys, IElementLocator locator, TimeOutDuration waitingTimeSpan,
            bool shouldCache)
            : base(bys, locator, waitingTimeSpan, shouldCache)
        {
            driver = WebDriverUnpackUtility.UnpackWebdriver(locator.SearchContext);
        }

        internal override object getTarget(IInvocation ignored)
        {
            if (cached != null)
                return cached;

            ITimeouts timeOuts = WebDriverUnpackUtility.UnpackWebdriver(locator.SearchContext).Manage().Timeouts();
            try
            {
                timeOuts.ImplicitWait = zeroTimeSpan;
                waitingForElementList.Timeout = waitingTimeSpan.WaitingDuration;
                var result = waitingForElementList.Until(ReturnWaitingFunction(locator, bys))[0];
                if (shouldCache && cached == null)
                    cached = result;
                return result;
            }
            catch (WebDriverTimeoutException e)
            {
                string bysString = "";
                foreach (var by in bys)
                    bysString = bysString + by.ToString() + " ";
                throw new NoSuchElementException("Couldn't locate an element by these strategies: " + bysString, e);
            }
            finally
            {
                timeOuts.ImplicitWait = waitingTimeSpan.WaitingDuration;
            }
        }

        public override void Intercept(IInvocation invocation)
        {
            MethodInfo m = invocation.Method;
            if (typeof(IWrapsDriver).IsAssignableFrom(m.DeclaringType))
            {
                invocation.ReturnValue = driver;
                return;
            }

            if (typeof(IWrapsElement).IsAssignableFrom(m.DeclaringType))
            {
                var e = (IWebElement) getTarget(invocation);
                invocation.ReturnValue = e;
                return;
            }

            base.Intercept(invocation);
        }
    }
}