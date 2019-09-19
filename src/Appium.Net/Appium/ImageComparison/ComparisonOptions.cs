using System.Collections.Generic;

namespace OpenQA.Selenium.Appium.ImageComparison
{
    public abstract class ComparisonOptions
    {
        public bool? Visualize { get; set; }

        public abstract Dictionary<string, object> GetParameters();
    }
}
