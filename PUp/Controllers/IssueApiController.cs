using Newtonsoft.Json;
using PUp.Models;
using PUp.Services;
using PUp.ViewModels.Issue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
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
        public HttpResponseMessage Get(int id)
        {
            Init();
            return this.CreateJsonResponse(issueService.GetIssue(id).ToJson());
        }


        /// <summary>
        /// Get Issue for a giving project!
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage GetAll(int id)
        {
            Init();
            var list = JsonConvert.SerializeObject(issueService.GetByProject(id),
            Formatting.None, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore });
            return this.CreateJsonResponse(list);
        }


        /// <summary>
        /// Get model
        /// Useful to initialize the model with data in case of an Edit
        /// </summary>
        /// <param name="id">ProjectId : change this later to include Issue Id</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Add(int id)
        {
            Init();
            var model = new AddIssueViewModel(id);
            return this.CreateJsonResponse(model.ToJson());
        }

        [HttpPost]
        public HttpResponseMessage Add( AddIssueViewModel model)
        {
            Init();
            var checkModel = issueService.CheckModel(model);
            if (checkModel.IsValid())
            {
                issueService.Add(model);
                return this.CreateJsonResponse(checkModel.ToJson());
            }
            return this.CreateJsonResponse(checkModel.ToJson());
        }

        /// <summary>
        /// Change ths status of an issue: mark CLOSE|OPEN
        /// </summary>
        /// <param name="id">issue id</param>
        /// <returns>ValidationMessage</returns>
        [HttpGet]
        public HttpResponseMessage OpenCloseIssue(int id)
        {
            Init();
            return this.CreateJsonResponse(issueService.OpenCloseIssue(id).ToJson());
        }
    }
}