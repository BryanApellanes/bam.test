namespace Bam.Test;

public class SetupContext
{
    public SetupContext(ShouldContext should, Action<TestCaseRegistry> action)
    {
        this.Should = should;
        this.Action = action;
        this.When = new WhenContext(should);
    }
    public ShouldContext Should { get; set; }
    public WhenContext When { get; set; }
    public Action<TestCaseRegistry> Action { get; set; }
}