using BddWithReqnroll.GeekPizza.Specs.Drivers;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.Support
{
    [Binding]
    public class Injector
    {
        private readonly ScenarioContext _scenarioContext;

        public Injector(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }


        [BeforeScenario(Order = -1)]
        public void ConfigureScenarioContext()
        {
            switch (ConfigurationProvider.GetSetting("AutomationTarget").ToLower())
            {
                case "api":
                    _scenarioContext.ScenarioContainer.RegisterTypeAs<MenuApiDriver, IMenuDriver>();
                    break;
                case "controller":
                    _scenarioContext.ScenarioContainer.RegisterTypeAs<ControllerMenuDriver, IMenuDriver>();
                    break;
            }
        }
    }
}
