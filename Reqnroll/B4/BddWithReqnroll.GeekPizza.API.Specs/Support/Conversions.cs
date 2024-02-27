using System;
using Reqnroll;

namespace BddWithReqnroll.GeekPizza.Specs.Support
{
    [Binding]
    public class Conversions
    {
        [StepArgumentTransformation(@"(\d+):(\d+)")]
        public TimeSpan ConvertTimeSpan(int hours, int minutes)
        {
            return new TimeSpan(hours, minutes, 0);
        }
    }
}
