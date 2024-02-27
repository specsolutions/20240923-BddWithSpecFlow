using System;
using BddWithReqnroll.GeekPizza.Web.DataAccess;

namespace BddWithReqnroll.GeekPizza.Web.Models
{
    public class AddToOrderInputModel
    {
        public string Name { get; set; }
        public OrderItemSize Size { get; set; }
    }
}
