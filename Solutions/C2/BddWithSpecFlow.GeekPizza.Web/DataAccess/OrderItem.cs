using System;
using System.Linq;

namespace BddWithSpecFlow.GeekPizza.Web.DataAccess
{
    public class OrderItem
    {
        public string Name { get; set; }
        public OrderItemSize Size { get; set; }
    }
}