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

        public static TestCaseRegistry Setup(Func<TestCaseRegistry, Task<TestCaseRegistry>> setup)
        {
            TestCaseRegistry testCaseRegistryInstance = new TestCaseRegistry();
            return setup(testCaseRegistryInstance).Result;
        }
    }
}
