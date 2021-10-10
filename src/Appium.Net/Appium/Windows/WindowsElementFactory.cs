using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium.Windows
{
    public class WindowsElementFactory : CachedElementFactory<WindowsElement>
    {
        public WindowsElementFactory(WebDriver parentDriver) : base(parentDriver)
        {
        }

        protected override WindowsElement CreateCachedElement(WebDriver parentDriver, string elementId)
        {
            return new WindowsElement(parentDriver, elementId);
        }
    }
}
