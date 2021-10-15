
namespace OpenQA.Selenium.Appium.Mac
{
    public class MacElementFactory : CachedElementFactory<MacElement>
    {
        public MacElementFactory(WebDriver parentDriver) : base(parentDriver)
        {
        }

        protected override MacElement CreateCachedElement(WebDriver parentDriver, string elementId)
        {
            return new MacElement(parentDriver, elementId);
        }
    }
}
