/*
	Copyright © Bryan Apellanes 2015  
*/

namespace Bam.Test
{
    /// <summary>
    /// Defines methods used to write passed and failed assertions.
    /// </summary>
    public interface IAssertionWriter
    {
        /// <summary>
        /// When implemented in a derived class writes  
        /// the specified assertions in the "passed" format.
        /// Implementers "should" validate that the assertions
        /// actually passed.
        /// </summary>
        /// <param name="assertions"></param>
        void WritePassedAssertions(Assertion[] assertions);

        /// <summary>
        /// When implemented in a derived class writes the
        /// specified assertions in the "failed" format.
        /// Implementers "should" validate that the assertions actually
        /// failed.
        /// </summary>
        /// <param name="assertions"></param>
        void WriteFailedAssertions(Assertion[] assertions);
    }
}
