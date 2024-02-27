using System;
using BddWithSpecFlow.GeekPizza.Web.Models;

namespace BddWithSpecFlow.GeekPizza.Specs.Drivers
{
    public interface IMenuDriver
    {
        PizzaMenuModel GetPizzaMenu();
    }
}
