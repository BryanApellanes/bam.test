using Bam.Data.Repositories;
using Bam.Test.Data;

namespace Bam.Test.Unit
{
    /// <summary>
    /// A local test run listener for basic 
    /// reporting of test results
    /// </summary>
    public class UnitTestRunListener : TestRunListener<UnitTestMethod>
    {
        public UnitTestRunListener()
        {
        }
        public IRepository Repository { get; set; } = null!;

        public override void TestFailed(object? sender, TestExceptionEventArgs args)
        {
            TestResult result = new TestResult(args);
            Repository.Save(result);
        }

        public override void TestPassed(object? sender, TestEventArgs<UnitTestMethod> args)
        {
            TestResult result = new TestResult(args.Test);
            Repository.Save(result);
        }
    }
}
