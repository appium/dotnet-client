using NUnit.Framework;
using OpenQA.Selenium.Appium.Android.UiAutomator;

namespace Appium.Net.Integration.Tests.Android
{
    public class AndroidUiSelectorTests
    {
        private AndroidUiSelector _sut;
        private const string CtorStatement = "new UiSelector()";

        [SetUp]
        public void Setup()
        {
            _sut = new AndroidUiSelector();
        }

        [Test]
        public void NewSelectorStartsWithJustConstructorCall()
        {
            var statement = _sut.Build();
            Assert.That(statement, Is.EqualTo(CtorStatement));
        }

        [Test]
        public void IsCheckableAddsCorrectCallToStatement()
        {
            var statement = _sut.IsCheckable(true).Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".checkable(true)"));
        }

        [Test]
        public void IsCheckedAddsCorrectCallToStatement()
        {
            var statement = _sut.IsChecked(false).Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".checked(false)"));
        }

        [Test]
        public void ChildSelectorAddsCorrectCallToStatement()
        {
            var statement = _sut.ChildSelector(new AndroidUiSelector()).Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + $".childSelector({CtorStatement})"));
        }

        [Test]
        public void ClassNameEqualsAddsCorrectCallToStatement()
        {
            var statement = _sut.ClassNameEquals("Class1").Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".className(\"Class1\")"));
        }

        [Test]
        public void ClassNameMatchesAddsCorrectCallToStatement()
        {
            var statement = _sut.ClassNameMatches("regex").Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".classNameMatches(\"regex\")"));
        }

        [Test]
        public void IsClickableAddsCorrectCallToStatement()
        {
            var statement = _sut.IsClickable(true).Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".clickable(true)"));
        }

        [Test]
        public void DescriptionEqualsAddsCorrectCallToStatement()
        {
            var statement = _sut.DescriptionEquals("Desc").Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".description(\"Desc\")"));
        }

        [Test]
        public void DescriptionContainsAddsCorrectCallToStatement()
        {
            var statement = _sut.DescriptionContains("Val").Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".descriptionContains(\"Val\")"));
        }

        [Test]
        public void DescriptionMatchesAddsCorrectCallToStatement()
        {
            var statement = _sut.DescriptionMatches("regex").Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".descriptionMatches(\"regex\")"));
        }

        [Test]
        public void DescriptionStartsWithAddsCorrectCallToStatement()
        {
            var statement = _sut.DescriptionStartsWith("Hello").Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".descriptionStartsWith(\"Hello\")"));
        }

        [Test]
        public void IsEnabledAddsCorrectCallToStatement()
        {
            var statement = _sut.IsEnabled(true).Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".enabled(true)"));
        }

        [Test]
        public void IsFocusableAddsCorrectCallToStatement()
        {
            var statement = _sut.IsFocusable(true).Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".focusable(true)"));
        }

        [Test]
        public void IsFocusedAddsCorrectCallToStatement()
        {
            var statement = _sut.IsFocused(false).Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".focused(false)"));
        }

        [Test]
        public void FromParentAddsCorrectCallToStatement()
        {
            var statement = _sut.FromParent(new AndroidUiSelector()).Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + $".fromParent({CtorStatement})"));
        }

        [Test]
        public void IndexAddsCorrectCallToStatement()
        {
            var statement = _sut.Index(7).Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".index(7)"));
        }

        [Test]
        public void InstanceAddsCorrectCallToStatement()
        {
            var statement = _sut.Instance(4).Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".instance(4)"));
        }

        [Test]
        public void LongClickableAddsCorrectCallToStatement()
        {
            var statement = _sut.IsLongClickable(false).Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".longClickable(false)"));
        }

        [Test]
        public void PackageNameEqualsAddsCorrectCallToStatement()
        {
            var statement = _sut.PackageNameEquals("com.org.unique").Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".packageName(\"com.org.unique\")"));
        }

        [Test]
        public void PackageNameMatchesAddsCorrectCallToStatement()
        {
            var statement = _sut.PackageNameMatches("regex").Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".packageNameMatches(\"regex\")"));
        }

        [Test]
        public void ResourceIdEqualsAddsCorrectCallToStatement()
        {
            var statement = _sut.ResourceIdEquals("my-id").Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".resourceId(\"my-id\")"));
        }

        [Test]
        public void ResourceIdMatchesAddsCorrectCallToStatement()
        {
            var statement = _sut.ResourceIdMatches("regex").Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".resourceIdMatches(\"regex\")"));
        }

        [Test]
        public void IsScrollableAddsCorrectCallToStatement()
        {
            var statement = _sut.IsScrollable(true).Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".scrollable(true)"));
        }

        [Test]
        public void IsSelectedAddsCorrectCallToStatement()
        {
            var statement = _sut.IsSelected(false).Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".selected(false)"));
        }

        [Test]
        public void TextEqualsAddsCorrectCallToStatement()
        {
            var statement = _sut.TextEquals("some text").Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".text(\"some text\")"));
        }

        [Test]
        public void TextContainsAddsCorrectCallToStatement()
        {
            var statement = _sut.TextContains("some text").Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".textContains(\"some text\")"));
        }

        [Test]
        public void TextMatchesAddsCorrectCallToStatement()
        {
            var statement = _sut.TextMatches("some text").Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".textMatches(\"some text\")"));
        }

        [Test]
        public void TextStartsWithAddsCorrectCallToStatement()
        {
            var statement = _sut.TextStartsWith("some text").Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".textStartsWith(\"some text\")"));
        }

        [Test]
        public void AddRawTextAppendsText()
        {
            var statement = _sut.AddRawText("@").Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + "@"));
        }

        [Test]
        public void CanChainCalls()
        {
            var statement = _sut
                .ResourceIdMatches(".*my_id")
                .ClassNameEquals("andoid.widget.TextField")
                .IsLongClickable(true)
                .Build();
            Assert.That(statement, Is.EqualTo(CtorStatement + ".resourceIdMatches(\".*my_id\").className(\"andoid.widget.TextField\").longClickable(true)"));
        }
    }
}
