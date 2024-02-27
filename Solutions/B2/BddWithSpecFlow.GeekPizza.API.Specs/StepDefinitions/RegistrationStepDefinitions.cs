using System;
using System.Net;
using BddWithSpecFlow.GeekPizza.Specs.Support;
using BddWithSpecFlow.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class RegistrationStepDefinitions
    {
        private readonly WebApiContext _webApiContext;
        private WebApiResponse _registerResult;

        public RegistrationStepDefinitions(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        [When("the client attempts to register with user name {string} and password {string}")]
        public void WhenTheClientAttemptsToRegisterWithUserNameAndPassword(string userName, string password)
        {
            // prepare JSON payload data
            var data = new RegisterInputModel { UserName = userName, Password = password, PasswordReEnter = password};

            // execute request
            _registerResult = _webApiContext.ExecutePost("/api/user", data);
        }

        [Then("the registration should be successful")]
        public void ThenTheRegistrationShouldBeSuccessful()
        {
            // functional check
            Assert.AreEqual(HttpStatusCode.OK, _registerResult.StatusCode);
        }
    }
}
