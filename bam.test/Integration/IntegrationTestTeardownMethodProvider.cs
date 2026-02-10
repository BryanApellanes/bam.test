using System.Reflection;
using Bam.Console;

namespace Bam.Test.Integration
{
    public class IntegrationTestTeardownMethodProvider : ITeardownMethodProvider
    {
        public List<ConsoleMethod> GetAfterAllMethods(Assembly assembly)
        {
            return ConsoleMethod.FromAssembly<AfterIntegrationTests>(assembly);
        }

        public List<ConsoleMethod> GetAfterEachMethods(Assembly assembly)
        {
            return ConsoleMethod.FromAssembly<AfterEachIntegrationTest>(assembly);
        }
    }
}
