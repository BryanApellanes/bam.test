using System.Reflection;
using Bam.Console;

namespace Bam.Test.Specification
{
    public class SpecTestSetupMethodProvider : ISetupMethodProvider
    {
        public List<ConsoleMethod> GetBeforeAllMethods(Assembly assembly)
        {
            return new List<ConsoleMethod>();//ConsoleMethod.FromAssembly<BeforeUnitTests>(assembly);
        }

        public List<ConsoleMethod> GetBeforeEachMethods(Assembly assembly)
        {
            return new List<ConsoleMethod>();// ConsoleMethod.FromAssembly<BeforeEachUnitTest>(assembly);
        }
    }
}
