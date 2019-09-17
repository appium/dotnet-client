using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.ImageComparison
{
    public class FeaturesMatchingOptions : ComparisonOptions
    {
        public string DetectorName { get; set; }
        public string MatchFunc { get; set; }
        public int? GoodMatchesFactor { get; set; }

        public override Dictionary<string, object> GetParameters()
        {
            var parameters = new Dictionary<string, object>();
            if (!string.IsNullOrWhiteSpace(DetectorName))
            {
                parameters.Add("detectorName", DetectorName);
            }

            if (!string.IsNullOrEmpty(MatchFunc))
            {
                parameters.Add("matchFunc", MatchFunc);
            }

            if (GoodMatchesFactor.HasValue)
            {
                parameters.Add("goodMatchesFactor", GoodMatchesFactor);
            }

            return parameters;
        }
    }
}
