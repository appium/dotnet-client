using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
