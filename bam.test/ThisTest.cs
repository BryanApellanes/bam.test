namespace Bam.Test;

public class ThisTest
{
    public static ShouldContext Should(string testCaseSummary)
    {
        return new ShouldContext(testCaseSummary);
    } 
}