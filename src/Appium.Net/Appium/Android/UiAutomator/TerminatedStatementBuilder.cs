using System.Text;
using OpenQA.Selenium.Appium.Interfaces;

namespace OpenQA.Selenium.Appium.Android.UiAutomator
{
    public class TerminatedStatementBuilder : IUiAutomatorStatementBuilder
    {
        private readonly StringBuilder _builder;

        internal TerminatedStatementBuilder(StringBuilder builder)
        {
            _builder = builder;
        }

        public string Build()
        {
            return _builder.ToString();
        }
    }
}
