using System;

namespace BddWithSpecFlow.GeekPizza.Web.DataAccess
{
    /// <summary>
    /// Represents a menu item on the restaurant menu
    /// </summary>
    public class PizzaMenuItem
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Ingredients { get; set; }
        public int Calories { get; set; }
        public bool Inactive { get; set; }
    }
}