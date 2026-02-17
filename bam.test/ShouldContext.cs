using bam.test;

namespace Bam.Test;

/// <summary>
/// Provides a fluent context for defining test expectations, offering access to After (setup) and When (test action) contexts.
/// </summary>
public class ShouldContext
{
    /// <summary>
    /// Initializes a new instance with the specified test summary description.
    /// </summary>
    /// <param name="testSummary">A summary description of what the test should verify.</param>
    public ShouldContext(string testSummary)
    {
        this.TestSummary = testSummary;
    }

    /// <summary>
    /// Gets or sets the summary description of what the test should verify.
    /// </summary>
    public string TestSummary { get; set; }

    AfterContext _afterContext = null!;
    /// <summary>
    /// Gets the AfterContext for configuring test setup via a fluent interface.
    /// </summary>
    public AfterContext After
    {
        get
        {
            if (_afterContext == null)
            {
                _afterContext = new AfterContext(this);
            }

            return _afterContext;
        }
    }
    
    private WhenContext _when = null!;
    /// <summary>
    /// Gets the WhenContext for defining the test action via a fluent interface.
    /// </summary>
    public WhenContext When
    {
        get
        {
            if (_when == null)
            {
                _when = new WhenContext(this);
            }
            return _when;
        }
    }
}