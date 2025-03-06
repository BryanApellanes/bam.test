using Bam.Data.Repositories;
using Bam.Data.Schema;
using Bam.DependencyInjection;
using Bam.Generators;
using Bam.Services;
using Bam.Test.Tests.TestClasses;

namespace Bam.Test.Tests.Unit;

[UnitTestMenu("DaoRepositoryShould")]
public class DaoRepositoryShould : UnitTestMenuContainer
{
    [UnitTest]
    public async Task CreateEntry()
    {
        string testCaseDescription = "DaoRepository creates entry";
        string testName = 32.RandomLetters();
        
        After.Setup(Setup)
        .When<IDaoRepository>(testCaseDescription, (daoRepo) =>
        {
            TestData testData = new TestData()
            {
                Name = testName,
            };
            return daoRepo.Create(testData);
        })
        .TheTest
        .ShouldPass(because =>
        {
            TestData? testResult = because.TheResult.As<TestData>();
            
            because.TheObjectUnderTest
                .IsNotNull()
                .As<DaoRepository>("is not null", (o) => o != null)
                .As<DaoRepository>("LastException property was not null", (o) => o?.LastException == null)
                .As<DaoRepository>("has its Database property set", (o) => o?.Database != null);
            
            because.TheResult
                .IsNotNull()
                .Is<TestData>()
                .As<TestData>($"has an id greater than zero: {testResult?.Id}", result => result?.Id > 0)
                .As<TestData>($"has the correct name: {testName}", result => result?.Name.Equals(testName));
            
        })
        .SoBeHappy()
        .UnlessItFailed();
    }

    private void Setup(TestCaseRegistry testCaseRegistry)
    {
        ServiceRegistry svcRegistry = Configure(testCaseRegistry)
            .For<IDaoCodeWriter>().Use<HandlebarsCSharpDaoCodeWriter>()
            .For<ISchemaProvider>().Use<SchemaProvider>()
            .For<IDaoGenerator>().Use<DaoGenerator>()
            .For<IWrapperGenerator>().Use<HandlebarsWrapperGenerator>();
        DaoRepository repo = svcRegistry.Get<DaoRepository>();
        repo.AddType<TestData>();
        svcRegistry.For<IDaoRepository>().Use(repo);
    }
}