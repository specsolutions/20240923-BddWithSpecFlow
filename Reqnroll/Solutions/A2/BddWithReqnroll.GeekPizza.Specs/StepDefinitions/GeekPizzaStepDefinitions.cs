using System;
using BddWithReqnroll.GeekPizza.Web.Controllers;
using BddWithReqnroll.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BddWithReqnroll.GeekPizza.Web.DataAccess;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class GeekPizzaStepDefinitions
    {
        private HomePageModel _homePageModel;
        private PizzaMenuModel _menuModel;

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

        [When("the client checks the home page")]
        public void WhenTheClientChecksTheHomePage()
        {
            var controller = new HomeController();
            _homePageModel = controller.GetHomePageModel();
        }

        [Then("the home page main message should be: {string}")]
        public void ThenTheHomePageMainMessageShouldBe(string expectedMessage)
        {
            Assert.AreEqual(expectedMessage, _homePageModel.MainMessage);
        }

        [When("the client checks the menu page")]
        public void WhenTheClientChecksTheMenuPage()
        {
            var controller = new MenuController();
            _menuModel = controller.GetPizzaMenu();
        }

        [Then("there should be {int} pizzas listed")]
        public void ThenThereShouldBePizzasListed(int expectedCount)
        {
            Assert.AreEqual(expectedCount, _menuModel.Items.Count);
        }

        [Then("the following pizzas should be listed in this order")]
        public void ThenTheFollowingPizzasShouldBeListedInThisOrder(DataTable expectedMenuItemsTable)
        {
            // The CompereToSet helper method compares the menu item list with the data table
            // by matching the column headers with the property names. The manual for-loop
            // implementation is replaced by that.
            expectedMenuItemsTable.CompareToSet(_menuModel.Items, sequentialEquality: true);

            // Assert.AreEqual(expectedMenuItemsTable.RowCount, _menuModel.Items.Count);
            // for (int i = 0; i < expectedMenuItemsTable.RowCount; i++)
            // {
            //     if (expectedMenuItemsTable.ContainsColumn("name"))
            //         Assert.AreEqual(expectedMenuItemsTable.Rows[i]["name"], _menuModel.Items[i].Name);
            //     if (expectedMenuItemsTable.ContainsColumn("ingredients"))
            //         Assert.AreEqual(expectedMenuItemsTable.Rows[i]["ingredients"], _menuModel.Items[i].Ingredients);
            // }
        }
    }
}
