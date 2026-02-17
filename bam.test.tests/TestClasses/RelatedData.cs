namespace Bam.Test.Tests.TestClasses;

public class RelatedData
{
    public string Name { get; set; } = null!;

    public virtual ulong TestDataId { get; set; }
    public virtual TestData TestData { get; set; } = null!;
    public ulong Id { get; set; }

    public int IntProperty { get; set; }
    public uint UIntProperty { get; set; }
    public bool BooleanProperty { get; set; }
    public ulong ULongProperty { get; set; }
    public long LongProperty { get; set; }
    public decimal DecimalProperty { get; set; }
    public byte[] ByteArrayProperty { get; set; } = null!;
    public DateTime? DateTimeProperty { get; set; }
}