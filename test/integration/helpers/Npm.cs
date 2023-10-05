using OpenQA.Selenium;
using System;
using System.Diagnostics;
using System.Linq;

namespace Appium.Net.Integration.Tests.helpers
{
    internal class Npm
    {

        public static string GetNpmPrefixPath()
        {

            string npmPath = "npm";

            if (Platform.CurrentPlatform.IsPlatformType(PlatformType.Windows))
            {
                npmPath = GetNpmExecutablePath();
            }

            string npmPrefixPath = RunCommand(npmPath, "-g root");

            return npmPrefixPath.Trim();
        }

        private static string RunCommand(string command, string arguments, int timeoutMilliseconds = 30000)
        {
            try
            {
                using (Process process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = command,
                        Arguments = arguments,
                        RedirectStandardOutput = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                })
                {
                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit(timeoutMilliseconds);

                    return output;
                }
            }
            catch (Exception)
            {
                throw new NpmNotFoundException();
            }
        }

            private static string GetNpmExecutablePath()
        {
            string result = RunCommand("where", "npm");
            string npmPath;

            string[] lines = result?.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            npmPath = lines?.FirstOrDefault(line => line.EndsWith("npm.cmd"));

            if (string.IsNullOrWhiteSpace(npmPath))
            {
                throw new ApplicationException("NPM path not found.");
            }

            return npmPath;
        }
    }
}
