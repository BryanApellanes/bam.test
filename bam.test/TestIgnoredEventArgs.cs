using Bam.Test.Unit;
using System;
using System.Collections.Generic;
using System.Text;

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
