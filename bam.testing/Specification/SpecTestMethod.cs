using Bam.CommandLine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Bam;

namespace Bam.Testing.Specification
{
    [Serializable]
    public class SpecTestMethod : TestMethod
    {
        public SpecTestMethod() : base()
        {
        }

        public SpecTestMethod(MethodInfo method) : base(method)
        {
        }

        public SpecTestMethod(MethodInfo method, Attribute actionInfo) : base(method, actionInfo)
        {
        }

        public string Description { get { return Information; } }

        public static List<SpecTestMethod> FromAssembly(Assembly assembly)
        {
            List<SpecTestMethod> tests = new List<SpecTestMethod>();
            tests.AddRange(FromAssembly<SpecTestMethod>(assembly, typeof(SpecTestAttribute)));
            tests.Sort((l, r) => l.Information.CompareTo(r.Information));
            return tests;
        }

        public static List<SpecTestMethod> FromAssembly(Assembly assembly, string testGroup)
        {
            return FromAssembly(assembly).Where(stm => stm.Method.HasCustomAttributeOfType(out TestGroupAttribute testGroupAttribute)
                                                       && testGroupAttribute.Groups.Contains(testGroup)).ToList();
        }
    }
}
