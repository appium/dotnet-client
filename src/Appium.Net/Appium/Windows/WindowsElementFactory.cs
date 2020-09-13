using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium.Windows
{
    public class WindowsElementFactory : CachedElementFactory<WindowsElement>
    {
        public WindowsElementFactory(RemoteWebDriver parentDriver) : base(parentDriver)
        {
        }

        protected override WindowsElement CreateCachedElement(RemoteWebDriver parentDriver, string elementId)
        {
            return new WindowsElement(parentDriver, elementId);
        }
    }
}
