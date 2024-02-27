using System;
using BddWithReqnroll.GeekPizza.Web.Controllers;
using BddWithReqnroll.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class HomeStepDefinitions
    {
        private HomePageModel _homePageModel;

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
    }
}
