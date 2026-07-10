using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace OpenQA.Selenium.Appium
{
    internal static class PathHelpers
    {
        private static readonly System.Reflection.PropertyInfo LinkTargetProperty = 
            typeof(FileSystemInfo).GetProperty("LinkTarget");

        private static readonly bool IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

        public static string ValidateAndGetFullPath(string fileName)
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

            var invalidFileNameChars = Path.GetInvalidFileNameChars();
            var separators = new[] { Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar };
            string[] parts = fileName.Split(separators, StringSplitOptions.RemoveEmptyEntries);

            var reservedDeviceNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "CON", "PRN", "AUX", "NUL",
                "COM0", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6", "COM7", "COM8", "COM9",
                "LPT0", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5", "LPT6", "LPT7", "LPT8", "LPT9"
            };

            for (int i = 0; i < parts.Length; i++)
            {
                string part = parts[i];

                if (IsWindows)
                {
                    string baseName = Path.GetFileNameWithoutExtension(part);
                    if (reservedDeviceNames.Contains(baseName))
                    {
                        throw new ArgumentException("The file name contains a reserved device name.", nameof(fileName));
                    }
                }

                foreach (char c in part)
                {
                    if (Array.IndexOf(invalidFileNameChars, c) >= 0)
                    {
                        if (IsWindows && c == ':' && i == 0 && part.Length == 2 && char.IsLetter(part[0]) && part[1] == ':')
                        {
                            continue;
                        }
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

            var comparison = IsWindows
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

            return fullPath;
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
                var comparison = IsWindows ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
                foreach (string entry in Directory.EnumerateFileSystemEntries(parent))
                {
                    if (Path.GetFileName(entry).Equals(name, comparison))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
