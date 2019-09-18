using System;
using System.Collections.Generic;
using System.Drawing;

namespace OpenQA.Selenium.Appium.ImageComparison
{
    public class FeaturesMatchingResult : ComparisonResult
    {
        public int Count
        {
            get { return Convert.ToInt32(Result["count"]); }
        }

        public int Totalcount
        {
            get { return Convert.ToInt32(Result["totalCount"]);  }
        }

        public Point Points1
        {
            get { return ConvertToPoint(Result["points1"]); }
        }

        public Point Points2
        {
            get { return ConvertToPoint(Result["points2"]); }
        }

        public Rectangle Rect1
        {
            get { return ConvertToRect(Result["rect1"]); }
        }

        public Rectangle Rect2
        {
            get { return ConvertToRect(Result["rect2"]); }
        }
    
        public FeaturesMatchingResult(Dictionary<string, object> result) : base(result)
        {
        }
    }
}
