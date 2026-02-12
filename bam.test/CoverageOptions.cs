using System.Diagnostics;
using Bam.Console;

namespace Bam.Test
{
    public class CoverageOptions
    {
        public const string DefaultOutputFile = "coverage.cobertura.xml";
        public const string DefaultFormat = "cobertura";
        public const string ToolName = "dotnet-coverage";

        public string OutputFile { get; set; } = DefaultOutputFile;
        public string Format { get; set; } = DefaultFormat;

        public static CoverageOptions? FromArguments(IParsedArguments arguments)
        {
            if (!arguments.Contains("coverage"))
            {
                return null;
            }

            var options = new CoverageOptions();
            if (arguments.Contains("coverage-output", out string? output) && !string.IsNullOrEmpty(output))
            {
                options.OutputFile = output;
            }
            if (arguments.Contains("coverage-format", out string? format) && !string.IsNullOrEmpty(format))
            {
                options.Format = format;
            }
            return options;
        }

        public static bool IsToolInstalled()
        {
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = ToolName,
                    Arguments = "--version",
                    UseShellExecute = false,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    CreateNoWindow = true
                };

                using var process = Process.Start(startInfo);
                if (process == null)
                {
                    return false;
                }
                process.WaitForExit(5000);
                return process.ExitCode == 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
