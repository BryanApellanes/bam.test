using Bam.Console;
using System.Reflection;

namespace Bam.Test
{
    /// <summary>
    /// Provides methods for discovering setup (before) methods in a test assembly.
    /// </summary>
    public interface ISetupMethodProvider
    {
        /// <summary>
        /// Gets the methods marked to run once before all tests in the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to scan for before-all methods.</param>
        /// <returns>A list of console methods to execute before all tests.</returns>
        List<ConsoleMethod> GetBeforeAllMethods(Assembly assembly);

        /// <summary>
        /// Gets the methods marked to run before each individual test in the specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to scan for before-each methods.</param>
        /// <returns>A list of console methods to execute before each test.</returns>
        List<ConsoleMethod> GetBeforeEachMethods(Assembly assembly);
    }
}
