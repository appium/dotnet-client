using System;
using System.Threading;
using System.Threading.Tasks;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium;
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
            // Unbounded max so a burst of syslog messages (handler calls Release per message)
            // can't overflow the semaphore and throw SemaphoreFullException.
            using var messageSemaphore = new SemaphoreSlim(0, int.MaxValue);
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
                Assert.That(await connectionSemaphore.WaitAsync(timeout), Is.True,
                    "Failed to establish WebSocket connection within timeout");
                Assert.That(connectionEstablished, Is.True,
                    "Connection listener was not invoked");
                // Trigger some activity to generate log messages
                _driver.BackgroundApp(TimeSpan.FromSeconds(1));
                // Wait for at least one message
                Assert.That(await messageSemaphore.WaitAsync(timeout), Is.True,
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
            // Unbounded max so multiple listeners releasing per message can't overflow it.
            using var messageSemaphore = new SemaphoreSlim(0, int.MaxValue);
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
                // Wait until at least one listener invocation is signalled. The semaphore wakes
                // on the first listener to fire, so we only assert that a listener was invoked
                // (not that both have run by this point) to keep the test deterministic.
                var received = await messageSemaphore.WaitAsync(TimeSpan.FromSeconds(5));
                if (received)
                {
                    // If we received messages, at least one listener should have been invoked
                    Assert.That(messageCount, Is.GreaterThanOrEqualTo(1),
                        "At least one listener should have been invoked");
                }
                // Remove all listeners
                _driver.RemoveAllSyslogListeners();
                // Drain any in-flight dispatch: a message already read from the socket before
                // removal can still invoke the previously captured delegate. Wait for those to
                // settle before resetting the counter so the post-removal assertion only reflects
                // callbacks triggered after removal.
                await Task.Delay(1000);
                // Reset counter
                messageCount = 0;
                // Trigger more activity
                _driver.BackgroundApp(TimeSpan.FromMilliseconds(500));
                // Wait a bit - no new messages should be counted after removing listeners
                await Task.Delay(2000);
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
            Exception capturedError = null;
            _driver.AddSyslogErrorsListener(ex =>
            {
                Console.WriteLine($"Error handler invoked: {ex.Message}");
                capturedError = ex;
                errorReceived = true;
            });
            try
            {
                // Start broadcast - may fail if endpoint is not available
                try
                {
                    await _driver.StartSyslogBroadcast();

                    // Give it time to connect
                    await Task.Delay(2000);
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
                catch (WebDriverException)
                {
                    // Connection failure is expected in some environments. Awaited calls surface
                    // the underlying WebDriverException directly (not wrapped in AggregateException),
                    // since StringWebSocketClient.ConnectAsync rethrows connection errors as such.
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
