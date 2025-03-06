using Bam.Test.Tests.TestClasses;

namespace Bam.Test.Tests.Unit;

[UnitTestMenu("BecauseShould")]
public class BecauseObjectShould : UnitTestMenuContainer
{
    [UnitTest]
    public void ShouldHaveBaseAssertions()
    {
        Because because = new Because("test test description", new TestCaseRegistry());
        TestCase<TestData> testCase = When.A<TestData>("is used for testing", (td) => { });
        Because<TestData> becauseWithGenericParameter = new Because<TestData>(because, testCase);

        because.ItsTrue("This is an assertion", true);
        becauseWithGenericParameter.Assertions.Count.ShouldEqual(1);
        if (becauseWithGenericParameter.TestCase == null)
        {
            throw new Exception("TestCase was null");
        }
        because.Passed.ShouldBeTrue();
        becauseWithGenericParameter.Passed.ShouldBeTrue();
        becauseWithGenericParameter.TestCase.ShouldNotBeNull();
        becauseWithGenericParameter.TestCase.Description.ShouldBe(testCase.Description);
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