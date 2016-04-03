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
    public class TimelineApiController : ApiController
    {
        private TimelineService timelineService;
        public void Init()
        {
            var email = RequestContext.Principal.Identity.Name;
            timelineService = new  TimelineService(email,new Models.ModelStateWrapper(new Models.ValidationMessageHolder(), ModelState));

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns>Get a timeline by project </returns>
        [HttpGet]
        public JsonResult<string> ByProject(int id)
        {
            Init();
            return Json(timelineService.GetByProject(id).ToJson());
        }

    }
}