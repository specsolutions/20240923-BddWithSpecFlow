using System;
using TechTalk.SpecFlow;

namespace BddWithSpecFlow.GeekPizza.Specs.Support
{
    [Binding]
    public class WebApiHooks
    {
        private readonly WebApiContext _webApiContext;
        private readonly TestFolders _testFolders;
        private readonly ScenarioContext _scenarioContext;

        public WebApiHooks(WebApiContext webApiContext, TestFolders testFolders, ScenarioContext scenarioContext)
        {
            _webApiContext = webApiContext;
            _testFolders = testFolders;
            _scenarioContext = scenarioContext;
        }

        [AfterTestRun]
        public static void StopApp()
        {
            AppHostingContext.StopApp();
        }
    }
}
