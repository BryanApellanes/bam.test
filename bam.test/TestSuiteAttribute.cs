namespace Bam.Test
{
    /// <summary>
    /// An attribute used to logically group tests together
    /// in a "Suite" for organization and reporting.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TestSuiteAttribute : Attribute
    {
        public TestSuiteAttribute(string title)
        {
            Title = title;
        }
        public string Title { get; set; }
    }
}
