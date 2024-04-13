using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.ImageComparison;
using OpenQA.Selenium.Appium.Windows;

namespace Appium.Net.Integration.Tests.Windows
{
    public class ImagesComparisonTests
    {
        private WindowsDriver _calculatorSession;
        protected static WebElement CalculatorResult;
        private readonly string _appId = "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App";

        [OneTimeSetUp]
        public void BeforeAll()
        {
            var appCapabilities = new AppiumOptions
            {
                App = _appId,
                DeviceName = "WindowsPC",
                PlatformName = "Windows",
                AutomationName = "Windows"
            };

            var serverUri = Env.ServerIsRemote() ? AppiumServers.RemoteServerUri : AppiumServers.LocalServiceUri;
            _calculatorSession = new WindowsDriver(serverUri, appCapabilities,
                Env.InitTimeoutSec);
            _calculatorSession.Manage().Timeouts().ImplicitWait = Env.ImplicitTimeoutSec;
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            CalculatorResult = null;
            _calculatorSession?.CloseApp();
            _calculatorSession?.Dispose();
            _calculatorSession = null;
        }

        [Test]
        public void SimilarityCalculation()
        {
            var screenshot = _calculatorSession.GetScreenshot();
            var options = new SimilarityMatchingOptions { Visualize = true };

            var similarityResult = _calculatorSession.GetImagesSimilarity(screenshot.AsBase64EncodedString, screenshot.AsBase64EncodedString, options);

            Assert.Multiple(() =>
            {
                Assert.That(similarityResult.Score, Is.GreaterThan(0));
                Assert.That(similarityResult.Visualization, Is.Not.Null);
            });
        }

        [Test]
        public void OccurencesLookup()
        {
            var screenshot = _calculatorSession.GetScreenshot();
            var options = new OccurenceMatchingOptions { Visualize = true };

            var occurencesResult = _calculatorSession.FindImageOccurence(screenshot.AsBase64EncodedString, screenshot.AsBase64EncodedString, options);

            Assert.Multiple(() =>
            {
                Assert.That(occurencesResult.Rect.IsEmpty, Is.False);
                Assert.That(occurencesResult.Rect.Bottom, Is.GreaterThan(0));
                Assert.That(occurencesResult.Visualization, Is.Not.Null);
            });
        }

        [Test]
        public void FeaturesMatching()
        {
            var screenshot = _calculatorSession.GetScreenshot();
            var options = new FeaturesMatchingOptions
            {
                Visualize = true,
                DetectorName = "ORB",
                MatchFunc = "BruteForce",
                GoodMatchesFactor = 40
            };

            var occurencesResult = _calculatorSession.MatchImageFeatures(screenshot.AsBase64EncodedString, screenshot.AsBase64EncodedString, options);

            Assert.Multiple(() =>
            {
                Assert.That(occurencesResult.Visualization, Is.Not.Null);
                Assert.That(occurencesResult.TotalCount, Is.GreaterThan(0));
                Assert.That(occurencesResult.Points1, Is.Not.Empty);
                Assert.That(occurencesResult.Points2, Is.Not.Empty);
            });
            Assert.Multiple(() =>
            {
                Assert.That(occurencesResult.Rect1.Width, Is.Positive);
                Assert.That(occurencesResult.Rect2.Height, Is.Positive);
            });
        }
    }
}
