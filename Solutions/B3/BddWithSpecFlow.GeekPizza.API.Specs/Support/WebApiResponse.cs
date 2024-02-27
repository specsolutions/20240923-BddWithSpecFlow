using System;
using System.Net;

namespace BddWithSpecFlow.GeekPizza.Specs.Support
{
    public class WebApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ResponseMessage { get; set; }
    }
}
