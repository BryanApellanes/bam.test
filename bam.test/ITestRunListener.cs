namespace Bam.Test
{
    /// <summary>
    /// Defines a strongly-typed test run listener that subscribes to lifecycle events of a test runner.
    /// </summary>
    /// <typeparam name="TTestMethod">The type of test method this listener handles.</typeparam>
    public interface ITestRunListener<TTestMethod> : ITestRunListener where TTestMethod : TestMethod
    {
        /// <summary>
        /// Subscribes this listener to the specified test runner's events.
        /// </summary>
        /// <param name="runner">The test runner to listen to.</param>
        void Listen(ITestRunner<TTestMethod> runner);

        /// <summary>
        /// Called when an individual test finishes execution.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="args">The event data containing test details.</param>
        void TestFinished(object sender, TestEventArgs<TTestMethod> args);

        /// <summary>
        /// Called when an individual test passes.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="args">The event data containing test details.</param>
        void TestPassed(object sender, TestEventArgs<TTestMethod> args);

        /// <summary>
        /// Called when all tests have finished execution.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="args">The event data containing test run details.</param>
        void TestsFinished(object sender, TestEventArgs<TTestMethod> args);

        /// <summary>
        /// Called when all tests are about to start execution.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="args">The event data containing test run details.</param>
        void TestsStarting(object sender, TestEventArgs<TTestMethod> args);

        /// <summary>
        /// Called when an individual test is about to start execution.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="args">The event data containing test details.</param>
        void TestStarting(object sender, TestEventArgs<TTestMethod> args);
    }
}

namespace Bam.Test
{
    /// <summary>
    /// Defines a base test run listener with untyped event handlers for test lifecycle events.
    /// </summary>
    public interface ITestRunListener
    {
        /// <summary>
        /// Gets or sets a tag string to associate with the test execution.
        /// </summary>
        string? Tag { get; set; }

        /// <summary>
        /// Called when a test passes.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The event data.</param>
        void TestPassed(object sender, EventArgs e);

        /// <summary>
        /// Called when a test fails.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The event data.</param>
        void TestFailed(object sender, EventArgs e);

        /// <summary>
        /// Called when a test fails with exception details.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="args">The event data containing the exception.</param>
        void TestFailed(object sender, TestExceptionEventArgs args);

        /// <summary>
        /// Called when all tests are about to start.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The event data.</param>
        void TestsStarting(object sender, EventArgs e);

        /// <summary>
        /// Called when an individual test is about to start.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The event data.</param>
        void TestStarting(object sender, EventArgs e);

        /// <summary>
        /// Called when an individual test finishes.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The event data.</param>
        void TestFinished(object sender, EventArgs e);

        /// <summary>
        /// Called when all tests have finished.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="e">The event data.</param>
        void TestsFinished(object sender, EventArgs e);
    }
}