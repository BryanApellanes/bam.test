/*
	Copyright © Bryan Apellanes 2015  
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Runtime.CompilerServices;
using Bam;

namespace Bam.Testing
{
    public class AssertionProvider<T> : AssertionProvider
    {
        public static implicit operator T(AssertionProvider<T> provider)
        {
            return provider.Value;
        }

        public AssertionProvider(Because because, object wrapped, string name = null) : base(because, wrapped, name)
        {
            Value = (T)wrapped;
        }

        public AssertionProvider(Because because, T wrapped, string name = null) : base(because, wrapped, name)
        {
            Value = wrapped;
        }

        public new T Value { get; set; }
    }
}

namespace Bam.Testing
{
    /// <summary>
    /// Provides a means to make specific assertions about the specified wrapped object.
    /// </summary>
    public class AssertionProvider
    {
        public AssertionProvider(Because because, object wrapped, string name = null)
        {
            Value = wrapped;
            Because = because;
            Name = string.IsNullOrEmpty(name) ? "the value" : name;
        }

        public string Name { get; set; }
        public object Value { get; private set; }
        public Type TypeOfValue => Value.GetType();
        protected Because Because { get; set; }

        public void IsA<T>()
        {
            IsA(typeof(T));
        }

        public void IsA(Type type)
        {
            Because.ItsTrue($"the {Name} is a {type.Name}", Value.GetType() == type, "the object under test is NOT a {0}".Format(type.Name));
        }

        public void IsEqualTo(object obj)
        {
            Because.ItsTrue($"{Name} .Equals({obj})", Value.Equals(obj), "{0} did not .Equals({1})".Format(Name, obj));
        }

        public void HasProperty(string propertyName)
        {
            Because.ItsTrue($"{Name} has a property named {propertyName}", TypeOfValue.GetProperty(propertyName) != null, $"{Name} did not have a property named {propertyName}");
        }
    }
}
