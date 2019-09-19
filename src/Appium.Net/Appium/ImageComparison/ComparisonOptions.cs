using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.ImageComparison
{
    public abstract class ComparisonOptions
    {
        /// <summary>
        /// Makes the endpoint to return an image,
        /// which contains the visualized result of the corresponding
        /// picture matching operation. This option is disabled by default.
        /// </summary>
        public bool? Visualize { get; set; }

        public abstract Dictionary<string, object> GetParameters();
    }
}
