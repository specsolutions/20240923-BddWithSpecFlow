using BddWithSpecFlow.GeekPizza.Web.Controllers;
using BddWithSpecFlow.GeekPizza.Web.Models;

namespace BddWithSpecFlow.GeekPizza.Specs.Drivers
{
    public class ControllerMenuDriver : IMenuDriver
    {
        public PizzaMenuModel GetPizzaMenu()
        {
            var controller = new MenuController();
            return controller.GetPizzaMenu();
        }
    }
}