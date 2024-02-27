using System;
using System.Linq;
using BddWithReqnroll.GeekPizza.Specs.Drivers;
using BddWithReqnroll.GeekPizza.Specs.Support;
using BddWithReqnroll.GeekPizza.Web.DataAccess;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class MyOrderStepDefinitions
    {
        private readonly OrderApiDriver _orderApiDriver;

        private Order _myOrderResponse;
        private DataTable _orderedItems;

        public MyOrderStepDefinitions(OrderApiDriver orderApiDriver)
        {
            _orderApiDriver = orderApiDriver;
        }

        [Given("the client has the following items in the basket")]
        public void GivenTheClientHasTheFollowingItemsInTheBasket(DataTable orderItemsTable)
        {
            var orderItems = orderItemsTable.CreateSet(DomainDefaults.CreateAddToOrderInputModel).ToArray();
            foreach (var orderItem in orderItems)
            {
                _orderApiDriver.EnsureAddToOrder(orderItem);
            }
            _orderedItems = orderItemsTable;
        }

        [Given("the client has items in the basket")]
        public void GivenTheClientHasItemsInTheBasket()
        {
            // add a "default" pizza to the basket
            _orderApiDriver.EnsureAddToOrder(DomainDefaults.MenuItemName, DomainDefaults.OrderItemSize);
        }

        [When("the client checks the my order page")]
        public void WhenTheClientChecksTheMyOrderPage()
        {
            _myOrderResponse = _orderApiDriver.GetMyOrder();
        }

        [Then("the following items should be listed on the my order page")]
        public void ThenTheFollowingItemsShouldBeListedOnTheMyOrderPage(DataTable expectedOrderItemsTable)
        {
            expectedOrderItemsTable.CompareToSet(_myOrderResponse.OrderItems);
        }

        [Then("the ordered items should be listed on the my order page")]
        public void ThenTheOrderedItemsShouldBeListedOnTheMyOrderPage()
        {
            _orderedItems.CompareToSet(_myOrderResponse.OrderItems);
        }
    }
}
