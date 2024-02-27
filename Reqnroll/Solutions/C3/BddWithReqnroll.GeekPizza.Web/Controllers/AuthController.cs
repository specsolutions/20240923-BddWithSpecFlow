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
    /// Processes requests related to authentication and authorization
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // GET: api/auth
        public IActionResult GetCurrentUser(string token = null)
        {
            var currentUser = AuthenticationServices.GetCurrentUserName(HttpContext, token);
            if (currentUser == null)
                return NotFound();
            return Content(currentUser);
        }

        // POST: api/auth
        [HttpPost]
        public string Login([FromBody] LoginInputModel args)
        {
            //for the sake of the course, we ensure that the default user always exists
            DefaultDataServices.EnsureDefaultUser();

            var db = new DataContext();
            var user = db.FindUserByName(args.Name);
            if (user == null || !user.Password.Equals(args.Password))
                throw new HttpResponseException(HttpStatusCode.Forbidden, "Invalid user name or password");

            var token = AuthenticationServices.SetCurrentUser(user.Name);
            if (token == null)
                throw new HttpResponseException(HttpStatusCode.Forbidden, "Authentication error");

            AuthenticationServices.AddAuthCookie(this.Response, token);
            return token;
        }

        // DELETE: api/auth
        [HttpDelete]
        public void Logout(string token = null)
        {
            AuthenticationServices.ClearLoggedInUser(HttpContext, token);
        }
    }
}