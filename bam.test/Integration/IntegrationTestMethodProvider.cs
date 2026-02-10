namespace Bam.Test.Integration
{
    public class IntegrationTestMethodProvider : TestMethodProvider<IntegrationTestMethod>
    {
        public IntegrationTestMethodProvider()
        {
        }

        public override List<IntegrationTestMethod> GetTests(string testGroup = null)
        {
            return string.IsNullOrEmpty(testGroup)
                ? IntegrationTestMethod.FromAssembly(Assembly)
                : IntegrationTestMethod.FromAssembly(Assembly, testGroup);
        }
    }
}
