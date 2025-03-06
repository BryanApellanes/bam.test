using Bam.Test;

namespace bam.test;

public class AfterContext
{
    public AfterContext(ShouldContext should)
    {
        Should = should;
    }

    public ShouldContext Should { get; set; }

    public SetupContext Setup(Action<TestCaseRegistry> setup)
    {
        return new SetupContext(Should, setup);
    }
}