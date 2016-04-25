﻿using Newtonsoft.Json;
using PUp.Models.Repository;
using PUp.Services;
using PUp.Models;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;

namespace PUp.Controllers
{
    [App_Start.MyBasicAuth]
    [Authorize]
    public class TaskApiController : ApiController
    {
        private TaskService  taskService;


        public void Init()
        {
            var email = RequestContext.Principal.Identity.Name;
            taskService = new TaskService(email, new Models.ModelStateWrapper(new Models.ValidationMessageHolder(), ModelState));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Task id</param>
        /// <returns>Get a task by id </returns>
        [HttpGet]
        public HttpResponseMessage Get(int id)
        {
            Init();
            return this.CreateJsonResponse(taskService.GetTask(id).ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns>Instance of TaskboardViewModel initialized with data </returns>
        [HttpGet]
        public HttpResponseMessage Taskboard(int id)
        {
            Init();
            return this.CreateJsonResponse(taskService.Taskboard(id).ToJson());
        }

        [HttpGet]
        public HttpResponseMessage Postpone(int id)
        {
            Init();
            return this.CreateJsonResponse(taskService.Postpone(id).ToJson());
        }

        /// <summary>
        /// Get a view model for task Add
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns>AddTaskViewModel </returns>
        [HttpGet]
        public HttpResponseMessage Add(int id)
        {
            Init();
            return this.CreateJsonResponse(taskService.GetAddTaskViewModelByProject(id).ToJson());
        }

        /// <summary>
        /// Get a view model for task Add
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns>AddTaskViewModel </returns>
        [HttpPost]
        public HttpResponseMessage Add(ViewModels.Task.AddTaskViewModel model)
        {
            Init();
            var modelStateWrapper = taskService.CheckModel(model);
            if (modelStateWrapper.IsValid())
            {
                taskService.Add(model);
                return this.CreateJsonResponse(modelStateWrapper.ToJson());
            }
            return this.CreateJsonResponse(modelStateWrapper.ToJson());
        }

        /// <summary>
        /// Change state of a task: mark DONE if not | UNDONE if done
        /// </summary>
        /// <param name="id">task id</param>
        /// <returns>ValidationMessage</returns>
        [HttpGet]
        public HttpResponseMessage ChangeTaskState(int id) 
        {
            Init();
            return this.CreateJsonResponse(taskService.ChangeTaskState(id).ToJson());
        }

        [HttpGet]
        public HttpResponseMessage PlanTaskForCurrentDay(int id)
        {
            Init();
            return this.CreateJsonResponse(taskService.PlanTaskForCurrentDay(id).ToJson());
        }

    }
}