using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using System.Collections.Generic;

namespace Appium.Net.Integration.Tests.Element
{
    /// <summary>
    /// Unit tests for AppiumElement caching behavior.
    /// These tests verify client-side caching logic without requiring an Appium server.
    /// </summary>
    public class AppiumElementCacheTest
    {
        private TestableAppiumElement _element;

        [SetUp]
        public void SetUp()
        {
            _element = new TestableAppiumElement();
        }

        [Test]
        public void SetCacheValues_WithValidDictionary_EnablesCache()
        {
            var cacheValues = new Dictionary<string, object>
            {
                { "name", "android.widget.TextView" },
                { "text", "Sample Text" }
            };

            _element.SetCacheValues(cacheValues);

            // Verify cached values are returned (no server call needed)
            Assert.Multiple(() =>
            {
                Assert.That(_element.TagName, Is.EqualTo("android.widget.TextView"));
                Assert.That(_element.Text, Is.EqualTo("Sample Text"));
            });
        }

        [Test]
        public void TagName_WithCacheEnabled_ReturnsCachedValue()
        {
            var expectedTagName = "android.widget.Button";

            _element.SetCacheValues(new Dictionary<string, object>
            {
                { "name", expectedTagName }
            });

            var tagName = _element.TagName;

            Assert.That(tagName, Is.EqualTo(expectedTagName));
        }

        [Test]
        public void Text_WithCacheEnabled_ReturnsCachedValue()
        {
            var expectedText = "Click Me";

            _element.SetCacheValues(new Dictionary<string, object>
            {
                { "text", expectedText }
            });

            var text = _element.Text;

            Assert.That(text, Is.EqualTo(expectedText));
        }

        [Test]
        public void Displayed_WithCacheEnabled_ReturnsCachedValue()
        {
            _element.SetCacheValues(new Dictionary<string, object>
            {
                { "displayed", true }
            });

            var displayed = _element.Displayed;

            Assert.That(displayed, Is.True);
        }

        [Test]
        public void Enabled_WithCacheEnabled_ReturnsCachedValue()
        {
            _element.SetCacheValues(new Dictionary<string, object>
            {
                { "enabled", false }
            });

            var enabled = _element.Enabled;

            Assert.That(enabled, Is.False);
        }

        [Test]
        public void Selected_WithCacheEnabled_ReturnsCachedValue()
        {
            _element.SetCacheValues(new Dictionary<string, object>
            {
                { "selected", true }
            });

            var selected = _element.Selected;

            Assert.That(selected, Is.True);
        }

        [Test]
        public void Rect_WithCacheEnabled_ReturnsCachedValue()
        {
            var rectData = new Dictionary<string, object>
            {
                { "x", 10 },
                { "y", 20 },
                { "width", 100 },
                { "height", 50 }
            };

            _element.SetCacheValues(new Dictionary<string, object>
            {
                { "rect", rectData }
            });

            var rect = _element.Rect;

            Assert.Multiple(() =>
            {
                Assert.That(rect.X, Is.EqualTo(10));
                Assert.That(rect.Y, Is.EqualTo(20));
                Assert.That(rect.Width, Is.EqualTo(100));
                Assert.That(rect.Height, Is.EqualTo(50));
            });
        }

        [Test]
        public void GetAttribute_WithCacheEnabled_ReturnsCachedValue()
        {
            var attributeName = "content-desc";
            var expectedValue = "Graphics Button";

            _element.SetCacheValues(new Dictionary<string, object>
            {
                { "attribute/" + attributeName, expectedValue }
            });

            var attributeValue = _element.GetAttribute(attributeName);

            Assert.That(attributeValue, Is.EqualTo(expectedValue));
        }

        [Test]
        public void ClearCache_AfterSettingValues_ClearsAllCachedValues()
        {
            _element.SetCacheValues(new Dictionary<string, object>
            {
                { "name", "cachedTagName" },
                { "text", "cachedText" }
            });

            // Verify values are cached
            Assert.That(_element.TagName, Is.EqualTo("cachedTagName"));

            // Clear cache
            _element.ClearCache();

            // Re-populate cache with different values to verify clear worked
            _element.SetCacheValues(new Dictionary<string, object>
            {
                { "name", "newTagName" }
            });

            Assert.That(_element.TagName, Is.EqualTo("newTagName"));
        }

        [Test]
        public void DisableCache_AfterSettingValues_DisablesCaching()
        {
            _element.SetCacheValues(new Dictionary<string, object>
            {
                { "name", "cachedTagName" }
            });

            // Verify cache is working
            Assert.That(_element.TagName, Is.EqualTo("cachedTagName"));

            // Disable cache
            _element.DisableCache();

            // Re-enable cache with new values
            _element.SetCacheValues(new Dictionary<string, object>
            {
                { "name", "newCachedTagName" }
            });

            Assert.That(_element.TagName, Is.EqualTo("newCachedTagName"));
        }

        [Test]
        public void SetCacheValues_CalledMultipleTimes_ReplacesCache()
        {
            _element.SetCacheValues(new Dictionary<string, object>
            {
                { "name", "firstTagName" }
            });

            Assert.That(_element.TagName, Is.EqualTo("firstTagName"));

            _element.SetCacheValues(new Dictionary<string, object>
            {
                { "name", "secondTagName" }
            });

            Assert.That(_element.TagName, Is.EqualTo("secondTagName"));
        }

        [Test]
        public void CacheValues_WithMultipleProperties_AllReturnCorrectly()
        {
            _element.SetCacheValues(new Dictionary<string, object>
            {
                { "name", "android.widget.EditText" },
                { "text", "Enter text here" },
                { "displayed", true },
                { "enabled", true },
                { "selected", false }
            });

            Assert.Multiple(() =>
            {
                Assert.That(_element.TagName, Is.EqualTo("android.widget.EditText"));
                Assert.That(_element.Text, Is.EqualTo("Enter text here"));
                Assert.That(_element.Displayed, Is.True);
                Assert.That(_element.Enabled, Is.True);
                Assert.That(_element.Selected, Is.False);
            });
        }

        [Test]
        public void ClearCache_WhenCacheIsNull_DoesNotThrow()
        {
            // Element starts with null cache, clearing should not throw
            Assert.DoesNotThrow(() => _element.ClearCache());
        }

        [Test]
        public void DisableCache_WhenCacheIsNull_DoesNotThrow()
        {
            // Element starts with null cache, disabling should not throw
            Assert.DoesNotThrow(() => _element.DisableCache());
        }

        [Test]
        public void TagName_WithCacheEnabled_DoesNotCallServerWhenCached()
        {
            _element.SetCacheValues(new Dictionary<string, object>
            {
                { "name", "cachedTagName" }
            });

            // Access multiple times
            _ = _element.TagName;
            _ = _element.TagName;
            _ = _element.TagName;

            // Should not have made any server calls since value was in cache
            Assert.That(_element.ServerCallCount, Is.EqualTo(0));
        }

        [Test]
        public void TagName_WithCacheDisabled_CallsServerEveryTime()
        {
            // Access multiple times without cache
            _ = _element.TagName;
            _ = _element.TagName;
            _ = _element.TagName;

            // Should have made 3 server calls
            Assert.That(_element.ServerCallCount, Is.EqualTo(3));
        }

        [Test]
        public void TagName_WithEmptyCache_CallsServerOnceAndCaches()
        {
            // Enable cache with empty dictionary
            _element.SetCacheValues(new Dictionary<string, object>());

            // First access should call server
            _ = _element.TagName;
            Assert.That(_element.ServerCallCount, Is.EqualTo(1));

            // Subsequent accesses should use cache
            _ = _element.TagName;
            _ = _element.TagName;
            Assert.That(_element.ServerCallCount, Is.EqualTo(1));
        }
    }
}
