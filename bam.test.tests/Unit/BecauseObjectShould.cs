using Bam.Test.Tests.TestClasses;

namespace Bam.Test.Tests.Unit;

[UnitTestMenu("BecauseShould")]
public class BecauseObjectShould : UnitTestMenuContainer
{
    [UnitTest]
    public void ShouldHaveBaseAssertions()
    {
        TestCase<TestData> sampleTestCase = When.A<TestData>("is used for testing", (td) => { });

        When.A<Because>("tracks assertions correctly",
            () =>
            {
                Because sampleBecause = new Because("test test description", new TestCaseRegistry());
                sampleBecause.ItsTrue("This is an assertion", true);
                return sampleBecause;
            },
            (sampleBecause) =>
            {
                Because<TestData> genericBecause = new Because<TestData>(sampleBecause, sampleTestCase);
                return new object[] { sampleBecause, genericBecause };
            })
        .TheTest
        .ShouldPass(because =>
        {
            object[] results = (object[])because.Result;
            Because sampleBecause = (Because)results[0];
            Because<TestData> genericBecause = (Because<TestData>)results[1];
            because.ItsTrue("has 1 assertion", genericBecause.Assertions.Count == 1);
            because.ItsTrue("Passed is true", sampleBecause.Passed);
            because.ItsTrue("generic Passed is true", genericBecause.Passed);
            because.ItsTrue("TestCase is not null", genericBecause.TestCase != null);
            because.ItsTrue("TestCase Description matches", genericBecause.TestCase?.Description == sampleTestCase.Description);
        })
        .SoBeHappy()
        .UnlessItFailed();
    }

    [UnitTest]
    public void ReportThrownExceptionInTestCase()
    {
        When.A<TestData>("is used for testing, but an exception is thrown by the test case", (td) => throw new Exception("this test throws on purpose"))
        .ExpectException(true)
        .TheTest
        .ShouldPass(because =>
        {
            because.TheTestCase("is not null", (tc)=> tc != null);
            because.TestCase.ShouldNotBeNull();
            because.TheTestCase("threw an exception as expected", (tc) => tc.Exception != null);
        })
        .SoBeHappy()
        .UnlessItFailed();
    }

    [UnitTest]
    public void ViewResults()
    {
        When.A<TestData>("is used for testing, but an exception is thrown by the test case", (td) => td)
            .TheTest
            .ShouldPass(because =>
            {
                because.TheTestCase("is not null", (tc)=> tc != null);
                because.TheResult.Is<TestData>();
                because.TheResult.As<TestData>("has a name", tc => !string.IsNullOrEmpty(tc.Name), "does NOT have a name");
                because.TheResult.As<TestData>("has an Id greater than 0", tc => tc.Id > 0, "does NOT have an Id greater than 0");
            })
            .SoBeHappy()
            .UnlessItFailed();
    }
}
