using System;
using BddWithReqnroll.GeekPizza.Web.Controllers;
using BddWithReqnroll.GeekPizza.Web.Models;
using BddWithReqnroll.GeekPizza.Specs.Support;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class AuthStepDefinitions
    {
        private readonly AuthContext _authContext;

        public AuthStepDefinitions(AuthContext authContext)
        {
            _authContext = authContext;
        }

        [Given("the client is logged in")]
        public void GivenTheClientIsLoggedIn()
        {
            // The login process generates an authentication token that has to be passed in
            // for all subsequent controller method calls in the same user session.
            // You can see this in the method below, where we call the HomeController.GetHomePageModel
            // method.
            var defaultUserName = "Marvin";
            var controller = new AuthController();
            var token = controller.Login(new LoginInputModel { Name = defaultUserName, Password = "1234" });

            _authContext.AuthToken = token;
            _authContext.LoggedInUserName = defaultUserName;
        }
    }
}
