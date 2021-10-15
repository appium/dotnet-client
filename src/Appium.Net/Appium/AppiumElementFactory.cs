
namespace OpenQA.Selenium.Appium
{
    public class AppiumElementFactory : CachedElementFactory<AppiumWebElement>
    {
        public AppiumElementFactory(WebDriver parentDriver) : base(parentDriver)
        {
        }

        protected override AppiumWebElement CreateCachedElement(WebDriver parentDriver, string elementId)
        {
            return new AppiumWebElement(parentDriver, elementId);
        }
    }
}