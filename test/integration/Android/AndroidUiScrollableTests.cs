using System;
using NUnit.Framework;
using OpenQA.Selenium.Appium.Android.Enums;
using OpenQA.Selenium.Appium.Android.UiAutomator;

namespace Appium.Net.Integration.Tests.Android
{
    public class AndroidUiScrollableTests
    {
        private AndroidUiScrollable _sut;
        private const string ScrollableCtor = "new UiScrollable(new UiSelector().scrollable(true))";

        [SetUp]
        public void Setup()
        {
            _sut = new AndroidUiScrollable();
        }

        [Test]
        public void NewScrollablesStartWithEmptyConstructorCall()
        {
            var statement = _sut.Build();
            Assert.AreEqual(ScrollableCtor, statement);
        }

        [Test]
        public void NewScrollableUsesCustomScrollContainerSelector()
        {
            var selector = new AndroidUiSelector().Instance(7);
            var statement = new AndroidUiScrollable(selector).Build();
            Assert.AreEqual("new UiScrollable(new UiSelector().instance(7))", statement);
        }

        [Test]
        public void FlingBackwardAddsCorrectCallToStatement()
        {
            var statement = _sut.FlingBackward().Build();
            Assert.AreEqual(ScrollableCtor + ".flingBackward()", statement);
        }

        [Test]
        public void FlingForwardAddsCorrectCallToStatement()
        {
            var statement = _sut.FlingForward().Build();
            Assert.AreEqual(ScrollableCtor + ".flingForward()", statement);
        }

        [Test]
        public void FlingToBeginningAddsCorrectCallToStatement()
        {
            var statement = _sut.FlingToBeginning(45).Build();
            Assert.AreEqual(ScrollableCtor + ".flingToBeginning(45)", statement);
        }

        [Test]
        public void FlingToEndAddsCorrectCallToStatement()
        {
            var statement = _sut.FlingToEnd(77).Build();
            Assert.AreEqual(ScrollableCtor + ".flingToEnd(77)", statement);
        }

        [Test]
        public void GetChildByDescriptionAddsCorrectCallToStatement()
        {
            var statement = _sut.GetChildByDescription(new AndroidUiSelector(), "Hello World", false).Build();
            Assert.AreEqual(ScrollableCtor + ".getChildByDescription(new UiSelector(), \"Hello World\", false)", statement);
        }

        [Test]
        public void GetChildByInstanceAddsCorrectCallToStatement()
        {
            var statement = _sut.GetChildByInstance(new AndroidUiSelector(), 9).Build();
            Assert.AreEqual(ScrollableCtor + ".getChildByInstance(new UiSelector(), 9)", statement);
        }

        [Test]
        public void GetChildByTextAddsCorrectCallToStatement()
        {
            var statement = _sut.GetChildByText(new AndroidUiSelector(), "Help", false).Build();
            Assert.AreEqual(ScrollableCtor + ".getChildByText(new UiSelector(), \"Help\", false)", statement);
        }

        [Test]
        public void ScrollBackwardAddsCorrectCallToStatement()
        {
            var statement = _sut.ScrollBackward(32).Build();
            Assert.AreEqual(ScrollableCtor + ".scrollBackward(32)", statement);
        }

        [Test]
        public void ScrollDescriptionIntoViewAddsCorrectCallToStatement()
        {
            var statement = _sut.ScrollDescriptionIntoView("Description Here").Build();
            Assert.AreEqual(ScrollableCtor + ".scrollDescriptionIntoView(\"Description Here\")", statement);
        }

        [Test]
        public void ScrollForwardAddsCorrectCallToStatement()
        {
            var statement = _sut.ScrollForward(46).Build();
            Assert.AreEqual(ScrollableCtor + ".scrollForward(46)", statement);
        }

        [Test]
        public void ScrollIntoViewAddsCorrectCallToStatement()
        {
            var statement = _sut.ScrollIntoView(new AndroidUiSelector()).Build();
            Assert.AreEqual(ScrollableCtor + ".scrollIntoView(new UiSelector())", statement);
        }

        [Test]
        public void ScrollTextIntoViewAddsCorrectCallToStatement()
        {
            var statement = _sut.ScrollTextIntoView("Some Text").Build();
            Assert.AreEqual(ScrollableCtor + ".scrollTextIntoView(\"Some Text\")", statement);
        }

        [Test]
        public void ScrollToBeginningAddsCorrectCallToStatement()
        {
            var statement = _sut.ScrollToBeginning(12, 90).Build();
            Assert.AreEqual(ScrollableCtor + ".scrollToBeginning(12, 90)", statement);
        }

        [Test]
        public void ScrollToEndAddsCorrectCallToStatement()
        {
            var statement = _sut.ScrollToEnd(32, 51).Build();
            Assert.AreEqual(ScrollableCtor + ".scrollToEnd(32, 51)", statement);
        }

        [Test]
        public void SetScrollDirectionVerticalAddsCorrectCallToStatement()
        {
            var statement = _sut.SetScrollDirection(ListDirection.Vertical).Build();
            Assert.AreEqual(ScrollableCtor + ".setAsVerticalList()", statement);
        }

        [Test]
        public void SetScrollDirectionHorizontalAddsCorrectCallToStatement()
        {
            var statement = _sut.SetScrollDirection(ListDirection.Horizontal).Build();
            Assert.AreEqual(ScrollableCtor + ".setAsHorizontalList()", statement);
        }

        [Test]
        public void SetMaxSearchSwipesAddsCorrectCallToStatement()
        {
            var statement = _sut.SetMaxSearchSwipes(80).Build();
            Assert.AreEqual(ScrollableCtor + ".setMaxSearchSwipes(80)", statement);
        }

        [Test]
        public void SetSwipeDeadZonePercentageAddsCorrectCallToStatement()
        {
            var statement = _sut.SetSwipeDeadZonePercentage(.67).Build();
            Assert.AreEqual(ScrollableCtor + ".setSwipeDeadZonePercentage(0.67)", statement);
        }

        [TestCase(1.1)]
        [TestCase(-1)]
        public void SetSwipeDeadZonePercentageThrowsExceptionIfOutOfRange(double invalidValue)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => 
                _sut.SetSwipeDeadZonePercentage(invalidValue));
        }

        [Test]
        public void SomeStatementsCanBeChained()
        {
            var statement = _sut
                .SetSwipeDeadZonePercentage(0.11)
                .SetMaxSearchSwipes(44)
                .Build();
            Assert.AreEqual(ScrollableCtor + ".setSwipeDeadZonePercentage(0.11).setMaxSearchSwipes(44)", statement);
        }

        [Test]
        public void AddRawTextAppendsText()
        {
            var statment = _sut.AddRawText("@").Build();
            Assert.AreEqual(ScrollableCtor + "@", statment);
        }

        [Test]
        public void RequestingStatementTerminationOnBuildAppendsSemicolon()
        {
            var statement = _sut.SetSwipeDeadZonePercentage(.67).Build(true);
            var lastCharOfStatement = statement[statement.Length - 1];
            Assert.AreEqual(';', lastCharOfStatement);
        }
    }
}
