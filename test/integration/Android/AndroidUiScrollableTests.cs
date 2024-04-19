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
            Assert.That(statement, Is.EqualTo(ScrollableCtor));
        }

        [Test]
        public void NewScrollableUsesCustomScrollContainerSelector()
        {
            var selector = new AndroidUiSelector().Instance(7);
            var statement = new AndroidUiScrollable(selector).Build();
            Assert.That(statement, Is.EqualTo("new UiScrollable(new UiSelector().instance(7))"));
        }

        [Test]
        public void FlingBackwardAddsCorrectCallToStatement()
        {
            var statement = _sut.FlingBackward().Build();
            Assert.That(statement, Is.EqualTo(ScrollableCtor + ".flingBackward()"));
        }

        [Test]
        public void FlingForwardAddsCorrectCallToStatement()
        {
            var statement = _sut.FlingForward().Build();
            Assert.That(statement, Is.EqualTo(ScrollableCtor + ".flingForward()"));
        }

        [Test]
        public void FlingToBeginningAddsCorrectCallToStatement()
        {
            var statement = _sut.FlingToBeginning(45).Build();
            Assert.That(statement, Is.EqualTo(ScrollableCtor + ".flingToBeginning(45)"));
        }

        [Test]
        public void FlingToEndAddsCorrectCallToStatement()
        {
            var statement = _sut.FlingToEnd(77).Build();
            Assert.That(statement, Is.EqualTo(ScrollableCtor + ".flingToEnd(77)"));
        }

        [Test]
        public void GetChildByDescriptionAddsCorrectCallToStatement()
        {
            var statement = _sut.GetChildByDescription(new AndroidUiSelector(), "Hello World", false).Build();
            Assert.That(statement, Is.EqualTo(ScrollableCtor + ".getChildByDescription(new UiSelector(), \"Hello World\", false)"));
        }

        [Test]
        public void GetChildByInstanceAddsCorrectCallToStatement()
        {
            var statement = _sut.GetChildByInstance(new AndroidUiSelector(), 9).Build();
            Assert.That(statement, Is.EqualTo(ScrollableCtor + ".getChildByInstance(new UiSelector(), 9)"));
        }

        [Test]
        public void GetChildByTextAddsCorrectCallToStatement()
        {
            var statement = _sut.GetChildByText(new AndroidUiSelector(), "Help", false).Build();
            Assert.That(statement, Is.EqualTo(ScrollableCtor + ".getChildByText(new UiSelector(), \"Help\", false)"));
        }

        [Test]
        public void ScrollBackwardAddsCorrectCallToStatement()
        {
            var statement = _sut.ScrollBackward(32).Build();
            Assert.That(statement, Is.EqualTo(ScrollableCtor + ".scrollBackward(32)"));
        }

        [Test]
        public void ScrollDescriptionIntoViewAddsCorrectCallToStatement()
        {
            var statement = _sut.ScrollDescriptionIntoView("Description Here").Build();
            Assert.That(statement, Is.EqualTo(ScrollableCtor + ".scrollDescriptionIntoView(\"Description Here\")"));
        }

        [Test]
        public void ScrollForwardAddsCorrectCallToStatement()
        {
            var statement = _sut.ScrollForward(46).Build();
            Assert.That(statement, Is.EqualTo(ScrollableCtor + ".scrollForward(46)"));
        }

        [Test]
        public void ScrollIntoViewAddsCorrectCallToStatement()
        {
            var statement = _sut.ScrollIntoView(new AndroidUiSelector()).Build();
            Assert.That(statement, Is.EqualTo(ScrollableCtor + ".scrollIntoView(new UiSelector())"));
        }

        [Test]
        public void ScrollTextIntoViewAddsCorrectCallToStatement()
        {
            var statement = _sut.ScrollTextIntoView("Some Text").Build();
            Assert.That(statement, Is.EqualTo(ScrollableCtor + ".scrollTextIntoView(\"Some Text\")"));
        }

        [Test]
        public void ScrollToBeginningAddsCorrectCallToStatement()
        {
            var statement = _sut.ScrollToBeginning(12, 90).Build();
            Assert.That(statement, Is.EqualTo(ScrollableCtor + ".scrollToBeginning(12, 90)"));
        }

        [Test]
        public void ScrollToEndAddsCorrectCallToStatement()
        {
            var statement = _sut.ScrollToEnd(32, 51).Build();
            Assert.That(statement, Is.EqualTo(ScrollableCtor + ".scrollToEnd(32, 51)"));
        }

        [Test]
        public void SetScrollDirectionVerticalAddsCorrectCallToStatement()
        {
            var statement = _sut.SetScrollDirection(ListDirection.Vertical).Build();
            Assert.That(statement, Is.EqualTo(ScrollableCtor + ".setAsVerticalList()"));
        }

        [Test]
        public void SetScrollDirectionHorizontalAddsCorrectCallToStatement()
        {
            var statement = _sut.SetScrollDirection(ListDirection.Horizontal).Build();
            Assert.That(statement, Is.EqualTo(ScrollableCtor + ".setAsHorizontalList()"));
        }

        [Test]
        public void SetMaxSearchSwipesAddsCorrectCallToStatement()
        {
            var statement = _sut.SetMaxSearchSwipes(80).Build();
            Assert.That(statement, Is.EqualTo(ScrollableCtor + ".setMaxSearchSwipes(80)"));
        }

        [Test]
        public void SetSwipeDeadZonePercentageAddsCorrectCallToStatement()
        {
            var statement = _sut.SetSwipeDeadZonePercentage(.67).Build();
            Assert.That(statement, Is.EqualTo(ScrollableCtor + ".setSwipeDeadZonePercentage(0.67)"));
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
            Assert.That(statement, Is.EqualTo(ScrollableCtor + ".setSwipeDeadZonePercentage(0.11).setMaxSearchSwipes(44)"));
        }

        [Test]
        public void AddRawTextAppendsText()
        {
            var statment = _sut.AddRawText("@").Build();
            Assert.That(statment, Is.EqualTo(ScrollableCtor + "@"));
        }

        [Test]
        public void RequestingStatementTerminationOnBuildAppendsSemicolon()
        {
            var statement = _sut.SetSwipeDeadZonePercentage(.67).Build(true);
            var lastCharOfStatement = statement[statement.Length - 1];
            Assert.That(lastCharOfStatement, Is.EqualTo(';'));
        }
    }
}
