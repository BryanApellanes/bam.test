using Bam.Shell;

namespace Bam.Test
{
    /// <summary>
    /// Marks a method as a test of the specified type(s), with options for synchronous execution.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestAttribute : MenuItemAttribute
    {
        /// <summary>
        /// Initializes a new instance with the specified test types.
        /// </summary>
        /// <param name="testType">The test types that this method belongs to.</param>
        public TestAttribute(params TestType[] testType)
        {
            this.TypeTypes = testType;
        }

        /// <summary>
        /// Gets the test types associated with this attribute.
        /// </summary>
        public TestType[] TypeTypes { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the test should be run synchronously.
        /// </summary>
        public bool RunSynchronously { get; set; } = false;

        /// <summary>
        /// Gets the object used to run tests synchronously.
        /// </summary>
        public static object Lock { get; } = new object();
    }
}
