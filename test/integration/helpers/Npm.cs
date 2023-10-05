using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Linq;

namespace Appium.Net.Integration.Tests.helpers
{
    internal class Npm
    {
        static private Platform envPlatform;

        public static string GetNpmPrefixPath()
        {
            envPlatform = Platform.CurrentPlatform;
            string npmPath;
            if (envPlatform.IsPlatformType(PlatformType.Windows))
            {
                npmPath = GetNpmExecutablePath();
            }
            else
            {
                npmPath = "npm";
            }
            string result = RunCommand(npmPath, "list -g --depth=0");
            string[] lines = result?.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            string npmPrefixPath = lines[0];

            return npmPrefixPath;
        }

        private static string RunCommand(string command, string arguments)
        {
            try
            {
                Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = command,
                        Arguments = arguments,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                };

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                return output;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing command: {ex.Message}");
                return null;
            }
        }

        private static string GetNpmExecutablePath()
        {
            string result = RunCommand("where", "npm");
            string npmPath;

            string[] lines = result?.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            npmPath = lines?.FirstOrDefault(line => line.EndsWith("npm.cmd"));

            return npmPath;
        }
    }
}
