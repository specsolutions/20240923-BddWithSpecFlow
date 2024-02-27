using BddWithReqnroll.GeekPizza.Web.Controllers;
using BddWithReqnroll.GeekPizza.Web.Models;

namespace BddWithReqnroll.GeekPizza.Specs.Drivers
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