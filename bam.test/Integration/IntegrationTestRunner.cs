using System.Reflection;
using Bam.Logging;

namespace Bam.Test.Integration
{
    public class IntegrationTestRunner : TestRunner<IntegrationTestMethod>
    {
        public IntegrationTestRunner(Assembly assembly, ILogger logger = null!) : base(assembly, new IntegrationTestMethodProvider { Assembly = assembly }, logger)
        {
            SetupMethodProvider = new IntegrationTestSetupMethodProvider();
            TeardownMethodProvider = new IntegrationTestTeardownMethodProvider();
        }
    }
}
