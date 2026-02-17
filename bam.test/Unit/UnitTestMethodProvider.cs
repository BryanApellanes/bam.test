namespace Bam.Test.Unit
{
    public class UnitTestMethodProvider : TestMethodProvider<UnitTestMethod>
    {
        public UnitTestMethodProvider() { }

        public override List<UnitTestMethod> GetTests(string? testGroup = null)
        {
            return string.IsNullOrEmpty(testGroup) ? UnitTestMethod.FromAssembly(Assembly) : UnitTestMethod.FromAssembly(Assembly, testGroup);
        }
    }
}
