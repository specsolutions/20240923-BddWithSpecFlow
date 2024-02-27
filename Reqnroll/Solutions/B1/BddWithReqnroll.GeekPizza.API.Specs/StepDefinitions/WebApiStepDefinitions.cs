using System;
using System.Net;
using System.Net.Http;
using System.Text;
using BddWithReqnroll.GeekPizza.Web;
using BddWithReqnroll.GeekPizza.Web.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class WebApiStepDefinitions
    {
        private WebApplicationFactory<Startup> _webApplicationFactory;
        private HttpClient _httpClient;

        private HomePageModel _homePageModel;

        [BeforeScenario("@webapi")]
        public void StartApplication()
        {
            // start application
            _webApplicationFactory = new WebApplicationFactory<Startup>();

            // create HTTP client
            _httpClient = _webApplicationFactory.CreateClient();
        }

        [AfterScenario("@webapi")]
        public void StopApplication()
        {
            // dispose HttpClient
            _httpClient.Dispose();

            // stop application
            _webApplicationFactory.Dispose();
        }

        [Given("the client is logged in")]
        public void GivenTheClientIsLoggedIn()
        {
            // prepare JSON payload data
            var data = new LoginInputModel { Name = "Marvin", Password = "1234" };

            // execute request
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = _httpClient.PostAsync("/api/auth", content).Result;

            // sanity check
            Assert.IsTrue((int)response.StatusCode >= 200 && (int)response.StatusCode < 500);

            // functional check
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [When("the client checks the home page")]
        public void WhenTheClientChecksTheHomePage()
        {
            // execute request
            // (we need to use the same HttpClient otherwise the auth token cookie gets lost)
            var response = _httpClient.GetAsync("/api/home").Result;

            // sanity check
            Assert.IsTrue((int)response.StatusCode >= 200 && (int)response.StatusCode < 300);

            // deserialize response data
            var content = response.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<HomePageModel>(content);

            // functional check (not needed here)
            Assert.AreEqual("Welcome to Geek Pizza!", data.MainMessage);

            // save result for assertions in Then steps
            _homePageModel = data;
        }

        [Then("the user name of the client should be on the home page")]
        public void ThenTheUserNameOfTheClientShouldBeOnTheHomePage()
        {
            Assert.AreEqual("Marvin", _homePageModel.UserName);
        }
    }
}
