using System;
using System.Net;
using BddWithReqnroll.GeekPizza.Specs.Support;
using BddWithReqnroll.GeekPizza.Web.DataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class OrderDetailsStepDefinitions
    {
        private readonly WebApiContext _webApiContext;

        public OrderDetailsStepDefinitions(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        [When("the client specifies {DateTime} at {TimeSpan} as delivery time")]
        public void WhenTheClientSpecifiesDateAtTimeAsDeliveryTime(DateTime deliveryDate, TimeSpan deliveryTime)
        {
            var orderChange = new Order
            {
                DeliveryDate = deliveryDate,
                DeliveryTime = deliveryTime
            };
            // execute request
            var response = _webApiContext.ExecutePut("/api/order", orderChange);
            // functional check
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode, response.ResponseMessage);
        }

        [Then("the order should indicate that the delivery date is {DateTime}")]
        public void ThenTheOrderShouldIndicateThatTheDeliveryDateIsDate(DateTime expectedDate)
        {
            var myOrderResponse = _webApiContext.ExecuteGet<Order>("api/order");
            Assert.AreEqual(expectedDate, myOrderResponse.DeliveryDate.ToLocalTime());
        }

        [Then("the delivery time should be {TimeSpan}")]
        public void ThenTheDeliveryTimeShouldBe(TimeSpan expectedTime)
        {
            var myOrderResponse = _webApiContext.ExecuteGet<Order>("api/order");
            Assert.AreEqual(expectedTime, myOrderResponse.DeliveryTime);
        }
    }
}
