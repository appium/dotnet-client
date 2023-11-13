using System;

namespace Appium.Net.Integration.Tests.helpers
{
    public class NpmNotFoundException : Exception
    {
        public NpmNotFoundException()
            : base("Node Package Manager (npm) cannot be found. Make sure Node.js is installed and present in PATH.")
        {
        }

        public NpmNotFoundException(string message)
            : base(message)
        {
        }

        public NpmNotFoundException(string message, Exception innerException)
        : base(message, innerException)
        {
        }
    }
}
