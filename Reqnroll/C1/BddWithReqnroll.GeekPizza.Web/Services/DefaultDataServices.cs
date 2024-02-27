using System;
using BddWithReqnroll.GeekPizza.Web.DataAccess;

namespace BddWithReqnroll.GeekPizza.Web.Services
{
    /// <summary>
    /// Helper methods to add default data to the database
    /// </summary>
    public static class DefaultDataServices
    {
        public const string DefaultUserName = "Marvin";
        public const string DefaultPassword = "1234";

        public const string AdminUserName = "admin";
        public const string AdminPassword = "secret";

        internal static void EnsureDefaultUser()
        {
            var db = new DataContext();
            var user = db.FindUserByName(DefaultUserName);
            if (user == null)
            {
                db.Users.Add(new User { Name = DefaultUserName, Password = DefaultPassword });
                db.SaveChanges();
            }
            var admin = db.FindUserByName(AdminUserName);
            if (admin == null)
            {
                db.Users.Add(new User { Name = AdminUserName, Password = AdminPassword });
                db.SaveChanges();
            }
        }

        public static void SeedWithDefaultData(DataContext db)
        {
            AddDefaultUsers(db);
            AddDefaultPizzas(db);
            db.SaveChanges();
        }

        private static void AddDefaultUsers(DataContext db)
        {
            db.Users.Add(new User { Name = AdminUserName, Password = AdminPassword });
            db.Users.Add(new User { Name = DefaultUserName, Password = DefaultPassword });
        }

        private static void AddDefaultPizzas(DataContext db)
        {
            db.MenuItems.Add(new PizzaMenuItem
            {
                Name = "Aslak Hellesøy's Cucumber",
                Ingredients = "Cucumber, Gherkin, Pickles",
                Calories = 1920
            });
            db.MenuItems.Add(new PizzaMenuItem
            {
                Name = "Uncle Bob's FitNesse",
                Ingredients = "Chicken, Low cal cheese",
                Calories = 1340
            });
            db.MenuItems.Add(new PizzaMenuItem
            {
                Name = "Chris Matts' GWT",
                Ingredients = "Garlic, Wasabi, Tomato",
                Calories = 1580
            });
            db.MenuItems.Add(new PizzaMenuItem
            {
                Name = "Gojko Adzic's 50Q",
                Ingredients = "Quail, Quinoa, quark, quince & 46 others",
                Calories = 2340
            });
            db.MenuItems.Add(new PizzaMenuItem
            {
                Name = "Dan North's b-hake",
                Ingredients = "Hake/cod/haddock, mushy peas, chips",
                Calories = 2150
            });
            db.MenuItems.Add(new PizzaMenuItem
            {
                Name = "A very old pizza",
                Ingredients = "No one remembers...",
                Calories = 1010,
                Inactive = true
            });
        }
    }
}