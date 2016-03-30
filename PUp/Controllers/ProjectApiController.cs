using Newtonsoft.Json;
using PUp.Models.Entity;
using PUp.Services;
using PUp.ViewModels.Project;
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
            projectService = new ProjectService(email,ModelState);

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

        [HttpPost]
        public JsonResult<string> Add(AddProjectViewModel model)
        {
            Init();
            var checkModel = projectService.CheckModel(model);
             
            if (!checkModel.IsValid())  
            {
                return Json(checkModel.ToJson());
            }
            var project = projectService.GetInitializedProjectFromModel(model);
            projectService.GetRepositoryManager().ProjectRepository.Add(project);
            projectService.GetRepositoryManager().NotificationRepository.GenerateFor(project, new HashSet<UserEntity>(projectService.GetRepositoryManager().UserRepository.GetAll()));
            return Json(checkModel.ToJson());
        }


        /// <summary>
        /// PUT is not used because the need to create a different param => different Json req
        /// I find it more simple to just test if id >0 and find the appropriate Entity to update!
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult<string> Edit(AddProjectViewModel model)
        {
            Init();           
            var checkModel = projectService.CheckModel(model,true);

            if (!checkModel.IsValid())
            {
                return Json(checkModel.ToJson());
            }
            ProjectEntity project = projectService.GetRepositoryManager().ProjectRepository.FindById(model.Id);
            project.Name = model.Name;
            project.StartAt = model.StartAt;
            project.EndAt = model.EndAt;
            project.Objective = model.Objective;
            project.Benifite = model.Benifite;
            projectService.GetRepositoryManager().ProjectRepository.DbContext.SaveChanges();

            foreach (var u in project.Contributors)
            {
                projectService.GetRepositoryManager().NotificationRepository.Add(u, "Project: " + project.Name + " updated", "~/Project/Timeline" + project.Id, Models.LevelFlag.Info);
            }
            return Json(checkModel.ToJson());
        }


        /// <summary>
        /// Get the ProjectView for Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public JsonResult<string> Edit(int id)
        {
            Init();
            return Json(projectService.GetInitializedViewByProjectId(id).ToJson());
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
