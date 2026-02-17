//using Bam.Testing.Unit;
using System.Reflection;

namespace Bam.Test
{
    public class TestsDiscoveredEventArgs<TTestMethod> : EventArgs where TTestMethod : TestMethod
    {
        public TestsDiscoveredEventArgs()
        {
            Tests = new List<TestMethod>();
        }
        public Assembly Assembly { get; set; } = null!;
        public ITestRunner<TTestMethod> TestRunner { get; set; } = null!;
        public List<TestMethod> Tests { get; set; }
    }
}
