using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenQA.Selenium.Appium.iOS
{
    public class IOSElementFactory : CachedElementFactory<IOSElement>
    {
        public IOSElementFactory(RemoteWebDriver parentDriver) : base(parentDriver)
        {
        }

        protected override IOSElement CreateCachedElement(RemoteWebDriver parentDriver, string elementId)
        {
            return new IOSElement(parentDriver, elementId);
        }
    }
}
