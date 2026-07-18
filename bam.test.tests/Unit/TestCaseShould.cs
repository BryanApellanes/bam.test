using Bam.Test.Tests.TestClasses;

namespace Bam.Test.Tests.Unit;

[UnitTestMenu("TestCaseShould")]
public class TestCaseShould : UnitTestMenuContainer
{
    [UnitTest]
    public void HaveSummary()
    {
        string testCaseSummary = "validate that the test case has a summary";

        ThisTest
            .Should(testCaseSummary)
            .When.A<TestData>("is instantiated for testing but is ignored by this test", td=> td)
            .TheTest
            .ShouldPass(because =>
            {
                because.TestCase.IsNotNull();
                because.TheTestCase("has a summary", tc=> !string.IsNullOrEmpty(tc.Summary));
                because.TheTestCase("has the expected summary", tc=> tc.Summary.Equals(testCaseSummary));

                because.AdditionalInformation("This test is intended to test whether the test case has a summary");
            })
            .SoBeHappy()
            .UnlessItFailed();
    }

    [UnitTest]
    public void HaveSummaryAfterSetup()
    {
        string testCaseSummary = "validate that the test case has a summary after setup";

        ThisTest
            .Should(testCaseSummary)
            .After.Setup(tcr => { })
            .When.A<TestData>("is instantiated for testing but is ignored by this test", td=> td)
            .TheTest
            .ShouldPass(because =>
            {
                because.TestCase.IsNotNull(); // throws if TestCase is null
                because.TheTestCase("is not null", tc => tc != null);
                because.TheTestCase("has a summary", tc=> !string.IsNullOrEmpty(tc.Summary));
                because.TheTestCase("has the expected summary", tc=> tc.Summary.Equals(testCaseSummary));

                because.AdditionalInformation("This test is intended to test whether the test case has a summary");
            })
            .SoBeHappy()
            .UnlessItFailed();
    }

    [UnitTest]
    public void ThrowExceptionDuringAssertions()
    {
        string randomTextForValidation = 32.RandomLetters();

        When.A<TestData>("throws exception during assertions",
            (td) =>
            {
                bool exceptionCaught = false;
                string? caughtMessage = null;
                try
                {
                    When.A<TestData>("is used for testing", (inner) => { })
                        .TheTest
                        .ShouldPass(because => throw new Exception(randomTextForValidation));
                }
                catch (Exception ex)
                {
                    exceptionCaught = true;
                    caughtMessage = ex.Message;
                }
                return new object?[] { exceptionCaught, caughtMessage };
            })
        .TheTest
        .ShouldPass(because =>
        {
            object?[] results = (object?[])because.Result;
            bool exceptionCaught = (bool)results[0]!;
            string? caughtMessage = (string?)results[1];
            because.ItsTrue("exception was caught", exceptionCaught);
            because.ItsTrue("exception message matches", randomTextForValidation.Equals(caughtMessage));
        })
        .SoBeHappy()
        .UnlessItFailed();
    }

    [UnitTest]
    public void PassTypedResultToTypedShouldPass()
    {
        string expected = 32.RandomLetters();

        When.A<TestData>("returns a string result for typed validation", (td) => expected)
            .TheTest
            .ShouldPass<string>((because, result) =>
            {
                because.ItsTrue("the typed result is not null", result != null);
                because.ItsTrue("the typed result is the expected value", expected.Equals(result));
                because.TheObjectUnderTest.IsNotNull();
            })
            .SoBeHappy()
            .UnlessItFailed();
    }

    [UnitTest]
    public void TrackExceptionThrownInTypedShouldPass()
    {
        string exceptionMessage = 32.RandomLetters();

        When.A<TestData>("captures an exception thrown during typed result validation", (td) =>
        {
            Because? innerBecause = null;
            bool exceptionEscaped = false;
            try
            {
                When.A<TestData>("returns a string result", (inner) => "the result")
                    .TheTest
                    .ShouldPass<string>((because, result) =>
                    {
                        innerBecause = because;
                        throw new Exception(exceptionMessage);
                    });
            }
            catch (Exception)
            {
                exceptionEscaped = true;
            }
            bool failureWasTracked = innerBecause != null && innerBecause.Assertions.Any(a => !a.Passed && (a.FailureMessage ?? string.Empty).Contains(exceptionMessage));
            return new TypedShouldPassExceptionOutcome(exceptionEscaped, failureWasTracked);
        })
        .TheTest
        .ShouldPass<TypedShouldPassExceptionOutcome>((because, outcome) =>
        {
            because.ItsFalse("the exception did not escape ShouldPass", outcome.ExceptionEscaped);
            because.ItsTrue("the exception was tracked as a failed assertion", outcome.FailureWasTracked);
        })
        .SoBeHappy()
        .UnlessItFailed();
    }

    [UnitTest]
    public void TrackResultTypeMismatchInTypedShouldPass()
    {
        When.A<TestData>("captures an invalid cast when the typed result type does not match", (td) =>
        {
            TestCase<TestData> innerTestCase = When.A<TestData>("returns a string result", (inner) => "not an int").TheTest;
            bool lambdaWasInvoked = false;
            bool exceptionEscaped = false;
            try
            {
                innerTestCase.ShouldPass<int>((because, result) => { lambdaWasInvoked = true; });
            }
            catch (Exception)
            {
                exceptionEscaped = true;
            }
            bool failureWasTracked = innerTestCase.GetResult().Assertions.Any(a => !a.Passed);
            return new TypedShouldPassMismatchOutcome(exceptionEscaped, lambdaWasInvoked, failureWasTracked);
        })
        .TheTest
        .ShouldPass<TypedShouldPassMismatchOutcome>((because, outcome) =>
        {
            because.ItsFalse("the invalid cast did not escape ShouldPass", outcome.ExceptionEscaped);
            because.ItsFalse("the assert lambda was not invoked", outcome.LambdaWasInvoked);
            because.ItsTrue("the invalid cast was tracked as a failed assertion", outcome.FailureWasTracked);
        })
        .SoBeHappy()
        .UnlessItFailed();
    }

    private sealed record TypedShouldPassExceptionOutcome(bool ExceptionEscaped, bool FailureWasTracked);

    private sealed record TypedShouldPassMismatchOutcome(bool ExceptionEscaped, bool LambdaWasInvoked, bool FailureWasTracked);
}
