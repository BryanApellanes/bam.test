# bam.test.tests

Unit tests for the bam.test framework itself, validating the test case lifecycle, assertion tracking, and DaoRepository integration.

## Overview

bam.test.tests is an executable test project that validates the internal behavior of the bam.test framework. It uses the same menu-driven test runner it is testing, bootstrapping via `BamConsoleContext.StaticMain(args)`. This project serves both as a test suite and as a living example of how to author tests with the Bam Framework test API.

The test suite covers several critical areas: `TestCase<T>` lifecycle (ensuring test cases have summaries, summaries survive setup chains, and exceptions during assertions are properly caught and reported), the `Because` object behavior (assertion tracking, pass/fail state, exception reporting, result inspection via `TheResult.As<T>`), and `DaoRepository` integration (creating entries and validating repository state). The project also includes a BDD specification test example demonstrating the `Feature` / `Scenario` / `Given` / `When` / `Then` syntax.

## Key Classes

| Class | Description |
|---|---|
| `TestCaseShould` | Tests for `TestCase<T>`: verifies that test cases have summaries, summaries persist through `After.Setup`, and exceptions during assertions are caught correctly. |
| `BecauseObjectShould` | Tests for `Because` and `Because<T>`: validates assertion tracking, `Passed` state, `TestCase` reference, exception reporting via `ExpectException`, and `TheResult.As<T>` result inspection. |
| `DaoRepositoryShould` | Integration-style test validating `DaoRepository.Create()` with service registry configuration, schema generation, and result verification. |
| `SpecTestExample` | BDD specification test demonstrating `Feature` / `Scenario` / `Given` / `And` / `When` / `Then` syntax. |
| `TestData` | Simple POCO used as the object under test in test cases. |
| `RelatedData` | Additional POCO for testing data relationships. |

## Dependencies

**Project References:**
- `bam.base` -- Core framework primitives
- `bam.generators` -- Code generation (for DaoRepository tests)
- `bam.test` -- The project under test

**Target Framework:** net10.0
**Output Type:** Exe

## Usage Examples

### Run all unit tests
```bash
dotnet run --project bam.test.tests.csproj -- --ut
```

### Test case summary validation example
```csharp
[UnitTest]
public void HaveSummary()
{
    string testCaseSummary = "validate that the test case has a summary";

    ThisTest
        .Should(testCaseSummary)
        .When.A<TestData>("is instantiated for testing", td => td)
        .TheTest
        .ShouldPass(because =>
        {
            because.TestCase.IsNotNull();
            because.TheTestCase("has a summary", tc => !string.IsNullOrEmpty(tc.Summary));
            because.TheTestCase("has the expected summary", tc => tc.Summary.Equals(testCaseSummary));
        })
        .SoBeHappy()
        .UnlessItFailed();
}
```

## Known Gaps / Not Yet Implemented

- The `Integration\` folder is defined in the .csproj but currently empty.
- The `SpecTestExample` has placeholder (empty) `Given`, `And`, `When`, and `Then` action bodies, serving as a structural example rather than a functional test.
