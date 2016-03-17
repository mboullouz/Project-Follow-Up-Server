using PUp.Models.Entity;
using PUp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PUp.Controllers
{
    public class ProjectApiController : ApiController
    {
        private ProjectService projectService;
        private UserEntity currentUser = null;

        public void Init()
        {
            var email = RequestContext.Principal.Identity.Name;
             
            projectService = new ProjectService(email);
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