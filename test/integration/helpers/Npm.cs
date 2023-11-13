using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace Appium.Net.Integration.Tests.helpers
{
    internal class Npm
    {

        public static string GetNpmPrefixPath()
        {
            string npmPath = GetNpmExecutablePath();
            string npmPrefixPath = RunCommand(npmPath, "-g root");

            return npmPrefixPath.Trim();
        }

        private static string RunCommand(string command, string arguments, int timeoutMilliseconds = 30000)
        {
            int exitCode;
            string output;
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

                    output = process.StandardOutput.ReadToEnd();
                    string errorOutput = process.StandardError.ReadToEnd();
                    _ = process.WaitForExit(timeoutMilliseconds);

                    exitCode = process.ExitCode;
                }
                if ((exitCode == 1) && command.Contains("npm"))
                {
                    Console.WriteLine($"npm Error upon command: `{arguments}`. {output}");
                    throw new NpmUnknownCommandException($"Command: `{arguments}` exited with code {exitCode}. Error: {output}");
                }

                return output;
            }

            catch (Win32Exception ex) when (command.Contains("npm"))
            {
                Console.WriteLine(ex.Message);
                throw new NpmNotFoundException($"npm not found under {command}", ex);
            }
        }

        private static string GetNpmExecutablePath()
        {
            string commandName = IsWindows() ? "where" : "which";
            string result = RunCommand(commandName, "npm");

            string npmPath;

            string[] lines = result?.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            if (IsWindows())
            {
                npmPath = lines?.FirstOrDefault(line => !string.IsNullOrWhiteSpace(line) && line.EndsWith("npm.cmd"));
            }
            else
            {
                npmPath = lines?.FirstOrDefault(line => !string.IsNullOrWhiteSpace(line));
            }

            if (string.IsNullOrWhiteSpace(npmPath))
            {
                throw new NpmNotFoundException("NPM executable not found. Please make sure the NPM executable is installed and check the configured PATH environment variable.");
            }

            return npmPath;
        }

        private static bool IsWindows()
        {
            return Environment.OSVersion.Platform == PlatformID.Win32NT;
        }
    }
}
