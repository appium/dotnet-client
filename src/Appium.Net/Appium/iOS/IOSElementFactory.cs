using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium.iOS
{
    public class IOSElementFactory : CachedElementFactory<IOSElement>
    {
        public IOSElementFactory(RemoteWebDriver parentDriver) : base(parentDriver)
        {
        }

        protected override IOSElement CreateCachedElement(RemoteWebDriver parentDriver, string elementId)
        {
            return new IOSElement(parentDriver, elementId);
        }
    }
}
