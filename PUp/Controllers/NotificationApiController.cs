using Newtonsoft.Json;
using PUp.Models;
using PUp.Models.Repository;
using PUp.Models.Dto;
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
    [Authorize]
    public class NotificationApiController : ApiController
    {
        private DatabaseContext dbContext = new DatabaseContext();
        private NotificationRepository notifRepo;
        private UserRepository userRepo;
        private Models.Entity.UserEntity currentUser = null;


        public NotificationApiController()
        {
            notifRepo = new NotificationRepository(dbContext);
            userRepo = new UserRepository(dbContext);
        }


        [HttpGet]
        public NegotiatedContentResult<string> All()
        {
            var email = RequestContext.Principal.Identity.Name;
            currentUser = userRepo.FindByEmail(email);
            List<NotificationDto> notifs = new List<NotificationDto>();
            notifRepo.GetByUser(currentUser)
                .OrderByDescending(n => n.AddAt)
                .Take(10).ToList()
                .ForEach(n => notifs.Add(new NotificationDto(n)));
            var list = JsonConvert.SerializeObject(notifs,
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
            var res = notifRepo.RemoveById(id);
            return Content(HttpStatusCode.OK, res ? "Deleted" : "Nothing to delete!");
        }
    }
}
