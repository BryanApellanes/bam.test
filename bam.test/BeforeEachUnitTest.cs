/*
	Copyright © Bryan Apellanes 2015  
*/

using Bam.Console;

namespace Bam.Test
{
    /// <summary>
    /// Marks a method to be executed before each individual unit test runs (setup per test).
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class BeforeEachUnitTest : ConsoleCommandAttribute
    {
    }
}
