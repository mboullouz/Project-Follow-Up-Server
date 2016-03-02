using PUp.Models;
using PUp.Models.Entity;
using PUp.Models.SimpleObject;
using PUp.Services;
using PUp.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PUp.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private RepositoryManager repo = new RepositoryManager(); 
        private UserEntity currentUser = null;
        private TaskService taskService;
        
        public TaskController()
        {
            currentUser = repo.UserRepository.GetCurrentUser();
            taskService = new TaskService(new ModelStateWrapper(TempData, ModelState));
        }

        // GET: Task
        public ActionResult Index(int id)
        {
            return View(taskService.GetTaskViewModelByProject(id));
        }

        [HttpPost]
        public ActionResult ChangeState(TaskBasic taskBasic)
        {
            repo.TaskRepository.ChangeTaskState(taskBasic.Id, taskBasic.Done);
            //Generate a notification
            GenericJsonResponse res = new GenericJsonResponse
            {
                Success = true,
                State = "OK",
                Message = "Task updated succefully",
                IdEntity = taskBasic.Id
            };
            return Json(res);
        }

        public ActionResult MarkDone(int id)
        {
            taskService.MarkDone(id);
            return RedirectToAction("Index", "Dashboard", new { id = id });
        }
        //This is the same as MarkDone just the view rendered is different!
        public ActionResult SetDone(int id)
        {
            var task = taskService.MarkDone(id);
            return RedirectToAction("Index", "Task", new { id = task.Project.Id });
        }

        /// <summary>
        /// Set an avelaible date for tasks in the current day!
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SetDate(int id)
        {
            taskService.SetDateForTask(id);
            return RedirectToAction("Index", "Dashboard", new { id = id });
        }

        /// <summary>
        /// Get Method, this is useful just to go the the view that contains the form
        /// id (idProject) must be in sent in the POST
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Add(int id)
        {
            return View(taskService.GetAddTaskViewModelByProject(id));
        }

        public ActionResult Edit(int id)
        {   
            return View("~/Views/Task/Add.cshtml", taskService.GetAddTaskViewModelByTask(id));
        }


        /// <summary>
        /// Save data comming from the form
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Edit(AddTaskViewModel model)
        {
            //If startDate is set! must be handled by cheking the interval, else raise an error!
            ProjectEntity project = taskService.GetRepositoryManager().ProjectRepository.FindById(model.IdProject);
            var executor = taskService.GetRepositoryManager().UserRepository.FindById(model.ExecutorId);
            if (!ModelState.IsValid && !taskService.GetRepositoryManager().ProjectRepository.IsActive(project) && executor==null)
            {
                this.Flash("Can't save the task, The form is not valid Or you are trying to edit an inactive project", FlashLevel.Warning);
                return Edit(model.Id);
            }

            var task = taskService.GetInitializedTaskFromModel(model);
            if (!project.Contributors.Contains(task.Executor)) {
                project.Contributors.Add(task.Executor);
            }
            task.Executor.Tasks.Add(task);
            taskService.GetRepositoryManager().NotificationRepository.Add(task.Executor, "Task <" + task.Title + "> Is updated", "~/Task/" + project.Id, LevelFlag.Info);
            taskService.GetRepositoryManager().DbContext.SaveChanges();
            this.Flash("Saved successfully and assigned to: "+executor.Name, FlashLevel.Success);
            return RedirectToAction("Index", "Task", new { id = project.Id });
        }


        [HttpPost]
        public ActionResult Add(AddTaskViewModel model)
        {
            if (!taskService.Add(model))
            {
                return Add(model.IdProject);
            }
            return RedirectToAction("Index", "Task", new { id = model.IdProject });
        }

        public ActionResult Delete(int id)
        {
            return RedirectToAction("Index", "Task", new { id = taskService.Delete(id).Project.Id });
        }

        public ActionResult MarkUndone(int id)
        {
            return RedirectToAction("Index", "Task", new { id = taskService.MarkUndone(id).Project.Id });
        }

        public ActionResult Postpone(int id)
        {
            taskService.Postpone(id);
            return RedirectToAction("Index", "Dashboard", new { id = id });
        }
        public ActionResult GenerateFromIssue(int projectId,int id)
        {
            var issue = taskService.GetRepositoryManager().IssueRepository.FindById(id);//temp to not reference entity by different DBContext!
            issue.Deleted = true;
            issue.DeleteAt = DateTime.Now;
            var project = taskService.GetRepositoryManager().ProjectRepository.FindById(projectId);
            TaskEntity task = new TaskEntity
            {
                Title = "Task from unresolved issue",
                Description = issue.Description,
                Done = false,
                Project = project,
                AddAt = DateTime.Now,
                EditAt = DateTime.Now,
                EstimatedTimeInMinutes = 120,
                StartAt = null,
                KeyFactor = false,
                Deleted = false,
                Critical = true,
                Urgent = true,
                Executor = taskService.GetRepositoryManager().UserRepository.GetCurrentUser()
            };
            taskService.SaveNewTask(task);
            return Edit(task.Id);
        }
    }
}
