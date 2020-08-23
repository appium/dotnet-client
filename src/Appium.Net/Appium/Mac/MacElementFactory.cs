using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium.Mac
{
    public class MacElementFactory : CachedElementFactory<MacElement>
    {
        public MacElementFactory(RemoteWebDriver parentDriver) : base(parentDriver)
        {
        }

        protected override MacElement CreateCachedElement(RemoteWebDriver parentDriver, string elementId)
        {
            return new MacElement(parentDriver, elementId);
        }
    }
}
