using System;
using System.Linq;

namespace BddWithSpecFlow.GeekPizza.Web.DataAccess
{
    public class Address
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string Zip { get; set; }

        public override string ToString()
        {
            return $"{StreetAddress}, {City}, {Zip}";
        }
    }
}