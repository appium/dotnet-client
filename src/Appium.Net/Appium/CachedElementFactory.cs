using OpenQA.Selenium.Appium.Interfaces;
using System;
using System.Collections.Generic;

namespace OpenQA.Selenium.Appium
{
    public abstract class CachedElementFactory<T> : WebElementFactory where T : WebElement, IWebElementCached
    {
        public CachedElementFactory(WebDriver parentDriver) : base(parentDriver)
        {
        }

        protected abstract T CreateCachedElement(WebDriver parentDriver, string elementId);

        public virtual bool CacheElementAttributes
        {
            get
            {
                object compactResponses = ParentDriver.Capabilities.GetCapability("shouldUseCompactResponses");
                return compactResponses != null && Convert.ToBoolean(compactResponses) == false;
            }
        }

        public override WebElement CreateElement(Dictionary<string, object> elementDictionary)
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