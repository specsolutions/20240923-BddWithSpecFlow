using System;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.Support
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
