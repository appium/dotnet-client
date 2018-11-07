using System;
using System.Collections.Generic;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium.PageObjects;
using OpenQA.Selenium.Appium.PageObjects.Attributes;
using OpenQA.Selenium.Firefox;
using SeleniumExtras.PageObjects;

namespace Appium.Net.Integration.Tests.PageObjectTests.TimeOutManagement
{
    public class TimeOutManagementTest
    {
        private IWebDriver _driver;
        private TimeOutDuration _duration;

        [FindsBy(How = How.ClassName, Using = "FakeClass", Priority = 1)]
        [FindsBy(How = How.TagName, Using = "FakeTag", Priority = 2)] private IList<IWebElement> _stubElements;

        [WithTimeSpan(Seconds = 15)] [FindsBy(How = How.ClassName, Using = "FakeClass", Priority = 1)]
        [FindsBy(How = How.TagName, Using = "FakeTag", Priority = 2)] private IList<IWebElement> _stubElements2;

        private readonly TimeSpan _accepableTimeDelta = new TimeSpan(0, 0, 2)
            ; //Another procceses/environment issues can interfere 
        //the checking

        [SetUp]
        public void Before()
        {
            if (_driver == null)
            {
                _driver = new FirefoxDriver();
            }
            _duration = new TimeOutDuration(new TimeSpan(0, 0, 5));
            PageFactory.InitElements(_driver, this, new AppiumPageObjectMemberDecorator(_duration));
        }

        [OneTimeTearDown]
        public void AfterAll()
        {
            _driver?.Quit();
        }

        private bool IsInTime(TimeSpan span, IList<IWebElement> list)
        {
            var start = DateTime.Now;
            var checkingSpan =
                new TimeSpan(span.Hours, span.Seconds + _accepableTimeDelta.Seconds, span.Milliseconds);
            var deadline = DateTime.Now.Add(checkingSpan);
            var size = list.Count;
            var now = DateTime.Now;

            return (now <= deadline & start.Add(span) <= now);
        }


        [Test]
        public void CheckAbilityToChangeWaitingTime()
        {
            Assert.AreEqual(true, IsInTime(new TimeSpan(0, 0, 5), _stubElements));
            var newTime = new TimeSpan(0, 0, 0, 20, 500);
            _duration.WaitingDuration = newTime;
            Assert.AreEqual(true, IsInTime(newTime, _stubElements));
            newTime = new TimeSpan(0, 0, 0, 2, 0);
            _duration.WaitingDuration = newTime;
            Assert.AreEqual(true, IsInTime(newTime, _stubElements));
        }

        [Test]
        public void CheckWaitingTimeIfMemberHasAttribute_WithTimeSpan()
        {
            var fifteenSeconds = new TimeSpan(0, 0, 0, 15, 0);
            ;
            Assert.AreEqual(true, IsInTime(new TimeSpan(0, 0, 5), _stubElements));
            Assert.AreEqual(true, IsInTime(fifteenSeconds, _stubElements2));

            var newTime = new TimeSpan(0, 0, 0, 2, 0);
            _duration.WaitingDuration = newTime;
            Assert.AreEqual(true, IsInTime(newTime, _stubElements));
            Assert.AreEqual(true, IsInTime(fifteenSeconds, _stubElements2));
        }
    }
}