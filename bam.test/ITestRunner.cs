namespace Bam.Test
{
    public interface ITestRunner<TTestMethod> where TTestMethod : TestMethod
    {
        string Tag { get; set; }
        TestRunnerSummary TestSummary { get; set; }

        event EventHandler InvalidTestNumberSpecified;
        event EventHandler NoTestsDiscovered;
        event EventHandler TestFailed;
        event EventHandler TestIgnored;
        event EventHandler TestFinished;
        event EventHandler TestPassed;
        event EventHandler TestsDiscovered;
        event EventHandler TestsFinished;
        event EventHandler TestsStarting;
        event EventHandler TestStarting;

        TestMethodProvider<TTestMethod> TestMethodProvider { get; set; }
        List<TTestMethod> GetTests();
        void RunAllTests();
        void RunTestGroup(string testGroup);
        void RunSpecifiedTests(string testIdentifiers);
        void RunTest(string testNumber);
        void RunTest(TestMethod test);
        void RunTestRange(string fromNumber, string toNumber);
        void RunTestSet(string[] testNumbers);
    }
}