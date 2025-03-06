/*
	Copyright © Bryan Apellanes 2015  
*/

namespace Bam.Test
{
    /// <summary>
    /// Convenience entry point into a test that requires no setup through the SetupContext
    /// </summary>
    public static class When
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="testCaseDescription"></param>
        /// <param name="test"></param>
        /// <returns></returns>
        public static TestCase<T> A<T>(string testCaseDescription, Func<T, object> test) where T : new()
        {
            return A(testCaseDescription, new T(), test);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="testCaseDescription"></param>
        /// <param name="objectUnderTest"></param>
        /// <param name="test"></param>
        /// <returns></returns>
        public static TestCase<T> A<T>(string testCaseDescription, T objectUnderTest, Func<T, object> test)
        {
            TestCaseRegistry testCaseRegistry = new TestCaseRegistry();
            testCaseRegistry.Set(objectUnderTest);
            return new TestCase<T>(testCaseRegistry, TestCaseRegistry.GetActionDescription<T>(testCaseDescription), test);
        }

        /// <summary>
        /// Prepares the test Context with an empty SetupContext instantiating the 
        /// object under test to an instance of T using the default constructor of type T.
        /// </summary>
        /// <typeparam name="T">The type of the </typeparam>
        /// <param name="testCaseDescription"></param>
        /// <param name="test"></param>
        /// <returns></returns>
        public static TestCase<T> A<T>(string testCaseDescription, Action<T> test) where T : new()
        {
            return A(testCaseDescription, new T(), test);
        }
        /// <summary>
        /// Prepares the test case with an empty SetupContext instantiating the 
        /// object under test to an instance of T using the default constructor of type T.
        /// </summary>
        /// <typeparam name="T">The type of the </typeparam>
        /// <param name="testCaseDescription"></param>
        /// <param name="test"></param>
        /// <returns></returns>
        public static TestCase<T> A<T>(string testCaseDescription, Action<T, TestCaseRegistry> test) where T : new()
        {
            return A(testCaseDescription, new T(), test);
        }

        /// <summary>
        /// Prepares the test Context with an empty SetupContext setting the object under test
        /// to the specified objectUnderTest
        /// </summary>
        /// <typeparam name="T">The type of the object under test.</typeparam>
        /// <param name="testCaseDescription">A description of what the test action will do</param>
        /// <param name="objectUnderTest">The object instance being tested</param>
        /// <param name="test">the test delegate</param>
        /// <returns>Context</returns>
        public static TestCase<T> A<T>(string testCaseDescription, T objectUnderTest, Action<T> test)
        {
            TestCaseRegistry testCaseRegistry = new TestCaseRegistry();
            testCaseRegistry.Set(objectUnderTest);
            return new TestCase<T>(testCaseRegistry, TestCaseRegistry.GetActionDescription<T>(testCaseDescription), test);
        }
        /// <summary>
        /// Prepares the TestCase with an empty TestCaseRegistry setting the object under test
        /// to the specified objectUnderTest
        /// </summary>
        /// <typeparam name="T">The type of the object under test.</typeparam>
        /// <param name="testCaseDescription">A description of what the test action will do</param>
        /// <param name="objectUnderTest">The object instance being tested</param>
        /// <param name="test">the test delegate</param>
        /// <returns>Context</returns>
        public static TestCase<T> A<T>(string testCaseDescription, T objectUnderTest, Action<T, TestCaseRegistry> test)
        {
            TestCaseRegistry testCaseRegistry = new TestCaseRegistry();
            testCaseRegistry.Set(objectUnderTest);
            return new TestCase<T>(testCaseRegistry, TestCaseRegistry.GetActionDescription<T>(testCaseDescription), test);
        }
    }
}
