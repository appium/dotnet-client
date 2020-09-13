using System.Collections.Generic;
using System.Drawing;

namespace OpenQA.Selenium.Appium.ImageComparison
{
    public class OccurenceMatchingResult : ComparisonResult
    {
        /// <summary>
        /// The region of the partial image occurence on the full image.
        /// </summary>
        public Rectangle Rect => ConvertToRect(Result["rect"]);

        public OccurenceMatchingResult(Dictionary<string, object> result) : base(result)
        {
        }
    }
}
