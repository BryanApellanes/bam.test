/*
	Copyright © Bryan Apellanes 2015  
*/

namespace Bam.Test
{
    public class ConsoleBecauseWriter : IBecauseWriter, IAssertionWriter
    {
        #region IBecauseWriter Members

        public void Write(Because because)
        {
            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.Write("{0} ", because.TestDescription);
            string result = because.Passed ? "passed" : "failed";
            ConsoleColor color = because.Passed ? ConsoleColor.Green : ConsoleColor.Red;
            System.Console.ForegroundColor = color;
            System.Console.WriteLine("{0} ", result);
            System.Console.ForegroundColor = ConsoleColor.Cyan;
            System.Console.Write("\tbecause ");

            if (because.Passed)
            {
                Assertion[] passedAssertions = because.Assertions;
                WritePassedAssertions(passedAssertions);
            }
            else
            {
                Assertion[] failed = (from assertion in because.Assertions
                                      where !assertion.Passed
                                      select assertion).ToArray();
                System.Console.ForegroundColor = ConsoleColor.Red;
                WriteFailedAssertions(failed);
            }

            System.Console.WriteLine();
            System.Console.ResetColor();
        }

        public void WriteFailedAssertions(Assertion[] failed)
        {
            for (int i = 0; i < failed.Length; i++)
            {
                if (i >= 1)
                {
                    System.Console.ForegroundColor = ConsoleColor.Cyan;
                    System.Console.Write("\tand ");
                }
                Assertion assertion = failed[i];
                System.Console.WriteLine("{0}", assertion.FailureMessage);
            }
        }

        public void WritePassedAssertions(Assertion[] passedAssertions)
        {
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
