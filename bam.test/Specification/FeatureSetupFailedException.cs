using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Test.Specification
{
    public class FeatureSetupFailedException : SpecTestFailedException
    {
        public FeatureSetupFailedException(string description) : base($"Feature ({description}") { }
    }
}
