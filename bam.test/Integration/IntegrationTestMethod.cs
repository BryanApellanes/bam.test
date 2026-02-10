using System.Reflection;

namespace Bam.Test.Integration
{
    [Serializable]
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
            tests.AddRange(FromAssembly<IntegrationTestMethod>(assembly, typeof(IntegrationTest)));
            tests.Sort((l, r) => l.Information.CompareTo(r.Information));
            return tests;
        }

        public static List<IntegrationTestMethod> FromAssembly(Assembly assembly, string testGroup)
        {
            return FromAssembly(assembly).Where(itm =>
                itm.Method.GetCustomAttributes<TestGroupAttribute>()
                   .FirstOrDefault(attr => attr.Groups.Contains(testGroup)) != null)
                .ToList();
        }
    }
}
