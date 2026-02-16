using Bam.Test;

namespace bam.test;

/// <summary>
/// Provides a fluent context for configuring test setup after defining what the test should do.
/// </summary>
public class AfterContext
{
    /// <summary>
    /// Initializes a new instance with the specified ShouldContext.
    /// </summary>
    /// <param name="should">The ShouldContext that this AfterContext is associated with.</param>
    public AfterContext(ShouldContext should)
    {
        Should = should;
    }

    /// <summary>
    /// Gets or sets the ShouldContext that this AfterContext is associated with.
    /// </summary>
    public ShouldContext Should { get; set; }

    /// <summary>
    /// Creates a SetupContext with the specified setup action for configuring test dependencies.
    /// </summary>
    /// <param name="setup">The action to configure the TestCaseRegistry.</param>
    /// <returns>A new <see cref="SetupContext"/> for further fluent configuration.</returns>
    public SetupContext Setup(Action<TestCaseRegistry> setup)
    {
        return new SetupContext(Should, setup);
    }
}