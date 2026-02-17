namespace Bam.Test.Specification
{
    public class ThenAction
    {
        public string Description { get; set; } = null!;
        public Action<ThenDelegate> Action { get; set; } = null!;
    }
}
