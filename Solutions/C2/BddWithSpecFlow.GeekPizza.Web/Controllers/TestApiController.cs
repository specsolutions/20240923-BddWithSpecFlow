using System.Net;
using BddWithSpecFlow.GeekPizza.Web.DataAccess;
using BddWithSpecFlow.GeekPizza.Web.Models;
using BddWithSpecFlow.GeekPizza.Web.Services;
using BddWithSpecFlow.GeekPizza.Web.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BddWithSpecFlow.GeekPizza.Web.Controllers
{
    /// <inheritdoc />
    /// <summary>
    /// API to enable testing back-doors. Should not be deployed in production.
    /// </summary>
    [Route("api/test/[action]")]
    [ApiController]
    public class TestApiController : ControllerBase
    {
        // POST /api/test/Reset -- clears up the database
        [HttpPost]
        public void Reset()
        {
            AuthenticationServices.ClearLoggedInUser(HttpContext);
            var dataContext = new DataContext();
            dataContext.TruncateTables();
        }

        // POST /api/test/Seed -- clears up the database and adds default data
        [HttpPost]
        public void Seed()
        {
            AuthenticationServices.ClearLoggedInUser(HttpContext);
            var dataContext = new DataContext();
            dataContext.TruncateTables();
            DefaultDataServices.SeedWithDefaultData(dataContext);
        }

        // POST /api/test/DefaultLogin -- logs in with a default user
        [HttpPost]
        public string DefaultLogin()
        {
            DefaultDataServices.EnsureDefaultUser();
            var token = AuthenticationServices.SetCurrentUser(DefaultDataServices.DefaultUserName);
            if (token == null)
                throw new HttpResponseException(HttpStatusCode.Forbidden);

            AuthenticationServices.AddAuthCookie(this.Response, token);
            return token;
        }

        // POST: api/test/BulkAddToOrder -- add multiple items to my order
        [HttpPost]
        public void BulkAddToOrder([FromBody] AddToOrderInputModel[] addToOrderInputs, string token = null)
        {
            var orderController = new OrderController();
            foreach (var addToOrderInput in addToOrderInputs)
            {
                orderController.AddToOrder(addToOrderInput, token);
            }
        }
    }
}
