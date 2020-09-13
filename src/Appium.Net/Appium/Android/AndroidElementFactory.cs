using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium.Android
{
    public class AndroidElementFactory : CachedElementFactory<AndroidElement>
    {
        public AndroidElementFactory(RemoteWebDriver parentDriver) : base(parentDriver)
        {
        }

        protected override AndroidElement CreateCachedElement(RemoteWebDriver parentDriver, string elementId)
        {
            return new AndroidElement(parentDriver, elementId);
        }
    }
}
