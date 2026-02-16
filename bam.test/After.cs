/*
	Copyright © Bryan Apellanes 2015  
*/

namespace Bam.Test
{
    /// <summary>
    /// Convenience entry point into creating and initializing the 
    /// TestCaseSetup for a test.
    /// </summary>
    public static class After
    {
        /// <summary>
        /// Instantiates the SetupContext for a test and passes that
        /// instance to the specified setup Action delegate.
        /// </summary>
        /// <param name="setup"></param>
        /// <returns></returns>
        public static TestCaseRegistry Setup(Action<TestCaseRegistry> setup)
        {
            TestCaseRegistry testCaseRegistryInstance = new TestCaseRegistry();
            setup(testCaseRegistryInstance);
            return testCaseRegistryInstance;
        }

        /// <summary>
        /// Instantiates the TestCaseRegistry for a test and passes it to the specified async setup function,
        /// blocking until the setup completes.
        /// </summary>
        /// <param name="setup">An async function that configures the TestCaseRegistry and returns it.</param>
        /// <returns>The configured TestCaseRegistry instance.</returns>
        public static TestCaseRegistry Setup(Func<TestCaseRegistry, Task<TestCaseRegistry>> setup)
        {
            TestCaseRegistry testCaseRegistryInstance = new TestCaseRegistry();
            return setup(testCaseRegistryInstance).Result;
        }
    }
}
