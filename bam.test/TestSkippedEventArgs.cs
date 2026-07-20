namespace Bam.Test
{
    /// <summary>
    /// Event data for a test that was skipped at runtime via the <see cref="Skip"/> entry points.
    /// </summary>
    public class TestSkippedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TestSkippedEventArgs"/> class.
        /// </summary>
        public TestSkippedEventArgs()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TestSkippedEventArgs"/> class.
        /// </summary>
        /// <param name="test">The test method that was skipped.</param>
        /// <param name="reason">The human-readable explanation of why the test was skipped.</param>
        public TestSkippedEventArgs(TestMethod test, string reason)
        {
            Test = test;
            Reason = reason;
        }

        /// <summary>
        /// Gets or sets the test method that was skipped.
        /// </summary>
        public TestMethod Test { get; set; } = null!;

        /// <summary>
        /// Gets or sets the human-readable explanation of why the test was skipped.
        /// </summary>
        public string Reason { get; set; } = null!;
    }
}
