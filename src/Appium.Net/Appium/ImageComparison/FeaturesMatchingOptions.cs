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
            if (!string.IsNullOrEmpty(DetectorName))
            {
                parameters.Add("detectorName", DetectorName);
            }

            if (!string.IsNullOrEmpty(MatchFunc))
            {
                parameters.Add("matchFunc", MatchFunc);
            }

            if (GoodMatchesFactor != null)
            {
                parameters.Add("goodMatchesFactor", GoodMatchesFactor);
            }

            if (Visualize != null)
            {
                parameters.Add("visualize", Visualize);
            }

            return parameters;
        }
    }
}
