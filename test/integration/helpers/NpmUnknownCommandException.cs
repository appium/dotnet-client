using System;

namespace Appium.Net.Integration.Tests.helpers
{
    public class NpmUnknownCommandException : Exception
    {
        public NpmUnknownCommandException()
            : base("Unknown npm command encountered. ")
        {
        }

        public NpmUnknownCommandException(string message)
            : base(message)
        {
        }
    }
}
