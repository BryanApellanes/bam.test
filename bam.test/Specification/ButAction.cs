using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Test.Specification
{
    public class ButAction
    {
        public string Description { get; set; }
        public Action<ButDelegate> Action { get; set; }
    }
}
