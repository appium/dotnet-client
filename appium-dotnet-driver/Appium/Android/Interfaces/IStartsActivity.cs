using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.Android
{
    public interface IStartsActivity
    {
        void StartActivity(string appPackage, string appActivity, string appWaitPackage = "", string appWaitActivity = "");
    }
}
