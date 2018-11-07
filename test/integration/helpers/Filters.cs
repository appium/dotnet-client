using System.Collections.Generic;
using OpenQA.Selenium;

namespace Appium.Net.Integration.Tests.helpers
{
    public class Filters
    {
        public static IWebElement FirstWithName<TW>(IList<TW> els, string name) where TW : IWebElement
        {
            for (var i = 0; i < els.Count; i++)
            {
                if (els[i].GetAttribute("name") == name)
                {
                    return (TW) els[i];
                }
            }
            return null;
        }

        public static IList<IWebElement> FilterWithName<TW>(IList<TW> els, string name) where TW : IWebElement
        {
            var res = new List<IWebElement>();
            for (var i = 0; i < els.Count; i++)
            {
                if (els[i].GetAttribute("name") == name)
                {
                    res.Add(els[i]);
                }
            }
            return res;
        }

        public static IList<IWebElement> FilterDisplayed<TW>(IList<TW> els) where TW : IWebElement
        {
            var res = new List<IWebElement>();
            for (var i = 0; i < els.Count; i++)
            {
                IWebElement el = els[i];
                if (els[i].Displayed)
                {
                    res.Add(els[i]);
                }
            }
            return res;
        }
    }
}