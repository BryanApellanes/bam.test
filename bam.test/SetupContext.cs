namespace Bam.Test;

/// <summary>
/// Provides a fluent context that holds the setup action and provides access to the When context for defining the test action.
/// </summary>
public class SetupContext
{
    /// <summary>
    /// Initializes a new instance with the specified ShouldContext and setup action.
    /// </summary>
    /// <param name="should">The ShouldContext defining the test summary.</param>
    /// <param name="action">The action to configure the TestCaseRegistry during setup.</param>
    public SetupContext(ShouldContext should, Action<TestCaseRegistry> action)
    {
        this.Should = should;
        this.Action = action;
        this.When = new WhenContext(should);
    }

    /// <summary>
    /// Gets or sets the ShouldContext containing the test summary.
    /// </summary>
    public ShouldContext Should { get; set; }

    /// <summary>
    /// Gets or sets the WhenContext for defining the test action.
    /// </summary>
    public WhenContext When { get; set; }

    /// <summary>
    /// Gets or sets the setup action that configures the TestCaseRegistry.
    /// </summary>
    public Action<TestCaseRegistry> Action { get; set; }
}