/*
	Copyright © Bryan Apellanes 2015  
*/

using Bam.DependencyInjection;
using Bam.Logging;
using Bam.Services;

namespace Bam.Test.Specification
{
    public abstract class SpecTestContainer 
    {
        Dictionary<string, object> _features;

        protected SpecTestContainer()
        {
            FeatureContext = new FeatureContext();
            ScenarioContext = new ScenarioContext();
            SpecTestRegistry = new ServiceRegistry();
            _features = new Dictionary<string, object>();
            //TestResults = new TestReporter();
        }

        public ServiceRegistry SpecTestRegistry { get; set; }
        internal FeatureContext FeatureContext { get; set; }
        internal ScenarioContext ScenarioContext { get; set; }
        internal TestReporter TestResults = null!;

        FeatureContextSetup _currentFeatureSetupContext = null!;
        ScenarioSetupContext _currentScenarioContextSetup = null!;

        public virtual void Setup() { }
        public virtual void TearDown() { }

        public virtual ILogger Logger { get; set; } = null!;
        
        public void RunSpecTest(SpecTestContainer container, SpecTestMethod testMethod)
        {
            testMethod.Provider = container;
            testMethod.Invoke();
            while (container.FeatureContext.Features.Count > 0)
            {
                _currentFeatureSetupContext = container.FeatureContext.Features.Dequeue();
                _currentFeatureSetupContext.SpecTestContainer = this;
                if (!_currentFeatureSetupContext.TrySetup((f, x) => Logger.AddEntry("Feature ({0}) failed: {1}", x, f.Description, x.Message)))
                {
                    Logger.Error("Feature prep failed");
                    throw new FeatureSetupFailedException(_currentFeatureSetupContext.Description);
                }
                else
                {
                    while (ScenarioContext.Scenarios.Count > 0)
                    {
                        _currentScenarioContextSetup = ScenarioContext.Scenarios.Dequeue();
                        _currentScenarioContextSetup.FeatureContext = container.FeatureContext;
                        _currentScenarioContextSetup.CurrentFeature = _currentFeatureSetupContext;
                        _currentScenarioContextSetup.SpecTestContainer = this;
                        bool success = _currentScenarioContextSetup.Execute();
                        if (!success)
                        {
                            throw new SpecTestFailedException(_currentScenarioContextSetup.Description);
                        }
                    }
                }
            }
        }

        public void Feature<T>(string feature, Action<T> featureSetup)
        {
            FeatureContext.Features.Enqueue(new FeatureContextSetup<T>(feature, featureSetup));
        }

        public void Feature(string feature, Action featureAction)
        {
            FeatureContext.Features.Enqueue(new FeatureContextSetup(feature, featureAction));
        }

        public ScenarioSetupContext Scenario(string scenario, Action scenarioAction)
        {
            return ScenarioContext.AddScenario(scenario, scenarioAction);
        }

        public ScenarioSetupContext Given(string given, Action givenAction)
        {
            return ScenarioContext.CurrentScenario.Given(given, givenAction);
        }

        /// <summary>
        /// Gets the reporter from the SpecTestRegistry if it exists, otherwise returns a new SpecTestReporter.
        /// </summary>
        /// <returns></returns>
        public TestReporter? GetReporter()
        {
            if (SpecTestRegistry.TryGet(out TestReporter reporter))
            {
                return reporter;
            }

            return null;
        }
    }
}
