namespace Bam.Test.Integration
{
    public class IntegrationTest : TestAttribute
    {
        public IntegrationTest() : base(TestType.Integration)
        {
        }

        public bool Ignore => !string.IsNullOrEmpty(IgnoreBecause);

        public string IgnoreBecause { get; set; } = null!;
    }
}
