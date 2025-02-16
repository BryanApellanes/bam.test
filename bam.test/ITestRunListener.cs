namespace Bam.Test
{
    public interface ITestRunListener<TTestMethod> : ITestRunListener where TTestMethod : TestMethod
    {
        void Listen(ITestRunner<TTestMethod> runner);
        void TestFinished(object sender, TestEventArgs<TTestMethod> args);
        void TestPassed(object sender, TestEventArgs<TTestMethod> args);
        void TestsFinished(object sender, TestEventArgs<TTestMethod> args);
        void TestsStarting(object sender, TestEventArgs<TTestMethod> args);
        void TestStarting(object sender, TestEventArgs<TTestMethod> args);
    }
}

namespace Bam.Test
{
    public interface ITestRunListener
    {
        string? Tag { get; set; }
        void TestPassed(object sender, EventArgs e);
        void TestFailed(object sender, EventArgs e);
        void TestFailed(object sender, TestExceptionEventArgs args);
        void TestsStarting(object sender, EventArgs e);
        void TestStarting(object sender, EventArgs e);
        void TestFinished(object sender, EventArgs e);
        void TestsFinished(object sender, EventArgs e);
    }
}