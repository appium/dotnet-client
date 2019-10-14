using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.ImageComparison
{
    public class FeaturesMatchingOptions : ComparisonOptions
    {
        /// <summary>
        /// Sets the detector name for features matching algorithm.
        /// Some of these detectors (FAST, AGAST, GFTT, FAST, SIFT and MSER) are not available
        /// in the default OpenCV installation and have to be enabled manually before
        /// library compilation. The default detector name is 'ORB'.
        /// </summary>
        public string DetectorName { get; set; }

        /// <summary>
        /// The name of the matching function.
        /// The default one is 'BruteForce'.
        /// </summary>
        public string MatchFunc { get; set; }

        /// <summary>
        /// The maximum count of "good" matches (e. g. with minimal distances).
        /// </summary>
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
