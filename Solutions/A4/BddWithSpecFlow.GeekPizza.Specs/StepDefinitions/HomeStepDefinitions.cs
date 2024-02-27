using System;
using BddWithSpecFlow.GeekPizza.Specs.Support;
using BddWithSpecFlow.GeekPizza.Web.Controllers;
using BddWithSpecFlow.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class HomeStepDefinitions
    {
        private readonly AuthContext _authContext;
        private HomePageModel _homePageModel;

        public HomeStepDefinitions(AuthContext authContext)
        {
            _authContext = authContext;
        }

        [When("the client checks the home page")]
        public void WhenTheClientChecksTheHomePage()
        {
            var controller = new HomeController();
            _homePageModel = controller.GetHomePageModel(_authContext.AuthToken);
        }

        [Then("the home page main message should be: {string}")]
        public void ThenTheHomePageMainMessageShouldBe(string expectedMessage)
        {
            Assert.AreEqual(expectedMessage, _homePageModel.MainMessage);
        }

        [Then("the user name of the client should be on the home page")]
        public void ThenTheUserNameOfTheClientShouldBeOnTheHomePage()
        {
            Assert.IsTrue(_authContext.IsLoggedIn);
            Assert.AreEqual(_authContext.LoggedInUserName, _homePageModel.UserName);
        }
    }
}
