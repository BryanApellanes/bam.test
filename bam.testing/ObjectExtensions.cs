using Bam.Net;

namespace bam.testing;

public static class ObjectExtensions
{
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