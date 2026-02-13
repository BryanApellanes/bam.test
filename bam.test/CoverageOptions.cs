using System.Diagnostics;
using System.Text;
using Bam.Console;

namespace Bam.Test
{
    public class CoverageOptions
    {
        public const string DefaultOutputFile = "coverage.cobertura.xml";
        public const string DefaultFormat = "cobertura";
        public const string ToolName = "dotnet-coverage";

        public static readonly List<string> DefaultIncludePatterns = new()
        {
            @".*[/\\]bam[.].*[.]dll$",
            @".*[/\\]bamdb.*[.]dll$",
            @".*[/\\]bamfs.*[.]dll$",
            @".*[/\\]bamsvc.*[.]dll$",
            @".*[/\\]bambot.*[.]dll$",
            @".*[/\\]bamux.*[.]dll$",
            @".*[/\\]oax[.].*[.]dll$"
        };

        public string OutputFile { get; set; } = DefaultOutputFile;
        public string Format { get; set; } = DefaultFormat;
        public List<string> IncludePatterns { get; set; } = new(DefaultIncludePatterns);

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
            if (arguments.Contains("coverage-include", out string? include) && !string.IsNullOrEmpty(include))
            {
                if (include == "*")
                {
                    options.IncludePatterns = new List<string>();
                }
                else
                {
                    options.IncludePatterns = new List<string>(include.Split(';', StringSplitOptions.RemoveEmptyEntries));
                }
            }
            return options;
        }

        public string? GenerateSettingsFile()
        {
            if (IncludePatterns.Count == 0)
            {
                return null;
            }

            var sb = new StringBuilder();
            sb.AppendLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            sb.AppendLine("<Configuration>");
            sb.AppendLine("  <CodeCoverage>");
            sb.AppendLine("    <ModulePaths>");
            sb.AppendLine("      <Include>");
            foreach (string pattern in IncludePatterns)
            {
                sb.AppendLine($"        <ModulePath>{pattern}</ModulePath>");
            }
            sb.AppendLine("      </Include>");
            sb.AppendLine("    </ModulePaths>");
            sb.AppendLine("  </CodeCoverage>");
            sb.AppendLine("</Configuration>");

            string tempPath = Path.Combine(Path.GetTempPath(), $"bam-coverage-{Guid.NewGuid():N}.xml");
            File.WriteAllText(tempPath, sb.ToString());
            return tempPath;
        }

        public static void CleanupSettingsFile(string? settingsPath)
        {
            if (!string.IsNullOrEmpty(settingsPath))
            {
                try { File.Delete(settingsPath); } catch { }
            }
        }

        public static string GetExtensionForFormat(string format)
        {
            return format.ToLowerInvariant() switch
            {
                "cobertura" => "cobertura.xml",
                "xml" => "xml",
                _ => format
            };
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
