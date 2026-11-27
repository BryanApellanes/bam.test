using Bam.Test.Integration;

namespace Bam.Test.Tests.Integration;

/// <summary>
/// A fixture that TestExitCodeShould runs in a child process with <c>--it=exit-code-fixture</c>. Its one test fails
/// only when the child's <see cref="ModeVariable"/> is <see cref="FailMode"/>, so a normal <c>--it</c> run passes it.
/// </summary>
[IntegrationTestMenu("Exit code fixture", Selector)]
public class ExitCodeFixture : IntegrationTestMenuContainer
{
    public const string Selector = "exit-code-fixture";
    public const string ModeVariable = "BAM_TEST_EXIT_CODE_FIXTURE";
    public const string FailMode = "fail";
    public const string PassMode = "pass";
    public const string FailureMessage = "deliberate exit code fixture failure";

    [IntegrationTest]
    public void FailOnlyWhenAsked()
    {
        if (FailMode.Equals(Environment.GetEnvironmentVariable(ModeVariable), StringComparison.Ordinal))
        {
            throw new InvalidOperationException(FailureMessage);
        }
    }
}
