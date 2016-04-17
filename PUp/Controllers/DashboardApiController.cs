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
    public class DashboardApiController : ApiController
    {
        DashboardService dashboardService;

        public void Init()
        {
            var email = RequestContext.Principal.Identity.Name;
            dashboardService = new DashboardService(email, new Models.ModelStateWrapper(new Models.ValidationMessageHolder(), ModelState));
        }
        [HttpGet]
        public JsonResult<string> Index()
        {
            Init();

            return Json("OK");
        }
 
    }
}