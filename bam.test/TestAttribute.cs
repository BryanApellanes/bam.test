using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bam.Test
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class TestAttribute : MenuItemAttribute
    {
        public TestAttribute(params TestType[] testType)
        {
            this.TypeTypes = testType;
        }

        public TestType[] TypeTypes { get; private set; }

        /// <summary>
        /// Gets or sets a value indicating whether the test should be run synchronously.
        /// </summary>
        public bool RunSynchronously { get; set; } = false;

        /// <summary>
        /// Gets the object used to run tests synchronously.
        /// </summary>
        public static object Lock { get; } = new object();
    }
}
