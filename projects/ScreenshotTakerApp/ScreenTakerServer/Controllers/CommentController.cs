using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ScreenTakerServer.Controllers
{
    using System.IO;

    using ScreenTakerServer.Models;

    public class CommentController : ApiController
    {
        private string filePath;

        public CommentController()
        {
            string today = DateTime.Now.Date.ToString("yyyy MMMM dd");
            this.filePath = $@"{AppConstants.Folder}\{today}.txt";
            bool exists = File.Exists(this.filePath);
            if (!exists)
            {
                File.Create(this.filePath);
            }
        }

        [HttpPost]
        public IHttpActionResult Post()
        {
            string data = this.Request.Content.ReadAsStringAsync().Result;
            var writer = new StreamWriter(this.filePath, true);
            writer.WriteLine($"{DateTime.Now} : \t {data}");
            writer.Close();            
            return this.Ok();
        }
    }
}
