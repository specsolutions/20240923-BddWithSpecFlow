using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BddWithSpecFlow.GeekPizza.Specs.Support
{
    public class AuthContext
    {
        public string LoggedInUserName { get; set; }

        public bool IsLoggedIn => LoggedInUserName != null;

        public string AssertLoggedInUser()
        {
            Assert.IsTrue(IsLoggedIn, "No user has logged in yet");
            return LoggedInUserName;
        }
    }
}
