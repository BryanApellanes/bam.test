namespace Bam.Test
{
    public class UnitTestFailure
    {
        public UnitTestFailure()
        {
        }

        public UnitTestFailure(TestMethod testMethod, Exception ex)
        {
            TestMethod = testMethod;
            Exception = ex;
        }

        public UnitTestFailure(TestExceptionEventArgs testExceptionEventArgs)
        {
            this.CopyProperties(testExceptionEventArgs);
        }

        public Exception Exception { get; set; } = null!;
        public TestMethod TestMethod { get; set; } = null!;
    }
}