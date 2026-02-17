/*
	Copyright © Bryan Apellanes 2015  
*/

//using Bam.Testing.Unit;

namespace Bam.Test
{
    /// <summary>
    /// Provides event data for test failure events, including the test method and the exception that occurred.
    /// </summary>
    public class TestExceptionEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TestExceptionEventArgs"/>.
        /// </summary>
        public TestExceptionEventArgs() { }

        /// <summary>
        /// Initializes a new instance with the specified test method and exception.
        /// </summary>
        /// <param name="testMethod">The test method that failed.</param>
        /// <param name="ex">The exception that caused the failure.</param>
        public TestExceptionEventArgs(TestMethod testMethod, Exception ex)
        {
            TestMethod = testMethod;
            Exception = ex;
        }

        /// <summary>
        /// Gets or sets the exception that caused the test failure.
        /// </summary>
        public Exception Exception { get; set; } = null!;

        /// <summary>
        /// Gets or sets the test method that failed.
        /// </summary>
        public TestMethod TestMethod { get; set; } = null!;
    }
}
