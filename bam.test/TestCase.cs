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
    /// <typeparam name="T">The type of the object under test.</typeparam>
    public class TestCase<T>
    {
        protected readonly Because _because;
        protected Because<T> _testCaseBecause;
        protected readonly TestCaseRegistry _testCaseRegistry;
        readonly Action<T> _testMethod;
        readonly Action<T, TestCaseRegistry> _altTestMethod;
        
        readonly Func<T, object> _outputAction;
        readonly Func<T, TestCaseRegistry, object> _altOutputAction;

        internal TestCase(TestCaseRegistry testCaseRegistry, string testDescription)
        {
            Description = testDescription;
            _testCaseRegistry = testCaseRegistry;
            _because = new Because(testDescription, testCaseRegistry);
            _testCaseBecause = new Because<T>(_because, this);
            _testMethod = null;//(o) => { };
            _altTestMethod = null;//(o, c) => { };
            _outputAction = null;// (o) => o;
            _altOutputAction = null;//(o, c) => null;
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
        
        public TestCase(TestCaseRegistry testCaseRegistry, string testDescription, Func<T, TestCaseRegistry, object> altOutputAction)
            : this(testCaseRegistry, testDescription)
        {
            _altOutputAction = altOutputAction;
        }

        public string Summary { get; set; }
        public string Description { get; init; }
        /// <summary>
        /// Causes the test case to run, same as It.
        /// </summary>
        public TestCase<T> TheTest => It;

        public Exception? Exception { get; protected set; }

        protected bool _shouldThrow;
        
        /// <summary>
        /// Causes the test case not to automatically fail if an exception is thrown so assertions can be made about an expected exception.
        /// </summary>
        /// <param name="shouldThrow"></param>
        /// <returns></returns>
        public TestCase<T> ExpectException(bool shouldThrow)
        {
            _shouldThrow = shouldThrow;
            return this;
        }
        
        bool? _run;
        readonly object _runLock = new object();
        /// <summary>
        /// Causes the test case to run, same as TheTest.
        /// </summary>
        public virtual TestCase<T> It
        {
            get
            {
                lock (_runLock)
                {
                    if (_run == null || _run == false)
                    {
                        _run = true;
                        try
                        {
                            T objectUnderTest = _testCaseRegistry.Get<T>();
                            if (objectUnderTest == null)
                            {
                                throw new InvalidOperationException($"Failed to instantiate ObjectUnderTest of type {typeof(T).Name}");
                            }

                            if (_testMethod != null)
                            {
                                _testMethod(objectUnderTest);
                            }

                            if (_altTestMethod != null)
                            {
                                _altTestMethod(objectUnderTest, _testCaseRegistry);
                            }

                            if (_outputAction != null)
                            {
                                _because.Result = _outputAction(objectUnderTest);
                            }

                            if (_altOutputAction != null)
                            {
                                _because.Result = _altOutputAction(objectUnderTest, _testCaseRegistry);
                            }
                            _testCaseBecause = new Because<T>(_because, this);
                            _testCaseRegistry.ObjectUnderTest = objectUnderTest;
                        }
                        catch (Exception ex)
                        {
                            Exception = ex;
                            if (!_shouldThrow)
                            {
                                _because.ExceptionWasThrown(ex);
                            }
                            _testCaseBecause = new Because<T>(_because, this);
                        }
                    }    
                }
                
                return this;
            }
        }

        public TestCase<T> ShouldPass(Action<Because<T>> actionToAssertResults)
        {
            actionToAssertResults(_testCaseBecause);
            return this;
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

        public TestCaseResult<T> GetResult()
        {
            return new TestCaseResult<T>(this, _testCaseBecause);
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
