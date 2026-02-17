using System.Reflection;
using Bam.Console;

namespace Bam.Test
{
    [Serializable]
    public abstract class TestMethod : ConsoleMethod
    {
        public TestMethod() : base()
        {
        }

        public TestMethod(MethodInfo method) : base(method)
        {
        }

        public TestMethod(MethodInfo method, Attribute actionInfo) : base(method, actionInfo)
        {
        }

        public string Tag { get; set; } = null!;
    }
}
