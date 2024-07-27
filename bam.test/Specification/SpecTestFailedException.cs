using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Test.Specification
{
    public class SpecTestFailedException : Exception
    {
        public SpecTestFailedException(string description) : base($"Specification test failed ({description})") { }
    }
}
