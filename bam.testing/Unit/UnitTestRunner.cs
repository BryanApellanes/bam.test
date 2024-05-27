using System.Reflection;
using Bam.Logging;

namespace Bam.Testing.Unit
{
    public class UnitTestRunner : TestRunner<UnitTestMethod>
    {
        public UnitTestRunner(Assembly assembly, ILogger logger = null) : base(assembly, new UnitTestMethodProvider { Assembly = assembly }, logger)
        {
            SetupMethodProvider = new UnitTestSetupMethodProvider();
            TeardownMethodProvider = new UnitTestTeardownMethodProvider();
        }
    }
}
