using Bam.Console;
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
        try
        {
            When.A<TestData>("is used for testing", (td) => { })
                .TheTest
                .ShouldPass(because => throw new Exception(randomTextForValidation));
        }
        catch (Exception ex)
        {
            ex.Message.ShouldEqual(randomTextForValidation);
            Message.PrintLine($"Exception was thrown and had the expected text {ex.Message}");
        }
    }
}