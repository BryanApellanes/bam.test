using Bam.Console;

namespace Bam.Test
{
    public class TestRunnerSummary
    {
        public TestRunnerSummary()
        {
            FailedTests = new List<FailedTest>();
            PassedTests = new List<ConsoleMethod>();
        }
        public List<FailedTest> FailedTests { get; set; }
        public List<ConsoleMethod> PassedTests { get; set; }
    }
}
