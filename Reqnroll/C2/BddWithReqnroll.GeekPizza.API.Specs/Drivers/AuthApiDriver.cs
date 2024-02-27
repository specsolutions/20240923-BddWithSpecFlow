using System;
using System.Net;
using BddWithReqnroll.GeekPizza.Specs.Support;
using BddWithReqnroll.GeekPizza.Web.Models;

namespace BddWithReqnroll.GeekPizza.Specs.Drivers
{
    public class AuthApiDriver
    {
        private readonly WebApiContext _webApiContext;

        private WebApiResponse _lastResponse;

        public string LastError => _lastResponse?.ResponseMessage;

        public AuthApiDriver(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        // In a real project the return value of "AttemptLogin" would not just be a simple "bool"
        // but a structure that can hold information about unsuccessful login. 
        // In this project the "LastError" property can be queried for error details.
        public bool AttemptLogin(string userName, string password)
        {
            var data = new LoginInputModel { Name = userName, Password = password };
            _lastResponse = _webApiContext.ExecutePost("api/auth", data);
            return _lastResponse.StatusCode == HttpStatusCode.OK;
        }
    }
}
