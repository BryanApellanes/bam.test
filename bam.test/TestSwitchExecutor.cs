using System.Reflection;
using System.Runtime.CompilerServices;
using Bam.Console;
using Bam.Logging;
using Bam.Test.Integration;
using Bam.Test.Specification;
using Bam.Test.Unit;

namespace Bam.Test
{
    public class TestSwitchExecutor : ITestSwitchExecutor
    {
        public bool ExecuteTestSwitches(Assembly assembly, ILogger logger, IParsedArguments arguments)
        {
            bool hasTestSwitch = arguments.Contains("ut") || arguments.Contains("spec") || arguments.Contains("it");

            if (arguments.Contains("coverage") && hasTestSwitch)
            {
                var coverageRunner = new CoverageRunner(logger);
                int exitCode = coverageRunner.Run(arguments);
                if (exitCode != 0)
                {
                    BamConsoleContext.Exit(exitCode);
                }
                return true;
            }

            bool executed = false;

            if (arguments.Contains("ut"))
            {
                var runner = new UnitTestRunner(assembly, logger);
                runner.RunAllTests();
                LogSummary(logger, runner.TestSummary);
                executed = true;
            }

            if (arguments.Contains("spec"))
            {
                var runner = new SpecTestRunner(assembly, logger);
                runner.RunAllTests();
                LogSummary(logger, runner.TestSummary);
                executed = true;
            }

            if (arguments.Contains("it", out string? itGroup))
            {
                var runner = new IntegrationTestRunner(assembly, logger);
                if (!string.IsNullOrEmpty(itGroup))
                    runner.RunTestGroup(itGroup);
                else
                    runner.RunAllTests();
                LogSummary(logger, runner.TestSummary);
                executed = true;
            }

            return executed;
        }

        private static void LogSummary(ILogger logger, TestRunnerSummary summary)
        {
            logger.Info("Test Summary: {0} passed, {1} failed, {2} skipped", summary.PassedTests.Count, summary.FailedTests.Count, summary.SkippedTests.Count);
        }
    }

    internal static class TestModuleInitializer
    {
        // Deliberate use in a library: referencing bam.test is what registers the in-process test
        // executor (BamConsoleContext warns "ensure bam.test is referenced" when none is found), so
        // test projects do not each have to wire it up in Program.cs.
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA2255:The 'ModuleInitializer' attribute should not be used in libraries",
            Justification = "Self-registration on load is the contract bam.console relies on; see BamConsoleContext's ITestSwitchExecutor lookup.")]
        [ModuleInitializer]
        internal static void Initialize()
        {
            BamConsoleContext.Current.ServiceRegistry.Set<ITestSwitchExecutor>(new TestSwitchExecutor());
        }
    }
}
