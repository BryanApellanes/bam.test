namespace Bam.Test.Specification
{
    public class FeatureSetupFailedException : SpecTestFailedException
    {
        public FeatureSetupFailedException(string description) : base($"Feature ({description}") { }
    }
}
