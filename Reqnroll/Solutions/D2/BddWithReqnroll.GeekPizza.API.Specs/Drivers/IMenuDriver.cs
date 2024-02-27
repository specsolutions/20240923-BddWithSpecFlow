using System;
using BddWithReqnroll.GeekPizza.Web.Models;

namespace BddWithReqnroll.GeekPizza.Specs.Drivers
{
    public interface IMenuDriver
    {
        PizzaMenuModel GetPizzaMenu();
    }
}
