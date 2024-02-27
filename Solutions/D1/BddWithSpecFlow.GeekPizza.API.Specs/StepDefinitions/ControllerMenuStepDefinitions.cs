using System;
using BddWithSpecFlow.GeekPizza.Web.Controllers;
using BddWithSpecFlow.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class ControllerMenuStepDefinitions
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
        public void ThenTheFollowingPizzasShouldBeListedInThisOrder(Table expectedMenuItemsTable)
        {
            expectedMenuItemsTable.CompareToSet(_menuModel.Items, sequentialEquality: true);
        }
    }
}
