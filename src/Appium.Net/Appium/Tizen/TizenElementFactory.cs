using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium.Tizen
{
    public class TizenElementFactory : CachedElementFactory<TizenElement>
    {
        public TizenElementFactory(WebDriver parentDriver) : base(parentDriver)
        {
        }

        protected override TizenElement CreateCachedElement(WebDriver parentDriver, string elementId)
        {
            return new TizenElement(parentDriver, elementId);
        }
    }
}
