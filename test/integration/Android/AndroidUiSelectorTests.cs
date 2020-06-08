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
            Assert.AreEqual(CtorStatement, statement);
        }

        [Test]
        public void IsCheckableAddsCorrectCallToStatement()
        {
            var statement = _sut.IsCheckable(true).Build();
            Assert.AreEqual(CtorStatement + ".checkable(true)", statement);
        }

        [Test]
        public void IsCheckedAddsCorrectCallToStatement()
        {
            var statement = _sut.IsChecked(false).Build();
            Assert.AreEqual(CtorStatement + ".checked(false)", statement);
        }

        [Test]
        public void ChildSelectorAddsCorrectCallToStatement()
        {
            var statement = _sut.ChildSelector(new AndroidUiSelector()).Build();
            Assert.AreEqual(CtorStatement + $".childSelector({CtorStatement})", statement);
        }

        [Test]
        public void ClassNameEqualsAddsCorrectCallToStatement()
        {
            var statement = _sut.ClassNameEquals("Class1").Build();
            Assert.AreEqual(CtorStatement + ".className(\"Class1\")", statement);
        }

        [Test]
        public void ClassNameMatchesAddsCorrectCallToStatement()
        {
            var statement = _sut.ClassNameMatches("regex").Build();
            Assert.AreEqual(CtorStatement + ".classNameMatches(\"regex\")", statement);
        }

        [Test]
        public void IsClickableAddsCorrectCallToStatement()
        {
            var statement = _sut.IsClickable(true).Build();
            Assert.AreEqual(CtorStatement + ".clickable(true)", statement);
        }

        [Test]
        public void DescriptionEqualsAddsCorrectCallToStatement()
        {
            var statement = _sut.DescriptionEquals("Desc").Build();
            Assert.AreEqual(CtorStatement + ".description(\"Desc\")", statement);
        }

        [Test]
        public void DescriptionContainsAddsCorrectCallToStatement()
        {
            var statement = _sut.DescriptionContains("Val").Build();
            Assert.AreEqual(CtorStatement + ".descriptionContains(\"Val\")", statement);
        }

        [Test]
        public void DescriptionMatchesAddsCorrectCallToStatement()
        {
            var statement = _sut.DescriptionMatches("regex").Build();
            Assert.AreEqual(CtorStatement + ".descriptionMatches(\"regex\")", statement);
        }

        [Test]
        public void DescriptionStartsWithAddsCorrectCallToStatement()
        {
            var statement = _sut.DescriptionStartsWith("Hello").Build();
            Assert.AreEqual(CtorStatement + ".descriptionStartsWith(\"Hello\")", statement);
        }

        [Test]
        public void IsEnabledAddsCorrectCallToStatement()
        {
            var statement = _sut.IsEnabled(true).Build();
            Assert.AreEqual(CtorStatement + ".enabled(true)", statement);
        }

        [Test]
        public void IsFocusableAddsCorrectCallToStatement()
        {
            var statement = _sut.IsFocusable(true).Build();
            Assert.AreEqual(CtorStatement + ".focusable(true)", statement);
        }

        [Test]
        public void IsFocusedAddsCorrectCallToStatement()
        {
            var statement = _sut.IsFocused(false).Build();
            Assert.AreEqual(CtorStatement + ".focused(false)", statement);
        }

        [Test]
        public void FromParentAddsCorrectCallToStatement()
        {
            var statement = _sut.FromParent(new AndroidUiSelector()).Build();
            Assert.AreEqual(CtorStatement + $".fromParent({CtorStatement})", statement);
        }

        [Test]
        public void IndexAddsCorrectCallToStatement()
        {
            var statement = _sut.Index(7).Build();
            Assert.AreEqual(CtorStatement + ".index(7)", statement);
        }

        [Test]
        public void InstanceAddsCorrectCallToStatement()
        {
            var statement = _sut.Instance(4).Build();
            Assert.AreEqual(CtorStatement + ".instance(4)", statement);
        }

        [Test]
        public void LongClickableAddsCorrectCallToStatement()
        {
            var statement = _sut.IsLongClickable(false).Build();
            Assert.AreEqual(CtorStatement + ".longClickable(false)", statement);
        }

        [Test]
        public void PackageNameEqualsAddsCorrectCallToStatement()
        {
            var statement = _sut.PackageNameEquals("com.org.unique").Build();
            Assert.AreEqual(CtorStatement + ".packageName(\"com.org.unique\")", statement);
        }

        [Test]
        public void PackageNameMatchesAddsCorrectCallToStatement()
        {
            var statement = _sut.PackageNameMatches("regex").Build();
            Assert.AreEqual(CtorStatement + ".packageNameMatches(\"regex\")", statement);
        }

        [Test]
        public void ResourceIdEqualsAddsCorrectCallToStatement()
        {
            var statement = _sut.ResourceIdEquals("my-id").Build();
            Assert.AreEqual(CtorStatement + ".resourceId(\"my-id\")", statement);
        }

        [Test]
        public void ResourceIdMatchesAddsCorrectCallToStatement()
        {
            var statement = _sut.ResourceIdMatches("regex").Build();
            Assert.AreEqual(CtorStatement + ".resourceIdMatches(\"regex\")", statement);
        }

        [Test]
        public void IsScrollableAddsCorrectCallToStatement()
        {
            var statement = _sut.IsScrollable(true).Build();
            Assert.AreEqual(CtorStatement + ".scrollable(true)", statement);
        }

        [Test]
        public void IsSelectedAddsCorrectCallToStatement()
        {
            var statement = _sut.IsSelected(false).Build();
            Assert.AreEqual(CtorStatement + ".selected(false)", statement);
        }

        [Test]
        public void TextEqualsAddsCorrectCallToStatement()
        {
            var statement = _sut.TextEquals("some text").Build();
            Assert.AreEqual(CtorStatement + ".text(\"some text\")", statement);
        }

        [Test]
        public void TextContainsAddsCorrectCallToStatement()
        {
            var statement = _sut.TextContains("some text").Build();
            Assert.AreEqual(CtorStatement + ".textContains(\"some text\")", statement);
        }

        [Test]
        public void TextMatchesAddsCorrectCallToStatement()
        {
            var statement = _sut.TextMatches("some text").Build();
            Assert.AreEqual(CtorStatement + ".textMatches(\"some text\")", statement);
        }

        [Test]
        public void TextStartsWithAddsCorrectCallToStatement()
        {
            var statement = _sut.TextStartsWith("some text").Build();
            Assert.AreEqual(CtorStatement + ".textStartsWith(\"some text\")", statement);
        }

        [Test]
        public void AddRawTextAppendsText()
        {
            var statement = _sut.AddRawText("@").Build();
            Assert.AreEqual(CtorStatement + "@", statement);
        }

        [Test]
        public void CanChainCalls()
        {
            var statement = _sut
                .ResourceIdMatches(".*my_id")
                .ClassNameEquals("andoid.widget.TextField")
                .IsLongClickable(true)
                .Build();
            Assert.AreEqual(CtorStatement + ".resourceIdMatches(\".*my_id\").className(\"andoid.widget.TextField\").longClickable(true)", 
                statement);
        }
    }
}
