using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using PUp.Models;
using PUp.Models.Entity;
using PUp.Models.SimpleObject;
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
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class AccountApiController : ApiController
    {
        [HttpPost]
        public NegotiatedContentResult<string> Check(AuthObject model)
        {

            var UserManager = new UserManager<UserEntity>(new UserStore<UserEntity>(new DatabaseContext()));
            var user = UserManager.Find(model.Username, model.Password);

            var jsonContent = JsonConvert.SerializeObject( model,
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