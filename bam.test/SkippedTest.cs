using Bam.Console;

namespace Bam.Test
{
    /// <summary>
    /// Represents a test that was skipped at runtime, holding the test method and the reason it was skipped.
    /// </summary>
    public class SkippedTest
    {
        /// <summary>
        /// Gets or sets the test method that was skipped.
        /// </summary>
        public ConsoleMethod Test { get; set; } = null!;

        /// <summary>
        /// Gets or sets the human-readable explanation of why the test was skipped.
        /// </summary>
        public string Reason { get; set; } = null!;
    }
}
