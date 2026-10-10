//Licensed under the Apache License, Version 2.0 (the "License");
//you may not use this file except in compliance with the License.
//See the NOTICE file distributed with this work for additional
//information regarding copyright ownership.
//You may obtain a copy of the License at
//
//   http://www.apache.org/licenses/LICENSE-2.0
//
//Unless required by applicable law or agreed to in writing, software
//distributed under the License is distributed on an "AS IS" BASIS,
//WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//See the License for the specific language governing permissions and
//limitations under the License.

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Appium.Net.Integration.Tests.helpers
{
    public class AppsTest
    {
        [Test]
        public void LocalServerUsesBundledIosTestAppWithoutDownloading()
        {
            Assume.That(Env.ServerIsRemote(), Is.False, "A remote server is given the download URL instead.");

            var app = Apps.Get(Apps.iosTestApp);

            Assert.Multiple(() =>
            {
                Assert.That(app, Is.EqualTo(Path.Combine(AppContext.BaseDirectory, "apps", "TestApp.app.zip")));
                Assert.That(File.Exists(app), Is.True);
            });
        }

        [Test]
        public void DownloadRetriesAfterTransientFailure()
        {
            var handler = new StubHandler(HttpStatusCode.ServiceUnavailable, HttpStatusCode.OK);
            var sleeps = new List<TimeSpan>();
            var destination = TempFile();

            try
            {
                using (var client = new HttpClient(handler))
                {
                    Apps.DownloadIfMissing("https://example.test/app.zip", destination, client, sleeps.Add);
                }

                Assert.Multiple(() =>
                {
                    Assert.That(handler.Requests, Is.EqualTo(2));
                    Assert.That(sleeps, Is.EqualTo(new[] { TimeSpan.FromSeconds(5) }));
                    Assert.That(File.ReadAllBytes(destination), Is.EqualTo(StubHandler.Payload));
                });
            }
            finally
            {
                File.Delete(destination);
            }
        }

        [Test]
        public void DownloadGivesUpAfterThreeAttempts()
        {
            var handler = new StubHandler(HttpStatusCode.ServiceUnavailable, HttpStatusCode.ServiceUnavailable, HttpStatusCode.ServiceUnavailable);
            var sleeps = new List<TimeSpan>();
            var destination = TempFile();

            using (var client = new HttpClient(handler))
            {
                var ex = Assert.Throws<Exception>(() => Apps.DownloadIfMissing("https://example.test/app.zip", destination, client, sleeps.Add));

                Assert.Multiple(() =>
                {
                    Assert.That(ex.Message, Does.Contain("after 3 attempts"));
                    Assert.That(ex.InnerException, Is.InstanceOf<HttpRequestException>());
                    Assert.That(handler.Requests, Is.EqualTo(3));
                    Assert.That(sleeps, Is.EqualTo(new[] { TimeSpan.FromSeconds(5), TimeSpan.FromSeconds(10) }));
                    Assert.That(File.Exists(destination), Is.False);
                });
            }
        }

        [Test]
        public void DownloadSkipsExistingFile()
        {
            var handler = new StubHandler(HttpStatusCode.OK);
            var destination = TempFile();
            File.WriteAllText(destination, "cached");

            try
            {
                using (var client = new HttpClient(handler))
                {
                    Apps.DownloadIfMissing("https://example.test/app.zip", destination, client, _ => { });
                }

                Assert.Multiple(() =>
                {
                    Assert.That(handler.Requests, Is.Zero);
                    Assert.That(File.ReadAllText(destination), Is.EqualTo("cached"));
                });
            }
            finally
            {
                File.Delete(destination);
            }
        }

        private static string TempFile()
        {
            return Path.Combine(Path.GetTempPath(), $"AppsTest-{Guid.NewGuid():N}.zip");
        }

        // Returns the given status codes in order, one per request.
        private class StubHandler : HttpMessageHandler
        {
            public static readonly byte[] Payload = { 1, 2, 3 };

            private readonly Queue<HttpStatusCode> _statuses;

            public StubHandler(params HttpStatusCode[] statuses)
            {
                _statuses = new Queue<HttpStatusCode>(statuses);
            }

            public int Requests { get; private set; }

            protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                Requests++;
                return Task.FromResult(new HttpResponseMessage(_statuses.Dequeue())
                {
                    Content = new ByteArrayContent(Payload)
                });
            }
        }
    }
}
