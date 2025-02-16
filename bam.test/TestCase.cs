/*
	Copyright © Bryan Apellanes 2015  
*/

namespace Bam.Test
{
    /// <summary>
    /// The context specific to a single test.  Tracks
    /// the SetupContext, the test delegate and the assertions
    /// made during the verification phase of the test.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class TestCase<T>
    {
        readonly TestCaseRegistry _testCaseRegistry;
        readonly Because _because;
        readonly Action<T> _testMethod;
        readonly Action<T, TestCaseRegistry> _altTestMethod;
        readonly Func<T, object> _outputAction;

        internal TestCase(TestCaseRegistry testCaseRegistry, string testDescription)
        {
            Description = testDescription;
            _testCaseRegistry = testCaseRegistry;
            _because = new Because(testDescription, testCaseRegistry);
            _testMethod = (o) => { };
            _altTestMethod = (o, c) => { };
            _outputAction = (o) => o;
        }

        /// <summary>
        /// Creates a new test Context instance using the specified setupContext and 
        /// test description.
        /// </summary>
        /// <param name="testCaseRegistry">The setup or initialization context used for this
        /// test.</param>
        /// <param name="testDescription">The description of the current test.</param>
        /// <param name="testMethod">The delegate containing the test actions</param>
        public TestCase(TestCaseRegistry testCaseRegistry, string testDescription, Action<T> testMethod)
            : this(testCaseRegistry, testDescription)
        {
            _testMethod = testMethod;
        }

        public TestCase(TestCaseRegistry testCaseRegistry, string testDescription, Action<T, TestCaseRegistry> altTestMethod)
            : this(testCaseRegistry, testDescription)
        {
            _altTestMethod = altTestMethod;
        }

        /// <summary>
        /// Creates a new test case instance using the specified setupContext and 
        /// test description.
        /// </summary>
        /// <param name="testCaseRegistry">The setup or initialization context used for this
        /// test.</param>
        /// <param name="testDescription">The description of the current test.</param>
        /// <param name="outputAction">The delegate containing the test which returns a value
        /// that can be validated during the verification phase of the test.</param>
        public TestCase(TestCaseRegistry testCaseRegistry, string testDescription, Func<T, object> outputAction)
            : this(testCaseRegistry, testDescription)
        {
            _outputAction = outputAction;
        }

        public string Description { get; init; }
        /// <summary>
        /// Causes the test case to run, same as It.
        /// </summary>
        public TestCase<T> TheTest => It;

        bool run;
        /// <summary>
        /// Causes the test case to run, same as TheTest.
        /// </summary>
        public TestCase<T> It
        {
            get
            {
                if (!run)
                {
                    run = true;
                    try
                    {
                        T objectUnderTest = _testCaseRegistry.Get<T>();
                        _testMethod(objectUnderTest);
                        _altTestMethod(objectUnderTest, _testCaseRegistry);
                        _because.Result = _outputAction(objectUnderTest);
                        _testCaseRegistry.ObjectUnderTest = objectUnderTest;
                    }
                    catch (Exception ex)
                    {
                        _because.ExceptionWasThrown(ex);
                    }
                }
                return this;
            }
        }

        /// <summary>
        /// The entry point into test validation.  Calls the specified
        /// actionToAssertResults passing it the Because object of the 
        /// current test case.
        /// </summary>
        /// <param name="actionToAssertResults"></param>
        /// <returns></returns>
        public TestCase<T> ShouldPass(Action<Because> actionToAssertResults)
        {
            actionToAssertResults(_because);
            return this;
        }

        /// <summary>
        /// The entry point into test validation.  Calls the specified 
        /// actionToAssertResults passing it the Because object of the
        /// current test context and the object under test.
        /// </summary>
        /// <param name="actionToAssertResults"></param>
        /// <returns></returns>
        public TestCase<T> ShouldPass(Action<Because, AssertionProvider<T>> actionToAssertResults)
        {
            try
            {
                actionToAssertResults(_because, new AssertionProvider<T>(_because, (T)_testCaseRegistry.ObjectUnderTest, "Object Under Test"));
            }
            catch (Exception ex)
            {
                _because.ExceptionWasThrown(ex);
            }
            return this;
        }

        public TestCase<T> ShouldPass<TResult>(Action<Because, AssertionProvider<T>, TResult> actionToAssertResults)
        {
            try
            {
                actionToAssertResults(_because, new AssertionProvider<T>(_because, (T)_testCaseRegistry.ObjectUnderTest, "Object Under Test"), _because.ResultAs<TResult>());
            }
            catch (Exception ex)
            {
                _because.ExceptionWasThrown(ex);
            }
            return this;
        }

        /// <summary>
        /// Calls Write() on the IBecauseWriter for the current test and
        /// marks the test complete.
        /// </summary>
        /// <returns></returns>
        public Because SoBeHappy()
        {
            return _because.TestIsDone;
        }

        /// <summary>
        /// Calls Write() on the IBecauseWriter for the current test and
        /// marks the test complete then runs the specified cleanup action.  
        /// Same as Cleanup.
        /// </summary>
        /// <param name="cleanup"></param>
        /// <returns></returns>
        public Because SoBeHappy(Action<TestCaseRegistry> cleanup)
        {
            return Cleanup(cleanup);
        }

        /// <summary>
        /// Calls Write() on the IBecauseWriter for the current test and
        /// marks the test complete then runs the specified cleanup action.  
        /// Same as SoBeHappy.
        /// </summary>
        /// <param name="cleanup"></param>
        /// <returns></returns>
        public Because Cleanup(Action<TestCaseRegistry> cleanup)
        {
            return _because.TestIsDone.CleanUp(cleanup);
        }
    }
}
