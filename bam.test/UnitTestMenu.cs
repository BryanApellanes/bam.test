using Bam.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bam;

namespace Bam.Test
{
    /// <summary>
    /// Attribute used to adorn a class that contains unit tests.
    /// </summary>
    public class UnitTestMenu : MenuAttribute<UnitTest>
    {
        public UnitTestMenu() 
        {
            this.Selector = "ut";
        }

        public UnitTestMenu(string name) : base(name)
        {
            this.Selector = name?.PascalCase(true, " ").CaseAcronym().ToLowerInvariant() ?? "ut";
        }

        public UnitTestMenu(string name, string selector) : base(name)
        {
            this.Selector = selector;
        }
    }
}
