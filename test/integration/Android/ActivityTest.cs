using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text.Json;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using OpenQA.Selenium.Appium.iOS;


[TestFixture]
public class ActivityTest
{
    [Test]
    public void StartActivityInThisAppTestCase()
    {
        var capabilities = new AppiumOptions();
        capabilities.AutomationName = "xcuitest";
        capabilities.PlatformName = "ios";
        capabilities.DeviceName = "iPhone 16 Plus";
        capabilities.PlatformVersion = "18.1";

        var serverUri = new Uri("http://127.0.0.1:4723/");
        var _driver = new IOSDriver(serverUri, capabilities);

        Console.WriteLine(JsonSerializer.Serialize(_driver.GetEvents()));
        Console.WriteLine(JsonSerializer.Serialize(_driver.GetEvents(type: ["commands"])));

        _driver.LogEvent("appium", "neko");
        _driver.LogEvent("appium", "neko");
        _driver.LogEvent("appium", "neko");

        Console.WriteLine(JsonSerializer.Serialize(_driver.GetEvents()));
        Console.WriteLine(JsonSerializer.Serialize(_driver.GetEvents(type: ["commands"])));
   }
}