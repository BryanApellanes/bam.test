namespace Bam.Test;

public class WhenContext
{
    public WhenContext(ShouldContext should)
    {
        this.Should = should;
    }

    protected ShouldContext Should
    {
        get;
        set;
    }
    
    public TestCase<T> A<T>(string testCaseDescription, Func<T, object> test) where T : new()
    {
        TestCaseRegistry testCaseRegistry = new TestCaseRegistry();
        testCaseRegistry.Set(new T());
        return new TestCase<T>(testCaseRegistry, TestCaseRegistry.GetActionDescription<T>(testCaseDescription), test)
        {
            Summary = this.Should.TestSummary
        };
    }
}