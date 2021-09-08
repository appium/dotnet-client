using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.ImageComparison;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;

namespace Appium.Net.Integration.Tests.Windows
{
    public class ImagesComparisonTest
    {
        private WindowsDriver<WindowsElement> _calculatorSession;
        protected static WebElement CalculatorResult;

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var appCapabilities = new AppiumOptions();
            appCapabilities.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
            appCapabilities.AddAdditionalCapability("deviceName", "WindowsPC");
            appCapabilities.AddAdditionalCapability("platformName", "Windows");

            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _calculatorSession = new WindowsDriver<WindowsElement>(serverUri, appCapabilities,
                Env.InitTimeoutSec);
            _calculatorSession.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            CalculatorResult = null;
            _calculatorSession.CloseApp();
            _calculatorSession.Dispose();
            _calculatorSession = null;
        }

        [Test]
        public void SimilarityCalculation()
        {
            var screenshot = _calculatorSession.GetScreenshot();
            var options = new SimilarityMatchingOptions { Visualize = true };

            var similarityResult = _calculatorSession.GetImagesSimilarity(screenshot.AsBase64EncodedString, screenshot.AsBase64EncodedString, options);

            Assert.Greater(similarityResult.Score, 0);
            Assert.IsNotNull(similarityResult.Visualization);
        }

        [Test]
        public void OccurencesLookup()
        {
            var screenshot = _calculatorSession.GetScreenshot();
            var options = new OccurenceMatchingOptions { Visualize = true };

            var occurencesResult = _calculatorSession.FindImageOccurence(screenshot.AsBase64EncodedString, screenshot.AsBase64EncodedString, options);

            Assert.IsNotNull(occurencesResult.Rect);
            Assert.IsNotNull(occurencesResult.Visualization);
        }

        [Test]
        public void FeaturesMatching()
        {
            var screenshot = _calculatorSession.GetScreenshot();
            var options = new FeaturesMatchingOptions {
                Visualize = true,
                DetectorName = "ORB",
                MatchFunc = "BruteForce",
                GoodMatchesFactor = 40
            };

            var occurencesResult = _calculatorSession.MatchImageFeatures(screenshot.AsBase64EncodedString, screenshot.AsBase64EncodedString, options);

            Assert.IsNotNull(occurencesResult.Visualization);
            Assert.Greater(occurencesResult.TotalCount, 0);
            Assert.Greater(occurencesResult.Points1.Count, 0);
            Assert.Greater(occurencesResult.Points2.Count, 0);
            Assert.IsNotNull(occurencesResult.Rect1);
            Assert.IsNotNull(occurencesResult.Rect2);
        }
    }
}
