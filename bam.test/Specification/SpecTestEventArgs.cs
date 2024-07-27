using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Test.Specification
{
    public class SpecTestEventArgs : EventArgs
    {
        public ScenarioSetupContext ScenarioSetupContext { get; set; }
        public ScenarioSetupAction ScenarioSetupAction { get; set; }
        public WhenAction TestAction { get; set; }
        public ThenAction AssertionAction { get; set; }

        public Exception Exception { get; set; }
    }
}
