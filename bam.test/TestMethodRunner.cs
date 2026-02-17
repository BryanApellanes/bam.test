using Bam.Logging;

namespace Bam.Test
{
    public abstract class TestMethodRunner<T> where T : TestMethod
    {
        TestRunner<T> _runner;
        TestMethod _testMethod;
        ILogger _logger;

        public TestMethodRunner(TestRunner<T> runner, TestMethod testMethod, ILogger logger = null!)
        {
            this._runner = runner;
            this._testMethod = testMethod;
            this._logger = logger ?? new TextFileLogger(new ProcessApplicationNameProvider());
        }

        protected TestRunner<T> Runner { get { return _runner; } }

        protected TestMethod TestMethod { get { return _testMethod; } }

        protected ILogger Logger { get { return _logger; } }

        public void List(TestRunListener<T> testRunListener)
        {
            testRunListener.Listen(this.Runner);
        }

        public abstract void Run();

    }
}
