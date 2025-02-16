/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam.Console;

namespace Bam.Test.Specification
{

    [AttributeUsage(AttributeTargets.Method)]
    public class SpecTestAttribute : ConsoleCommandAttribute
    {
    }
}
