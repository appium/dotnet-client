using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.ImageComparison
{
    public class SimilarityMatchingOptions : ComparisonOptions
    {
        public override Dictionary<string, object> GetParameters()
        {
            return new Dictionary<string, object> {
                { "visualize", Visualize }
            };
        }
    }
}
