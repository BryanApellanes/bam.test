using System.Reflection;
using System.Runtime.CompilerServices;
using Bam.Console;
using Bam.Logging;
using Bam.Test.Specification;
using Bam.Test.Unit;

namespace Bam.Test
{
    public class TestSwitchExecutor : ITestSwitchExecutor
    {
        public bool ExecuteTestSwitches(Assembly assembly, ILogger logger, IParsedArguments arguments)
        {
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

            // TODO: add "it" handling when IntegrationTestRunner is implemented

            return executed;
        }

        private static void LogSummary(ILogger logger, TestRunnerSummary summary)
        {
            logger.Info("Test Summary: {0} passed, {1} failed", summary.PassedTests.Count, summary.FailedTests.Count);
        }
    }

    internal static class TestModuleInitializer
    {
        [ModuleInitializer]
        internal static void Initialize()
        {
            BamConsoleContext.Current.ServiceRegistry.Set<ITestSwitchExecutor>(new TestSwitchExecutor());
        }
    }
}
