using OpenQA.Selenium.Remote;
using System;
using System.Net.Http;

namespace OpenQA.Selenium.Appium.Service
{
    public class AppiumHttpCommandExecutor : HttpCommandExecutor
    {
        private readonly AppiumClientConfig _clientConfig;

        public AppiumHttpCommandExecutor(Uri addressOfRemoteServer, TimeSpan timeout)
            : base(addressOfRemoteServer, timeout, enableKeepAlive: true)
        {

        }

        public AppiumHttpCommandExecutor(Uri addressOfRemoteServer, TimeSpan timeout, AppiumClientConfig clientConfig)
            : base(addressOfRemoteServer, timeout, enableKeepAlive: true)
        {
            _clientConfig = clientConfig;
        }

        protected override HttpClientHandler CreateHttpClientHandler()
        {
            var handler = base.CreateHttpClientHandler();

            if (_clientConfig != null && _clientConfig.RelaxSslValidation)
            {
                handler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;
            }
            return handler;
        }

    }
}
