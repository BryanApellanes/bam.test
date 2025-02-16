using System.Reflection;

namespace Bam.Test
{
    public abstract class TestMethodProvider<TTestMethod> where TTestMethod : TestMethod
    {
        public Assembly Assembly { get; set; }

        public abstract List<TTestMethod> GetTests(string testGroup = null);
    }
}
