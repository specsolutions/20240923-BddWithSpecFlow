using System;
using BddWithReqnroll.GeekPizza.Web.Controllers;
using BddWithReqnroll.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class MenuStepDefinitions
    {
        private PizzaMenuModel _menuModel;

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
