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
                        RedirectStandardError = true,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                    }
                })
                {
                    process.Start();
                    string output = process.StandardOutput.ReadToEnd();
                    string errorOutput = process.StandardError.ReadToEnd();
                    process.WaitForExit(timeoutMilliseconds);

                    int exitCode = process.ExitCode;
                    if (exitCode != 0)
                    {
                        throw new ApplicationException($"Command exited with code {exitCode}. Error: {errorOutput}");
                    }

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
                throw new NpmNotFoundException($"NPM executable not found at path: {npmPath}. Please make sure the NPM executable is installed and check the configured path.");
            }

            return npmPath;
        }
    }
}
