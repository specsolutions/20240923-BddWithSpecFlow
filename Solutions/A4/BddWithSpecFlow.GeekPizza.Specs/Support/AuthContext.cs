using System;

namespace BddWithSpecFlow.GeekPizza.Specs.Support
{
    public class AuthContext
    {
        public string AuthToken { get; set; }
        public string LoggedInUserName { get; set; }

        public bool IsLoggedIn => LoggedInUserName != null;
    }
}
