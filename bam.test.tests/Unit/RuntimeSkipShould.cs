using System.Reflection;
using Bam.Test.Tests.TestClasses;
using UnitTestMethod = Bam.Test.Unit.UnitTestMethod;
using UnitTestRunner = Bam.Test.Unit.UnitTestRunner;

namespace Bam.Test.Tests.Unit;

[UnitTestMenu("RuntimeSkipShould")]
public class RuntimeSkipShould : UnitTestMenuContainer
{
    [UnitTest]
    public void ReportRuntimeSkipAsSkippedNotPassedOrFailed()
    {
        When.A<UnitTestRunner>("reports a Skip.Because test as skipped",
            () => NewRunner(),
            (runner) =>
            {
                return new object[] { RunFixtureMethod(runner, nameof(SkipFixture.SkipsUnconditionally)) };
            })
        .TheTest
        .ShouldPass<object[]>((because, results) =>
        {
            SkipRunOutcome outcome = (SkipRunOutcome)results[0];
            because.ItsTrue("one test is recorded as skipped", outcome.Summary.SkippedTests.Count == 1);
            because.ItsTrue("no test is recorded as passed", outcome.Summary.PassedTests.Count == 0);
            because.ItsTrue("no test is recorded as failed", outcome.Summary.FailedTests.Count == 0);
            because.ItsTrue("the skipped test carries the reason", outcome.Summary.SkippedTests[0].Reason.Equals(SkipFixture.UnconditionalReason));
            because.ItsTrue("the TestSkipped event fired once", outcome.SkippedEvents.Count == 1);
            because.ItsTrue("the event args carry the reason", outcome.SkippedEvents[0].Reason.Equals(SkipFixture.UnconditionalReason));
            because.ItsTrue("the event args carry the test method", outcome.SkippedEvents[0].Test.Method.Name.Equals(nameof(SkipFixture.SkipsUnconditionally)));
            because.ItsTrue("the TestFinished event still fired", outcome.TestFinishedCount == 1);
        })
        .SoBeHappy()
        .UnlessItFailed();
    }

    [UnitTest]
    public void SkipWhenConditionIsTrueAndRunWhenFalse()
    {
        When.A<UnitTestRunner>("honors Skip.When by its condition",
            () => NewRunner(),
            (runner) =>
            {
                SkipRunOutcome skipped = RunFixtureMethod(runner, nameof(SkipFixture.SkipsWhenConditionIsTrue));
                SkipRunOutcome ran = RunFixtureMethod(NewRunner(), nameof(SkipFixture.DoesNotSkipWhenConditionIsFalse));
                return new object[] { skipped, ran };
            })
        .TheTest
        .ShouldPass<object[]>((because, results) =>
        {
            SkipRunOutcome skipped = (SkipRunOutcome)results[0];
            SkipRunOutcome ran = (SkipRunOutcome)results[1];
            because.ItsTrue("Skip.When(true) is skipped", skipped.Summary.SkippedTests.Count == 1);
            because.ItsTrue("Skip.When(true) carries its reason", skipped.Summary.SkippedTests[0].Reason.Equals(SkipFixture.WhenReason));
            because.ItsTrue("Skip.When(false) is not skipped", ran.Summary.SkippedTests.Count == 0);
            because.ItsTrue("Skip.When(false) passes", ran.Summary.PassedTests.Count == 1);
        })
        .SoBeHappy()
        .UnlessItFailed();
    }

    [UnitTest]
    public void SkipUnlessConditionIsFalseAndRunWhenTrue()
    {
        When.A<UnitTestRunner>("honors Skip.Unless by its condition",
            () => NewRunner(),
            (runner) =>
            {
                SkipRunOutcome skipped = RunFixtureMethod(runner, nameof(SkipFixture.SkipsUnlessConditionIsTrue));
                SkipRunOutcome ran = RunFixtureMethod(NewRunner(), nameof(SkipFixture.DoesNotSkipUnlessConditionIsTrue));
                return new object[] { skipped, ran };
            })
        .TheTest
        .ShouldPass<object[]>((because, results) =>
        {
            SkipRunOutcome skipped = (SkipRunOutcome)results[0];
            SkipRunOutcome ran = (SkipRunOutcome)results[1];
            because.ItsTrue("Skip.Unless(false) is skipped", skipped.Summary.SkippedTests.Count == 1);
            because.ItsTrue("Skip.Unless(false) carries its reason", skipped.Summary.SkippedTests[0].Reason.Equals(SkipFixture.UnlessReason));
            because.ItsTrue("Skip.Unless(true) is not skipped", ran.Summary.SkippedTests.Count == 0);
            because.ItsTrue("Skip.Unless(true) passes", ran.Summary.PassedTests.Count == 1);
        })
        .SoBeHappy()
        .UnlessItFailed();
    }

    [UnitTest]
    public void StillFailFailingTestsAndPassPassingTests()
    {
        When.A<UnitTestRunner>("leaves pass and fail behavior unchanged",
            () => NewRunner(),
            (runner) =>
            {
                SkipRunOutcome failed = RunFixtureMethod(runner, nameof(SkipFixture.FailsDeliberately));
                SkipRunOutcome passed = RunFixtureMethod(NewRunner(), nameof(SkipFixture.PassesQuietly));
                return new object[] { failed, passed };
            })
        .TheTest
        .ShouldPass<object[]>((because, results) =>
        {
            SkipRunOutcome failed = (SkipRunOutcome)results[0];
            SkipRunOutcome passed = (SkipRunOutcome)results[1];
            because.ItsTrue("a deliberate failure is recorded as failed", failed.Summary.FailedTests.Count == 1);
            because.ItsTrue("a deliberate failure is not recorded as skipped", failed.Summary.SkippedTests.Count == 0);
            because.ItsTrue("the failure exception is preserved", failed.Summary.FailedTests[0].Exception.Message.Equals(SkipFixture.FailureMessage));
            because.ItsTrue("a passing test still passes", passed.Summary.PassedTests.Count == 1);
            because.ItsTrue("a passing test is not skipped", passed.Summary.SkippedTests.Count == 0);
        })
        .SoBeHappy()
        .UnlessItFailed();
    }

    [UnitTest]
    public void LeaveStaticIgnoreBehaviorUnchanged()
    {
        When.A<UnitTestRunner>("still ignores tests marked IgnoreBecause",
            () => NewRunner(),
            (runner) =>
            {
                List<TestIgnoredEventArgs> ignoredEvents = new List<TestIgnoredEventArgs>();
                runner.TestIgnored += (sender, e) => ignoredEvents.Add((TestIgnoredEventArgs)e);
                UnitTestMethod ignoredTest = FixtureTestMethod(nameof(SkipFixture.PassesQuietly), new UnitTest { IgnoreBecause = "statically ignored" });
                runner.RunTest(ignoredTest);
                return new object[] { new IgnoreRunOutcome(runner.TestSummary, ignoredEvents) };
            })
        .TheTest
        .ShouldPass<object[]>((because, results) =>
        {
            IgnoreRunOutcome outcome = (IgnoreRunOutcome)results[0];
            because.ItsTrue("the TestIgnored event fired once", outcome.IgnoredEvents.Count == 1);
            because.ItsTrue("an ignored test is not recorded as passed", outcome.Summary.PassedTests.Count == 0);
            because.ItsTrue("an ignored test is not recorded as failed", outcome.Summary.FailedTests.Count == 0);
            because.ItsTrue("an ignored test is not recorded as skipped", outcome.Summary.SkippedTests.Count == 0);
        })
        .SoBeHappy()
        .UnlessItFailed();
    }

    private static UnitTestRunner NewRunner()
    {
        return new UnitTestRunner(typeof(RuntimeSkipShould).Assembly);
    }

    private static UnitTestMethod FixtureTestMethod(string methodName, UnitTest attribute)
    {
        MethodInfo methodInfo = typeof(SkipFixture).GetMethod(methodName)!;
        return new UnitTestMethod(methodInfo, attribute);
    }

    private static SkipRunOutcome RunFixtureMethod(UnitTestRunner runner, string methodName)
    {
        List<TestSkippedEventArgs> skippedEvents = new List<TestSkippedEventArgs>();
        int testFinishedCount = 0;
        runner.TestSkipped += (sender, e) => skippedEvents.Add((TestSkippedEventArgs)e);
        runner.TestFinished += (sender, e) => testFinishedCount++;
        runner.RunTest(FixtureTestMethod(methodName, new UnitTest()));
        return new SkipRunOutcome(runner.TestSummary, skippedEvents, testFinishedCount);
    }

    private sealed record SkipRunOutcome(TestRunnerSummary Summary, List<TestSkippedEventArgs> SkippedEvents, int TestFinishedCount);

    private sealed record IgnoreRunOutcome(TestRunnerSummary Summary, List<TestIgnoredEventArgs> IgnoredEvents);
}
