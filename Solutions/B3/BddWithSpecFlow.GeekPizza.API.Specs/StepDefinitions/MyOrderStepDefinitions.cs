using System;
using System.Net;
using BddWithSpecFlow.GeekPizza.Specs.Support;
using BddWithSpecFlow.GeekPizza.Web.DataAccess;
using BddWithSpecFlow.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class MyOrderStepDefinitions
    {
        private readonly WebApiContext _webApiContext;

        private Order _myOrderResponse;
        private Table _orderedItems;

        public MyOrderStepDefinitions(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        [Given("the client has the following items in the basket")]
        public void GivenTheClientHasTheFollowingItemsInTheBasket(Table orderItemsTable)
        {
            foreach (var orderItemRow in orderItemsTable.Rows)
            {
                // prepare JSON payload data
                var data = new AddToOrderInputModel
                {
                    Name = orderItemRow["name"], 
                    Size = (OrderItemSize)Enum.Parse(typeof(OrderItemSize), orderItemRow["size"])
                };

                // execute request
                var response = _webApiContext.ExecutePost("/api/order", data);

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
        public void ThenTheFollowingItemsShouldBeListedOnTheMyOrderPage(Table expectedOrderItemsTable)
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
