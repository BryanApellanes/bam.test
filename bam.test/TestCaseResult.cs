namespace Bam.Test;

public class TestCaseResult<T>: TestCaseResult
{
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

    public TestCase<T> TestCase { get; set; }
}

public class TestCaseResult
{
    public TestCaseResult(Because because)
    {
        this.Because = because;
    }

    protected Because Because { get; set; }

    public Assertion[] Assertions => this.Because.Assertions.ToArray();
    public string Summary { get; set; } = string.Empty;
    
    public string TestDescription => this.Because.TestDescription;

    public bool Passed => this.Because.Passed;
}