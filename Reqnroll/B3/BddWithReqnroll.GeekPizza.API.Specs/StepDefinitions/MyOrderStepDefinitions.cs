using System;
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
    }
}
