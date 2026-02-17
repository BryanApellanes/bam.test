namespace Bam.Test.Specification
{
    public class AssertionAction : Assertion
    {
        public string ShouldDescription { get; set; } = null!;
        public Action Action { get; set; } = null!;
        public virtual AssertionAction Execute()
        {
            try
            {
                Action();
                Passed = true;
            }
            catch (Exception ex)
            {
                Passed = false;
                FailureMessage = ex.Message;
            }
            return this;
        }
    }
}
