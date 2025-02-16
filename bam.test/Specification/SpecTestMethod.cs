using System.Reflection;

namespace Bam.Test.Specification
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
            tests.Sort((l, r) => String.Compare(l.Information, r.Information, StringComparison.Ordinal));
            return tests;
        }

        public static List<SpecTestMethod> FromAssembly(Assembly assembly, string testGroup)
        {
            return FromAssembly(assembly).Where(stm => stm.Method.HasCustomAttributeOfType(out TestGroupAttribute testGroupAttribute)
                                                       && testGroupAttribute.Groups.Contains(testGroup)).ToList();
        }
    }
}
