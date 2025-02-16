using System.Reflection;
using Bam.Console;

namespace Bam.Test.Unit
{
    public class UnitTestTeardownMethodProvider : ITeardownMethodProvider
    {
        public List<ConsoleMethod> GetAfterAllMethods(Assembly assembly)
        {
            return ConsoleMethod.FromAssembly<AfterUnitTests>(assembly);
        }

        public List<ConsoleMethod> GetAfterEachMethods(Assembly assembly)
        {
            return ConsoleMethod.FromAssembly<AfterEachUnitTest>(assembly);
        }
    }
}
