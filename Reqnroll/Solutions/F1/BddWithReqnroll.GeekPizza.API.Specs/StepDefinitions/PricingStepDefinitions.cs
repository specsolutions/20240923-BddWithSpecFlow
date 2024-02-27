using System;
using System.Linq;
using BddWithReqnroll.GeekPizza.Specs.Drivers;
using BddWithReqnroll.GeekPizza.Specs.Support;
using BddWithReqnroll.GeekPizza.Web.DataAccess;
using BddWithReqnroll.GeekPizza.Web.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class PricingStepDefinitions
    {
        private readonly OrderApiDriver _orderApiDriver;
        private OrderPrice _orderPrice;

        public PricingStepDefinitions(OrderApiDriver orderApiDriver)
        {
            _orderApiDriver = orderApiDriver;
        }

        private OrderPrice LoadCurrentPrice()
        {
            var myOrderResponse = _orderApiDriver.GetMyOrder();
            return myOrderResponse.Prices;
        }

        [Given("the client has items in the basket with subtotal of ${decimal}")]
        public void GivenTheClientHasItemsInTheBasketWithSubtotalOf(decimal subtotal)
        {
            var price = LoadCurrentPrice();
            while (price.Subtotal < subtotal)
            {
                _orderApiDriver.EnsureAddToOrder(DomainDefaults.MenuItemName, OrderItemSize.Small);
                price = LoadCurrentPrice();
            }

            Assert.AreEqual(subtotal, price.Subtotal, "Could not make an order with the expected subtotal");
        }


        [When("the client checks the price")]
        public void WhenTheClientChecksThePrice()
        {
            _orderPrice = LoadCurrentPrice();
        }

        [Then("the subtotal should be ${decimal}")]
        public void ThenTheSubtotalShouldBe(decimal expectedSubtotal)
        {
            Assert.AreEqual(expectedSubtotal, _orderPrice.Subtotal);
        }

        [Then("the delivery costs should be ${decimal}")]
        public void ThenTheDeliveryCostsShouldBe(decimal expectedDeliveryCosts)
        {
            Assert.AreEqual(expectedDeliveryCosts, _orderPrice.DeliveryCosts);
        }

        [Then("the total should be ${decimal}")]
        public void ThenTheTotalShouldBe(decimal expectedTotal)
        {
            Assert.AreEqual(expectedTotal, _orderPrice.Total);
        }

        [Then("the subtotal should be the sum of the order item prices")]
        public void ThenTheSubtotalShouldBeTheSumOfTheOrderItemPrices()
        {
            // get the order
            var order = _orderApiDriver.GetMyOrder();

            // service to calculate item prices
            var priceCalculatorService = new PriceCalculatorService();

            decimal expectedSubtotal = 0;
            foreach (var orderOrderItem in order.OrderItems)
            {
                var tempOrder = new Order();
                tempOrder.OrderItems.Add(orderOrderItem);

                var tempPrice = priceCalculatorService.GetOrderPrice(tempOrder);
                expectedSubtotal += tempPrice.Subtotal;
            }

            Assert.AreEqual(expectedSubtotal, _orderPrice.Subtotal);
        }

        [Then("the delivery should not be free")]
        public void ThenTheDeliveryShouldNotBeFree()
        {
            Assert.AreNotEqual(0m, _orderPrice.DeliveryCosts);
        }

        [Then("the total should be the sum of subtotal and delivery costs")]
        public void ThenTheTotalShouldBeTheSumOfSubtotalAndDeliveryCosts()
        {
            Assert.AreEqual(_orderPrice.Subtotal + _orderPrice.DeliveryCosts, _orderPrice.Total);
        }

        [Then("the subtotal should be for {int} small and {int} medium pizzas")]
        public void ThenTheSubtotalShouldBeForSmallAndMediumPizzas(int paidSmall, int paidMedium)
        {
            // get the order
            var order = _orderApiDriver.GetMyOrder();

            // service to calculate item prices
            var priceCalculatorService = new PriceCalculatorService();

            var paidSmallItems = order.OrderItems.Where(oi => oi.Size == OrderItemSize.Small).Take(paidSmall);
            var paidMediumItems = order.OrderItems.Where(oi => oi.Size == OrderItemSize.Medium).Take(paidMedium);
            var paidItems = paidSmallItems.Concat(paidMediumItems);

            decimal expectedSubtotal = 0;
            foreach (var orderOrderItem in paidItems)
            {
                var tempOrder = new Order();
                tempOrder.OrderItems.Add(orderOrderItem);

                var tempPrice = priceCalculatorService.GetOrderPrice(tempOrder);
                expectedSubtotal += tempPrice.Subtotal;
            }

            Assert.AreEqual(expectedSubtotal, _orderPrice.Subtotal);
        }

    }
}
