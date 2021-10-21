
namespace OpenQA.Selenium.Appium
{
    public class AppiumElementFactory : CachedElementFactory<AppiumElement>
    {
        public AppiumElementFactory(WebDriver parentDriver) : base(parentDriver)
        {
        }

        protected override AppiumElement CreateCachedElement(WebDriver parentDriver, string elementId)
        {
            return new AppiumElement(parentDriver, elementId);
        }
    }
}