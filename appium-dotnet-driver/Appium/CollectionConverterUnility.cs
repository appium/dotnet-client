using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium
{
    internal class CollectionConverterUnility
    {
        public static ReadOnlyCollection<T> ConvertToExtendedWebElementCollection<T>(IList list) where T : IWebElement
        {
            List<T> result = new List<T>();
            foreach (var element in list)
            {
                result.Add((T) element);
            }
            return result.AsReadOnly();
        }
    }
}
