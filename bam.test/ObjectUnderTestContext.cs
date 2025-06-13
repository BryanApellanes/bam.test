namespace Bam.Test;

public class ObjectUnderTestContext
{
    public ObjectUnderTestContext(Because because, object objectUnderTest)
    {
        this.Because = because;
        this.ObjectUnderTest = objectUnderTest;
    }
    
    public object ObjectUnderTest { get; set; }
    public Because Because { get; set; }

    public ObjectUnderTestContext As<T>(string truthStatementAboutTheObjectUnderTest, Func<T?, bool> assertAction, string? failureMessage = null)
    {
        this.Because.TheObjectUnderTestAs(truthStatementAboutTheObjectUnderTest, assertAction, failureMessage);
        return this;
    }

    public ObjectUnderTestContext IsNotNull()
    {
        this.Because.TheObjectUnderTestIsNotNull();
        return this;
    }

    public ObjectUnderTestContext IsObjectOfType<T>()
    {
        this.Because.ItsTrue($"The `ObjectUnderTest is {typeof(T).Name}`", ObjectUnderTest is T, $"The object under test is NOT a {typeof(T).Name}");
        return this;
    }
}