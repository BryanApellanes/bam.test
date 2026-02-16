/*
	Copyright © Bryan Apellanes 2015  
*/

namespace Bam.Test
{
    /// <summary>
    /// Writes test results and assertions to the console with color-coded output:
    /// green for passed, red for failed, and cyan for descriptive text.
    /// </summary>
    public class ConsoleBecauseWriter : IBecauseWriter, IAssertionWriter
    {
        #region IBecauseWriter Members

        /// <summary>
        /// Writes the complete test result including the test description, pass/fail status,
        /// and all individual assertions to the console.
        /// </summary>
        /// <param name="because">The Because object containing the test description and assertions.</param>
        public void Write(Because because)
        {
            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.Write("{0} ", because.TestDescription);
            string result = because.Passed ? "passed" : "failed";
            ConsoleColor color = because.Passed ? ConsoleColor.Green : ConsoleColor.Red;
            System.Console.ForegroundColor = color;
            System.Console.WriteLine("{0} ", result);
            System.Console.ForegroundColor = ConsoleColor.Cyan;

            if (because.Passed)
            {
                System.Console.Write("\tbecause ");
            }
            else
            {
                System.Console.Write("\twhile ");
            }

            Assertion[] assertions = because.Assertions.ToArray();
            WritePassedAssertions(assertions);
            
            if(!because.Passed)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.Write("\t");
                
                Assertion[] failed = (from assertion in because.Assertions
                                      where !assertion.Passed
                                      select assertion).ToArray();
                
                WriteFailedAssertions(failed);
            }

            System.Console.WriteLine();
            System.Console.ResetColor();
        }

        /// <summary>
        /// Writes the failed assertions to the console in red, filtering out passed assertions.
        /// </summary>
        /// <param name="assertions">The assertions to filter and display.</param>
        public void WriteFailedAssertions(Assertion[] assertions)
        {
            Assertion[] failedAssertions = assertions.Where(a => !a.Passed).ToArray();
            for (int i = 0; i < failedAssertions.Length; i++)
            {
                if (i >= 1)
                {
                    System.Console.ForegroundColor = ConsoleColor.Cyan;
                    System.Console.Write("\tand ");
                }
                System.Console.ForegroundColor = ConsoleColor.Red;
                Assertion assertion = failedAssertions[i];
                System.Console.WriteLine("{0}", assertion.FailureMessage);
            }
        }

        /// <summary>
        /// Writes the passed assertions to the console in green, filtering out failed assertions.
        /// </summary>
        /// <param name="assertions">The assertions to filter and display.</param>
        public void WritePassedAssertions(Assertion[] assertions)
        {
            Assertion[] passedAssertions = assertions.Where(x => x.Passed).ToArray();
            for (int i = 0; i < passedAssertions.Length; i++)
            {
                Assertion assertion = passedAssertions[i];
                if (i >= 1)
                {
                    System.Console.ForegroundColor = ConsoleColor.Cyan;
                    System.Console.Write("\tand ");
                }

                System.Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("{0}", assertion.SuccessMessage);
            }
        }

        #endregion
    }
}
