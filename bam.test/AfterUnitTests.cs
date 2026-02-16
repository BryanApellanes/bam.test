/*
	Copyright © Bryan Apellanes 2015  
*/

using Bam.Console;

namespace Bam.Test
{
    /// <summary>
    /// Marks a method to be executed once after all unit tests in the assembly have run (global teardown).
    /// </summary>
    [Serializable]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public class AfterUnitTests : ConsoleCommandAttribute
    {
	}
}
