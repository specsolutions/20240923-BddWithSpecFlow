using System;
using BddWithReqnroll.GeekPizza.Specs.Support;
using BddWithReqnroll.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class HomeStepDefinitions
    {
        private readonly WebApiContext _webApiContext;
        private HomePageModel _homePageModel;

        public HomeStepDefinitions(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        [When("the client checks the home page")]
        public void WhenTheClientChecksTheHomePage()
        {
            _homePageModel = _webApiContext.ExecuteGet<HomePageModel>("/api/home");
        }

        [Then("the user name of the client should be on the home page")]
        public void ThenTheUserNameOfTheClientShouldBeOnTheHomePage()
        {
            Assert.AreEqual("Marvin", _homePageModel.UserName);
        }
    }
}
