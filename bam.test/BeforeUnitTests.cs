/*
	Copyright © Bryan Apellanes 2015  
*/

using Bam.Console;

namespace Bam.Test
{
    /// <summary>
    /// Marks a method to be executed once before all unit tests in the assembly run (global setup).
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class BeforeUnitTests : ConsoleCommandAttribute
    {
    }
}
