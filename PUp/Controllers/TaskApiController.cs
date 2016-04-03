using Newtonsoft.Json;
using PUp.Models.Repository;
using PUp.Services;
using System.Net;
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
        public JsonResult<string> Get(int id)
        {
            Init();
            return Json(taskService.GetTask(id).ToJson());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns>Instance of TaskboardViewModel initialized with data </returns>
        [HttpGet]
        public JsonResult<string> Taskboard(int id)
        {
            Init();
            return Json(taskService.Taskboard(id).ToJson());
        }

        /// <summary>
        /// Get a view model for task Add
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns>AddTaskViewModel </returns>
        [HttpGet]
        public JsonResult<string> Add(int id)
        {
            Init();
            return Json(taskService.GetAddTaskViewModelByProject(id).ToJson());
        }

        /// <summary>
        /// Get a view model for task Add
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns>AddTaskViewModel </returns>
        [HttpPost]
        public JsonResult<string> Add(ViewModels.Task.AddTaskViewModel model)
        {
            Init();
            var checkModel = taskService.CheckModel(model);
            if (checkModel.IsValid())
            {
                taskService.Add(model);
                return Json(checkModel.ToJson());
            }
            return Json(checkModel.ToJson());

        }

        /// <summary>
        /// Change state of a task: mark DONE if not | UNDONE if done
        /// </summary>
        /// <param name="id">task id</param>
        /// <returns>ValidationMessage</returns>
        [HttpGet]
        public JsonResult<string> ChangeTaskState(int id) 
        {
            Init();
            return Json(taskService.ChangeTaskState(id).ToJson());
        }

    }
}