using BddWithReqnroll.GeekPizza.Web.DataAccess;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.Support
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
