using System.Reflection;

namespace Bam.Test.Integration
{
    public class IntegrationTestMethod : TestMethod
    {
        public IntegrationTestMethod() : base()
        {
        }

        public IntegrationTestMethod(MethodInfo method) : base(method)
        {
        }

        public IntegrationTestMethod(MethodInfo method, Attribute actionInfo) : base(method, actionInfo)
        {
        }

        public string Description { get { return Information; } }

        public static List<IntegrationTestMethod> FromAssembly(Assembly assembly)
        {
            List<IntegrationTestMethod> tests = new List<IntegrationTestMethod>();
            tests.AddRange(FromAssembly<IntegrationTestMethod>(assembly, typeof(IntegrationTestAttribute)));
            tests.Sort((l, r) => l.Information.CompareTo(r.Information));
            return tests;
        }
    }
}
