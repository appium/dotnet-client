using OpenQA.Selenium.Appium.Interfaces;
using System;
using System.Collections.Generic;

namespace Appium.Net.Integration.Tests.helpers
{
    /// <summary>
    /// A testable implementation of IWebElementCached that mimics AppiumElement caching behavior
    /// without requiring a real WebDriver connection.
    /// This class replicates the exact caching logic from AppiumElement for unit testing purposes.
    /// </summary>
    public class TestableAppiumElement : IWebElementCached
    {
        private Dictionary<string, object> _cache;
        private int _serverCallCount;

        public TestableAppiumElement()
        {
            _cache = null;
            _serverCallCount = 0;
        }

        /// <summary>
        /// Gets the number of times a "server call" was simulated (for testing cache misses).
        /// </summary>
        public int ServerCallCount => _serverCallCount;

        /// <summary>
        /// Resets the server call counter to zero.
        /// </summary>
        public void ResetServerCallCount()
        {
            _serverCallCount = 0;
        }

        #region IWebElementCached Implementation

        public void SetCacheValues(Dictionary<string, object> cacheValues)
        {
            _cache = new Dictionary<string, object>(cacheValues);
        }

        public void ClearCache()
        {
            _cache?.Clear();
        }

        public void DisableCache()
        {
            _cache = null;
        }

        #endregion

        #region Cached Properties (mirroring AppiumElement behavior)

        public string TagName => CacheValue("name", () => SimulateServerCall("server-tag-name"))?.ToString();

        public string Text => CacheValue("text", () => SimulateServerCall("server-text"))?.ToString();

        public bool Displayed => Convert.ToBoolean(CacheValue("displayed", () => SimulateServerCall(true)));

        public bool Enabled => Convert.ToBoolean(CacheValue("enabled", () => SimulateServerCall(true)));

        public bool Selected => Convert.ToBoolean(CacheValue("selected", () => SimulateServerCall(false)));

        public System.Drawing.Rectangle Rect
        {
            get
            {
                Dictionary<string, object> rect = null;
                if (_cache != null && _cache.TryGetValue("rect", out var value))
                {
                    rect = value as Dictionary<string, object>;
                }
                if (rect == null)
                {
                    _serverCallCount++;
                    rect = new Dictionary<string, object>
                    {
                        { "x", 0 },
                        { "y", 0 },
                        { "width", 100 },
                        { "height", 50 }
                    };
                    if (_cache != null)
                    {
                        _cache["rect"] = rect;
                    }
                }
                return new System.Drawing.Rectangle(
                    Convert.ToInt32(rect["x"]),
                    Convert.ToInt32(rect["y"]),
                    Convert.ToInt32(rect["width"]),
                    Convert.ToInt32(rect["height"]));
            }
        }

        public string GetAttribute(string attributeName) => CacheValue(
            "attribute/" + attributeName,
            () => SimulateServerCall("server-attribute-value"))?.ToString();

        #endregion

        #region IWebElement Implementation (not used for cache testing)

        public System.Drawing.Point Location => throw new NotImplementedException("Not needed for cache testing");

        public System.Drawing.Size Size => throw new NotImplementedException("Not needed for cache testing");

        public void Clear() => throw new NotImplementedException("Not needed for cache testing");

        public void Click() => throw new NotImplementedException("Not needed for cache testing");

        public OpenQA.Selenium.IWebElement FindElement(OpenQA.Selenium.By by) => throw new NotImplementedException("Not needed for cache testing");

        public System.Collections.ObjectModel.ReadOnlyCollection<OpenQA.Selenium.IWebElement> FindElements(OpenQA.Selenium.By by) => throw new NotImplementedException("Not needed for cache testing");

        public string GetCssValue(string propertyName) => throw new NotImplementedException("Not needed for cache testing");

        public string GetDomAttribute(string attributeName) => throw new NotImplementedException("Not needed for cache testing");

        public string GetDomProperty(string propertyName) => throw new NotImplementedException("Not needed for cache testing");

        public OpenQA.Selenium.ISearchContext GetShadowRoot() => throw new NotImplementedException("Not needed for cache testing");

        public void SendKeys(string text) => throw new NotImplementedException("Not needed for cache testing");

        public void Submit() => throw new NotImplementedException("Not needed for cache testing");

        #endregion

        #region Private Helper Methods (same logic as AppiumElement)

        /// <summary>
        /// Caches the value with the given key, or returns the cached value if it exists.
        /// This is the same logic used in AppiumElement.CacheValue().
        /// </summary>
        private object CacheValue(string key, Func<object> getter)
        {
            if (_cache == null)
            {
                return getter();
            }
            if (!_cache.TryGetValue(key, out var value))
            {
                value = getter();
                _cache.Add(key, value);
            }
            return value;
        }

        /// <summary>
        /// Simulates a server call by incrementing the counter and returning the value.
        /// </summary>
        private object SimulateServerCall(object returnValue)
        {
            _serverCallCount++;
            return returnValue;
        }

        #endregion
    }
}

