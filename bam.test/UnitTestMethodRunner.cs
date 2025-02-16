using Bam.Logging;
using Bam.Test.Unit;

namespace Bam.Test
{
    public class UnitTestMethodRunner : TestMethodRunner<UnitTestMethod>
    {
        public UnitTestMethodRunner(TestMethod testMethod, ILogger logger = null) : base(new UnitTestRunner(testMethod.Method?.DeclaringType?.Assembly), testMethod, logger)
        {
        }

        public override void Run()
        {
            this.Runner.RunTest(this.TestMethod);
        }
    }
}
