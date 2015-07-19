using Castle.DynamicProxy;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpenQA.Selenium.Appium.PageObjects.Interceptors
{
    internal abstract class SearchingInterceptor: IInterceptor
    {
        protected readonly IElementLocator locator;
        protected readonly TimeOutDuration waitingTimeSpan;
        protected readonly IEnumerable<By> bys;
        protected readonly DefaultWait<IElementLocator> waitingForElementList;
        protected static readonly TimeSpan zeroTimeSpan = new TimeSpan(0, 0, 0, 0, 0);
        protected readonly bool shouldCache;
        protected object cached;

        public SearchingInterceptor(IEnumerable<By> bys, IElementLocator locator, TimeOutDuration waitingTimeSpan, bool shouldCache)
        {
            this.locator = locator;
            this.waitingTimeSpan = waitingTimeSpan;
            this.shouldCache = shouldCache;
            this.bys = bys;
            waitingForElementList = new DefaultWait<IElementLocator>(locator);
            waitingForElementList.IgnoreExceptionTypes(new Type[] { typeof(StaleElementReferenceException)});
            
        }

        internal abstract object getTarget(IInvocation invocation);

        private static bool IsInvalidSelectorRootCause(Exception e)
        {
            String invalid_selector_pattern = "Invalid locator strategy:";
            if (e == null)
                return false;

            string message = e.Message;

            if (!String.IsNullOrWhiteSpace(message) && message.Contains(invalid_selector_pattern))
                return true;

            return IsInvalidSelectorRootCause(e.InnerException);
        }


        internal static Func<IElementLocator, ReadOnlyCollection<IWebElement>> ReturnWaitingFunction(IElementLocator locator, 
            IEnumerable<By> bys)
        {
            return (IElementLocator) => {
                List<IWebElement> result = new List<IWebElement>();
                try
                {
                    result.AddRange(locator.LocateElements(bys));
                }
                catch (NoSuchElementException e)
                {
                    return null;
                }
                catch (Exception e)
                {
                    if (!IsInvalidSelectorRootCause(e))
                        throw e;
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
