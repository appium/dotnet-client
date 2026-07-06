using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using OpenQA.Selenium.Appium.ImageComparison;

namespace Appium.Net.Integration.Tests.ImageComparison
{
    public class TestComparisonResult : ComparisonResult
    {
        public TestComparisonResult(Dictionary<string, object> result) : base(result)
        {
        }
    }

    [TestFixture]
    public class ComparisonResultTests
    {
        private string _testDir;
        private TestComparisonResult _comparisonResult;
        private const string DummyBase64 = "c29tZSB2aXN1YWxpemF0aW9uIGRhdGE="; // "some visualization data"
        private byte[] _expectedBytes;

        [SetUp]
        public void SetUp()
        {
            _testDir = Path.Combine(Directory.GetCurrentDirectory(), "test_output_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_testDir);
            Directory.SetCurrentDirectory(_testDir);

            var dict = new Dictionary<string, object>
            {
                { "visualization", DummyBase64 }
            };
            _comparisonResult = new TestComparisonResult(dict);
            _expectedBytes = Convert.FromBase64String(DummyBase64);
        }

        [TearDown]
        public void TearDown()
        {
            // Restore current directory before deleting
            string originalDir = Path.GetFullPath(Path.Combine(_testDir, ".."));
            Directory.SetCurrentDirectory(originalDir);

            try
            {
                if (Directory.Exists(_testDir))
                {
                    Directory.Delete(_testDir, true);
                }
            }
            catch
            {
                // Best effort cleanup
            }
        }

        [Test]
        public void SaveVisualizationAsFile_NullFileName_ThrowsArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => _comparisonResult.SaveVisualizationAsFile(null));
        }

        [Test]
        public void SaveVisualizationAsFile_EmptyFileName_ThrowsArgumentException()
        {
            var ex = Assert.Throws<ArgumentException>(() => _comparisonResult.SaveVisualizationAsFile(""));
            Assert.That(ex.Message, Does.Contain("The file name must not be an empty string."));
        }

        [Test]
        public void SaveVisualizationAsFile_InvalidCharacters_ThrowsArgumentException()
        {
            char[] invalidChars = Path.GetInvalidPathChars();
            if (invalidChars.Length > 0)
            {
                string invalidFileName = "test" + invalidChars[0] + ".png";
                var ex = Assert.Throws<ArgumentException>(() => _comparisonResult.SaveVisualizationAsFile(invalidFileName));
                Assert.That(ex.Message, Does.Contain("The file name contains invalid characters."));
            }
        }

        [Test]
        public void SaveVisualizationAsFile_ValidFileName_SavesFile()
        {
            string fileName = "valid_image.png";
            _comparisonResult.SaveVisualizationAsFile(fileName);

            string fullPath = Path.Combine(_testDir, fileName);
            Assert.That(File.Exists(fullPath), Is.True);
            Assert.That(File.ReadAllBytes(fullPath), Is.EqualTo(_expectedBytes));
        }

        [Test]
        public void SaveVisualizationAsFile_ValidSubdirectoryFileName_SavesFile()
        {
            string subDir = "subdir";
            Directory.CreateDirectory(Path.Combine(_testDir, subDir));
            string fileName = Path.Combine(subDir, "valid_image.png");
            
            _comparisonResult.SaveVisualizationAsFile(fileName);

            string fullPath = Path.Combine(_testDir, fileName);
            Assert.That(File.Exists(fullPath), Is.True);
            Assert.That(File.ReadAllBytes(fullPath), Is.EqualTo(_expectedBytes));
        }

        [Test]
        public void SaveVisualizationAsFile_PathTraversalParentDirectory_ThrowsIOException()
        {
            string fileName = "../traversal_image.png";
            Assert.Throws<IOException>(() => _comparisonResult.SaveVisualizationAsFile(fileName));
        }

        [Test]
        public void SaveVisualizationAsFile_AbsolutePathOutsideAllowed_ThrowsIOException()
        {
            string absolutePath = Path.Combine(Path.GetTempPath(), "absolute_image.png");
            Assert.Throws<IOException>(() => _comparisonResult.SaveVisualizationAsFile(absolutePath));
        }

        [Test]
        public void SaveVisualizationAsFile_SymlinkTraversal_ThrowsIOException()
        {
            // Create a subfolder inside allowed directory
            string localSubDir = Path.Combine(_testDir, "local_sub");
            Directory.CreateDirectory(localSubDir);

            // Create a target directory outside allowed directory (we'll simulate it by placing it next to _testDir)
            string parentDir = Path.GetFullPath(Path.Combine(_testDir, ".."));
            string externalDir = Path.Combine(parentDir, "external_target_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(externalDir);

            try
            {
                // Create a directory symlink inside local_sub pointing to externalDir
                string symlinkPath = Path.Combine(localSubDir, "symlink_dir");
                
#if NET6_0_OR_GREATER
                try
                {
                    Directory.CreateSymbolicLink(symlinkPath, externalDir);
                }
                catch (UnauthorizedAccessException)
                {
                    Assert.Ignore("Skipping symlink test: No permissions to create symbolic links on this OS/user.");
                    return;
                }
                catch (IOException)
                {
                    Assert.Ignore("Skipping symlink test: Failed to create symbolic link (not supported or permissions issue).");
                    return;
                }
#else
                Assert.Ignore("Skipping symlink test: Symbolic links are not supported on this target framework.");
                return;
#endif

                // Attempt to write a file via the symlink
                string fileName = Path.Combine("local_sub", "symlink_dir", "image.png");

                var ex = Assert.Throws<IOException>(() => _comparisonResult.SaveVisualizationAsFile(fileName));
                Assert.That(ex.Message, Does.Contain("symbolic link or reparse point"));
            }
            finally
            {
                try
                {
                    if (Directory.Exists(externalDir))
                    {
                        Directory.Delete(externalDir, true);
                    }
                }
                catch
                {
                    // Ignore cleanup errors for external directory
                }
            }
        }
    }
}
