using Newtonsoft.Json;
using PUp.Services;
using PUp.ViewModels.Issue;
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


        /// <summary>
        /// Get Issue for a giving project!
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<string> GetAll(int id)
        {
            Init();
            var list = JsonConvert.SerializeObject(issueService.GetByProject(id),
             Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return Json(list); 
        }


        /// <summary>
        /// Get model
        /// Useful to initialize the model with data in case of an Edit
        /// </summary>
        /// <param name="id">ProjectId : change this later to include Issue Id</param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<string> Add(int id)
        {
            Init();
            var model = new AddIssueViewModel(id);
            return Json(model.ToJson());
        }

        [HttpPost]
        public JsonResult<string> Add( AddIssueViewModel model)
        {
            Init();
            var checkModel = issueService.CheckModel(model);
            if (checkModel.IsValid())
            {
                issueService.Add(model);
                return Json(checkModel.ToJson());
            }
            return Json(checkModel.ToJson());
        }

        /// <summary>
        /// Change ths status of an issue: mark CLOSE|OPEN
        /// </summary>
        /// <param name="id">issue id</param>
        /// <returns>ValidationMessage</returns>
        [HttpGet]
        public JsonResult<string> OpenCloseIssue(int id)
        {
            Init();
            return Json(issueService.OpenCloseIssue(id).ToJson());
        }
    }
}