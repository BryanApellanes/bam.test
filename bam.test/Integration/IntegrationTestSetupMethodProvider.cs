using System.Reflection;
using Bam.Console;

namespace Bam.Test.Integration
{
    public class IntegrationTestSetupMethodProvider : ISetupMethodProvider
    {
        public List<ConsoleMethod> GetBeforeAllMethods(Assembly assembly)
        {
            return ConsoleMethod.FromAssembly<BeforeIntegrationTests>(assembly);
        }

        public List<ConsoleMethod> GetBeforeEachMethods(Assembly assembly)
        {
            return ConsoleMethod.FromAssembly<BeforeEachIntegrationTest>(assembly);
        }
    }
}
