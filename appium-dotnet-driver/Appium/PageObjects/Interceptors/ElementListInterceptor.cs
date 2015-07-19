using Castle.DynamicProxy;
using OpenQA.Selenium.Appium.PageObjects;
using OpenQA.Selenium.Appium.PageObjects.Interceptors;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.PageFactory.Interceptors
{
    class ElementListInterceptor : SearchingInterceptor
    {
        public ElementListInterceptor(IEnumerable<By> bys, IElementLocator locator, TimeOutDuration waitingTimeSpan, bool shouldCache)
            :base(bys, locator, waitingTimeSpan, shouldCache)
        {}

        private IList convert(ReadOnlyCollection<IWebElement> original, Type genericParameter)
        {
            Type generic = typeof(List<>).MakeGenericType(genericParameter);
            object result = generic.GetConstructor(new Type[] { }).Invoke(new object[] { });
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
            IList result = typeof(List<>).MakeGenericType(genericParameter).
                GetConstructor(new Type[] { }).Invoke(new object[] { }) as IList;


            ITimeouts timeOuts = WebDriverUnpackUtility.UnpackWebdriver(locator.SearchContext).Manage().Timeouts();
            try
            {
                timeOuts.ImplicitlyWait(zeroTimeSpan);
                waitingForElementList.Timeout = waitingTimeSpan.WaitingDuration;                    
                ReadOnlyCollection<IWebElement> found = waitingForElementList.Until(ReturnWaitingFunction(locator, bys));
                result = convert(found, genericParameter);
            }
            catch (WebDriverTimeoutException ignored)
            {}
            finally
            {
                timeOuts.ImplicitlyWait(waitingTimeSpan.WaitingDuration);
            }

            if (shouldCache && cached == null)
                cached = result;

            return result;
        }
    }
}
