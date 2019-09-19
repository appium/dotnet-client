using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.ImageComparison
{
    public class OccurenceMatchingOptions : ComparisonOptions
    {
        /// <summary>
        /// At what normalized threshold to reject an occurrence.
        /// Value in range 0..1. 0.5 is the default value.
        /// </summary>
        public double? Threshold { get; set; }

        public override Dictionary<string, object> GetParameters()
        {
            var parameters = new Dictionary<string, object>();

            if (Threshold != null)
            {
                parameters.Add("threshold", Threshold);
            }

            if (Visualize != null)
            {
                parameters.Add("visualize", Visualize);
            }

            return parameters;
        }
    }
}
