using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
