using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.CommandLine;
using Bam.Testing;

namespace Bam.Testing.Unit
{
    public class UnitTestTeardownMethodProvider : ITeardownMethodProvider
    {
        public List<ConsoleMethod> GetAfterAllMethods(Assembly assembly)
        {
            return ConsoleMethod.FromAssembly<AfterUnitTests>(assembly);
        }

        public List<ConsoleMethod> GetAfterEachMethods(Assembly assembly)
        {
            return ConsoleMethod.FromAssembly<AfterEachUnitTest>(assembly);
        }
    }
}
