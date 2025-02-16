using System.Reflection;
using Bam.Console;

namespace Bam.Test.Unit
{
    public class UnitTestSetupMethodProvider : ISetupMethodProvider
    {
        public List<ConsoleMethod> GetBeforeAllMethods(Assembly assembly)
        {
            return ConsoleMethod.FromAssembly<BeforeUnitTests>(assembly);
        }

        public List<ConsoleMethod> GetBeforeEachMethods(Assembly assembly)
        {
            return ConsoleMethod.FromAssembly<BeforeEachUnitTest>(assembly);
        }
    }
}
