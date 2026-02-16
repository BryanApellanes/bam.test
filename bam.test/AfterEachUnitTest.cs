/*
	Copyright © Bryan Apellanes 2015  
*/

using Bam.Console;

namespace Bam.Test
{
    /// <summary>
    /// Marks a method to be executed after each individual unit test runs (teardown per test).
    /// </summary>
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class AfterEachUnitTest : ConsoleCommandAttribute
    {
    }
}
