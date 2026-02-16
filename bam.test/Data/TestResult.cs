/*
	Copyright © Bryan Apellanes 2015  
*/

using System.Reflection;
using Bam.Data.Repositories;
using Bam.Console;

namespace Bam.Test.Data
{
    /// <summary>
    /// Represents a persisted test result record, storing test method details, pass/fail status,
    /// and any exception information.
    /// </summary>
    public class TestResult : AuditRepoData
    {
        /// <summary>
        /// Initializes a new instance with default values and TestType set to Unit.
        /// </summary>
        public TestResult() : base()
        {
            TestType = Test.TestType.Unit.ToString();
        }

        /// <summary>
        /// Initializes a new instance with the specified description and pass status.
        /// </summary>
        /// <param name="description">The description of the test.</param>
        /// <param name="passed">Whether the test passed.</param>
        public TestResult(string description, bool passed) : this()
        {
            Description = description;
            Passed = passed;
        }

        /// <summary>
        /// Initializes a new instance from a console method, marking it as passed.
        /// </summary>
        /// <param name="cim">The console method representing the test.</param>
        /// <param name="testType">The type of test (defaults to Unit).</param>
        public TestResult(ConsoleMethod cim, TestType testType = Test.TestType.Unit) : this()
        {
            MethodInfo method = cim.Method;
            MethodName = method.Name;
            Description = cim.Information;
            AssemblyFullName = method.DeclaringType.Assembly.FullName;
            Passed = true;
            TestType = testType.ToString();
        }

        /// <summary>
        /// Initializes a new instance from a test exception event, marking it as failed
        /// and recording the exception details.
        /// </summary>
        /// <param name="args">The test exception event args containing the test method and exception.</param>
        public TestResult(TestExceptionEventArgs args)
            : this(args.TestMethod)
        {
            Passed = false;
            Exception = args.Exception.Message;
            StackTrace = args.Exception.StackTrace;
        }

        /// <summary>
        /// Gets or sets the type of test (e.g., "Unit", "Integration", "Specification").
        /// </summary>
        public string TestType { get; set; }
        /// <summary>
        /// Boolean indicating whether the test passed
        /// </summary>
        public bool Passed { get; set; }
        /// <summary>
        /// The name of the test method 
        /// </summary>
		public string MethodName { get; set; }
        /// <summary>
        /// The information value of the test method if any
        /// </summary>
		public string Description { get; set; }
        /// <summary>
        /// The full name of the assembly the test was in
        /// </summary>
		public string AssemblyFullName { get; set; }
        /// <summary>
        /// The exception message if any
        /// </summary>
		public string Exception { get; set; }
        /// <summary>
        /// The stack trace if any
        /// </summary>
		public string StackTrace { get; set; }
    }
}
