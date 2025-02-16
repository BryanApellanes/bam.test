/*
	Copyright © Bryan Apellanes 2015  
*/

namespace Bam.Test.Integration
{
    [AttributeUsage(AttributeTargets.Class)]
    public class IntegrationTestContainerAttribute : Attribute
    {
        public IntegrationTestContainerAttribute() { }
        public IntegrationTestContainerAttribute(string description)
        {
            Description = description;
        }
        public string Description { get; set; }
    }
}
