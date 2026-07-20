namespace Bam.Test
{
    public abstract class TestRunListener<TTestMethod> : ITestRunListener<TTestMethod> where TTestMethod : TestMethod
    {
        public void TestFailed(object? sender, EventArgs e)
        {
            TestFailed(sender, e.CopyAs<TestExceptionEventArgs>());
        }

        public void TestPassed(object? sender, EventArgs e)
        {
            TestPassed(sender, e.CopyAs<TestEventArgs<TTestMethod>>());
        }

        public void TestSkipped(object? sender, EventArgs e)
        {
            TestSkipped(sender, e.CopyAs<TestSkippedEventArgs>());
        }

        public void TestsStarting(object? sender, EventArgs e)
        {
            TestsStarting(sender, e.CopyAs<TestEventArgs<TTestMethod>>());
        }

        public void TestsFinished(object? sender, EventArgs e)
        {
            TestsFinished(sender, e.CopyAs<TestEventArgs<TTestMethod>>());
        }

        public void TestStarting(object? sender, EventArgs e)
        {
            TestStarting(sender, e.CopyAs<TestEventArgs<TTestMethod>>());
        }

        public void TestFinished(object? sender, EventArgs e)
        {
            TestFinished(sender, e.CopyAs<TestEventArgs<TTestMethod>>());
        }

        public virtual void Listen(ITestRunner<TTestMethod> runner)
        {
            runner.TestFailed += TestFailed;
            runner.TestPassed += TestPassed;
            runner.TestSkipped += TestSkipped;
            runner.TestsStarting += TestsStarting;
            runner.TestStarting += TestStarting;
            runner.TestsFinished += TestsFinished;
            runner.TestFinished += TestFinished;
        }
        public string? Tag { get; set; }
        public abstract void TestFailed(object? sender, TestExceptionEventArgs args);
        public abstract void TestPassed(object? sender, TestEventArgs<TTestMethod> args);

        /// <summary>
        /// Called when a test is skipped at runtime via the <see cref="Skip"/> entry points. Override to observe skips; the default implementation does nothing.
        /// </summary>
        /// <param name="sender">The event source.</param>
        /// <param name="args">The event data containing the skipped test and the reason.</param>
        public virtual void TestSkipped(object? sender, TestSkippedEventArgs args) { }
        public virtual void TestsStarting(object? sender, TestEventArgs<TTestMethod> args) { }
        public virtual void TestStarting(object? sender, TestEventArgs<TTestMethod> args) { }
        public virtual void TestsFinished(object? sender, TestEventArgs<TTestMethod> args) { }
        public virtual void TestFinished(object? sender, TestEventArgs<TTestMethod> args) { }
    }
}

