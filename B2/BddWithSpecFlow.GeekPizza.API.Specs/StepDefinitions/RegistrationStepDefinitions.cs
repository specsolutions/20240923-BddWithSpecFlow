using System;
using System.Net;
using BddWithSpecFlow.GeekPizza.Specs.Support;
using BddWithSpecFlow.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
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
