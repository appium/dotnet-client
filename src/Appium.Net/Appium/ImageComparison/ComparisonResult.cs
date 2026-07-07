using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace OpenQA.Selenium.Appium.ImageComparison
{
    public abstract class ComparisonResult
    {
        private static readonly System.Reflection.PropertyInfo LinkTargetProperty = 
            typeof(FileSystemInfo).GetProperty("LinkTarget");

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

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                int colonIndex = fileName.IndexOf(':');
                if (colonIndex >= 0)
                {
                    bool isValidDriveSpecifier = fileName.Length >= 2 && 
                                                 char.IsLetter(fileName[0]) && 
                                                 colonIndex == 1 && 
                                                 fileName.IndexOf(':', 2) == -1;

                    if (!isValidDriveSpecifier)
                    {
                        throw new ArgumentException("The file name contains invalid characters or alternate data streams.", nameof(fileName));
                    }
                }
            }

            string currentDirectory = Directory.GetCurrentDirectory();
            string allowedDirectory = Path.GetFullPath(currentDirectory);
            if (!allowedDirectory.EndsWith(Path.DirectorySeparatorChar.ToString()) &&
                !allowedDirectory.EndsWith(Path.AltDirectorySeparatorChar.ToString()))
            {
                allowedDirectory += Path.DirectorySeparatorChar;
            }

            string fullPath = Path.GetFullPath(Path.Combine(currentDirectory, fileName));

            var comparison = RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
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

                if (IsSymbolicLink(currentPath))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsSymbolicLink(string path)
        {
            if (LinkTargetProperty != null)
            {
                try
                {
                    FileSystemInfo info = Directory.Exists(path) 
                        ? (FileSystemInfo)new DirectoryInfo(path) 
                        : new FileInfo(path);
                    var target = LinkTargetProperty.GetValue(info) as string;
                    if (target != null)
                    {
                        return true;
                    }
                }
                catch
                {
                    // Fall through
                }
            }

            try
            {
                if (FileSystemEntryExists(path))
                {
                    var attributes = File.GetAttributes(path);
                    if ((attributes & FileAttributes.ReparsePoint) != 0)
                    {
                        return true;
                    }
                }
            }
            catch
            {
                if (FileSystemEntryExists(path))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool FileSystemEntryExists(string path)
        {
            try
            {
                string parent = Path.GetDirectoryName(path);
                if (string.IsNullOrEmpty(parent))
                {
                    return false;
                }

                if (!Directory.Exists(parent))
                {
                    return false;
                }

                string name = Path.GetFileName(path);
                string[] entries = Directory.GetFileSystemEntries(parent, name);
                return entries != null && entries.Length > 0;
            }
            catch
            {
                return false;
            }
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
