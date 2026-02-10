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
}
