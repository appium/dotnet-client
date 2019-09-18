using System.Collections.Generic;
using System.Drawing;

namespace OpenQA.Selenium.Appium.ImageComparison
{
    public class OccurenceMatchingResult : ComparisonResult
    {
        public Rectangle Rect
        {
            get { return ConvertToRect(Result["rect"]); }
        }

        public OccurenceMatchingResult(Dictionary<string, object> result) : base(result)
        {
        }
    }
}
