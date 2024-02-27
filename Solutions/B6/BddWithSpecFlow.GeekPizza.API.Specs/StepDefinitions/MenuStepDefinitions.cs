using System;
using BddWithSpecFlow.GeekPizza.Specs.Support;
using BddWithSpecFlow.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class MenuStepDefinitions
    {
        private readonly WebApiContext _webApiContext;
        private PizzaMenuModel _menuModel;

        public MenuStepDefinitions(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        [When("the client checks the menu page")]
        public void WhenTheClientChecksTheMenuPage()
        {
            _menuModel = _webApiContext.ExecuteGet<PizzaMenuModel>("/api/menu");
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
