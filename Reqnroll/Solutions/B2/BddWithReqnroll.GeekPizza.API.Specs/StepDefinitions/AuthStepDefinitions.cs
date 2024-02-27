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
    }
}
