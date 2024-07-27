/*
	Copyright © Bryan Apellanes 2015  
*/
using Bam;
using Bam.CoreServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Test.Specification
{
    public class FeatureContextSetup<T> : FeatureContextSetup
    {
        public FeatureContextSetup(string featureDescription, Action<T> setupAction) : base(featureDescription, () => { })
        {
            Description = featureDescription;
            SetupAction = setupAction;
        }

        public new Action<T> SetupAction { get; set; }

        public override bool TrySetup(Action<ISpecTestContextSetupAction, Exception> exceptionHandler)
        {
            try
            {
                T featureArg = default;
                if (SpecTestContainer != null)
                {
                    featureArg = SpecTestContainer.SpecTestRegistry.Get<T>();
                }
                else
                {
                    featureArg = typeof(T).Construct<T>();
                }
                SetupAction(featureArg);
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

namespace Bam.Test.Specification
{
    public class FeatureContextSetup : SpecTestContextSetup
    {
        public FeatureContextSetup(string featureDescription, Action setupAction)
        {
            Description = featureDescription;
            SetupAction = setupAction;
        }

        /// <summary>
        /// Gets or sets the specification container where the feature is defined.  This property is
        /// set by the container when the feature is setup.
        /// </summary>
        /// <value>
        /// The specification container.
        /// </value>
        protected internal SpecTestContainer SpecTestContainer { get; set; }
    }
}
