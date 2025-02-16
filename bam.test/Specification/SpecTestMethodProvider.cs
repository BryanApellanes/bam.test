namespace Bam.Test.Specification
{
    public class SpecTestMethodProvider : TestMethodProvider<SpecTestMethod>
    {
        public SpecTestMethodProvider() { }

        public override List<SpecTestMethod> GetTests(string testGroup = null)
        {
            return SpecTestMethod.FromAssembly(Assembly);
        }
    }
}
