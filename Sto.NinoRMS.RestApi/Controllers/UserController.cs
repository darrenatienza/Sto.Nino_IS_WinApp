using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Hosting;
using System.Web.Http;

namespace WebApi.Jwt.Controllers
{
    public class UserController : ApiController
    {
        // GET api/user
        [HttpGet]
        [AllowAnonymous]
        [Route("api/user/image/get")]
        public HttpResponseMessage Get()
        {
            var response = Request.CreateResponse(HttpStatusCode.OK);
            var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), "Sto.NinoIS") +@"\1.jpg";
            //path = HostingEnvironment.MapPath(path);
            var ext = Path.GetExtension(path);
            var contents = File.ReadAllBytes(path);
            var ms = new MemoryStream(contents);
            response.Content = new StreamContent(ms);
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/" + ext);
            return response;
        }

        

        // GET api/user/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/user
        public void Post([FromBody]string value)
        {
        }

        // PUT api/user/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/user/5
        public void Delete(int id)
        {
        }
    }
}
