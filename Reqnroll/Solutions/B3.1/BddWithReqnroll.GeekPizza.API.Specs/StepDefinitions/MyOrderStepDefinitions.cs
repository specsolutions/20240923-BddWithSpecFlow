using System;
using System.Linq;
using System.Net;
using BddWithReqnroll.GeekPizza.Specs.Support;
using BddWithReqnroll.GeekPizza.Web.DataAccess;
using BddWithReqnroll.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class MyOrderStepDefinitions
    {
        private readonly WebApiContext _webApiContext;

        private Order _myOrderResponse;
        private DataTable _orderedItems;

        public MyOrderStepDefinitions(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        [Given("the client has the following items in the basket")]
        public void GivenTheClientHasTheFollowingItemsInTheBasket(DataTable orderItemsTable)
        {
            var orderItems = orderItemsTable.CreateSet(DomainDefaults.CreateAddToOrderInputModel).ToArray();
            foreach (var orderItem in orderItems)
            {
                // execute request
                var response = _webApiContext.ExecutePost("/api/order", orderItem);

                // functional check
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, response.ResponseMessage);
            }
            _orderedItems = orderItemsTable;
        }

        [When("the client checks the my order page")]
        public void WhenTheClientChecksTheMyOrderPage()
        {
            _myOrderResponse = _webApiContext.ExecuteGet<Order>("api/order");
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
