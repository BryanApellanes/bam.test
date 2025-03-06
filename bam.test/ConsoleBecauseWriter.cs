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
