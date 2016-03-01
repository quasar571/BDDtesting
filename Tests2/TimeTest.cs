using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;
using Newtonsoft.Json;
using WindowsService1;
using System.Web;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Net.Http.Formatting;
using System.Security.Policy;
using System.Threading;
using TestStack.BDDfy;

namespace Tests2
{
    public class TimeTest
    {
        private readonly HttpServer _server;
        private string _url = "http://www.fortest.test/";
        private DateTime ServTime;
        private DateTime ActualTime;

        public TimeTest()
        {
            Configuration conf = new Configuration(_url);

            _server = new HttpServer(conf.Config);
        }

        [Fact]
        public void ShouldGetCorrectTime()
        {
            var client = new HttpClient(_server);

            using (var wc = new WebClient())
            {
                
                //System.Diagnostics.Debugger.Launch();

                var request = createRequest("", "application/json", HttpMethod.Get);

                using (HttpResponseMessage response = client.SendAsync(request, new CancellationTokenSource().Token).Result)
                {
                    WindowsService1.Result res = JsonConvert.DeserializeObject<WindowsService1.Result>(response.Content.ReadAsStringAsync().Result);


                    this.Given(s => s.GivenTheTimeFromServer(res.Time))
                        .When(s => s.WhenRealTimeIs(DateTime.Now))
                        .Then(s => s.ServerGiveActualTime())
                        .BDDfy();
                }
            }
        }

        private void GivenTheTimeFromServer(string time)
        {
            ServTime = DateTime.Parse(time);
        }

        private void WhenRealTimeIs(DateTime acTime)
        {
            ActualTime = acTime;
        }

        private void ServerGiveActualTime()
        {
            double servUnixTime = ServTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            double actualUnixTime = ActualTime.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            Assert.InRange(servUnixTime, actualUnixTime - 15, actualUnixTime + 15);
        }

        private HttpRequestMessage createRequest(string url, string mthv, HttpMethod method)
        {
            var request = new HttpRequestMessage();

            request.RequestUri = new Uri(_url + url);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(mthv));
            request.Method = method;

            return request;
        }

        private HttpRequestMessage createRequest<T>(string url, string mthv, HttpMethod method, T content, MediaTypeFormatter formatter) where T : class
        {
            HttpRequestMessage request = createRequest(url, mthv, method);
            request.Content = new ObjectContent<T>(content, formatter);

            return request;
        }
    }
}
