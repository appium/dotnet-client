using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace OpenQA.Selenium.Appium.ImageComparison
{
    public abstract class ComparisonResult
    {
        /// <summary>
        /// The visualization of the matching result represented as base64-encoded PNG image.
        /// </summary>
        public string Visualization => Result["visualization"].ToString();

        protected Dictionary<string, object> Result { get; }

        protected ComparisonResult(Dictionary<string, object> result)
        {
            Result = result;
        }

        public void SaveVisualizationAsFile(string fileName)
        {
            if (fileName is null)
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (fileName.Length == 0)
            {
                throw new ArgumentException("The file name must not be an empty string.", nameof(fileName));
            }

            if (fileName.IndexOfAny(Path.GetInvalidPathChars()) >= 0)
            {
                throw new ArgumentException("The file name contains invalid characters.", nameof(fileName));
            }

            string currentDirectory = Directory.GetCurrentDirectory();
            string allowedDirectory = Path.GetFullPath(currentDirectory);
            if (!allowedDirectory.EndsWith(Path.DirectorySeparatorChar.ToString()) &&
                !allowedDirectory.EndsWith(Path.AltDirectorySeparatorChar.ToString()))
            {
                allowedDirectory += Path.DirectorySeparatorChar;
            }

            string fullPath = Path.GetFullPath(Path.Combine(currentDirectory, fileName));

            var comparison = (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                ? StringComparison.OrdinalIgnoreCase
                : StringComparison.Ordinal;

            if (!fullPath.StartsWith(allowedDirectory, comparison))
            {
                throw new IOException("Path traversal or absolute path overwrite is not allowed. The file must be saved within the allowed directory.");
            }

            string relativePath = fullPath.Substring(allowedDirectory.Length);

            if (ContainsSymlinkWithinBaseDirectory(allowedDirectory, relativePath))
            {
                throw new IOException("The path to the output file traverses a symbolic link or reparse point, which is not allowed.");
            }

            File.WriteAllBytes(fullPath, Convert.FromBase64String(Visualization));
        }

        private static bool ContainsSymlinkWithinBaseDirectory(string allowedDirectory, string relativePath)
        {
            if (string.IsNullOrEmpty(relativePath) || relativePath.Equals(".", StringComparison.Ordinal))
            {
                return false;
            }

            var separators = new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
            string[] parts = relativePath.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            string currentPath = allowedDirectory;
            foreach (string part in parts)
            {
                currentPath = Path.Combine(currentPath, part);

                if (Directory.Exists(currentPath) || File.Exists(currentPath))
                {
                    FileAttributes attributes;
                    try
                    {
                        attributes = File.GetAttributes(currentPath);
                    }
                    catch (IOException)
                    {
                        // If attributes cannot be read, treat the path as unsafe.
                        return true;
                    }
                    catch (UnauthorizedAccessException)
                    {
                        // If attributes cannot be read due to permissions, treat the path as unsafe.
                        return true;
                    }

                    if ((attributes & FileAttributes.ReparsePoint) != 0)
                    {
                        // A reparse point (including symlinks/junctions) is present along the path.
                        return true;
                    }
                }
            }

            return false;
        }

        protected Rectangle ConvertToRect(object value)
        {
            var rect = value as Dictionary<string, object>;
            return new Rectangle(
                Convert.ToInt32(rect["x"]),
                Convert.ToInt32(rect["y"]),
                Convert.ToInt32(rect["width"]),
                Convert.ToInt32(rect["height"])
            );
        }

        protected List<Point> ConvertToPoint(object value)
        {
            var points = value as object[];
            var convertedPoints = new List<Point>();
            foreach(var point in points)
            {
                var currentPoint = point as Dictionary<string, object>;
                convertedPoints.Add(new Point(
                    Convert.ToInt32(currentPoint["x"]),
                    Convert.ToInt32(currentPoint["y"])
                ));
            }

            return convertedPoints;
        }
    }
}
