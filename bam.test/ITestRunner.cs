namespace Bam.Test
{
    /// <summary>
    /// Defines a test runner that discovers, manages, and executes tests of a specific test method type.
    /// </summary>
    /// <typeparam name="TTestMethod">The type of test method this runner handles.</typeparam>
    public interface ITestRunner<TTestMethod> where TTestMethod : TestMethod
    {
        /// <summary>
        /// Gets or sets a tag to associate with this test execution.
        /// </summary>
        string Tag { get; set; }

        /// <summary>
        /// Gets or sets the summary tracking passed and failed tests.
        /// </summary>
        TestRunnerSummary TestSummary { get; set; }

        /// <summary>
        /// Raised when an invalid test number is specified.
        /// </summary>
        event EventHandler InvalidTestNumberSpecified;

        /// <summary>
        /// Raised when no tests are discovered in the assembly.
        /// </summary>
        event EventHandler NoTestsDiscovered;

        /// <summary>
        /// Raised when a test fails.
        /// </summary>
        event EventHandler TestFailed;

        /// <summary>
        /// Raised when a test is skipped because it is marked as ignored.
        /// </summary>
        event EventHandler TestIgnored;

        /// <summary>
        /// Raised when a test is skipped at runtime via the <see cref="Skip"/> entry points.
        /// </summary>
        event EventHandler TestSkipped;

        /// <summary>
        /// Raised when an individual test finishes execution.
        /// </summary>
        event EventHandler TestFinished;

        /// <summary>
        /// Raised when an individual test passes.
        /// </summary>
        event EventHandler TestPassed;

        /// <summary>
        /// Raised when tests are discovered in the assembly.
        /// </summary>
        event EventHandler TestsDiscovered;

        /// <summary>
        /// Raised when all tests have finished execution.
        /// </summary>
        event EventHandler TestsFinished;

        /// <summary>
        /// Raised when all tests are about to start execution.
        /// </summary>
        event EventHandler TestsStarting;

        /// <summary>
        /// Raised when an individual test is about to start.
        /// </summary>
        event EventHandler TestStarting;

        /// <summary>
        /// Gets or sets the provider used to discover test methods.
        /// </summary>
        TestMethodProvider<TTestMethod> TestMethodProvider { get; set; }

        /// <summary>
        /// Gets the list of discovered test methods.
        /// </summary>
        /// <returns>A list of test methods.</returns>
        List<TTestMethod> GetTests();

        /// <summary>
        /// Runs all discovered tests.
        /// </summary>
        void RunAllTests();

        /// <summary>
        /// Runs only the tests belonging to the specified test group.
        /// </summary>
        /// <param name="testGroup">The name of the test group to run.</param>
        void RunTestGroup(string testGroup);

        /// <summary>
        /// Runs the tests specified by the given identifiers string, which can be "all", a comma-separated list, a range (e.g., "1-5"), or a single test number.
        /// </summary>
        /// <param name="testIdentifiers">The test identifiers to run.</param>
        void RunSpecifiedTests(string testIdentifiers);

        /// <summary>
        /// Runs a single test identified by its 1-based number.
        /// </summary>
        /// <param name="testNumber">The 1-based test number to run.</param>
        void RunTest(string testNumber);

        /// <summary>
        /// Runs the specified test method.
        /// </summary>
        /// <param name="test">The test method to run.</param>
        void RunTest(TestMethod test);

        /// <summary>
        /// Runs all tests in the range from the specified start number to the end number (inclusive).
        /// </summary>
        /// <param name="fromNumber">The 1-based start test number.</param>
        /// <param name="toNumber">The 1-based end test number.</param>
        void RunTestRange(string fromNumber, string toNumber);

        /// <summary>
        /// Runs the tests identified by the specified array of test numbers.
        /// </summary>
        /// <param name="testNumbers">An array of 1-based test number strings.</param>
        void RunTestSet(string[] testNumbers);
    }
}