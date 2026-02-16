namespace Bam.Test;

/// <summary>
/// Defines methods for writing test case results and their assertions.
/// </summary>
public interface ITestCaseResultWriter : IAssertionWriter
{
    /// <summary>
    /// Writes a single test case result including its assertions.
    /// </summary>
    /// <param name="result">The test case result to write.</param>
    void WriteResult(TestCaseResult result);

    /// <summary>
    /// Writes multiple test case results.
    /// </summary>
    /// <param name="results">The test case results to write.</param>
    void WriteResults(IEnumerable<TestCaseResult> results);
}