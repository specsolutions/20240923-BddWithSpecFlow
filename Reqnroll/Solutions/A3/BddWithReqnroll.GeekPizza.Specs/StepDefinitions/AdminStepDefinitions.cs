using System;
using BddWithReqnroll.GeekPizza.Web.DataAccess;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class AdminStepDefinitions
    {
        [Given("the menu has been configured to contain {int} active and {int} inactive pizzas")]
        public void GivenTheMenuHasBeenConfiguredToContainActiveAndInactivePizzas(int activePizzaCount, int inactivePizzaCount)
        {
            // We ensure the preconditions by setting the menu records directly to the database (in a pretty verbose way).
            // Alternatively we could also ensure the preconditions by using the AdminController class...

            // create a database connection
            var db = new DataContext();

            // clear menu
            db.MenuItems.Clear();

            // add active pizzas
            for (int i = 0; i < activePizzaCount; i++)
            {
                var pizzaMenuItem = new PizzaMenuItem();
                pizzaMenuItem.Name = "Pizza " + i;
                pizzaMenuItem.Ingredients = "[default ingredients]";
                pizzaMenuItem.Calories = 1000;
                pizzaMenuItem.Inactive = false;
                db.MenuItems.Add(pizzaMenuItem);
            }

            // add inactive pizzas (ignore the code duplication for now)
            for (int i = 0; i < inactivePizzaCount; i++)
            {
                var pizzaMenuItem = new PizzaMenuItem();
                pizzaMenuItem.Name = "Old Pizza " + i;
                pizzaMenuItem.Ingredients = "[default ingredients]";
                pizzaMenuItem.Calories = 1000;
                pizzaMenuItem.Inactive = true;
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

            for (int i = 0; i < menuItemsTable.RowCount; i++)
            {
                var pizzaMenuItem = new PizzaMenuItem();
                pizzaMenuItem.Name = menuItemsTable.Rows[i]["name"];
                pizzaMenuItem.Ingredients = menuItemsTable.Rows[i]["ingredients"];
                pizzaMenuItem.Calories = int.Parse(menuItemsTable.Rows[i]["calories"]);
                pizzaMenuItem.Inactive = bool.Parse(menuItemsTable.Rows[i]["inactive"]);
                db.MenuItems.Add(pizzaMenuItem);
            }
            db.SaveChanges();
        }
    }
}
