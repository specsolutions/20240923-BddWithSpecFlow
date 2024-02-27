using System;
using BddWithSpecFlow.GeekPizza.Specs.Support;
using BddWithSpecFlow.GeekPizza.Web.DataAccess;
using BddWithSpecFlow.GeekPizza.Web.Models;

namespace BddWithSpecFlow.GeekPizza.Specs.Drivers
{
    public class MenuApiDriver : IMenuDriver
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

        public PizzaMenuItem GetPizzaMenuItem(Guid id)
        {
            return _webApiContext.ExecuteGet<PizzaMenuItem>($"api/menu/{id}");
        }
    }
}
