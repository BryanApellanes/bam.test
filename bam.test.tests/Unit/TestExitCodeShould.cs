using System.Diagnostics;
using System.Reflection;
using Bam.Test.Tests.Integration;

namespace Bam.Test.Tests.Unit;

/// <summary>
/// Pins bam.test#5: a test executable exits non-zero when any executed test fails, and 0 when none does.
/// </summary>
[UnitTestMenu("TestExitCodeShould")]
public class TestExitCodeShould : UnitTestMenuContainer
{
    private static readonly TimeSpan ChildTimeout = TimeSpan.FromMinutes(2);

    [UnitTest]
    public void ExitNonZeroWhenATestFails()
    {
        When.A<TestSwitchExecutor>("runs a test executable whose test fails",
            () => new TestSwitchExecutor(),
            (executor) => RunFixture(ExitCodeFixture.FailMode))
        .TheTest
        .ShouldPass<ChildRunOutcome>((because, outcome) =>
        {
            because.ItsTrue("the child finished in time", outcome.Finished);
            because.ItsTrue("the child reported the failure", outcome.Output.Contains("0 passed, 1 failed"));
            because.ItsTrue($"the child exited {TestSwitchExecutor.FailedTestsExitCode} (was {outcome.ExitCode})", outcome.ExitCode == TestSwitchExecutor.FailedTestsExitCode);
        })
        .SoBeHappy()
        .UnlessItFailed();
    }

    [UnitTest]
    public void ExitZeroWhenEveryTestPasses()
    {
        When.A<TestSwitchExecutor>("runs a test executable whose test passes",
            () => new TestSwitchExecutor(),
            (executor) => RunFixture(ExitCodeFixture.PassMode))
        .TheTest
        .ShouldPass<ChildRunOutcome>((because, outcome) =>
        {
            because.ItsTrue("the child finished in time", outcome.Finished);
            because.ItsTrue("the child reported the pass", outcome.Output.Contains("1 passed, 0 failed"));
            because.ItsTrue($"the child exited 0 (was {outcome.ExitCode})", outcome.ExitCode == 0);
        })
        .SoBeHappy()
        .UnlessItFailed();
    }

    [UnitTest]
    public void ComputeTheExitCodeFromEverySummary()
    {
        When.A<TestSwitchExecutor>("computes exit codes for passed, skipped, failed and empty runs",
            () => new TestSwitchExecutor(),
            (executor) =>
            {
                TestRunnerSummary passed = new TestRunnerSummary();
                passed.PassedTests.Add(new Bam.Console.ConsoleMethod());
                TestRunnerSummary skipped = new TestRunnerSummary();
                skipped.SkippedTests.Add(new SkippedTest { Reason = "skipped" });
                TestRunnerSummary failed = new TestRunnerSummary();
                failed.FailedTests.Add(new FailedTest { Exception = new InvalidOperationException("failed") });
                return new ExitCodeOutcome(
                    TestSwitchExecutor.ExitCodeFor(new List<TestRunnerSummary>()),
                    TestSwitchExecutor.ExitCodeFor(new List<TestRunnerSummary> { passed, skipped }),
                    TestSwitchExecutor.ExitCodeFor(new List<TestRunnerSummary> { passed, failed, skipped }));
            })
        .TheTest
        .ShouldPass<ExitCodeOutcome>((because, outcome) =>
        {
            because.ItsTrue("no runs is 0", outcome.NoRuns == 0);
            because.ItsTrue("passed and skipped runs are 0", outcome.PassedAndSkipped == 0);
            because.ItsTrue("one failed test in any run fails the process", outcome.OneFailed == TestSwitchExecutor.FailedTestsExitCode);
        })
        .SoBeHappy()
        .UnlessItFailed();
    }

    /// <summary>
    /// Runs this test executable again in a child process with only the exit code fixture selected.
    /// </summary>
    private static ChildRunOutcome RunFixture(string mode)
    {
        string processPath = Environment.ProcessPath ?? throw new InvalidOperationException("Unable to determine the current process path");
        ProcessStartInfo startInfo = new ProcessStartInfo(processPath)
        {
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        // Under `dotnet <dll>` (how bamtest runs assemblies) the process is the dotnet host, so pass the test assembly.
        if (Path.GetFileNameWithoutExtension(processPath).Equals("dotnet", StringComparison.OrdinalIgnoreCase))
        {
            string entryAssembly = Assembly.GetEntryAssembly()?.Location ?? throw new InvalidOperationException("Unable to determine the entry assembly");
            startInfo.ArgumentList.Add(entryAssembly);
        }

        startInfo.ArgumentList.Add($"--it={ExitCodeFixture.Selector}");
        startInfo.Environment["BAM_ARG_STYLE"] = "Posix";
        startInfo.Environment[ExitCodeFixture.ModeVariable] = mode;

        using Process process = new Process { StartInfo = startInfo };
        process.Start();
        Task<string> stdout = process.StandardOutput.ReadToEndAsync();
        Task<string> stderr = process.StandardError.ReadToEndAsync();
        bool finished = process.WaitForExit(ChildTimeout);
        if (!finished)
        {
            process.Kill(true);
            process.WaitForExit();
        }

        string output = stdout.GetAwaiter().GetResult() + stderr.GetAwaiter().GetResult();
        return new ChildRunOutcome(finished, finished ? process.ExitCode : -1, output);
    }

    private sealed record ChildRunOutcome(bool Finished, int ExitCode, string Output);

    private sealed record ExitCodeOutcome(int NoRuns, int PassedAndSkipped, int OneFailed);
}
