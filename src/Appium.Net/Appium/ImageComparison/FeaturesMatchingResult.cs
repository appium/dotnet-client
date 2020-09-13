using System;
using System.Collections.Generic;
using System.Drawing;

namespace OpenQA.Selenium.Appium.ImageComparison
{
    public class FeaturesMatchingResult : ComparisonResult
    {
        /// <summary>
        /// The count of matched edges on both images.
        /// The more matching edges there are no both images the more similar they are.
        /// </summary>
        public int Count => Convert.ToInt32(Result["count"]);

        /// <summary>
        /// The total count of matched edges on both images.
        /// It is equal to `count` if `goodMatchesFactor` does not limit the matches,
        /// otherwise it contains the total count of matches before `goodMatchesFactor` 
        /// is applied.
        /// </summary>
        public int TotalCount => Convert.ToInt32(Result["totalCount"]);

        /// <summary>
        /// The list of matching points on the first image.
        /// </summary>
        public List<Point> Points1 => ConvertToPoint(Result["points1"]);

        /// <summary>
        /// The list of matching points on the second image.
        /// </summary>
        public List<Point> Points2 => ConvertToPoint(Result["points2"]);

        /// <summary>
        /// The bounding rect for the `points1` list or a zero rect if not enough matching points were found.
        /// </summary>
        public Rectangle Rect1 => ConvertToRect(Result["rect1"]);

        /// <summary>
        /// The bounding rect for the `points2` list or a zero rect if not enough matching points were found.
        /// </summary>
        public Rectangle Rect2 => ConvertToRect(Result["rect2"]);

        public FeaturesMatchingResult(Dictionary<string, object> result) : base(result)
        {
        }
    }
}
