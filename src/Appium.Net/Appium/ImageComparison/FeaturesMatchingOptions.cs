using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.ImageComparison
{
    public class FeaturesMatchingOptions : ComparisonOptions
    {
        public string DetectorName { get; set; }
        public string MatchFunc { get; set; }
        public int GoodMatchesFactor { get; set; }

        public override Dictionary<string, object> GetParameters()
        {
            return new Dictionary<string, object> {
                { "detectorName", DetectorName },
                { "matchFunc", MatchFunc },
                { "goodMatchesFactor", GoodMatchesFactor }
            };
        }
    }
}
