/*
	Copyright © Bryan Apellanes 2015  
*/

namespace Bam.Test.Specification
{
    public class ScenarioContext
    {
        public ScenarioContext()
        {
            Scenarios = new Queue<ScenarioSetupContext>();
        }
        public Queue<ScenarioSetupContext> Scenarios { get; set; }
        public ScenarioSetupContext CurrentScenario { get; set; } = null!;

        public ScenarioSetupContext AddScenario(string scenarioDescription, Action scenarioSetup)
        {
            ScenarioSetupContext scenario = new ScenarioSetupContext(scenarioDescription, scenarioSetup);
            Scenarios.Enqueue(scenario);
            CurrentScenario = scenario;
            return scenario;
        }
    }
}
