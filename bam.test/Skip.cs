using System.Diagnostics.CodeAnalysis;

namespace Bam.Test
{
    /// <summary>
    /// Provides entry points for skipping a test at runtime — for decisions that cannot be made at compile time,
    /// such as the presence of an external tool or environment capability. Complements the static
    /// <c>[UnitTest(IgnoreBecause = "...")]</c> ignore, which is fixed at declaration time.
    /// </summary>
    public static class Skip
    {
        /// <summary>
        /// Unconditionally skips the current test with the specified reason.
        /// </summary>
        /// <param name="reason">A human-readable explanation of why the test is being skipped.</param>
        /// <exception cref="SkipTestException">Always thrown; the test runner reports the test as skipped.</exception>
        [DoesNotReturn]
        public static void Because(string reason)
        {
            throw new SkipTestException(reason);
        }

        /// <summary>
        /// Skips the current test with the specified reason when the condition is true.
        /// </summary>
        /// <param name="condition">The condition that, when true, causes the test to be skipped.</param>
        /// <param name="reason">A human-readable explanation of why the test is being skipped.</param>
        /// <exception cref="SkipTestException">Thrown when <paramref name="condition"/> is true.</exception>
        public static void When([DoesNotReturnIf(true)] bool condition, string reason)
        {
            if (condition)
            {
                throw new SkipTestException(reason);
            }
        }

        /// <summary>
        /// Skips the current test with the specified reason unless the condition is true.
        /// </summary>
        /// <param name="condition">The condition that, when false, causes the test to be skipped.</param>
        /// <param name="reason">A human-readable explanation of why the test is being skipped.</param>
        /// <exception cref="SkipTestException">Thrown when <paramref name="condition"/> is false.</exception>
        public static void Unless([DoesNotReturnIf(false)] bool condition, string reason)
        {
            if (!condition)
            {
                throw new SkipTestException(reason);
            }
        }
    }
}
