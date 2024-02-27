using System;
using BddWithSpecFlow.GeekPizza.Specs.Drivers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TechTalk.SpecFlow;

namespace BddWithSpecFlow.GeekPizza.Specs.StepDefinitions
{
    [Binding]
    public class RegistrationStepDefinitions
    {
        private readonly UserApiDriver _userApiDriver;
        private bool _registerResult;

        public RegistrationStepDefinitions(UserApiDriver userApiDriver)
        {
            _userApiDriver = userApiDriver;
        }

        [Given("there is a user registered with user name {string} and password {string}")]
        public void GivenThereIsAUserRegisteredWithUserNameAndPassword(string userName, string password)
        {
            var result = _userApiDriver.AttemptRegister(userName, password, password);
            Assert.IsTrue(result, _userApiDriver.LastError);
        }

        [When("the client attempts to register with user name {string} and password {string}")]
        public void WhenTheClientAttemptsToRegisterWithUserNameAndPassword(string userName, string password)
        {
            _registerResult = _userApiDriver.AttemptRegister(userName, password, password);
        }

        [Then("the registration should be successful")]
        public void ThenTheRegistrationShouldBeSuccessful()
        {
            Assert.IsTrue(_registerResult, _userApiDriver.LastError);
        }
    }
}
