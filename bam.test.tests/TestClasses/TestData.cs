namespace Bam.Test.Tests.TestClasses;

public class TestData
{
    public string Name { get; set; }

    public ulong Id { get; set; }

    public int IntProperty { get; set; }
    public uint UIntProperty { get; set; }
    public bool BooleanProperty { get; set; }
    public ulong ULongProperty { get; set; }
    public long LongProperty { get; set; }
    public decimal DecimalProperty { get; set; }
    public byte[] ByteArrayProperty { get; set; }
    public DateTime? DateTimeProperty { get; set; }
    
    public virtual List<RelatedData> RelatedData { get; set; }
}