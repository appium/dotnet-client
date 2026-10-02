using System;
using System.IO;
using NUnit.Framework;

namespace Appium.Net.Integration.Tests.helpers
{
    public class AppsHelperTest
    {
        [Test]
        public void LocalServerUsesBundledIosTestAppArchive()
        {
            Assume.That(Env.ServerIsRemote(), Is.False, "Remote Appium servers receive app URLs instead of local paths.");

            var expected = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "apps", "archives", "TestApp.app.zip"));

            var app = Apps.Get(Apps.iosTestApp);

            Assert.Multiple(() =>
            {
                Assert.That(app, Is.EqualTo(expected));
                Assert.That(File.Exists(app), Is.True);
            });
        }
    }
}
