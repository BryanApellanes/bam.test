using Bam.Logging;
using Bam.Testing.Unit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Testing
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
