using Newtonsoft.Json;
using PUp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace PUp.Controllers
{
    [App_Start.MyBasicAuth]
    [Authorize]
    public class ProjectApiController : ApiController
    {
        private ProjectService projectService;
        

        public void Init()
        {
            var email = RequestContext.Principal.Identity.Name;
            projectService = new ProjectService(email);
             
        }

        [HttpGet]
        public JsonResult<string> List()
        {
            Init();
            var jsonContent = JsonConvert.SerializeObject(projectService.GetTableProjectForCurrentUser(),
            Formatting.None,
            new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                MaxDepth = 1,
                
            });

            return Json(jsonContent);
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
