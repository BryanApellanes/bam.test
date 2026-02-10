using Bam.Console;

namespace Bam.Test
{
    [Serializable]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class BeforeIntegrationTests : ConsoleCommandAttribute
    {
    }
}
