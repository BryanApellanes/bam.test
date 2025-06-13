/*
	Copyright © Bryan Apellanes 2015  
*/

using Bam.DependencyInjection;
using Bam.Services;

namespace Bam.Test
{
    /// <summary>
    /// A dependency injection used for a test case. 
    /// </summary>
    public class TestCaseRegistry : ServiceRegistry
    {
        public TestCaseRegistry()
        {
            Set<IBecauseWriter>(new ConsoleBecauseWriter());
        }

        public string Description { get; set; }
        
        public TestCase<T> When<T>(string actionDescription, Action<T> test)
        {
            return new TestCase<T>(this, GetActionDescription<T>(actionDescription), test);
        }
        
        public TestCase<T> When<T>(string actionDescription, Action<T, TestCaseRegistry> test)
        {
            return new TestCase<T>(this, GetActionDescription<T>(actionDescription), test);
        }
        
        public TestCase<T> When<T>(string actionDescription, Func<T, TestCaseRegistry, object> test)
        {
            return new TestCase<T>(this, GetActionDescription<T>(actionDescription), test);
        }
        
        public TestCase<T> When<T>(string actionDescription, Func<T, Task> asyncTest)
        {
            return new AsyncTestCase<T>(this, GetActionDescription<T>(actionDescription), asyncTest);
        }
        
        public TestCase<T> When<T>(string actionDescription, Func<T, TestCaseRegistry, Task> asyncTest)
        {
            return new AsyncTestCase<T>(this, GetActionDescription<T>(actionDescription), asyncTest);
        }
        
        public TestCase<T> When<T>(string actionDescription, Func<T, Task<object>> asyncTest)
        {
            return new AsyncTestCase<T>(this, GetActionDescription<T>(actionDescription), asyncTest);
        }

        public TestCase<T> When<T>(string actionDescription, Func<T, TestCaseRegistry, Task<object>> asyncTest)
        {
            return new AsyncTestCase<T>(this, GetActionDescription<T>(actionDescription), asyncTest);
        }
        
        /// <summary>
        /// Prepares the test case with the object under test being of type T passed to the test function.
        /// </summary>
        /// <param name="testDescription"></param>
        /// <param name="test"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public TestCase<T> When<T>(string testDescription, Func<T, object> test)
        {
            return new TestCase<T>(this, testDescription, test);
        }
        
        /// <summary>
        /// Prepares the test case with the object under test being of type T, passed to the test action.
        /// </summary>
        /// <typeparam name="T">The type of the object under test</typeparam>
        /// <param name="actionDescription">A description of what action is being tested</param>
        /// <param name="test">The Action to run, the object under test will be provided as a 
        /// parameter so the developer can interact with it.</param>
        /// <returns></returns>
        [Obsolete("This method is obsolete and will be removed in a future version. Use When<T> instead.")]
        public TestCase<T> WhenA<T>(string actionDescription, Action<T> test)
        {
            return new TestCase<T>(this, GetActionDescription<T>(actionDescription), test);
        }

        [Obsolete("This method is obsolete and will be removed in a future version. Use When<T> instead.")]
        public TestCase<T> WhenA<T>(string actionDescription, Action<T, TestCaseRegistry> test)
        {
            return new TestCase<T>(this, GetActionDescription<T>(actionDescription), test);
        }

        /// <summary>
        /// Prepares the test case for object under test of type T.
        /// </summary>
        /// <typeparam name="T">The type of the object under test</typeparam>
        /// <param name="actionDescription">A description of what action is being tested</param>
        /// <param name="test">The Func to run, the object under test will be provided as a 
        /// parameter so the developer can interact with it. Must return a value.</param>
        /// <returns></returns>
        [Obsolete("This method is obsolete and will be removed in a future version. Use When<T> instead.")]
        public TestCase<T> WhenA<T>(string actionDescription, Func<T, object> test)
        {
            return new TestCase<T>(this, GetActionDescription<T>(actionDescription), test);
        }

        internal object ObjectUnderTest
        {
            get;
            set;
        }

        internal static string GetActionDescription<T>(string actionDescription)
        {
            return $"Testing when a {typeof(T).Name} {actionDescription}";
        }
    }
}
