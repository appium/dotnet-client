using Castle.DynamicProxy;
using OpenQA.Selenium.Internal;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace OpenQA.Selenium.Appium.PageObjects.Interceptors
{
    internal class ElementInterceptor: SearchingInterceptor
    {
        private readonly IWebDriver driver;

        public ElementInterceptor(IEnumerable<By> bys, IElementLocator locator, TimeSpan waitingTimeSpan, bool shouldCache)
            :base(bys, locator, waitingTimeSpan, shouldCache)
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
                timeOuts.ImplicitlyWait(zeroTimeSpan);
                var result = waitingForElementList.Until(ReturnWaitingFunction(locator, bys))[0];
                if (shouldCache && cached == null)
                    cached = result;
                return result;
            }
            catch (WebDriverTimeoutException e)
            {
                throw new NoSuchElementException("Can't locate an element by this strategies: " + bys.ToString());
            }
            finally
            {
                timeOuts.ImplicitlyWait(waitingTimeSpan);
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
