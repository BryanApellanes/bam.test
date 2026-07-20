namespace Bam.Test
{
    /// <summary>
    /// Thrown from within a running test to signal that the test should be reported as skipped rather than passed or failed.
    /// Use the <see cref="Skip"/> entry points instead of throwing this directly.
    /// </summary>
    public class SkipTestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SkipTestException"/> class with the specified reason.
        /// </summary>
        /// <param name="reason">A human-readable explanation of why the test was skipped.</param>
        public SkipTestException(string reason) : base(reason)
        {
            Reason = reason;
        }

        /// <summary>
        /// Gets the human-readable explanation of why the test was skipped.
        /// </summary>
        public string Reason { get; }
    }
}
