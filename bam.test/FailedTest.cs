using Bam.Console;

namespace Bam.Test
{
    /// <summary>
    /// Represents a test that failed, holding the test method and the exception that caused the failure.
    /// </summary>
    public class FailedTest
    {
        /// <summary>
        /// Gets or sets the test method that failed.
        /// </summary>
        public ConsoleMethod Test { get; set; }

        /// <summary>
        /// Gets or sets the exception that caused the test failure.
        /// </summary>
        public Exception Exception { get; set; }
    }
}
