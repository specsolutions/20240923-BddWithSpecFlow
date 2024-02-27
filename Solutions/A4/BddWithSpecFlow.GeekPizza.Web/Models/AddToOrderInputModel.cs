using System;
using BddWithSpecFlow.GeekPizza.Web.DataAccess;

namespace BddWithSpecFlow.GeekPizza.Web.Models
{
    public class AddToOrderInputModel
    {
        public string Name { get; set; }
        public OrderItemSize Size { get; set; }
    }
}
