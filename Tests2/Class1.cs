using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using Xunit;
using Newtonsoft.Json;
using WindowsService1;

namespace Tests2
{
    public class TimeTest
    {
        [Fact]
        public void ShouldGetCorrectTime()
        {
            using (var wc = new WebClient())
            {

                string response = wc.DownloadString("http://localhost:7777");
                //System.Diagnostics.Debugger.Launch();
                Result res = JsonConvert.DeserializeObject<Result>(response);
                Debug.WriteLine(res.Time);
                DateTime dt = DateTime.Parse(res.Time);
                double servUnixTime = dt.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                double actualUnixTime = DateTime.Now.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
                Assert.InRange(servUnixTime, actualUnixTime - 15, actualUnixTime + 15);
            }
        }
    }
}
