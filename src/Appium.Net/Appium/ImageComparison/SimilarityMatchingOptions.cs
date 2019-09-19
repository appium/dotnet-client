using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.ImageComparison
{
    public class SimilarityMatchingOptions : ComparisonOptions
    {
        public override Dictionary<string, object> GetParameters()
        {
            var parameters = new Dictionary<string, object>();

            if (Visualize != null)
            {
                parameters.Add("visualize", Visualize);
            }

            return parameters;
        }
    }
}
