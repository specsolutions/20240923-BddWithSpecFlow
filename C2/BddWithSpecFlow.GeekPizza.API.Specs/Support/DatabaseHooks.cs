using BddWithSpecFlow.GeekPizza.Web.DataAccess;
using TechTalk.SpecFlow;

namespace BddWithSpecFlow.GeekPizza.Specs.Support
{
    [Binding]
    public class DatabaseHooks
    {
        [BeforeScenario(Order = 100)]
        public void ResetDatabaseToBaseline()
        {
            ClearDatabase();

            DomainDefaults.AddDefaultUsers();
            DomainDefaults.AddDefaultPizzas();
        }

        private static void ClearDatabase()
        {
            var db = new DataContext();
            db.TruncateTables();
        }
    }
}
