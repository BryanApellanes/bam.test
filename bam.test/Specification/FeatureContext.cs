/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Test.Specification
{
    public class FeatureContext
    {
        public FeatureContext()
        {
            Features = new Queue<FeatureContextSetup>();
        }
        public Queue<FeatureContextSetup> Features { get; set; }
    }
}
