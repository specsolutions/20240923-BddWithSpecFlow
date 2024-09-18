using System;
using System.Net;
using BddWithReqnroll.GeekPizza.Web.DataAccess;
using BddWithReqnroll.GeekPizza.Web.Models;
using BddWithReqnroll.GeekPizza.Web.Services;
using BddWithReqnroll.GeekPizza.Web.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BddWithReqnroll.GeekPizza.Web.Controllers
{
    /// <summary>
    /// Processes ordering related requests
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly PriceCalculatorService _priceCalculatorService = new();

        // GET: api/order -- get my order (always exists, but might be empty)
        public Order GetMyOrder(string token = null)
        {
            var userName = AuthenticationServices.EnsureAuthenticated(HttpContext, token);

            var db = new DataContext();
            return db.GetMyOrder(userName);
        }

        // PUT: api/order -- update order (details)
        [HttpPut]
        public Order UpdateOrderDetails([FromBody] Order orderUpdates, string token = null)
        {
            var userName = AuthenticationServices.EnsureAuthenticated(HttpContext, token);

            var db = new DataContext();
            var myOrder = db.GetMyOrder(userName);
            if (orderUpdates.DeliveryAddress?.StreetAddress != null)
                myOrder.DeliveryAddress.StreetAddress = orderUpdates.DeliveryAddress?.StreetAddress;
            if (orderUpdates.DeliveryAddress?.City != null)
                myOrder.DeliveryAddress.City = orderUpdates.DeliveryAddress?.City;
            if (orderUpdates.DeliveryAddress?.Zip != null)
                myOrder.DeliveryAddress.Zip = orderUpdates.DeliveryAddress?.Zip;
            myOrder.DeliveryDate = orderUpdates.DeliveryDate;
            myOrder.DeliveryTime = orderUpdates.DeliveryTime != TimeSpan.Zero ? orderUpdates.DeliveryTime : DateTime.Now.TimeOfDay.Add(TimeSpan.FromMinutes(40));
            db.SaveChanges();

            return myOrder;
        }

        // POST: api/order -- add items to my order
        [HttpPost]
        public Order AddToOrder([FromBody] AddToOrderInputModel addToOrderInput, string token = null)
        {
            var userName = AuthenticationServices.EnsureAuthenticated(HttpContext, token);

            var db = new DataContext();
            var menuItem = db.FindMenuItemByName(addToOrderInput.Name);
            var myOrder = db.GetMyOrder(userName);

            if (menuItem == null)
                throw new HttpResponseException(HttpStatusCode.BadRequest, "Invalid menu item name");

            var pizzaOrderItem = new OrderItem
            {
                Name = menuItem.Name,
                Size = addToOrderInput.Size
            };
            myOrder.OrderItems.Add(pizzaOrderItem);
            _priceCalculatorService.UpdatePrice(myOrder);
            db.SaveChanges();

            return myOrder;
        }

        // PATCH: api/order -- places the order
        [HttpPatch]
        public void PlaceOrder(string token = null)
        {
            var userName = AuthenticationServices.EnsureAuthenticated(HttpContext, token);

            // we do not place an order for real, but just clear the current order
            var db = new DataContext();
            db.DeleteMyOrder(userName);
            db.SaveChanges();
        }
    }
}