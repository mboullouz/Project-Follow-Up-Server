using Newtonsoft.Json;
using PUp.Models;
using PUp.Models.Repository;
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
    [App_Start.MyBasicAuth]
    [EnableCors(origins: "http://localhost:3000,*", headers: "*", methods: "*")]
    public class NotificationApiController : ApiController
    {
        private DatabaseContext dbContext = new DatabaseContext();
        private NotificationRepository notifRepo;
        private UserRepository userRepo;
        private Models.Entity.UserEntity currentUser=null;


        public NotificationApiController()
        {
             notifRepo = new NotificationRepository(dbContext);
             userRepo = new UserRepository(dbContext);
        }

        // GET api/<controller>
        [Authorize]
        public NegotiatedContentResult<string> Get()
        {
            var email = RequestContext.Principal.Identity.Name;
            currentUser = userRepo.FindByEmail(email);
            var list = JsonConvert.SerializeObject(notifRepo.GetByUser(currentUser),
              Formatting.None,
              new JsonSerializerSettings()
              {
                  ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                  MaxDepth = 1,
              });
            return Content(HttpStatusCode.OK, list);
        }

        

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value \n you called GET api/NotificationApi/Get(int id)";
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
