namespace Bam.Test;

/// <summary>
/// Provides a fluent context for making assertions about the object under test.
/// </summary>
public class ObjectUnderTestContext
{
    /// <summary>
    /// Initializes a new instance with the specified Because tracker and object under test.
    /// </summary>
    /// <param name="because">The Because object used for tracking assertions.</param>
    /// <param name="objectUnderTest">The object being tested.</param>
    public ObjectUnderTestContext(Because because, object objectUnderTest)
    {
        this.Because = because;
        this.ObjectUnderTest = objectUnderTest;
    }

    /// <summary>
    /// Gets or sets the object being tested.
    /// </summary>
    public object ObjectUnderTest { get; set; }

    /// <summary>
    /// Gets or sets the Because object used for tracking assertions.
    /// </summary>
    public Because Because { get; set; }

    /// <summary>
    /// Casts the object under test to type T and asserts the specified condition about it.
    /// </summary>
    /// <typeparam name="T">The type to cast the object under test to.</typeparam>
    /// <param name="truthStatementAboutTheObjectUnderTest">A description of what should be true.</param>
    /// <param name="assertAction">A function that evaluates the assertion on the cast object.</param>
    /// <param name="failureMessage">An optional message to display if the assertion fails.</param>
    /// <returns>This context for fluent chaining.</returns>
    public ObjectUnderTestContext As<T>(string truthStatementAboutTheObjectUnderTest, Func<T?, bool> assertAction, string? failureMessage = null)
    {
        this.Because.TheObjectUnderTestAs(truthStatementAboutTheObjectUnderTest, assertAction, failureMessage);
        return this;
    }

    /// <summary>
    /// Asserts that the object under test is not null.
    /// </summary>
    /// <returns>This context for fluent chaining.</returns>
    public ObjectUnderTestContext IsNotNull()
    {
        this.Because.TheObjectUnderTestIsNotNull();
        return this;
    }

    /// <summary>
    /// Asserts that the object under test is an instance of type T.
    /// </summary>
    /// <typeparam name="T">The expected type of the object under test.</typeparam>
    /// <returns>This context for fluent chaining.</returns>
    public ObjectUnderTestContext IsObjectOfType<T>()
    {
        this.Because.ItsTrue($"The `ObjectUnderTest is {typeof(T).Name}`", ObjectUnderTest is T, $"The object under test is NOT a {typeof(T).Name}");
        return this;
    }
}