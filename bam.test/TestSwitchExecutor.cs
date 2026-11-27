using System.Reflection;
using System.Runtime.CompilerServices;
using Bam.Console;
using Bam.Logging;
using Bam.Test.Integration;
using Bam.Test.Specification;
using Bam.Test.Unit;

namespace Bam.Test
{
    /// <summary>
    /// Runs the test switches (<c>--ut</c>, <c>--spec</c>, <c>--it</c>) for a test executable and ends the process
    /// with a non-zero exit code when any executed test failed, so a plain <c>dotnet run -- --ut</c> is a valid CI gate.
    /// </summary>
    public class TestSwitchExecutor : ITestSwitchExecutor
    {
        /// <summary>
        /// The exit code a test executable ends with when at least one executed test failed.
        /// </summary>
        public const int FailedTestsExitCode = 1;

        /// <summary>
        /// Executes the test switches in <paramref name="arguments"/> against <paramref name="assembly"/>. When any
        /// executed test fails, the process exits with <see cref="FailedTestsExitCode"/> after the summaries are logged.
        /// </summary>
        /// <param name="assembly">The assembly containing the tests.</param>
        /// <param name="logger">The logger the summaries are written to.</param>
        /// <param name="arguments">The parsed command line arguments.</param>
        /// <returns>True if any test switch was executed; otherwise false. A run with a failed test never returns.</returns>
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
            List<TestRunnerSummary> summaries = new List<TestRunnerSummary>();

            // Every switch goes through Record, so the "Test Summary" line bamtest parses and the summary the exit
            // code is computed from can't drift apart for one switch.
            void Record(TestRunnerSummary summary)
            {
                LogSummary(logger, summary);
                summaries.Add(summary);
                executed = true;
            }

            if (arguments.Contains("ut"))
            {
                var runner = new UnitTestRunner(assembly, logger);
                runner.RunAllTests();
                Record(runner.TestSummary);
            }

            if (arguments.Contains("spec"))
            {
                var runner = new SpecTestRunner(assembly, logger);
                runner.RunAllTests();
                Record(runner.TestSummary);
            }

            if (arguments.Contains("it", out string? itGroup))
            {
                var runner = new IntegrationTestRunner(assembly, logger);
                if (!string.IsNullOrEmpty(itGroup))
                    runner.RunTestGroup(itGroup);
                else
                    runner.RunAllTests();
                Record(runner.TestSummary);
            }

            int runExitCode = ExitCodeFor(summaries);
            if (runExitCode != 0)
            {
                BamConsoleContext.Exit(runExitCode);
            }

            return executed;
        }

        /// <summary>
        /// Gets the exit code for a set of test run summaries: <see cref="FailedTestsExitCode"/> when any summary records
        /// a failed test, otherwise 0. Skipped tests don't fail a run, and no summaries at all is 0.
        /// </summary>
        /// <param name="summaries">The summaries of the test runs that executed.</param>
        /// <returns>The process exit code for the runs.</returns>
        public static int ExitCodeFor(IEnumerable<TestRunnerSummary> summaries)
        {
            foreach (TestRunnerSummary summary in summaries)
            {
                if (summary.FailedTests.Count > 0)
                {
                    return FailedTestsExitCode;
                }
            }

            return 0;
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
