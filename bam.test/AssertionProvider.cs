/*
	Copyright © Bryan Apellanes 2015  
*/

namespace Bam.Test
{
    /// <summary>
    /// Provides a strongly-typed means to make assertions about a wrapped object of type T.
    /// Supports implicit conversion to T.
    /// </summary>
    /// <typeparam name="T">The type of the wrapped value.</typeparam>
    public class AssertionProvider<T> : AssertionProvider
    {
        /// <summary>
        /// Implicitly converts an <see cref="AssertionProvider{T}"/> to its wrapped value of type T.
        /// </summary>
        /// <param name="provider">The assertion provider to convert.</param>
        public static implicit operator T(AssertionProvider<T> provider)
        {
            return provider.Value;
        }

        /// <summary>
        /// Initializes a new instance by casting the specified object to type T.
        /// </summary>
        /// <param name="because">The Because object used for tracking assertions.</param>
        /// <param name="wrapped">The object to wrap, cast to T.</param>
        /// <param name="name">An optional display name for the wrapped value.</param>
        public AssertionProvider(Because because, object wrapped, string name = null!) : base(because, wrapped, name)
        {
            Value = (T)wrapped;
        }

        /// <summary>
        /// Initializes a new instance wrapping the specified strongly-typed value.
        /// </summary>
        /// <param name="because">The Because object used for tracking assertions.</param>
        /// <param name="wrapped">The value to wrap.</param>
        /// <param name="name">An optional display name for the wrapped value.</param>
        public AssertionProvider(Because because, T wrapped, string name = null!) : base(because, wrapped!, name)
        {
            Value = wrapped;
        }

        /// <summary>
        /// Gets or sets the strongly-typed wrapped value.
        /// </summary>
        public new T Value { get; set; }
    }
}

namespace Bam.Test
{
    /// <summary>
    /// Provides a means to make specific assertions about the specified wrapped object.
    /// </summary>
    public class AssertionProvider
    {
        /// <summary>
        /// Initializes a new instance wrapping the specified object.
        /// </summary>
        /// <param name="because">The Because object used for tracking assertions.</param>
        /// <param name="wrapped">The object to wrap.</param>
        /// <param name="name">An optional display name for the wrapped value; defaults to "the value".</param>
        public AssertionProvider(Because because, object wrapped, string name = null!)
        {
            Value = wrapped;
            Because = because;
            Name = string.IsNullOrEmpty(name) ? "the value" : name;
        }

        /// <summary>
        /// Gets or sets the display name for this assertion target.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the wrapped value.
        /// </summary>
        public object Value { get; private set; }

        /// <summary>
        /// Gets the runtime type of the wrapped value.
        /// </summary>
        public Type TypeOfValue => Value.GetType();

        /// <summary>
        /// Gets or sets the Because object used for tracking assertions.
        /// </summary>
        protected Because Because { get; set; }

        /// <summary>
        /// Asserts that the wrapped value is of the specified type T.
        /// </summary>
        /// <typeparam name="T">The expected type.</typeparam>
        public void IsA<T>()
        {
            IsA(typeof(T));
        }

        /// <summary>
        /// Asserts that the wrapped value is of the specified type.
        /// </summary>
        /// <param name="type">The expected type.</param>
        public void IsA(Type type)
        {
            Because.ItsTrue($"the {Name} is a {type.Name}", Value.GetType() == type, "the object under test is NOT a {0}".Format(type.Name));
        }

        /// <summary>
        /// Asserts that the wrapped value is equal to the specified object using .Equals().
        /// </summary>
        /// <param name="obj">The object to compare against.</param>
        public void IsEqualTo(object obj)
        {
            Because.ItsTrue($"{Name} .Equals({obj})", Value.Equals(obj), "{0} did not .Equals({1})".Format(Name, obj));
        }

        /// <summary>
        /// Asserts that the wrapped value's type has a property with the specified name.
        /// </summary>
        /// <param name="propertyName">The name of the property to check for.</param>
        public void HasProperty(string propertyName)
        {
            Because.ItsTrue($"{Name} has a property named {propertyName}", TypeOfValue.GetProperty(propertyName) != null, $"{Name} did not have a property named {propertyName}");
        }
    }
}
