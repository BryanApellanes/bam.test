using bam.test;

namespace Bam.Test;

public class ShouldContext
{
    public ShouldContext(string testSummary)
    {
        this.TestSummary = testSummary;
    }
    public string TestSummary { get; set; }

    AfterContext _afterContext;
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
    
    private WhenContext _when;
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