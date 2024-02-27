using System;
using TechTalk.SpecFlow;

namespace BddWithSpecFlow.GeekPizza.Specs.Support
{
    [Binding]
    public class WebApiHooks
    {
        [AfterTestRun]
        public static void StopApp()
        {
            AppHostingContext.StopApp();
        }
    }
}
