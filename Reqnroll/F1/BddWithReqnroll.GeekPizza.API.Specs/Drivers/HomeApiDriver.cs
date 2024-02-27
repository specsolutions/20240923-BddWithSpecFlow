using System;
using BddWithReqnroll.GeekPizza.Specs.Support;
using BddWithReqnroll.GeekPizza.Web.Models;

namespace BddWithReqnroll.GeekPizza.Specs.Drivers
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
