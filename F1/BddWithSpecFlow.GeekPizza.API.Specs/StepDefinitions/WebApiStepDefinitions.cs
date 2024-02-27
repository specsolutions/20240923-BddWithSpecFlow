using System;
using BddWithSpecFlow.GeekPizza.Specs.Drivers;
using BddWithSpecFlow.GeekPizza.Web.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class WebApiStepDefinitions
    {
        private readonly MenuApiDriver _menuApiDriver;

        private PizzaMenuItem _retrievedMenuItem;

        public WebApiStepDefinitions(MenuApiDriver menuApiDriver)
        {
            _menuApiDriver = menuApiDriver;
        }

        [When("the {PizzaMenuItem} is retrieved from the menu API resource by ID")]
        public void WhenTheMenuItemIsRetrievedFromTheMenuApiResourceById(PizzaMenuItem menuItem)
        {
            _retrievedMenuItem = _menuApiDriver.GetPizzaMenuItem(menuItem.Id);
        }

        [Then("the retrieved menu item should contain")]
        public void ThenTheRetrievedMenuItemShouldContain(Table expectedMenuItemTable)
        {
            Assert.IsNotNull(_retrievedMenuItem);
            expectedMenuItemTable.CompareToInstance(_retrievedMenuItem);
        }
    }
}
