using System;
using BddWithSpecFlow.GeekPizza.Specs.Support;
using BddWithSpecFlow.GeekPizza.Web.Models;

namespace BddWithSpecFlow.GeekPizza.Specs.Drivers
{
    public class MenuApiDriver
    {
        private readonly WebApiContext _webApiContext;

        public MenuApiDriver(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        public PizzaMenuModel GetPizzaMenu()
        {
            return _webApiContext.ExecuteGet<PizzaMenuModel>("/api/menu");
        }
    }
}
