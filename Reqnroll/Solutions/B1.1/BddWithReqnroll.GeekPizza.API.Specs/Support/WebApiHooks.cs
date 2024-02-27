using System;
using BddWithReqnroll.GeekPizza.Web;
using Microsoft.AspNetCore.Mvc.Testing;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.Support
{
    [Binding]
    public class WebApiHooks
    {
        private readonly WebApiContext _webApiContext;

        public WebApiHooks(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        [BeforeScenario("@webapi")]
        public void StartApplication()
        {
            // start application
            _webApiContext.WebApplicationFactory = new WebApplicationFactory<Startup>();

            // create HTTP client
            _webApiContext.HttpClient = _webApiContext.WebApplicationFactory.CreateClient();
        }

        [AfterScenario("@webapi")]
        public void StopApplication()
        {
            // dispose HttpClient
            _webApiContext.HttpClient.Dispose();

            // stop application
            _webApiContext.WebApplicationFactory.Dispose();
        }
    }
}
