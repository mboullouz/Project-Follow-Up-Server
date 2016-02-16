using PUp.Models;
using PUp.Models.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace PUp.Controllers
{
    public class NotificationApiController : ApiController
    {
        //private DatabaseContext dbContext = new DatabaseContext();
        NotificationRepository notifRepo = new NotificationRepository();

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
        public NegotiatedContentResult<string> Delete(int id)
        {
            var res= notifRepo.RemoveById(id);
            return Content(HttpStatusCode.OK, res? "Deleted":"Nothing to delete!");
        }
    }
}
