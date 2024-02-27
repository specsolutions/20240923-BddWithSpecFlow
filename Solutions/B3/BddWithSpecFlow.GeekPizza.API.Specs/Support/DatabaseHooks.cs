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

            AddDefaultPizzas();
            AddDefaultUsers();
        }

        //TODO: Use this helper methods to ensure a "baseline" database
        private static void AddDefaultUsers()
        {
            var db = new DataContext();
            db.Users.Add(new User {Name = "Marvin", Password = "1234"});
            db.Users.Add(new User {Name = "Ford", Password = "1423" });
            db.SaveChanges();
        }

        private static void AddDefaultPizzas()
        {
            var db = new DataContext();
            db.MenuItems.Add(new PizzaMenuItem
            {
                Name = "Margherita",
                Ingredients = "tomato slices, oregano, mozzarella",
                Calories = 1920
            });
            db.MenuItems.Add(new PizzaMenuItem
            {
                Name = "Fitness",
                Ingredients = "sweetcorn, broccoli, feta cheese, mozzarella",
                Calories = 1340
            });
            db.MenuItems.Add(new PizzaMenuItem
            {
                Name = "BBQ",
                Ingredients = "BBQ sauce, bacon, chicken breast strips, onions",
                Calories = 1580
            });
            db.MenuItems.Add(new PizzaMenuItem
            {
                Name = "Mexican",
                Ingredients = "taco sauce, bacon, beans, sweetcorn, mozzarella",
                Calories = 2340
            });
            db.MenuItems.Add(new PizzaMenuItem
            {
                Name = "Quattro formaggi",
                Ingredients = "blue cheese, parmesan, smoked mozzarella, mozzarella",
                Calories = 2150
            });
            db.MenuItems.Add(new PizzaMenuItem
            {
                Name = "The old one",
                Ingredients = "No one remembers...",
                Calories = 1010,
                Inactive = true
            });
            db.SaveChanges();
        }

        private static void ClearDatabase()
        {
            var db = new DataContext();
            db.TruncateTables();
        }
    }
}
