using Bam.Console;
using System.Reflection;

namespace Bam.Test
{
    /// <summary>
    /// Provides event data for test lifecycle events, including the test runner, test method, and assembly.
    /// </summary>
    /// <typeparam name="TTestMethod">The type of test method.</typeparam>
    public class TestEventArgs<TTestMethod> : EventArgs where TTestMethod : TestMethod
    {
        /// <summary>
        /// Initializes a new instance of <see cref="TestEventArgs{TTestMethod}"/>.
        /// </summary>
        public TestEventArgs() { }

        /// <summary>
        /// Gets or sets the test runner that raised the event.
        /// </summary>
        public ITestRunner<TTestMethod> TestRunner { get; set; } = null!;

        /// <summary>
        /// Gets or sets the test method associated with the event.
        /// </summary>
        public ConsoleMethod Test { get; set; } = null!;

        /// <summary>
        /// Gets or sets the assembly containing the tests.
        /// </summary>
        public Assembly Assembly { get; set; } = null!;
        /// <summary>
        /// The tag to associate with a TestExecution
        /// </summary>
        public string Tag { get; set; } = null!;
    }
}
