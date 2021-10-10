using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium.Android
{
    public class AndroidElementFactory : CachedElementFactory<AndroidElement>
    {
        public AndroidElementFactory(WebDriver parentDriver) : base(parentDriver)
        {
        }

        protected override AndroidElement CreateCachedElement(WebDriver parentDriver, string elementId)
        {
            return new AndroidElement(parentDriver, elementId);
        }
    }
}
