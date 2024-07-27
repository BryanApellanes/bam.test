using System;
using System.Collections.Generic;
using System.Text;

namespace Bam.Test.Specification
{
    public interface ISpecTestContextSetupAction
    {
        string Description { get; set; }
        Action SetupAction { get; set; }
        bool TrySetup();
        bool TrySetup(Action<ISpecTestContextSetupAction, Exception> exceptionHandler);
    }
}
