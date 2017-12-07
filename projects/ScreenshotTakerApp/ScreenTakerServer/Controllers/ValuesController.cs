using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ScreenTakerServer.Controllers
{
    using System.Drawing;
    using System.IO;

    public class ValuesController : ApiController
    {
        public IHttpActionResult Get()
        {
            return this.Ok(DateTime.Now);
        }

        [HttpPost]
        public IHttpActionResult Post()
        {
            var base64String = Request.Content.ReadAsStringAsync().Result;
            Image img = Image.FromStream(new MemoryStream(Convert.FromBase64String(base64String)));
            string name = DateTime.Now.Ticks.ToString();
            string path = $@"C:\temp\images\{name}.jpeg";
            img.Save(path);
            return this.Ok(path);
        }
    }
}
