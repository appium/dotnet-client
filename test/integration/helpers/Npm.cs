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
            string npmPath = GetNpmExecutablePath();
            string result = RunCommand(npmPath, "config get prefix");

            result = result.Trim();

            return result;
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
            string npmPath;
            string command = envPlatform.IsPlatformType(PlatformType.Unix) || envPlatform.IsPlatformType(PlatformType.Mac) ? "which" : "where";
            string result =  RunCommand(command, "npm");

            string[] lines = result?.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            if (envPlatform.IsPlatformType(PlatformType.Windows))
            {
                npmPath = lines?.FirstOrDefault(line => line.EndsWith("npm.cmd"));
            }
            else 
            {
                npmPath = lines?.FirstOrDefault(line => line.EndsWith("npm"));
            }
            return npmPath;
        }
    }
}
