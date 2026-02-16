namespace Bam.Test;

/// <summary>
/// Represents the result of a strongly-typed test case, including the test case and its Because assertions.
/// </summary>
/// <typeparam name="T">The type of the object under test.</typeparam>
public class TestCaseResult<T>: TestCaseResult
{
    /// <summary>
    /// Initializes a new instance with the specified test case and Because assertions.
    /// </summary>
    /// <param name="testCase">The test case that produced this result.</param>
    /// <param name="because">The Because object containing the test's assertions.</param>
    public TestCaseResult(TestCase<T> testCase, Because<T> because) : base(because)
    {
        this.Because = because;
        this.TestCase = testCase;
        this.Summary = testCase.Summary;
    }

    protected new Because<T> Because
    {
        get => (base.Because as Because<T>)!;
        set => base.Because = value;
    }

    /// <summary>
    /// Gets or sets the test case that produced this result.
    /// </summary>
    public TestCase<T> TestCase { get; set; }
}

/// <summary>
/// Represents the result of a test case, including its assertions, description, and pass/fail status.
/// </summary>
public class TestCaseResult
{
    /// <summary>
    /// Initializes a new instance with the specified Because object containing assertions.
    /// </summary>
    /// <param name="because">The Because object containing the test's assertions.</param>
    public TestCaseResult(Because because)
    {
        this.Because = because;
    }

    protected Because Because { get; set; }

    /// <summary>
    /// Gets the array of assertions made during the test.
    /// </summary>
    public Assertion[] Assertions => this.Because.Assertions.ToArray();

    /// <summary>
    /// Gets or sets a summary description of the test case.
    /// </summary>
    public string Summary { get; set; } = string.Empty;

    /// <summary>
    /// Gets the test description from the Because object.
    /// </summary>
    public string TestDescription => this.Because.TestDescription;

    /// <summary>
    /// Gets a value indicating whether all assertions in the test passed.
    /// </summary>
    public bool Passed => this.Because.Passed;
}