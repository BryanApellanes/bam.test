using System.Reflection;
using Bam.Console;

namespace Bam.Test.Specification
{
    public class SpecTestTeardownMethodProvider : ITeardownMethodProvider
    {
        public List<ConsoleMethod> GetAfterAllMethods(Assembly assembly)
        {
            return new List<ConsoleMethod>();//ConsoleMethod.FromAssembly<AfterUnitTests>(assembly);
        }

        public List<ConsoleMethod> GetAfterEachMethods(Assembly assembly)
        {
            return new List<ConsoleMethod>();// ConsoleMethod.FromAssembly<AfterEachUnitTest>(assembly);
        }
    }
}
