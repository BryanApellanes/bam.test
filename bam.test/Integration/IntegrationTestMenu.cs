using Bam.Shell;

namespace Bam.Test.Integration
{
    public class IntegrationTestMenu : MenuAttribute<IntegrationTest>
    {
        public IntegrationTestMenu()
        {
            this.Selector = "it";
        }

        public IntegrationTestMenu(string name) : base(name)
        {
            this.Selector = name?.PascalCase(true, " ").CaseAcronym().ToLowerInvariant() ?? "it";
        }

        public IntegrationTestMenu(string name, string selector) : base(name)
        {
            this.Selector = selector;
        }
    }
}
