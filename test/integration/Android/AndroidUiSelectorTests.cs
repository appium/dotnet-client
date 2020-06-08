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
            var statment = _sut.Build();
            Assert.AreEqual(CtorStatement, statment);
        }

        [Test]
        public void IsCheckableAddsCorrectCallToStatement()
        {
            var statment = _sut.IsCheckable(true).Build();
            Assert.AreEqual(CtorStatement + ".checkable(true)", statment);
        }

        [Test]
        public void IsCheckedAddsCorrectCallToStatement()
        {
            var statment = _sut.IsChecked(false).Build();
            Assert.AreEqual(CtorStatement + ".checked(false)", statment);
        }

        [Test]
        public void ChildSelectorAddsCorrectCallToStatement()
        {
            var statment = _sut.ChildSelector(new AndroidUiSelector()).Build();
            Assert.AreEqual(CtorStatement + $".childSelector({CtorStatement})", statment);
        }

        [Test]
        public void ClassNameEqualsAddsCorrectCallToStatement()
        {
            var statment = _sut.ClassNameEquals("Class1").Build();
            Assert.AreEqual(CtorStatement + ".className(\"Class1\")", statment);
        }

        [Test]
        public void ClassNameMatchesAddsCorrectCallToStatement()
        {
            var statment = _sut.ClassNameMatches("regex").Build();
            Assert.AreEqual(CtorStatement + ".classNameMatches(\"regex\")", statment);
        }

        [Test]
        public void IsClickableAddsCorrectCallToStatement()
        {
            var statment = _sut.IsClickable(true).Build();
            Assert.AreEqual(CtorStatement + ".clickable(true)", statment);
        }

        [Test]
        public void DescriptionEqualsAddsCorrectCallToStatement()
        {
            var statment = _sut.DescriptionEquals("Desc").Build();
            Assert.AreEqual(CtorStatement + ".description(\"Desc\")", statment);
        }

        [Test]
        public void DescriptionContainsAddsCorrectCallToStatement()
        {
            var statment = _sut.DescriptionContains("Val").Build();
            Assert.AreEqual(CtorStatement + ".descriptionContains(\"Val\")", statment);
        }

        [Test]
        public void DescriptionMatchesAddsCorrectCallToStatement()
        {
            var statment = _sut.DescriptionMatches("regex").Build();
            Assert.AreEqual(CtorStatement + ".descriptionMatches(\"regex\")", statment);
        }

        [Test]
        public void DescriptionStartsWithAddsCorrectCallToStatement()
        {
            var statment = _sut.DescriptionStartsWith("Hello").Build();
            Assert.AreEqual(CtorStatement + ".descriptionStartsWith(\"Hello\")", statment);
        }

        [Test]
        public void IsEnabledAddsCorrectCallToStatement()
        {
            var statment = _sut.IsEnabled(true).Build();
            Assert.AreEqual(CtorStatement + ".enabled(true)", statment);
        }

        [Test]
        public void IsFocusableAddsCorrectCallToStatement()
        {
            var statment = _sut.IsFocusable(true).Build();
            Assert.AreEqual(CtorStatement + ".focusable(true)", statment);
        }

        [Test]
        public void IsFocusedAddsCorrectCallToStatement()
        {
            var statment = _sut.IsFocused(false).Build();
            Assert.AreEqual(CtorStatement + ".focused(false)", statment);
        }

        [Test]
        public void FromParentAddsCorrectCallToStatement()
        {
            var statment = _sut.FromParent(new AndroidUiSelector()).Build();
            Assert.AreEqual(CtorStatement + $".fromParent({CtorStatement})", statment);
        }

        [Test]
        public void IndexAddsCorrectCallToStatement()
        {
            var statment = _sut.Index(7).Build();
            Assert.AreEqual(CtorStatement + ".index(7)", statment);
        }

        [Test]
        public void InstanceAddsCorrectCallToStatement()
        {
            var statment = _sut.Instance(4).Build();
            Assert.AreEqual(CtorStatement + ".instance(4)", statment);
        }

        [Test]
        public void LongClickableAddsCorrectCallToStatement()
        {
            var statment = _sut.IsLongClickable(false).Build();
            Assert.AreEqual(CtorStatement + ".longClickable(false)", statment);
        }

        [Test]
        public void PackageNameEqualsAddsCorrectCallToStatement()
        {
            var statment = _sut.PackageNameEquals("com.org.unique").Build();
            Assert.AreEqual(CtorStatement + ".packageName(\"com.org.unique\")", statment);
        }

        [Test]
        public void PackageNameMatchesAddsCorrectCallToStatement()
        {
            var statment = _sut.PackageNameMatches("regex").Build();
            Assert.AreEqual(CtorStatement + ".packageNameMatches(\"regex\")", statment);
        }

        [Test]
        public void ResourceIdEqualsAddsCorrectCallToStatement()
        {
            var statment = _sut.ResourceIdEquals("my-id").Build();
            Assert.AreEqual(CtorStatement + ".resourceId(\"my-id\")", statment);
        }

        [Test]
        public void ResourceIdMatchesAddsCorrectCallToStatement()
        {
            var statment = _sut.ResourceIdMatches("regex").Build();
            Assert.AreEqual(CtorStatement + ".resourceIdMatches(\"regex\")", statment);
        }

        [Test]
        public void IsScrollableAddsCorrectCallToStatement()
        {
            var statment = _sut.IsScrollable(true).Build();
            Assert.AreEqual(CtorStatement + ".scrollable(true)", statment);
        }

        [Test]
        public void IsSelectedAddsCorrectCallToStatement()
        {
            var statment = _sut.IsSelected(false).Build();
            Assert.AreEqual(CtorStatement + ".selected(false)", statment);
        }

        [Test]
        public void TextEqualsAddsCorrectCallToStatement()
        {
            var statment = _sut.TextEquals("some text").Build();
            Assert.AreEqual(CtorStatement + ".text(\"some text\")", statment);
        }

        [Test]
        public void TextContainsAddsCorrectCallToStatement()
        {
            var statment = _sut.TextContains("some text").Build();
            Assert.AreEqual(CtorStatement + ".textContains(\"some text\")", statment);
        }

        [Test]
        public void TextMatchesAddsCorrectCallToStatement()
        {
            var statment = _sut.TextMatches("some text").Build();
            Assert.AreEqual(CtorStatement + ".textMatches(\"some text\")", statment);
        }

        [Test]
        public void TextStartsWithAddsCorrectCallToStatement()
        {
            var statment = _sut.TextStartsWith("some text").Build();
            Assert.AreEqual(CtorStatement + ".textStartsWith(\"some text\")", statment);
        }

        [Test]
        public void AddRawTextAppendsText()
        {
            var statment = _sut.AddRawText("@").Build();
            Assert.AreEqual(CtorStatement + "@", statment);
        }

        [Test]
        public void CanChainCalls()
        {
            var statment = _sut
                .ResourceIdMatches(".*my_id")
                .ClassNameEquals("andoid.widget.TextField")
                .IsLongClickable(true)
                .Build();
            Assert.AreEqual(CtorStatement + ".resourceIdMatches(\".*my_id\").className(\"andoid.widget.TextField\").longClickable(true)", 
                statment);
        }
    }
}
