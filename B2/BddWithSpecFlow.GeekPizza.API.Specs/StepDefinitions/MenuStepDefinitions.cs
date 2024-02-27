using System;
using BddWithSpecFlow.GeekPizza.Specs.Support;
using BddWithSpecFlow.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class MenuStepDefinitions
    {
        private readonly WebApiContext _webApiContext;

        public MenuStepDefinitions(WebApiContext webApiContext)
        {
            _webApiContext = webApiContext;
        }

        //TODO: add step definitions
    }
}
