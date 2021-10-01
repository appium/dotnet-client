using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium.iOS
{
    public class IOSElementFactory : CachedElementFactory<IOSElement>
    {
        public IOSElementFactory(WebDriver parentDriver) : base(parentDriver)
        {
        }

        protected override IOSElement CreateCachedElement(WebDriver parentDriver, string elementId)
        {
            return new IOSElement(parentDriver, elementId);
        }
    }
}
