using System;
using System.Net;
using BddWithSpecFlow.GeekPizza.Specs.Support;
using BddWithSpecFlow.GeekPizza.Web.Models;

namespace BddWithSpecFlow.GeekPizza.Specs.Drivers
{
    public class UserApiDriver
    {
        private readonly WebApiContext _webApiContext;

        private WebApiResponse _lastResponse;

        public string LastError => _lastResponse?.ResponseMessage;

        public UserApiDriver(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        public bool AttemptRegister(string userName, string password, string passwordReEnter)
        {
            var data = new RegisterInputModel { UserName = userName, Password = password, PasswordReEnter = passwordReEnter };
            _lastResponse = _webApiContext.ExecutePost("/api/user", data);
            return _lastResponse.StatusCode == HttpStatusCode.OK;
        }
    }
}
