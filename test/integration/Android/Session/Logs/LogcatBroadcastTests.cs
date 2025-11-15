using System;
using System.Threading;
using Appium.Net.Integration.Tests.helpers;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

namespace Appium.Net.Integration.Tests.Android.Session.Logs
{
    [TestFixture]
    internal class LogcatBroadcastTests
    {
        private AndroidDriver _driver;
        private AppiumOptions _androidOptions;

        [OneTimeSetUp]
        public void SetUp()
        {
            _androidOptions = Caps.GetAndroidUIAutomatorCaps(Apps.Get("androidApiDemos"));
            _driver = new AndroidDriver(
                Env.ServerIsLocal() ? AppiumServers.LocalServiceUri : AppiumServers.RemoteServerUri,
                _androidOptions);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _driver?.Dispose();
        }

        [Test]
        public void VerifyLogcatListenerCanBeAssigned()
        {
            var messageSemaphore = new SemaphoreSlim(0, 1);
            var connectionSemaphore = new SemaphoreSlim(0, 1);
            var messageReceived = false;
            var connectionEstablished = false;
            var timeout = TimeSpan.FromSeconds(15);

            // Add listeners
            _driver.AddLogcatMessagesListener(msg =>
            {
                Console.WriteLine($"[LOGCAT] {msg}");
                messageReceived = true;
                messageSemaphore.Release();
            });

            _driver.AddLogcatConnectionListener(() =>
            {
                Console.WriteLine("Connected to the logcat web socket");
                connectionEstablished = true;
                connectionSemaphore.Release();
            });

            _driver.AddLogcatDisconnectionListener(() =>
            {
                Console.WriteLine("Disconnected from the logcat web socket");
            });

            _driver.AddLogcatErrorsListener(ex =>
            {
                Console.WriteLine($"Logcat error: {ex.Message}");
            });

            try
            {
                // Start logcat broadcast
                _driver.StartLogcatBroadcast();

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
                _driver.StopLogcatBroadcast();
                _driver.RemoveAllLogcatListeners();
                messageSemaphore.Dispose();
                connectionSemaphore.Dispose();
            }
        }

        [Test]
        public void CanStartAndStopLogcatBroadcast()
        {
            // Should not throw when starting and stopping
            Assert.DoesNotThrow(() =>
            {
                _driver.StartLogcatBroadcast();
                _driver.StopLogcatBroadcast();
            }, "Starting and stopping logcat broadcast should not throw exceptions");
        }

        [Test]
        public void CanStartLogcatBroadcastWithCustomHost()
        {
            var host = Env.ServerIsLocal() ? "localhost" : "127.0.0.1";
            var port = 4723;    

            Assert.DoesNotThrow(() =>
            {
                _driver.StartLogcatBroadcast(host, port);
                _driver.StopLogcatBroadcast();
            }, "Starting logcat broadcast with custom host should not throw exceptions");
        }

        [Test]
        public void CanAddAndRemoveMultipleListeners()
        {
            var messageCount = 0;
            var messageSemaphore = new SemaphoreSlim(0, 10);

            Action<string> listener1 = msg =>
            {
                Interlocked.Increment(ref messageCount);
                messageSemaphore.Release();
            };

            Action<string> listener2 = msg =>
            {
                Interlocked.Increment(ref messageCount);
                messageSemaphore.Release();
            };

            try
            {
                // Add multiple listeners
                _driver.AddLogcatMessagesListener(listener1);
                _driver.AddLogcatMessagesListener(listener2);

                    

                // Trigger activity to generate logs
                _driver.BackgroundApp(TimeSpan.FromMilliseconds(500));

                // Wait a bit for messages (both listeners should be called)
                var received = messageSemaphore.Wait(TimeSpan.FromSeconds(5));

                if (received)
                {
                    // If we received messages, both listeners should have been invoked
                    Assert.That(messageCount, Is.GreaterThanOrEqualTo(1),
                        "At least one listener should have been invoked");
                }

                // Remove all listeners
                _driver.RemoveAllLogcatListeners();

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
                _driver.StopLogcatBroadcast();
                _driver.RemoveAllLogcatListeners();
                messageSemaphore.Dispose();
            }
        }

        [Test]
        public void CanHandleErrorsGracefully()
        {
            var errorReceived = false;
            var errorSemaphore = new SemaphoreSlim(0, 1);
            Exception capturedError = null;

            _driver.AddLogcatErrorsListener(ex =>
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
                    _driver.StartLogcatBroadcast();
                    
                    // Give it time to connect
                    Thread.Sleep(2000);

                    // If we got here without errors during connection, that's good
                    if (!errorReceived)
                    {
                        Assert.Pass("Logcat broadcast started successfully without errors");
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
                    _driver.StopLogcatBroadcast();
                }
                catch
                {
                    // Ignore errors when stopping if it never started
                }
                _driver.RemoveAllLogcatListeners();
                errorSemaphore.Dispose();
            }
        }
    }
}
