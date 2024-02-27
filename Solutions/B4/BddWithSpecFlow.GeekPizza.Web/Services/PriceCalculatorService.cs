using System.Linq;
using BddWithSpecFlow.GeekPizza.Web.DataAccess;

namespace BddWithSpecFlow.GeekPizza.Web.Services
{
    public class PriceCalculatorService
    {
        private const decimal LARGE_PRICE = 25;
        private const decimal MEDIUM_PRICE = 15;
        private const decimal SMALL_PRICE = 10;
        private const decimal DELIVERY_COST = 5;
        private const decimal DELIVERY_COST_THRESHOLD = 40;

        private decimal GetOrderItemPrice(OrderItem orderItem)
        {
            switch (orderItem.Size)
            {
                case OrderItemSize.Large:
                    return LARGE_PRICE;
                case OrderItemSize.Small:
                    return SMALL_PRICE;
                default:
                    return MEDIUM_PRICE;
            }
        }

        private decimal GetSubtotal(Order order)
        {
            decimal subtotal = 0;
            subtotal += order.OrderItems.Select(GetOrderItemPrice).Sum();
            return subtotal;
        }

        private static decimal GetDeliveryCosts(decimal subtotal)
        {
            decimal deliveryCosts = 0;
            if (subtotal <= DELIVERY_COST_THRESHOLD)
                deliveryCosts = DELIVERY_COST;
            return deliveryCosts;
        }

        public OrderPrice GetOrderPrice(Order order)
        {
            var subtotal = GetSubtotal(order);
            var deliveryCosts = GetDeliveryCosts(subtotal);
            return new OrderPrice
            {
                Subtotal = subtotal,
                DeliveryCosts = deliveryCosts,
                Total = subtotal + deliveryCosts
            };
        }

        public void UpdatePrice(Order order)
        {
            order.Prices = GetOrderPrice(order);
        }
    }
}