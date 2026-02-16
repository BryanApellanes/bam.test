# bam.test

Custom test framework for the Bam Framework providing fluent test case authoring, menu-driven test execution, and support for unit, integration, and specification (BDD) tests.

## Overview

bam.test is a self-contained test framework that does not rely on xUnit, NUnit, or MSTest. Instead, it provides its own test runner infrastructure built on top of bam.console's menu system. Tests are organized into classes adorned with `[UnitTestMenu]` and methods adorned with `[UnitTest]`, `[IntegrationTest]`, or `[SpecTest]`. The test runner is invoked via `BamConsoleContext.StaticMain` with command-line switches: `--ut` (unit tests), `--it` (integration tests), or `--spec` (specification tests).

The core authoring API is the `When.A<T>()` fluent interface. A test case starts with `When.A<T>("description", (objectUnderTest) => ...)`, which creates a `TestCase<T>`. Accessing `.TheTest` (or `.It`) triggers execution. Assertions are made in `.ShouldPass(because => ...)` using the `Because` object, which tracks assertions via `ItsTrue()`, `ItsFalse()`, provides result access via `ResultAs<T>()` and `TheResult`, and reports success/failure via `SoBeHappy().UnlessItFailed()`.

The framework also provides BDD-style specification tests through `SpecTestContainer`, which supports `Feature` / `Scenario` / `Given` / `And` / `When` / `Then` syntax. Test lifecycle hooks are available via `[BeforeUnitTests]`, `[AfterUnitTests]`, `[BeforeEachUnitTest]`, `[AfterEachUnitTest]`, and the `After.Setup(...)` fluent setup API. The `TestRunner<T>` base class manages test discovery, execution order, setup/teardown, and summary reporting.

## Key Classes

| Class | Description |
|---|---|
| `When` | Static entry point for fluent test authoring. `When.A<T>(description, test)` creates a `TestCase<T>`. |
| `TestCase<T>` | Represents a single test case. `.TheTest` / `.It` triggers execution. `.ShouldPass(...)` enters assertion phase. `.SoBeHappy()` finalizes. |
| `Because` / `Because<T>` | Assertion context passed to `ShouldPass`. Provides `ItsTrue`, `ItsFalse`, `ResultAs<T>`, `TheResult`, `TheObjectUnderTest`, `TheTestCase`, `AdditionalInformation`. |
| `TestCaseRegistry` | Per-test DI container holding the object under test and auxiliary registrations. |
| `UnitTest` | Attribute marking a method as a unit test. Extends `TestAttribute` with `TestType.Unit`. |
| `UnitTestMenu` | Class-level attribute marking a container for unit tests. Extends `MenuAttribute<UnitTest>`. |
| `UnitTestMenuContainer` | Abstract base class for unit test containers. Provides DI via `ServiceRegistry` and `Configure()`. |
| `TestRunner<T>` | Abstract test runner: discovers tests, executes with setup/teardown, fires events, reports summary. |
| `UnitTestRunner` | Concrete runner for unit tests. |
| `IntegrationTestRunner` | Concrete runner for integration tests. |
| `TestMethod` / `UnitTestMethod` / `IntegrationTestMethod` | Wrappers around test `MethodInfo` with metadata. |
| `TestRunnerSummary` | Tracks passed and failed tests for summary reporting. |
| `TestReporter` | Handles console output of test results. |
| `ThisTest` | Entry point for `ThisTest.Should(summary).After.Setup(...).When.A<T>(...)` fluent chain with test case summaries. |
| `ShouldContext` / `AfterContext` / `SetupContext` / `WhenContext` | Fluent context objects enabling the `ThisTest.Should(...).After.Setup(...).When.A<T>(...)` chain. |
| `SpecTestContainer` | Base class for BDD specification tests. Provides `Feature`, `Scenario`, `Given`, `And`, `When`, `Then` methods. |
| `SpecTestRunner` | Runner for specification tests. |
| `Assertion` | Represents a single pass/fail assertion with success and failure messages. |
| `FailedTest` | Records a failed test with its exception. |

## Dependencies

**Project References:**
- `bam.base` -- Core framework primitives, DI, logging, string extensions
- `bam.configuration` -- Configuration provider abstractions
- `bam.console` -- Console context, menu system integration, argument parsing
- `bam.data.repositories` -- Repository abstractions (for DaoRepository test integration)
- `bam.data` -- Data framework
- `bam.logging` -- Logging infrastructure
- `bam.shell` -- Menu system abstractions

**Target Framework:** net10.0
**Output Type:** Library

## Usage Examples

### Simple unit test
```csharp
[UnitTestMenu("MyTests")]
public class MyTests : UnitTestMenuContainer
{
    [UnitTest]
    public void ShouldAddNumbers()
    {
        When.A<Calculator>("adds two numbers", (calc) =>
        {
            return calc.Add(2, 3);
        })
        .TheTest
        .ShouldPass(because =>
        {
            int result = (int)because.Result;
            because.ItsTrue("result is 5", result == 5);
        })
        .SoBeHappy()
        .UnlessItFailed();
    }
}
```

### Test with setup and summary
```csharp
[UnitTest]
public void CreateEntry()
{
    ThisTest
        .Should("create an entry in the repository")
        .After.Setup(tcr =>
        {
            tcr.Set<IRepository>(new InMemoryRepository());
        })
        .When.A<IRepository>("creates an entry", (repo) =>
        {
            return repo.Create(new Item { Name = "test" });
        })
        .TheTest
        .ShouldPass(because =>
        {
            because.TheResult.IsNotNull()
                .As<Item>("has correct name", i => i.Name == "test");
        })
        .SoBeHappy()
        .UnlessItFailed();
}
```

### Expected exception test
```csharp
[UnitTest]
public void ThrowOnInvalidInput()
{
    When.A<Validator>("throws on null input", (v) =>
    {
        v.Validate(null);
    })
    .ExpectException(true)
    .TheTest
    .ShouldPass(because =>
    {
        because.ItsTrue("exception was thrown", because.TestCase?.Exception != null);
    })
    .SoBeHappy()
    .UnlessItFailed();
}
```

### BDD specification test
```csharp
public class CoffeeSpec : SpecTestContainer
{
    [UnitTest]
    public void BuyLastCoffee()
    {
        Feature("Serve coffee to earn money", () =>
        {
            Scenario("Buy last coffee", () =>
            {
                Given("there are 1 coffees left", () => { })
                    .And("I have deposited 1 dollar", () => { })
                    .When("I press the coffee button", () => { })
                    .Then("I should be served a coffee", (result) => { });
            });
        });
    }
}
```

### Run tests from command line
```bash
dotnet run --project my.tests.csproj -- --ut      # run unit tests
dotnet run --project my.tests.csproj -- --it      # run integration tests
dotnet run --project my.tests.csproj -- --spec    # run specification tests
```

## Known Gaps / Not Yet Implemented

- `TestRunner.InvokeTest` contains a `// TODO: implement isolation as a separate process runner` comment. Test isolation via separate AppDomain/process is not yet implemented; tests run in the current process.
- `IntegrationTestRunnerObsolete` is marked with `// TODO: Refactor this to follow the same pattern as unit and spec tests` and is excluded from compilation.
- The `Obsolete\` folder is excluded from compilation via the .csproj file.
- `CommandLineTestTool.cs` is excluded from compilation.
