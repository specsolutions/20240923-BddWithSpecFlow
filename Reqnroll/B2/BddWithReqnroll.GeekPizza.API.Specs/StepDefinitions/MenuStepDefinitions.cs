using System;
using BddWithReqnroll.GeekPizza.Specs.Support;
using BddWithReqnroll.GeekPizza.Web.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.StepDefinitions
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
