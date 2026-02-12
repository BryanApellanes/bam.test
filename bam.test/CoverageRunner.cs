using System.Diagnostics;
using Bam.Console;
using Bam.Logging;

namespace Bam.Test
{
    public class CoverageRunner
    {
        private readonly ILogger _logger;

        public CoverageRunner(ILogger logger)
        {
            _logger = logger;
        }

        public int Run(IParsedArguments arguments)
        {
            var options = CoverageOptions.FromArguments(arguments);
            if (options == null)
            {
                _logger.Warning("CoverageRunner.Run called without --coverage flag");
                return 1;
            }

            if (!CoverageOptions.IsToolInstalled())
            {
                _logger.Error("{0} is not installed. Install with: dotnet tool install --global {0}", CoverageOptions.ToolName, CoverageOptions.ToolName);
                return 1;
            }

            string? exePath = Environment.ProcessPath;
            if (string.IsNullOrEmpty(exePath))
            {
                _logger.Error("Unable to determine current process path");
                return 1;
            }

            string filteredArgs = FilterCoverageArgs(arguments.OriginalStrings);

            string collectArgs = $"collect --output \"{options.OutputFile}\" --output-format {options.Format} -- \"{exePath}\" {filteredArgs}";

            _logger.Info("Running: {0} {1}", CoverageOptions.ToolName, collectArgs);

            var startInfo = new ProcessStartInfo
            {
                FileName = CoverageOptions.ToolName,
                Arguments = collectArgs,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using var process = new Process { StartInfo = startInfo };

            process.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                {
                    System.Console.WriteLine(e.Data);
                }
            };

            process.ErrorDataReceived += (sender, e) =>
            {
                if (e.Data != null)
                {
                    System.Console.Error.WriteLine(e.Data);
                }
            };

            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
            process.WaitForExit();

            if (process.ExitCode == 0)
            {
                _logger.Info("Coverage report written to: {0}", options.OutputFile);
            }

            return process.ExitCode;
        }

        private static string FilterCoverageArgs(string[] originalArgs)
        {
            var filtered = new List<string>();
            foreach (string arg in originalArgs)
            {
                if (arg.StartsWith("--coverage", StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }
                filtered.Add(arg);
            }
            return string.Join(" ", filtered);
        }
    }
}
