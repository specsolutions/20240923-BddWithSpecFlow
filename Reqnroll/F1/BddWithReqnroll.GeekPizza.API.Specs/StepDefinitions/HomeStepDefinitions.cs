using System;
using BddWithReqnroll.GeekPizza.Specs.Drivers;
using BddWithReqnroll.GeekPizza.Specs.Support;
using BddWithReqnroll.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class HomeStepDefinitions
    {
        private readonly HomeApiDriver _homeApiDriver;
        private readonly AuthContext _authContext;
        private HomePageModel _homePageModel;

        public HomeStepDefinitions(AuthContext authContext, HomeApiDriver homeApiDriver)
        {
            _authContext = authContext;
            _homeApiDriver = homeApiDriver;
        }

        [When("the client checks the home page")]
        public void WhenTheClientChecksTheHomePage()
        {
            _homePageModel = _homeApiDriver.GetHomePageModel();
        }

        [Then("the home page main message should be: {string}")]
        public void ThenTheHomePageMainMessageShouldBe(string expectedMessage)
        {
            Assert.AreEqual(expectedMessage, _homePageModel.MainMessage);
        }

        [Then("the user name of the client should be on the home page")]
        public void ThenTheUserNameOfTheClientShouldBeOnTheHomePage()
        {
            Assert.AreEqual(_authContext.AssertLoggedInUser(), _homePageModel.UserName);
        }
    }
}
