namespace Bam.Test;

/// <summary>
/// Provides a context for assertions about the return value of a test.
/// </summary>
/// <typeparam name="T"></typeparam>
public class ResultContext<T> : ResultContext
{
    public ResultContext(Because because, object result) : base(because, result)
    {
    }

    /// <summary>
    /// The return value of the test.
    /// </summary>
    public new T Result
    {
        get => (T)base.Result;
        set => base.Result = value!;
    }
}

/// <summary>
/// Provides a context for assertions about the return value of a test.
/// </summary>
public class ResultContext
{
    public ResultContext(Because because, object result)
    {
        Because = because;
        Result = result;
    }
    
    /// <summary>
    /// Gets or sets the Because object used for tracking assertions.
    /// </summary>
    protected Because Because { get; set; }
    
    /// <summary>
    /// Gets or sets the return value of the test.
    /// </summary>
    public object Result { get; set; }

    /// <summary>
    /// Asserts that the result is not null.
    /// </summary>
    /// <returns></returns>
    public ResultContext IsNotNull()
    {
        this.Because.ItsTrue("the result is not null", Result != null, "the result IS null");
        return this;
    }

    /// <summary>
    /// Asserts that Result.GetType() == typeof(T) evaluates to True.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public ResultContext IsOfType<T>()
    {
        this.Because.ResultIsOfType<T>();
        return this;
    }

    public ResultContext IsEqualTo(object value)
    {
        this.Because.ResultEquals(value);
        return this;
    }

    public ResultContext EqualsEquals(object value)
    {
        this.Because.ResultEqualsEquals(value);
        return this;
    }

    /// <summary>
    /// Asserts that Result is T and returns the cast value.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns>The result cast to T.</returns>
    public T As<T>()
    {
        this.Because.ResultIs<T>();
        return (T)this.Result;
    }

    /// <summary>
    /// Casts Result to T and asserts using the provided function.
    /// Works with both value types and reference types.
    /// </summary>
    public ResultContext As<T>(string truthStatement, Func<T, bool?> assertAction, string? failureMessage = null)
    {
        this.Because.ItsTrue(truthStatement, assertAction((T)this.Result) == true, failureMessage);
        return this;
    }

    public T Cast<T>()
    {
        return (T)this.Result;
    }
}