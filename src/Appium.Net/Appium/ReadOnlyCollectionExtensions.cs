using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace OpenQA.Selenium.Appium
{
    internal static class ReadOnlyCollectionExtensions
    {
        internal static ReadOnlyCollection<T> AsReadOnly<T>(this IReadOnlyCollection<T> collection)
        {
            if (collection is ReadOnlyCollection<T> readOnlyCollection)
            {
                return readOnlyCollection;
            }
            return new ReadOnlyCollection<T>(collection as IList<T> ?? collection.ToList());
        }
    }
}
