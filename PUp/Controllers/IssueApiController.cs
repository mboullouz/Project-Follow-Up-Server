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
    public class IssueApiController : ApiController
    {
        private IssueService issueService;

        public void Init()
        {
            var email = RequestContext.Principal.Identity.Name;
            issueService = new IssueService(email, new Models.ModelStateWrapper(new Models.ValidationMessageHolder(), ModelState));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Issue id</param>
        /// <returns>Get an Issue by id </returns>
        [HttpGet]
        public JsonResult<string> Get(int id)
        {
            Init();
            return Json(issueService.GetIssue(id).ToJson());
        }
    }
}