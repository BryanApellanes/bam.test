namespace Bam.Testing.Obsolete;

public class ObsoleteMethodException : Exception
{
    public ObsoleteMethodException() : base("This method is obsolete, use BamContext instead")
    { }
}