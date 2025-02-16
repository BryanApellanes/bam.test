/*
	Copyright © Bryan Apellanes 2015  
*/

namespace Bam.Test.Integration
{
    [AttributeUsage(AttributeTargets.Method)]
    public class IntegrationTestAttribute : Attribute
    {
        public IntegrationTestAttribute()
        {
        }

        public IntegrationTestAttribute(string description)
        {
            Description = description;
        }

        public string Description
        {
            get;
            private set;
        }
    }
}
