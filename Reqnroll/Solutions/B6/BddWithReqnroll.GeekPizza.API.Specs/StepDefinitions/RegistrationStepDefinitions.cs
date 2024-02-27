using System;
using System.Net;
using BddWithReqnroll.GeekPizza.Specs.Support;
using BddWithReqnroll.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.StepDefinitions
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

        [Given("there is a user registered with user name {string} and password {string}")]
        public void GivenThereIsAUserRegisteredWithUserNameAndPassword(string userName, string password)
        {
            //TODO: the code duplication will be eliminated in a later exercise

            // prepare JSON payload data
            var data = new RegisterInputModel { UserName = userName, Password = password, PasswordReEnter = password };

            // execute request
            var result = _webApiContext.ExecutePost("/api/user", data);

            // functional check
            Assert.AreEqual(HttpStatusCode.OK, result.StatusCode);
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
