using Bam.Console;

namespace Bam.Test
{
    public class TestRunnerSummary
    {
        public TestRunnerSummary()
        {
            FailedTests = new List<FailedTest>();
            PassedTests = new List<ConsoleMethod>();
            SkippedTests = new List<SkippedTest>();
        }
        public List<FailedTest> FailedTests { get; set; }
        public List<ConsoleMethod> PassedTests { get; set; }

        /// <summary>
        /// Gets or sets the tests that were skipped at runtime via the <see cref="Skip"/> entry points.
        /// </summary>
        public List<SkippedTest> SkippedTests { get; set; }
    }
}
