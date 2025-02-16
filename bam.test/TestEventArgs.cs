using Bam.Console;
using System.Reflection;

namespace Bam.Test
{
    public class TestEventArgs<TTestMethod> : EventArgs where TTestMethod : TestMethod
    {
        public TestEventArgs() { }
        public ITestRunner<TTestMethod> TestRunner { get; set; }
        public ConsoleMethod Test { get; set; }
        public Assembly Assembly { get; set; }
        /// <summary>
        /// The tag to associate with a TestExecution
        /// </summary>
        public string Tag { get; set; }
    }
}
