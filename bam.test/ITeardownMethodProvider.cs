using Bam.Console;
using System.Reflection;

namespace Bam.Test
{
    public interface ITeardownMethodProvider
    {
        List<ConsoleMethod> GetAfterAllMethods(Assembly assembly);
        List<ConsoleMethod> GetAfterEachMethods(Assembly assembly);
    }
}
