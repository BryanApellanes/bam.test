namespace Bam.Test
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class TestGroupAttribute : Attribute
    {
        public TestGroupAttribute(params string[] groups)
        {
            Groups = new HashSet<string>(groups);
        }

        public HashSet<string> Groups { get; set; }
    }
}