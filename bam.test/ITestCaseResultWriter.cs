namespace Bam.Test;

public interface ITestCaseResultWriter : IAssertionWriter
{
    void WriteResult(TestCaseResult result);
    void WriteResults(IEnumerable<TestCaseResult> results);
}