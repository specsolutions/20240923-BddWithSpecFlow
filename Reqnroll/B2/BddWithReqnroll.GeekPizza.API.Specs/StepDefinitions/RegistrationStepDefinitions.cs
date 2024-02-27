using System;
using System.Net;
using BddWithReqnroll.GeekPizza.Specs.Support;
using BddWithReqnroll.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class RegistrationStepDefinitions
    {
        private readonly WebApiContext _webApiContext;

        public RegistrationStepDefinitions(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        //TODO: add step definitions
    }
}
