using Bam;

namespace bam.testing;

/// <summary>
/// Provides assertion extension methods for objects in the testing framework.
/// </summary>
public static class ObjectExtensions
{
    /// <summary>
    /// Asserts that the object is an instance of the specified type T. Throws <see cref="ExpectationFailedException"/>
    /// if the object is null or not the expected type.
    /// </summary>
    /// <typeparam name="T">The expected type.</typeparam>
    /// <param name="instance">The object to check.</param>
    public static void ShouldBeInstanceOfType<T>(this object instance)
    {
        if (instance == null)
        {
            throw new ExpectationFailedException(
                $"Expected value to be instance of type {typeof(T).Name} but it was null");
        }

        Type actual = instance.GetType();
        if (actual != typeof(T))
        {
            throw new ExpectationFailedException(
                $"Expected value to be instance of type {typeof(T).Name} but it was {actual.Name ?? "null"}");
        }
    }
}