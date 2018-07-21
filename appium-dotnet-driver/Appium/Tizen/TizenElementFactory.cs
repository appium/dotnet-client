using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Remote;

namespace OpenQA.Selenium.Appium.Tizen
{
    public class TizenElementFactory : CachedElementFactory<TizenElement>
    {
        public TizenElementFactory(RemoteWebDriver parentDriver) : base(parentDriver)
        {
        }

        protected override TizenElement CreateCachedElement(RemoteWebDriver parentDriver, string elementId)
        {
            return new TizenElement(parentDriver, elementId);
        }
    }
}
