using Bam.Data.Schema;
using Bam.Data.Repositories;
using Bam.Data.Schema;
using Bam.Logging;
using Bam.Test.Data;

namespace Bam.Test.Unit
{
    /// <summary>
    /// A local test run listener for basic 
    /// reporting of test results
    /// </summary>
    public class UnitTestRunListener : TestRunListener<UnitTestMethod>
    {
        public UnitTestRunListener()
        {
        }

        /*public UnitTestRunListener(IDaoCodeWriter daoCodeWriter, IWrapperGenerator wrapperGenerator, string resultDirectory, string resultFileName)
        {
            DaoRepository = new DaoRepository(new SchemaProvider(), new DaoGenerator(daoCodeWriter), wrapperGenerator, new SQLiteDatabase(resultDirectory, resultFileName), Log.Default);
            DaoRepository.AddType(typeof(TestResult));
            DaoRepository.EnsureDaoAssemblyAndSchema();
        }*/

        public IDaoRepository DaoRepository { get; set; }

        public override void TestFailed(object sender, TestExceptionEventArgs args)
        {
            TestResult result = new TestResult(args);
            DaoRepository.Save(result);
        }

        public override void TestPassed(object sender, TestEventArgs<UnitTestMethod> args)
        {
            TestResult result = new TestResult(args.Test);
            DaoRepository.Save(result);
        }
    }
}
