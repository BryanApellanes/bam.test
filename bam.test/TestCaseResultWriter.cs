using MongoDB.Driver.Linq;

namespace Bam.Test;

public abstract class TestCaseResultWriter : ITestCaseResultWriter
{
    public TestCaseResultWriter(TestCaseResult testCaseResult)
    {
        this.TestCaseResult = testCaseResult;
    }
    
    public TestCaseResult TestCaseResult { get; set; }

    public void WriteResult(TestCaseResult result)
    {
        WriteHeader(result);
        foreach (Assertion assertion in result.Assertions)
        {
            WriteAssertion(assertion);
        }
    }

    public void WriteResults(IEnumerable<TestCaseResult> results)
    {
        foreach (TestCaseResult result in results)
        {
            WriteResult(result);
        }
    }

    public void WritePassedAssertions(Assertion[] assertions)
    {
        foreach (Assertion assertion in assertions.Where(a => a.Passed))
        {
            WritePassedAssertion(assertion);
        }
    }

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