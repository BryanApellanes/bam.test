namespace Bam.Test.Specification
{
    public class AssertionFunc<T> : AssertionAction
    {
        public Func<ItContext, T> Func { get; set; } = null!;
        public T Result { get; set; } = default!;
        public virtual AssertionAction Execute(ItContext context)
        {
            try
            {
                Result = Func(context);
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
