using OpenQA.Selenium.Appium.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace OpenQA.Selenium.Appium.PageObjects
{
    internal class ContentMappedBy: By
    {
        private readonly Dictionary<ContentTypes, IEnumerable<By>> map;
        private readonly static String NATIVE_APP_PATTERN = "NATIVE_APP";

        internal ContentMappedBy(Dictionary<ContentTypes, IEnumerable<By>> map)
        {
            this.map = map;
        }

        private IEnumerable<By> GetReturnRelevantBys(ISearchContext context)
        {
		    IWebDriver driver = WebDriverUnpackUtility.UnpackWebdriver(context);
		    if (!typeof(IContextAware).IsAssignableFrom(driver.GetType())) //it is desktop browser 
			    return map[ContentTypes.HTML];

		    IContextAware contextAware = driver as IContextAware;
		    String currentContext = contextAware.Context;
		    if (currentContext.Contains(NATIVE_APP_PATTERN))
			    return map[ContentTypes.NATIVE];

		    return map[ContentTypes.HTML];
	    }


        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            List<IWebElement> result = new List<IWebElement>();
            IEnumerable<By> bys = GetReturnRelevantBys(context);

            foreach (var by in bys)
            {
                ReadOnlyCollection<IWebElement> list = context.FindElements(by);
                result.AddRange(list);
            }

            return result.AsReadOnly();
        }

        private static string ReturnByString(IEnumerable<By> bys)
        {
            String result = "";
            foreach (var by in bys)
            {
                result = result + by.ToString() + "; ";
            }
            return result;
        }
        
        public override string ToString()
        {
            IEnumerable<By> defaultBy = map[ContentTypes.HTML];
            IEnumerable<By> nativeBy = map[ContentTypes.NATIVE];

            if (defaultBy.Equals(nativeBy))
                return ReturnByString(defaultBy);

            return "Locator map: " + "\n" +
                    "- native content: \"" + ReturnByString(nativeBy) + "\" \n" +
                    "- html content: \"" + ReturnByString(defaultBy) + "\"";
        }
    }
}
