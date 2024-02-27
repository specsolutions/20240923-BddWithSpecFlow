using System;
using BddWithReqnroll.GeekPizza.Specs.Support;
using BddWithReqnroll.GeekPizza.Web.DataAccess;
using BddWithReqnroll.GeekPizza.Web.Models;

namespace BddWithReqnroll.GeekPizza.Specs.Drivers
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

        public PizzaMenuItem GetPizzaMenuItem(Guid id)
        {
            return _webApiContext.ExecuteGet<PizzaMenuItem>($"api/menu/{id}");
        }
    }
}
