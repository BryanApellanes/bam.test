/*
	Copyright © Bryan Apellanes 2015  
*/

using Bam.Console;

namespace Bam.Test.Unit
{
    /// <summary>
    /// Attribute used to mark a method as a Unit Test
    /// </summary>
    [Serializable]
    [Obsolete("Use UnitTest instead")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class UnitTestAttribute : ConsoleCommandAttribute
    {
        public UnitTestAttribute()
            : base()
        {
        }

        public UnitTestAttribute(string description)
            : base(description)
        {
        }

        public bool Ignore => !string.IsNullOrEmpty(IgnoreBecause);

        public string IgnoreBecause { get; set; } = null!;
    }
}
