namespace Bam.Test.Specification
{
    public class ButAction
    {
        public string Description { get; set; } = null!;
        public Action<ButDelegate> Action { get; set; } = null!;
    }
}
