/*
	Copyright © Bryan Apellanes 2015  
*/

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
