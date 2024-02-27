using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BddWithReqnroll.GeekPizza.Web.DataAccess;
using BddWithReqnroll.GeekPizza.Web.Models;
using BddWithReqnroll.GeekPizza.Web.Services;
using BddWithReqnroll.GeekPizza.Web.Utils;
using Microsoft.AspNetCore.Mvc;

namespace BddWithReqnroll.GeekPizza.Web.Controllers
{
    /// <summary>
    /// Processes user management requests
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext _db = new();

        // POST /api/user -- registers a user
        [HttpPost]
        public User Register(RegisterInputModel registerModel)
        {
            if (string.IsNullOrEmpty(registerModel.UserName))
                throw new HttpResponseException(HttpStatusCode.BadRequest, "Name must be provided");
            if (string.IsNullOrEmpty(registerModel.Password) || string.IsNullOrEmpty(registerModel.PasswordReEnter))
                throw new HttpResponseException(HttpStatusCode.BadRequest, "Password and password re-enter must be provided");
            if (registerModel.Password != registerModel.PasswordReEnter)
                throw new HttpResponseException(HttpStatusCode.BadRequest, "Re-entered password is different");
            if (registerModel.Password.Length < 4)
                throw new HttpResponseException(HttpStatusCode.BadRequest, "Password must be at least 4 characters long");

            var existingUser = _db.Users.FirstOrDefault(u => u.Name == registerModel.UserName);
            if (existingUser != null)
                _db.Users.Remove(existingUser);

            var user = new User
            {
                Name = registerModel.UserName,
                Password = registerModel.Password
            };
            _db.Users.Add(user);
            _db.SaveChanges();

            return user;
        }

        // GET: api/user -- returns all users
        [HttpGet]
        public List<User> GetUsers(string token = null)
        {
            AuthenticationServices.EnsureAdminAuthenticated(HttpContext, token);

            var users = _db.Users.OrderBy(u => u.Name).ToList();
            return users;
        }

        // GET: api/user/[guid] -- returns user details
        [HttpGet("{id}")]
        public User GetUser(Guid id, string token = null)
        {
            var currentUserName = AuthenticationServices.EnsureAuthenticated(HttpContext, token);

            var user = _db.Users.FirstOrDefault(u => u.Id == id);

            //only admins or the user with the ID should be able to call this
            if (user == null || user.Name != currentUserName)
                AuthenticationServices.EnsureAdminAuthenticated(HttpContext, token);

            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return user;
        }
    }
}
