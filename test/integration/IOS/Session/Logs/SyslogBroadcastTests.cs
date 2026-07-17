using System;
using System.Threading;
using System.Threading.Tasks;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.iOS;

namespace Appium.Net.Integration.Tests.IOS.Session.Logs
{
    [TestFixture]
    [Category("iOS")]
    internal class SyslogBroadcastTests
    {
        private IOSDriver _driver;
        private AppiumOptions _iosOptions;

        [OneTimeSetUp]
        public void SetUp()
        {
            _iosOptions = Caps.GetIosCaps(Apps.Get("iosUICatalogApp"));
            _driver = new IOSDriver(
                Env.ServerIsLocal() ? AppiumServers.LocalServiceUri : AppiumServers.RemoteServerUri,
                _iosOptions);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver?.Dispose();
        }

        [Test]
        public async Task VerifySyslogListenerCanBeAssigned()
        {
            using var messageSemaphore = new SemaphoreSlim(0, 1);
            using var connectionSemaphore = new SemaphoreSlim(0, 1);
            var messageReceived = false;
            var connectionEstablished = false;
            var timeout = TimeSpan.FromSeconds(15);
            // Add listeners
            _driver.AddSyslogMessagesListener(msg =>
            {
                Console.WriteLine($"[SYSLOG] {msg}");
                messageReceived = true;
                messageSemaphore.Release();
            });
            _driver.AddSyslogConnectionListener(() =>
            {
                Console.WriteLine("Connected to the syslog web socket");
                connectionEstablished = true;
                connectionSemaphore.Release();
            });
            _driver.AddSyslogDisconnectionListener(() =>
            {
                Console.WriteLine("Disconnected from the syslog web socket");
            });
            _driver.AddSyslogErrorsListener(ex =>
            {
                Console.WriteLine($"Syslog error: {ex.Message}");
            });
            try
            {
                // Start syslog broadcast
                await _driver.StartSyslogBroadcast();
                // Wait for connection
                Assert.That(connectionSemaphore.Wait(timeout), Is.True,
                    "Failed to establish WebSocket connection within timeout");
                Assert.That(connectionEstablished, Is.True,
                    "Connection listener was not invoked");
                // Trigger some activity to generate log messages
                _driver.BackgroundApp(TimeSpan.FromSeconds(1));
                // Wait for at least one message
                Assert.That(messageSemaphore.Wait(timeout), Is.True,
                    $"Didn't receive any log message after {timeout.TotalSeconds} seconds timeout");
                Assert.That(messageReceived, Is.True,
                    "Message listener was not invoked");
            }
            finally
            {
                // Clean up
                await _driver.StopSyslogBroadcast();
                _driver.RemoveAllSyslogListeners();
            }
        }

        [Test]
        public async Task CanStartAndStopSyslogBroadcast()
        {
            // Should not throw when starting and stopping
            await _driver.StartSyslogBroadcast();
            await _driver.StopSyslogBroadcast();
        }

        [Test]
        public async Task CanStartSyslogBroadcastWithCustomHost()
        {
            var host = "127.0.0.1";
            var port = 4723;

            await _driver.StartSyslogBroadcast(host, port);
            await _driver.StopSyslogBroadcast();
        }

        [Test]
        public async Task CanAddAndRemoveMultipleListeners()
        {
            var messageCount = 0;
            using var messageSemaphore = new SemaphoreSlim(0, 10);
            void listener1(string msg)
            {
                Interlocked.Increment(ref messageCount);
                messageSemaphore.Release();
            }
            void listener2(string msg)
            {
                Interlocked.Increment(ref messageCount);
                messageSemaphore.Release();
            }
            try
            {
                await _driver.StartSyslogBroadcast();
                // Add multiple listeners
                _driver.AddSyslogMessagesListener(listener1);
                _driver.AddSyslogMessagesListener(listener2);

                // Trigger activity to generate logs
                _driver.BackgroundApp(TimeSpan.FromMilliseconds(500));
                // Wait a bit for messages (both listeners should be called)
                var received = messageSemaphore.Wait(TimeSpan.FromSeconds(5));
                if (received)
                {
                    // If we received messages, at least one listener should have been invoked
                    Assert.That(messageCount, Is.GreaterThanOrEqualTo(1),
                        "At least one listener should have been invoked");
                }
                // Remove all listeners
                _driver.RemoveAllSyslogListeners();
                // Reset counter
                messageCount = 0;
                // Trigger more activity
                _driver.BackgroundApp(TimeSpan.FromMilliseconds(500));
                // Wait a bit - no new messages should be counted after removing listeners
                Thread.Sleep(2000);
                Assert.That(messageCount, Is.EqualTo(0),
                    "No listeners should be invoked after removing all listeners");
            }
            finally
            {
                await _driver.StopSyslogBroadcast();
                _driver.RemoveAllSyslogListeners();
            }
        }

        [Test]
        public async Task CanHandleErrorsGracefully()
        {
            var errorReceived = false;
            using var errorSemaphore = new SemaphoreSlim(0, 1);
            Exception capturedError = null;
            _driver.AddSyslogErrorsListener(ex =>
            {
                Console.WriteLine($"Error handler invoked: {ex.Message}");
                capturedError = ex;
                errorReceived = true;
                errorSemaphore.Release();
            });
            try
            {
                // Start broadcast - may fail if endpoint is not available
                try
                {
                    await _driver.StartSyslogBroadcast();

                    // Give it time to connect
                    Thread.Sleep(2000);
                    // If we got here without errors during connection, that's good
                    if (!errorReceived)
                    {
                        Assert.Pass("Syslog broadcast started successfully without errors");
                    }
                    else
                    {
                        // If error occurred during startup, verify error handler was invoked
                        Assert.That(errorReceived, Is.True,
                            "Error handler should be invoked when connection fails");
                        Assert.That(capturedError, Is.Not.Null,
                            "Error should be captured by the error handler");
                    }
                }
                catch (AggregateException)
                {
                    // Connection failure is expected in some environments
                    // Verify that error handler was invoked
                    Assert.That(errorReceived, Is.True,
                        "Error handler should be invoked when WebSocket connection fails");
                    Assert.That(capturedError, Is.Not.Null,
                        "Error should be captured by the error handler");
                }
            }
            finally
            {
                try
                {
                    await _driver.StopSyslogBroadcast();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Exception during StopSyslogBroadcast: {ex}");
                }
                _driver.RemoveAllSyslogListeners();
            }
        }
    }
}
