using System;
using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.ImageComparison
{
    public class SimilarityMatchingResult : ComparisonResult
    {
        public double Score
        {
            get { return Convert.ToDouble(Result["score"]); }
        }

        public SimilarityMatchingResult(Dictionary<string, object> result) : base(result)
        {
        }
    }
}
