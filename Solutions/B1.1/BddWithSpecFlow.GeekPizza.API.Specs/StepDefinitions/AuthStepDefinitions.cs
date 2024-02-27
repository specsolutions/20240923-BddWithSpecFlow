using System;
using System.Net;
using BddWithSpecFlow.GeekPizza.Specs.Support;
using BddWithSpecFlow.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class AuthStepDefinitions
    {
        private readonly WebApiContext _webApiContext;

        public AuthStepDefinitions(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        [Given("the client is logged in")]
        public void GivenTheClientIsLoggedIn()
        {
            // prepare JSON payload data
            var data = new LoginInputModel { Name = "Marvin", Password = "1234" };

            // execute request
            var statusCode = _webApiContext.ExecutePost("/api/auth", data);

            // functional check
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
        }
    }
}
