using System;
using System.Net;
using BddWithReqnroll.GeekPizza.Specs.Support;
using BddWithReqnroll.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class AuthStepDefinitions
    {
        private readonly WebApiContext _webApiContext;
        private readonly AuthContext _authContext;

        public AuthStepDefinitions(WebApiContext webApiContext, AuthContext authContext)
        {
            _webApiContext = webApiContext;
            _authContext = authContext;
        }

        [Given("the client is logged in")]
        [BeforeScenario("@login", Order = 200)]
        public void GivenTheClientIsLoggedIn()
        {
            var defaultUserName = "Marvin";

            // prepare JSON payload data
            var data = new LoginInputModel { Name = defaultUserName, Password = "1234" };

            // execute request
            var response = _webApiContext.ExecutePost("/api/auth", data);

            // functional check
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            _authContext.LoggedInUserName = defaultUserName;
        }

        [Given("the client is logged in with user name {string} and password {string}")]
        public void GivenTheClientIsLoggedInWithUserNameAndPassword(string userName, string password)
        {
            //TODO: the code duplication will be eliminated in a later exercise

            // prepare JSON payload data
            var data = new LoginInputModel { Name = userName, Password = password };

            // execute request
            var response = _webApiContext.ExecutePost("/api/auth", data);

            // functional check
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            _authContext.LoggedInUserName = userName;
        }
    }
}
