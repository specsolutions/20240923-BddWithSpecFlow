using System;
using System.Collections.Generic;

namespace BddWithSpecFlow.GeekPizza.Web.DataAccess
{
    public class Order
    {
        public string UserName { get; set; }
        public Address DeliveryAddress { get; set; }
        public DateTime DeliveryDate { get; set; }
        public TimeSpan DeliveryTime { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public OrderPrice Prices { get; set; } = new OrderPrice();

        public static Order CreateOrder(string userName)
        {
            return new Order
            {
                UserName = userName,
                DeliveryAddress = GetUserDefaultAddress(),
                DeliveryDate = DateTime.Today,
                DeliveryTime = DateTime.Now.TimeOfDay.Add(TimeSpan.FromMinutes(40))
            };
        }

        private static Address GetUserDefaultAddress()
        {
            // For the sake of the course, we have a hard-coded default address here
            return new Address
            {
                StreetAddress = "2-4 Waterloo Pl",
                City = "Edinburgh",
                Zip = "EH1 3EG"
            };
        }
    }
}