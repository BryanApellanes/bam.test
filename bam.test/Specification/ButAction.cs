namespace Bam.Test.Specification
{
    public class ButAction
    {
        public string Description { get; set; }
        public Action<ButDelegate> Action { get; set; }
    }
}
