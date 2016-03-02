using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using WindowsService1;
using System.Web.Http;
using System.Net.Http.Formatting;
using System.Threading;
using System.Web;
using Xunit;

namespace Tests2
{
    public class TimeTestController
    {
        //System.Diagnostics.Debugger.Launch();
        protected HttpServer Server;
        protected string Url = "http://www.fortest.test/";
        protected HttpResponseMessage Response;
        protected HttpRequestMessage Request;
        protected DateTime ServTime;
        protected DateTime ActualTime;
        protected WindowsService1.Result Result;
        protected double difference;
        protected string Content;

        protected void GivenTimeControllerTestContext()
        {
            Configuration conf = new Configuration(Url);

            Server = new HttpServer(conf.Config);
        }

        public void GivenRequest(HttpMethod method, string relativeUri)
        {
            this.Request = this.CreateHttpRequestMessage(method, relativeUri);
        }

        public void WhenPerformRequest()
        {
            var client = new HttpClient(Server);
            client.Timeout = TimeSpan.FromMilliseconds((double)int.MaxValue);
            this.Response = client.SendAsync(this.Request).Result;
        }

        public void ThenShouldReturnSuccessStatusCode()
        {
            this.Response.EnsureSuccessStatusCode();
        }

        public void ThenPersistResult()
        {
            Content = this.Response.Content.ReadAsStringAsync().Result;
        }

        protected void ParseJsonResponse()
        {
            this.Result = JsonConvert.DeserializeObject<WindowsService1.Result>(Content);
        }

        protected void ExtractServerTime()
        {
            this.ServTime = DateTime.Parse(this.Result.Time);
        }

        protected void GetActualTime()
        {
            this.ActualTime = DateTime.Now;
        }

        protected void ThenCompareResults()
        {
            difference = ActualTime.Subtract(ServTime).TotalSeconds;
        }

        protected void DifferenceIsLessThen15()
        {
            Assert.InRange(difference, -15, 15);
        }

        private HttpRequestMessage CreateHttpRequestMessage(HttpMethod method, string relativeUri)
        {
            var requestUrl = new Uri(Url + relativeUri);

            HttpContext.Current = new HttpContext(
                new HttpRequest(string.Empty, requestUrl.ToString(), string.Empty),
                new HttpResponse(new StringWriter()));

            var request = new HttpRequestMessage { RequestUri = requestUrl };
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Method = method;

            return request;
        }
    }
}
