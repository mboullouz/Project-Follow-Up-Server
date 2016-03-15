using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Results;

namespace PUp.Controllers
{
    [EnableCors(origins: "http://localhost:3000,*", headers: "*", methods: "*")]
    public class AccountApiController : ApiController
    {
        [HttpGet]
        public NegotiatedContentResult<string> Check()
        {
            var jsonContent = JsonConvert.SerializeObject( new {state=1 },
             Formatting.None,
             new JsonSerializerSettings()
             {
                 ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                 MaxDepth = 1,
             });
            return Content(HttpStatusCode.OK, jsonContent);
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}