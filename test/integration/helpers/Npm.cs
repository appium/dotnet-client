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
                    _ = process.WaitForExit(timeoutMilliseconds);

                    int exitCode = process.ExitCode;
                    if (exitCode == 1)
                    {
                        throw new NpmUnknownCommandException($"Command: `{arguments}` exited with code {exitCode}. Error: {output}");
                    }
                    else if (exitCode != 0)
                    {
                        throw new ApplicationException($"Command: `{command}` exited with code {exitCode}. Error: {errorOutput}");
                    }

                    return output;
                }
            }
            catch (ApplicationException ex)
            {
                throw ex;
            }

            catch (Win32Exception)
            {
                throw;
            }
            catch (NpmUnknownCommandException)
            {
                throw;
            }
            catch (Exception)
            {
                throw new NpmNotFoundException();
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
