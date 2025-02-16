using Bam.Logging;

namespace Bam.Test.Specification
{
    public abstract class SpecTestContextSetup : Loggable, ISpecTestContextSetupAction
    {
        public string Description { get; set; }
        public Action SetupAction { get; set; }

        public virtual bool TrySetup()
        {
            return TrySetup((f, x) => { });
        }

        public virtual bool TrySetup(Action<ISpecTestContextSetupAction, Exception> exceptionHandler)
        {
            try
            {
                SetupAction();
                return true;
            }
            catch (Exception ex)
            {
                exceptionHandler(this, ex);
                return false;
            }
        }
    }
}
