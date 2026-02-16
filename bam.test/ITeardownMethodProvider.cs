using Bam.Console;
using System.Reflection;

namespace Bam.Test
{
    /// <summary>
    /// Provides methods for discovering teardown (after) methods in a test assembly.
    /// </summary>
    public interface ITeardownMethodProvider
    {
        /// <summary>
        /// Gets the methods marked to run once after all tests in the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to scan for after-all methods.</param>
        /// <returns>A list of console methods to execute after all tests.</returns>
        List<ConsoleMethod> GetAfterAllMethods(Assembly assembly);

        /// <summary>
        /// Gets the methods marked to run after each individual test in the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to scan for after-each methods.</param>
        /// <returns>A list of console methods to execute after each test.</returns>
        List<ConsoleMethod> GetAfterEachMethods(Assembly assembly);
    }
}
