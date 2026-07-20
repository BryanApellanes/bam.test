namespace Bam.Test.Tests.TestClasses;

/// <summary>
/// Fixture methods driven directly by RuntimeSkipShould through a UnitTestRunner. Deliberately NOT
/// attributed with [UnitTest] so the main suite does not discover them; RuntimeSkipShould constructs
/// UnitTestMethod instances for them explicitly.
/// </summary>
public class SkipFixture
{
    public const string UnconditionalReason = "unconditional skip reason";
    public const string WhenReason = "skip-when-true reason";
    public const string UnlessReason = "skip-unless-false reason";
    public const string FailureMessage = "deliberate fixture failure";

    public void SkipsUnconditionally()
    {
        Skip.Because(UnconditionalReason);
    }

    public void SkipsWhenConditionIsTrue()
    {
        Skip.When(true, WhenReason);
    }

    public void DoesNotSkipWhenConditionIsFalse()
    {
        Skip.When(false, "this reason should never surface");
    }

    public void SkipsUnlessConditionIsTrue()
    {
        Skip.Unless(false, UnlessReason);
    }

    public void DoesNotSkipUnlessConditionIsTrue()
    {
        Skip.Unless(true, "this reason should never surface");
    }

    public void FailsDeliberately()
    {
        throw new InvalidOperationException(FailureMessage);
    }

    public void PassesQuietly()
    {
    }
}
