using BddWithReqnroll.GeekPizza.Specs.Drivers;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.Support
{
    [Binding]
    public class Injector : Steps
    {
        [BeforeScenario(Order = -1)]
        public void ConfigureScenarioContext()
        {
            switch (ConfigurationProvider.GetSetting("AutomationTarget").ToLower())
            {
                case "api":
                    ScenarioContext.ScenarioContainer.RegisterTypeAs<MenuApiDriver, IMenuDriver>();
                    break;
                case "controller":
                    ScenarioContext.ScenarioContainer.RegisterTypeAs<ControllerMenuDriver, IMenuDriver>();
                    break;
            }
        }
    }
}
