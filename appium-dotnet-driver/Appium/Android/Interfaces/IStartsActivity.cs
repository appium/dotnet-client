using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.Appium.Android
{
    interface IStartsActivity
    {
        void StartActivity(string appPackage, string appActivity, string appWaitPackage = "", string appWaitActivity = "");
    }
}
