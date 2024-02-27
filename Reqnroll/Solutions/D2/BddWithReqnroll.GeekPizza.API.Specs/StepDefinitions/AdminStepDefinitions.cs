using System;
using BddWithReqnroll.GeekPizza.Specs.Support;
using BddWithReqnroll.GeekPizza.Web.DataAccess;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class AdminStepDefinitions
    {
        private readonly CurrentObjectContext _currentObjectContext;

        public AdminStepDefinitions(CurrentObjectContext currentObjectContext)
        {
            _currentObjectContext = currentObjectContext;
        }

        [Given("the menu has been configured to contain {int} active and {int} inactive pizzas")]
        public void GivenTheMenuHasBeenConfiguredToContainActiveAndInactivePizzas(int activePizzaCount, int inactivePizzaCount)
        {
            // We ensure the preconditions by setting the menu records directly to the database (in a pretty verbose way).
            // Alternatively we could also ensure the preconditions by using the AdminController class...

            // create a database connection
            var db = new DataContext();

            // clear menu
            db.MenuItems.Clear();

            // add pizzas
            for (int i = 0; i < activePizzaCount + inactivePizzaCount; i++)
            {
                var pizzaMenuItem = DomainDefaults.CreateDefaultPizzaMenuItem();
                pizzaMenuItem.Name = "Pizza " + i;
                if (i >= activePizzaCount)
                {
                    pizzaMenuItem.Inactive = true;
                    pizzaMenuItem.Name = "Old " + pizzaMenuItem.Name;
                }

                db.MenuItems.Add(pizzaMenuItem);
            }

            // save changed to the database
            db.SaveChanges();
        }

        [Given("the menu has been configured to contain the following pizzas")]
        public void GivenTheMenuHasBeenConfiguredToContainTheFollowingPizzas(DataTable menuItemsTable)
        {
            var db = new DataContext();
            db.MenuItems.Clear();

            var menuItems = menuItemsTable.CreateSet(DomainDefaults.CreateDefaultPizzaMenuItem);
            db.MenuItems.AddRange(menuItems);

            db.SaveChanges();

            if (menuItemsTable.ContainsColumn("#"))
                _currentObjectContext.Map(db.MenuItems, menuItemsTable);
        }
    }
}
