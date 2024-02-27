using System;
using System.IO;
using System.Net.Http;
using System.Text;
using BddWithReqnroll.GeekPizza.Web.Utils;
using Newtonsoft.Json;

namespace BddWithReqnroll.GeekPizza.Specs.Support
{
    public class WebApiContext : IDisposable
    {
        private readonly AppHostingContext _appHostingContext;
        private readonly StringBuilder _log = new();

        private HttpClient _httpClient;

        public HttpClient HttpClient
        {
            get
            {
                if (_httpClient == null)
                    _httpClient = _appHostingContext.CreateClient();
                return _httpClient;
            }
        }

        public WebApiContext(AppHostingContext appHostingContext)
        {
            _appHostingContext = appHostingContext;
        }

        public void Dispose()
        {
            if (_httpClient != null)
            {
                _httpClient.Dispose();
                _httpClient = null;
            }
        }

        public TData ExecuteGet<TData>(string endpoint)
        {
            // execute request
            // (we need to use the same HttpClient otherwise the auth token cookie gets lost)
            var response = HttpClient.GetAsync(endpoint).Result;

            SanityCheck(response);

            // deserialize response data
            var content = ReadContent(response);
            LogResponse(response, content);

            return JsonConvert.DeserializeObject<TData>(content);
        }

        public WebApiResponse ExecutePost(string endpoint, object data)
        {
            return ExecuteSend(endpoint, data, HttpMethod.Post);
        }

        public WebApiResponse ExecutePut(string endpoint, object data)
        {
            return ExecuteSend(endpoint, data, HttpMethod.Put);
        }

        private WebApiResponse ExecuteSend(string endpoint, object data, HttpMethod httpMethod)
        {
            // execute request
            var content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
            var response = HttpClient.SendAsync(new HttpRequestMessage(httpMethod, endpoint)
            {
                Content = content
            }).Result;
            LogResponse(response);

            // for post requests the 2xx, 3xx and 4xx status codes are all "valid" results
            SanityCheck(response, 500);

            Console.WriteLine(GetResponseMessage(response));

            return new WebApiResponse
            {
                StatusCode = response.StatusCode,
                ResponseMessage = GetResponseMessage(response)
            };
        }

        private string ReadContent(HttpResponseMessage response)
        {
            try
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            catch
            {
                return null;
            }
        }

        private void SanityCheck(HttpResponseMessage response, int upperRange = 300)
        {
            if ((int)response.StatusCode < 200 || (int)response.StatusCode >= upperRange)
            {
                var responseMessage = GetResponseMessage(response);
                throw new HttpResponseException(response.StatusCode, responseMessage,
                    $"The Web API request should be completed with success, not with error '{responseMessage}'");
            }
        }

        private string GetResponseMessage(HttpResponseMessage response)
        {
            if (response == null)
                return null;

            var content = ReadContent(response);
            return $"{response.StatusCode}: {content ?? response.ReasonPhrase}";
        }

        private void LogResponse(HttpResponseMessage response, string content = null)
        {
            _log.AppendLine(response.RequestMessage?.RequestUri?.ToString() ?? "Unknown request URI");
            _log.AppendLine($"{response.StatusCode}: {response.ReasonPhrase}");
            content ??= ReadContent(response);
            if (content != null)
                _log.AppendLine(content);
            _log.AppendLine();
        }

        public void SaveLog(string outputFolder, string fileName)
        {
            var logFilePath = Path.Combine(outputFolder, fileName);
            Console.WriteLine($"Saving log to {logFilePath}");
            File.WriteAllText(logFilePath, _log.ToString());
        }
    }
}
