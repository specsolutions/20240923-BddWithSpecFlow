using System;
using System.Net;
using System.Net.Http;
using System.Text;
using BddWithReqnroll.GeekPizza.Web;
using BddWithReqnroll.GeekPizza.Web.Utils;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace BddWithReqnroll.GeekPizza.Specs.Support
{
    public class WebApiContext
    {
        public WebApplicationFactory<Startup> WebApplicationFactory;
        public HttpClient HttpClient;

        public TData ExecuteGet<TData>(string endpoint)
        {
            // execute request
            // (we need to use the same HttpClient otherwise the auth token cookie gets lost)
            var response = HttpClient.GetAsync(endpoint).Result;

            SanityCheck(response);

            // deserialize response data
            var content = response.Content.ReadAsStringAsync().Result;
            var data = JsonConvert.DeserializeObject<TData>(content);

            return data;
        }

        public HttpStatusCode ExecutePost(string endpoint, object data)
        {
            // execute request
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = HttpClient.PostAsync(endpoint, content).Result;

            // for post requests the 2xx, 3xx and 4xx status codes are all "valid" results
            SanityCheck(response, 500);

            return response.StatusCode;
        }

        private void SanityCheck(HttpResponseMessage response, int upperRange = 300)
        {
            if ((int)response.StatusCode < 200 || (int)response.StatusCode >= upperRange)
            {
                var responseMessage = GetResponseError(response);
                throw new HttpResponseException(response.StatusCode, responseMessage,
                    $"The Web API request should be completed with success, not with error '{responseMessage}'");
            }
        }

        private string GetResponseError(HttpResponseMessage response)
        {
            if (response == null)
                return null;
            return $"{response.StatusCode}: {response.ReasonPhrase}";
        }
    }
}
