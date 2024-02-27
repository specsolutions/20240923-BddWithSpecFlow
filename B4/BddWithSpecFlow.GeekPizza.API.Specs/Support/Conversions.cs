using System;
using TechTalk.SpecFlow;

namespace BddWithSpecFlow.GeekPizza.Specs.Support
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
