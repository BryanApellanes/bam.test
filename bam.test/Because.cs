/*
	Copyright © Bryan Apellanes 2015  
*/

namespace Bam.Test
{
    public class Because<T> : Because
    {
        internal Because(string testDescription, TestCaseRegistry testCaseRegistry, TestCase<T> testCase) : base(testDescription, testCaseRegistry)
        {
            TestCase = testCase;
        }

        public Because(Because because, TestCase<T> testCase) : base(because.TestDescription, because.TestCaseRegistry)
        {
            TestCase = testCase;
            _assertions = because.Assertions;
            Result = because.Result;
        }
        
        public TestCase<T> TestCase { get; set; }
        public void TheTestCase(string descriptionOfTestCaseAssertion, Func<TestCase<T>, bool> testCaseAssertion, string? failureMessage = null)
        {
            bool? assertionResult = false;
            try
            {
                assertionResult = testCaseAssertion(TestCase);
            }
            catch (Exception ex)
            {
                failureMessage = $"The test case assertion threw an exception: {ex}";
            }
            ItsTrue($"the test case {descriptionOfTestCaseAssertion}", assertionResult.Value, failureMessage);
        }
    }
    /// <summary>
    /// Provides a mechanism by which assertions are tracked for a test.
    /// </summary>
    public class Because
    {
        readonly TestCaseRegistry _testCaseRegistry;
        protected List<Assertion> _assertions;
        public Because(string testDescription, TestCaseRegistry testCaseRegistry)
        {
            TestDescription = testDescription;
            _assertions = new List<Assertion>();
            _testCaseRegistry = testCaseRegistry;
        }
        
        /// <summary>
        /// Gets the description of the current test being run
        /// </summary>
        public string TestDescription
        {
            get;
            private set;
        }
        
        /// <summary>
        /// Gets the SetupContext instance for the current test.
        /// </summary>
        public TestCaseRegistry TestCaseRegistry => _testCaseRegistry;

        /// <summary>
        /// Gets the object under test from the underlying TestCaseRegistry.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T ObjectUnderTest<T>()
        {
            return (T)_testCaseRegistry.ObjectUnderTest;
        }

        
        /// <summary>
        /// Gets a value indicating whether the current test has passed.
        /// </summary>
        public bool Passed =>
            (from item in _assertions
             where item.Passed == false
             select item).FirstOrDefault() == null;

        public void ItsTrue(string descriptionOfTrueAssertion, Func<bool> assertion, string? failureMessage = null)
        {
            ItsTrue(descriptionOfTrueAssertion, assertion(), failureMessage);
        }
        
        /// <summary>
        /// Asserts that the specified value is true
        /// </summary>
        /// <param name="descriptionOfTrueAssertion">A description of the true value.  Read as:  ItsTrue "Michael Jordan is the best of all time"</param>
        /// <param name="shouldBeTrue"></param>
        /// <param name="failureMessage"></param>
        public void ItsTrue(string descriptionOfTrueAssertion, bool shouldBeTrue, string? failureMessage = null)
        {
            _assertions.Add(
                new Assertion
                {
                    Passed = shouldBeTrue == true,
                    SuccessMessage = descriptionOfTrueAssertion,
                    FailureMessage = failureMessage
                });
        }

        public void ItsTrue(string descriptionOfTrueAssertion, Action doesntThrow, string? failureMessage = null)
        {
            _assertions.Add(
                new Assertion
                {
                    Passed = doesntThrow.Try(out Exception? ex),
                    SuccessMessage = descriptionOfTrueAssertion,
                    FailureMessage = failureMessage
                });
            if (ex != null)
            {
                ExceptionWasThrown(ex);
            }
        }

        /// <summary>
        /// Asserts that the specified value is false
        /// </summary>
        /// <param name="descriptionOfFalseAssertion">A description of the false value.  Read as: ItsFalse "John Stockton was the number one point guard of all time (Magic Johnson was, John Stockton was second)" </param>
        /// <param name="shouldBeFalse">A value that should evaluate to false.</param>
        /// <param name="failureMessage">The message to display if the `shouldBeFalse` value is actually `true`.</param>
        public void ItsFalse(string descriptionOfFalseAssertion, bool shouldBeFalse, string? failureMessage = null)
        {
            _assertions.Add(
                new Assertion
                {
                    Passed = shouldBeFalse == false,
                    SuccessMessage = descriptionOfFalseAssertion,
                    FailureMessage = failureMessage
                });
        }

        private ResultContext? _theResult;
        public virtual ResultContext TheResult
        {
            get
            {
                if (_theResult == null)
                {
                    _theResult = new ResultContext(this, Result);
                }
                return _theResult;
            }
        }

        private ObjectUnderTestContext? _objectUnderTest;
        public ObjectUnderTestContext TheObjectUnderTest
        {
            get
            {
                if (_objectUnderTest == null)
                {
                    _objectUnderTest = new ObjectUnderTestContext(this, _testCaseRegistry.ObjectUnderTest);
                }
                return _objectUnderTest;
            }
        }
        
        public void TheObjectUnderTestAs<T>(string truthStatementAboutTheObjectUnderTest, Func<T?, bool> assertAction, string? failureMessage = null)
        {
            _assertions.Add(new Assertion
            {
                Passed = assertAction(ObjectUnderTest<T>()),
                SuccessMessage = $"the ObjectUnderTest {truthStatementAboutTheObjectUnderTest}",
                FailureMessage = $"the ObjectUnderTest {failureMessage}"
            });
        }

        public void TheObjectUnderTestIsNotNull()
        {
            _assertions.Add(new Assertion
            {
                Passed = _testCaseRegistry?.ObjectUnderTest != null,
                SuccessMessage = "the ObjectUnderTest was not null",
                FailureMessage = "the ObjectUnderTest was null"
            });
        }
        
        /// <summary>
        /// Provides the result to an assertion function and records the resulting assertion.
        /// </summary>
        /// <param name="truthStatementAboutTheResult"></param>
        /// <param name="assertAction"></param>
        /// <param name="failureMessage"></param>
        /// <typeparam name="T"></typeparam>
        /// <remarks>
        /// Usage: because.TheResultAs&lt;int&gt;("is greater than zero", (result) => result > 0);
        /// </remarks>
        public void TheResultAs<T>(string truthStatementAboutTheResult, Func<T?, bool?> assertAction, string? failureMessage = null) where T: class
        {
            _assertions.Add(new Assertion
            {
                Passed = assertAction(TheResult.As<T>()) == true,
                SuccessMessage = $"the result {truthStatementAboutTheResult}",
                FailureMessage = $"the result {failureMessage}"
            });
        }
        
        /// <summary>
        /// Asserts that the type of the result of the test Function 
        /// is the same as the type specified by generic type T.  Only valid
        /// if the test method returned a value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void ResultIsOfType<T>()
        {
            Type type = typeof(T);
            _assertions.Add(
                new Assertion
                {
                    Passed = Result != null && Result.GetType() == typeof(T),
                    SuccessMessage = $"result is of type {type.Name}",
                    FailureMessage = $"result is NOT of type {type.Name}, but was of type {Result?.GetType().Name}"
                });
        }

        public void ResultIsOfType(Type type)
        {
            _assertions.Add(
                new Assertion
                {
                    Passed = Result != null && Result.GetType() == type,
                    SuccessMessage = $"result is of type {type.Name}",
                    FailureMessage = $"result is NOT of type {type.Name}, but was of type {Result?.GetType().Name}"
                });
        }
        
        public void ResultIs<T>()
        {
            _assertions.Add(
                new Assertion
                {
                    Passed = Result != null && Result is T,
                    SuccessMessage = $"the Result is {typeof(T).Name}",
                    FailureMessage = $"the Result is NOT {typeof(T).Name}"
                });
        }
        
        /// <summary>
        /// Asserts that the result of the test function
        /// is equal to the specified object using the .Equals 
        /// method.
        /// </summary>
        /// <param name="obj"></param>
        public void ResultEquals(object obj)
        {
            _assertions.Add(
                new Assertion
                {
                    Passed = Result.Equals(obj),
                    SuccessMessage = $"the Result equals the specified value ({obj.ToString()})",
                    FailureMessage = $"the Result does NOT equal the specified value ({obj.ToString()})"
                });
        }

        /// <summary>
        /// Asserts that the result of the test function
        /// is the same as the specified object using
        /// the equality comparison operator ==
        /// </summary>
        /// <param name="obj"></param>
        public void ResultEqualsEquals(object obj)
        {
            _assertions.Add(
                new Assertion
                {
                    Passed = Result == obj,
                    SuccessMessage = $"the Result is same as the specified value ({obj.ToString()})",
                    FailureMessage = $"the Result is NOT same as the specified value ({obj.ToString()})"
                });
        }

        /// <summary>
        /// Add a passed assertion with the specified message.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public void AdditionalInformation(string message)
        {
            _assertions.Add(
                new Assertion
                {
                    Passed = true,
                    SuccessMessage = message,
                });
        }
        
        /// <summary>
        /// Does not perform an assertion, rather outputs the string representation of the specified obj
        /// using ToString().
        /// </summary>
        /// <param name="obj"></param>
        public void IllLookAtIt(object obj)
        {
            _assertions.Add(
                new Assertion
                {
                    Passed = true,
                    SuccessMessage = $"I'll inspect the value ({obj.ToString()})"
                });
        }

        /// <summary>
        /// Does not perform an assertion, rather outputs the properties of the
        /// result of the test function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void IllLookAtTheResultsProperties<T>()
        {
            IllLookAtItsProperties((T)Result);
        }

        /// <summary>
        /// Does not perform an assertion, rather outputs the properties of the specified obj.
        /// </summary>
        /// <param name="obj"></param>
        public void IllLookAtItsProperties<T>(T obj)
        {
            _assertions.Add(
                new Assertion
                {
                    Passed = true,
                    SuccessMessage = $"I'll inspect the properties:\r\n{obj.PropertiesToString()}"
                });
        }

        /// <summary>
        /// Asserts that the result inherits from (derives from/is a subclass of) the specified
        /// generic type T.  Only valid if the test method returned a value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public void ResultDerivesFrom<T>()
        {
            Type type = typeof(T);
            _assertions.Add(
                new Assertion
                {
                    Passed = Result.GetType().IsSubclassOf(type),
                    SuccessMessage = $"the Result is a subclass of type {type.Name}",
                    FailureMessage = $"the Result is NOT of type {type.Name}"
                });
        }

        internal void ExceptionWasThrown(Exception ex)
        {
            _assertions.Add(new Assertion
            {
                Passed = false,
                FailureMessage =
                    $"an exception was thrown ({ex.Message}):\r\n{ex.StackTrace}"
            });
        }

        public List<Assertion> Assertions => _assertions;

        public T ResultAs<T>()
        {
            return (T)Result;
        }

        /// <summary>
        /// The return value of the test method execution
        /// </summary>        
        public object Result { get; set; }

        bool _testIsDone;
        internal Because TestIsDone
        {
            get
            {
                if (!_testIsDone)
                {
                    _testIsDone = true;
                    _testCaseRegistry.Get<IBecauseWriter>().Write(this);
                }
                return this;
            }
        }
        
        internal Because CleanUp(Action<TestCaseRegistry> cleanup)
        {
            cleanup(_testCaseRegistry);
            return this;
        }

        /// <summary>
        /// Throws an exception if the test failed.  Same as ThrowExceptionIfTheTestFailed. 
        /// </summary>
        /// <param name="message"></param>
        public void OrNot(string message = "The test failed, please see test output for more information.")
        {
            ThrowExceptionIfTheTestFailed(message);
        }

        /// <summary>
        /// Throws an exception if the test failed.  Same as OrNot.
        /// </summary>
        /// <param name="message"></param>
        public void ThrowExceptionIfTheTestFailed(string message = "The test failed, please see test output for more information.")
        {
            if (!Passed)
            {
                throw new Exception(message);
            }
        }

        /// <summary>
        /// Throws an exception if assertions failed.  Same as ThrowExceptionIfTheTestFailed.
        /// </summary>
        public void UnlessItFailed(string message = "The test failed, please see test output for more information.")
        {
            ThrowExceptionIfTheTestFailed(message);
        }
    }
}
