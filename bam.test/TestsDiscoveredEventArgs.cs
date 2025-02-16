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
        public Assembly Assembly { get; set; }
        public ITestRunner<TTestMethod> TestRunner { get; set; }
        public List<TestMethod> Tests { get; set; }
    }
}
