using MongoDB.Driver.Linq;

namespace Bam.Test;

/// <summary>
/// Abstract base class for writing test case results, providing a template for rendering
/// test summaries, descriptions, and individual assertion outcomes.
/// </summary>
public abstract class TestCaseResultWriter : ITestCaseResultWriter
{
    /// <summary>
    /// Initializes a new instance with the specified test case result.
    /// </summary>
    /// <param name="testCaseResult">The test case result to write.</param>
    public TestCaseResultWriter(TestCaseResult testCaseResult)
    {
        this.TestCaseResult = testCaseResult;
    }

    /// <summary>
    /// Gets or sets the test case result associated with this writer.
    /// </summary>
    public TestCaseResult TestCaseResult { get; set; }

    /// <summary>
    /// Writes a single test case result, including the header and all assertions.
    /// </summary>
    /// <param name="result">The test case result to write.</param>
    public void WriteResult(TestCaseResult result)
    {
        WriteHeader(result);
        foreach (Assertion assertion in result.Assertions)
        {
            WriteAssertion(assertion);
        }
    }

    /// <summary>
    /// Writes multiple test case results sequentially.
    /// </summary>
    /// <param name="results">The test case results to write.</param>
    public void WriteResults(IEnumerable<TestCaseResult> results)
    {
        foreach (TestCaseResult result in results)
        {
            WriteResult(result);
        }
    }

    /// <summary>
    /// Writes only the passed assertions from the given array.
    /// </summary>
    /// <param name="assertions">The assertions to filter and write.</param>
    public void WritePassedAssertions(Assertion[] assertions)
    {
        foreach (Assertion assertion in assertions.Where(a => a.Passed))
        {
            WritePassedAssertion(assertion);
        }
    }

    /// <summary>
    /// Writes only the failed assertions from the given array.
    /// </summary>
    /// <param name="assertions">The assertions to filter and write.</param>
    public void WriteFailedAssertions(Assertion[] assertions)
    {
        foreach (Assertion assertion in assertions.Where(a => !a.Passed))
        {
            WriteFailedAssertion(assertion);
        }
    }

    protected void WriteAssertion(Assertion assertion)
    {
        if (assertion.Passed)
        {
            WritePassedAssertion(assertion);
        }
        else
        {
            WriteFailedAssertion(assertion);
        }
    }

    protected virtual void WriteHeader(TestCaseResult testCaseResult)
    {
        WriteTestSummary(testCaseResult);
        WriteTestDescription(testCaseResult);
    }
    
    protected abstract void WriteTestSummary(TestCaseResult testCaseResult);
    protected abstract void WriteTestDescription(TestCaseResult because);
    protected abstract void WriteFailedAssertion(Assertion assertion);
    protected abstract void WritePassedAssertion(Assertion assertion);
}