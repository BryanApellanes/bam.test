namespace Bam.Test.Specification
{
    public class SpecTestEventArgs : EventArgs
    {
        public ScenarioSetupContext ScenarioSetupContext { get; set; } = null!;
        public ScenarioSetupAction ScenarioSetupAction { get; set; } = null!;
        public WhenAction TestAction { get; set; } = null!;
        public ThenAction AssertionAction { get; set; } = null!;

        public Exception Exception { get; set; } = null!;
    }
}
