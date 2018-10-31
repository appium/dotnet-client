using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenQA.Selenium.Appium
{
    public abstract class CachedElementFactory<T> : RemoteWebElementFactory where T : RemoteWebElement, IWebElementCached
    {
        public CachedElementFactory(RemoteWebDriver parentDriver) : base(parentDriver)
        {
        }

        protected abstract T CreateCachedElement(RemoteWebDriver parentDriver, string elementId);

        public virtual bool CacheElementAttributes
        {
            get
            {
                object compactResponses = ParentDriver.Capabilities.GetCapability("shouldUseCompactResponses");
                return compactResponses != null && Convert.ToBoolean(compactResponses) == false;
            }
        }

        public override RemoteWebElement CreateElement(Dictionary<string, object> elementDictionary)
        {
            string elementId = GetElementId(elementDictionary);
            T cachedElement = CreateCachedElement(ParentDriver, elementId);
            if (CacheElementAttributes)
            {
                cachedElement.SetCacheValues(elementDictionary);
            }
            return cachedElement;
        }
    }
}