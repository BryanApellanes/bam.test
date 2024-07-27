using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Bam.Test.Specification;
using Bam.Test.Unit;

namespace Bam.Test
{
    public class TestMethodDescriptor
    {
        public TestMethodDescriptor(string fullyQualifiedMethodName)
        {
            this.FullyQualifiedMethodName = fullyQualifiedMethodName;
        }
        protected void Parse()
        {
            string[] strings = this.FullyQualifiedMethodName.Split("::", StringSplitOptions.RemoveEmptyEntries);
            this.AssemblyPath = strings[0];
            string[] typeMethod = strings[1].Split(".");
            List<string> typeStrings = new List<string>();
            for (int i = 0; i < typeMethod.Length; i++)
            {
                if (i != typeMethod.Length - 1)
                {
                    typeStrings.Add(typeMethod[i]);
                }
                else
                {
                    this.MethodIdentifier = typeMethod[i];
                }
            }
            this.TypeIdentifier = string.Join(".", typeStrings.ToArray());
        }

        protected string FullyQualifiedMethodName { get; set; }
        public string AssemblyPath { get; set; }
        public string TypeIdentifier { get; set; }
        public string MethodIdentifier { get; set; }

        public MethodInfo ToMethodInfo()
        {
            Assembly assembly = Assembly.LoadFile(AssemblyPath);
            Type? type = assembly.GetTypes().FirstOrDefault(type => type.Name.Equals(TypeIdentifier));
            MethodInfo method = type?.GetMethod(MethodIdentifier);
            return method;
        }

        public SpecTestMethod ToSpecTestMethod()
        {
            return ToTestMethod<SpecTestMethod>();
        }

        public UnitTestMethod ToUnitTestMethod()
        {
            return ToTestMethod<UnitTestMethod>();
        }

        public T ToTestMethod<T>() where T : TestMethod, new()
        {
            return new T()
            {
                Method = ToMethodInfo(),
            };
        }
    }
}
