using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace WindowsService1
{
    public class TimeController : ApiController
    {
        private IItem item;

        public TimeController(IItem item)
        {
            this.item = item;
        }

        [HttpGet]
        public Result Get()
        {
            Result res = new Result();
            res.Time = item.GetTime();
            res.Path = item.GetPath();
            return res;
        }
    }

    public class Result
    {
        public string Time { get; set; }
        public string Path { get; set; }
    }
}
