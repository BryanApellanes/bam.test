/*
	Copyright © Bryan Apellanes 2015  
*/

//using Bam.Testing.Unit;

namespace Bam.Test
{
    public class TestExceptionEventArgs : EventArgs
    {
        public TestExceptionEventArgs() { }
        public TestExceptionEventArgs(TestMethod testMethod, Exception ex)
        {
            TestMethod = testMethod;
            Exception = ex;
        }
        public Exception Exception { get; set; }
        public TestMethod TestMethod { get; set; }
    }
}
