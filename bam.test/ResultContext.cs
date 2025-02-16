namespace Bam.Test;

public class ResultContext<T> : ResultContext
{
    public ResultContext(Because because, object result) : base(because, result)
    {
    }

    public new T Result
    {
        get => (T)base.Result;
        set => base.Result = value;
    }
}

public class ResultContext
{
    public ResultContext(Because because, object result)
    {
        Because = because;
        Result = result;
    }
    protected Because Because { get; set; }
    public object Result { get; set; }

    public ResultContext IsNotNull()
    {
        this.Because.ItsTrue("the result is not null", Result != null, "the result IS null");
        return this;
    }

    public ResultContext Is<T>()
    {
        this.Because.ResultIs<T>();
        return this;
    }
    
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

    public T? As<T>() where T : class
    {
        return this.Result as T;
    }

    public T Cast<T>()
    {
        return (T)this.Result;
    }
}