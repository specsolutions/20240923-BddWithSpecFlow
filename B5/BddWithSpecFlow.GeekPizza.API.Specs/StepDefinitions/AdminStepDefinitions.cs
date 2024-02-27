using System;
using System.Collections.Generic;
using BddWithSpecFlow.GeekPizza.Specs.Support;
using BddWithSpecFlow.GeekPizza.Web.Controllers;
using BddWithSpecFlow.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BddWithSpecFlow.GeekPizza.Web.DataAccess;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
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
        public void GivenTheMenuHasBeenConfiguredToContainTheFollowingPizzas(Table menuItemsTable)
        {
            var db = new DataContext();
            db.MenuItems.Clear();

            var menuItems = menuItemsTable.CreateSet(DomainDefaults.CreateDefaultPizzaMenuItem);
            db.MenuItems.AddRange(menuItems);

            db.SaveChanges();
        }
    }
}
