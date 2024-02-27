using System;
using BddWithSpecFlow.GeekPizza.Specs.Support;
using BddWithSpecFlow.GeekPizza.Web.Models;

namespace BddWithSpecFlow.GeekPizza.Specs.Drivers
{
    public class HomeApiDriver
    {
        private readonly WebApiContext _webApiContext;

        public HomeApiDriver(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        public HomePageModel GetHomePageModel()
        {
            return _webApiContext.ExecuteGet<HomePageModel>("/api/home");
        }
    }
}
