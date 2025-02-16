namespace Bam.Test
{
    public class TestIgnoredEventArgs : EventArgs
    {
        public TestIgnoredEventArgs(UnitTest attribute)
        {
            Attribute = attribute;
        }

        public UnitTest Attribute { get; set; }
    }
}
