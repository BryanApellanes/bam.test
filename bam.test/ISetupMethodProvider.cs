using Bam.Console;
using System.Reflection;

namespace Bam.Test
{
    public interface ISetupMethodProvider
    {
        List<ConsoleMethod> GetBeforeAllMethods(Assembly assembly);
        List<ConsoleMethod> GetBeforeEachMethods(Assembly assembly);
    }
}
