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

        /// <summary>
        /// Gets or sets a description for this test case registry.
        /// </summary>
        public string Description { get; set; } = null!;

        /// <summary>
        /// Creates a test case that executes the specified action on the object under test of type T.
        /// </summary>
        /// <typeparam name="T">The type of the object under test.</typeparam>
        /// <param name="actionDescription">A description of the action being tested.</param>
        /// <param name="test">The action to execute on the object under test.</param>
        /// <returns>A new <see cref="TestCase{T}"/>.</returns>
        public TestCase<T> When<T>(string actionDescription, Action<T> test)
        {
            return new TestCase<T>(this, GetActionDescription<T>(actionDescription), test);
        }
        
        /// <summary>
        /// Creates a test case that executes the specified action on the object under test and the registry.
        /// </summary>
        /// <typeparam name="T">The type of the object under test.</typeparam>
        /// <param name="actionDescription">A description of the action being tested.</param>
        /// <param name="test">The action to execute, receiving both the object under test and this registry.</param>
        /// <returns>A new <see cref="TestCase{T}"/>.</returns>
        public TestCase<T> When<T>(string actionDescription, Action<T, TestCaseRegistry> test)
        {
            return new TestCase<T>(this, GetActionDescription<T>(actionDescription), test);
        }

        /// <summary>
        /// Creates a test case that executes the specified function on the object under test and the registry, returning a result.
        /// </summary>
        /// <typeparam name="T">The type of the object under test.</typeparam>
        /// <param name="actionDescription">A description of the action being tested.</param>
        /// <param name="test">The function to execute, returning a result value.</param>
        /// <returns>A new <see cref="TestCase{T}"/>.</returns>
        public TestCase<T> When<T>(string actionDescription, Func<T, TestCaseRegistry, object> test)
        {
            return new TestCase<T>(this, GetActionDescription<T>(actionDescription), test);
        }

        /// <summary>
        /// Creates an async test case that executes the specified async function on the object under test.
        /// </summary>
        /// <typeparam name="T">The type of the object under test.</typeparam>
        /// <param name="actionDescription">A description of the action being tested.</param>
        /// <param name="asyncTest">The async function to execute.</param>
        /// <returns>A new <see cref="AsyncTestCase{T}"/>.</returns>
        public TestCase<T> When<T>(string actionDescription, Func<T, Task> asyncTest)
        {
            return new AsyncTestCase<T>(this, GetActionDescription<T>(actionDescription), asyncTest);
        }

        /// <summary>
        /// Creates an async test case that executes the specified async function on the object under test and the registry.
        /// </summary>
        /// <typeparam name="T">The type of the object under test.</typeparam>
        /// <param name="actionDescription">A description of the action being tested.</param>
        /// <param name="asyncTest">The async function to execute.</param>
        /// <returns>A new <see cref="AsyncTestCase{T}"/>.</returns>
        public TestCase<T> When<T>(string actionDescription, Func<T, TestCaseRegistry, Task> asyncTest)
        {
            return new AsyncTestCase<T>(this, GetActionDescription<T>(actionDescription), asyncTest);
        }

        /// <summary>
        /// Creates an async test case that executes the specified async function returning a result.
        /// </summary>
        /// <typeparam name="T">The type of the object under test.</typeparam>
        /// <param name="actionDescription">A description of the action being tested.</param>
        /// <param name="asyncTest">The async function to execute, returning a result value.</param>
        /// <returns>A new <see cref="AsyncTestCase{T}"/>.</returns>
        public TestCase<T> When<T>(string actionDescription, Func<T, Task<object>> asyncTest)
        {
            return new AsyncTestCase<T>(this, GetActionDescription<T>(actionDescription), asyncTest);
        }

        /// <summary>
        /// Creates an async test case that executes the specified async function with registry access, returning a result.
        /// </summary>
        /// <typeparam name="T">The type of the object under test.</typeparam>
        /// <param name="actionDescription">A description of the action being tested.</param>
        /// <param name="asyncTest">The async function to execute, returning a result value.</param>
        /// <returns>A new <see cref="AsyncTestCase{T}"/>.</returns>
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
        } = null!;

        internal static string GetActionDescription<T>(string actionDescription)
        {
            return $"Testing when a {typeof(T).Name} {actionDescription}";
        }
    }
}
