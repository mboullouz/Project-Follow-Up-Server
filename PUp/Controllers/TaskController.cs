using PUp.Models;
using PUp.Models.Entity;
using PUp.Models.Repository;
using PUp.Models.SimpleObject;
using PUp.ViewModels;
using PUp.ViewModels.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PUp.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private RepositoryManager repo = new RepositoryManager(); 
        private UserEntity currentUser = null;

        
        public TaskController()
        {
            currentUser = repo.UserRepository.GetCurrentUser();
        }

        // GET: Task
        public ActionResult Index(int id)
        {
            //TODO remove this cause not necessary !
            ProjectEntity project = repo.ProjectRepository.FindById(id);
            TaskViewModel tVM = new TaskViewModel(project);
             
            tVM.ActiveTasks =  repo.DbContext.TaskSet.Include("Executor").Where(t => t.Deleted == false && t.Project.Id==id).ToList();
            tVM.DeletedTasks = repo.DbContext.TaskSet.Include("Executor").Where(t => t.Deleted == true && t.Project.Id == id).ToList();

            return View(tVM);
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
            var task = repo.TaskRepository.MarkDone(id);

            return RedirectToAction("Index", "Dashboard", new { id = task.Id });
        }
        //TODO REMOVE or MERGE this
        public ActionResult SetDone(int id)
        {
            var task = repo.TaskRepository.MarkDone(id);

            return RedirectToAction("Index", "Task", new { id = task.Project.Id });
        }

        /// <summary>
        /// Set an avelaible date for tasks in the current day!
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult SetDate(int id)
        {
            var task = repo.TaskRepository.FindById(id);
            var interval = repo.TaskRepository.AvelaibleHoursForUserAndDate(currentUser, DateTime.Parse("00:01"));
            bool added = false;
            foreach (var vK in interval.Interval)
            {
                string dateStartStr = vK.Key + ":00";
                var dateForTest = DateTime.Parse(dateStartStr);
                if (!vK.Value && interval.CheckForDateAndDuration(dateForTest, task.EstimatedTimeInMinutes / 60))
                {
                    task.StartAt = dateForTest;
                    repo.DbContext.SaveChanges();
                    added = true;
                }
            }
             if(added)
                this.Flash("Task added to the current day pile! time to work on it hard, Good luck", FlashLevel.Success);
             else
                {
                this.Flash("The task can't fit in the remaining time", FlashLevel.Warning);
            }
            return RedirectToAction("Index", "Dashboard", new { id = task.Id });
        }

        /// <summary>
        /// Get Method, this is useful just to go the the view that contains the form
        /// id (idProject) must be in sent in the POST
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Add(int id)
        {
            ProjectEntity project = repo.ProjectRepository.FindById(id);
            AddTaskViewModel addTaskVM = new AddTaskViewModel(project.Id, repo.UserRepository.GetAll());
            addTaskVM.Project = project;
            addTaskVM.AvelaibleDates = repo.TaskRepository.AvelaibleHoursForUserAndDate(currentUser, DateTime.Parse("00:01"));
            return View(addTaskVM);
        }

        public ActionResult Edit(int id)
        { 
            TaskEntity task = repo.TaskRepository.FindById(id);
            ProjectEntity project = repo.ProjectRepository.FindById(task.Project.Id);
            AddTaskViewModel addTaskVM = new AddTaskViewModel(task, repo.UserRepository.GetAll());
       
            addTaskVM.AvelaibleDates = repo.TaskRepository.AvelaibleHoursForUserAndDate(currentUser, DateTime.Parse("00:01"));
            addTaskVM.Project = project;

            return View("~/Views/Task/Add.cshtml", addTaskVM);
        }

        [HttpPost]
        public ActionResult Edit(AddTaskViewModel model)
        {
            //If startDate is set! must be handled by cheking the interval, else raise an error!
            ProjectEntity project = repo.ProjectRepository.FindById(model.IdProject);
            var executor = repo.UserRepository.FindById(model.ExecutorId);
            if (!ModelState.IsValid && !repo.ProjectRepository.IsActive(project) && executor==null)
            {
                this.Flash("Can't save the task, The form is not valid Or you are trying to edit an inactive project", FlashLevel.Warning);
                return Edit(model.Id);
            }
           
            TaskEntity task = repo.TaskRepository.FindById(model.Id);
            //TODO Move this elsewhere!
            task.Title = model.Title;
            task.Description = model.Description;
            task.Done = false;
            //task.Project = project;
            task.AddAt = DateTime.Now;
            task.EditAt = DateTime.Now;
            task.EstimatedTimeInMinutes = model.EstimatedTimeInMinutes;
            task.StartAt = model.StartAt;
            task.KeyFactor = model.KeyFactor;
            task.Critical = model.Important;
            task.Urgent = model.Urgent;
            task.Executor = executor;
            if (!project.Contributors.Contains(task.Executor)) {
                project.Contributors.Add(task.Executor);
            }
            executor.Tasks.Add(task);
            repo.NotificationRepository.Add(task.Executor, "Task <" + task.Title + "> Is updated", "~/Task/" + project.Id, LevelFlag.Info);
            repo.DbContext.SaveChanges();
            this.Flash("Saved successfully and assigned to: "+executor.Name, FlashLevel.Success);
            return RedirectToAction("Index", "Task", new { id = project.Id });
        }


        [HttpPost]
        public ActionResult Add(AddTaskViewModel model)
        {
            ProjectEntity project = repo.ProjectRepository.FindById(model.IdProject);
            if (!ModelState.IsValid && !repo.ProjectRepository.IsActive(project))
            {
                this.Flash("Can't save the task, The form is not valid Or you are trying to edit an inactive project", FlashLevel.Warning);
                return Add(project.Id);
            }
            var selectedUser = repo.UserRepository.FindById(model.ExecutorId);
            TaskEntity task = new TaskEntity
            {
                Title = model.Title,
                Description = model.Description,
                Done = false,
                Project = project,
                AddAt = DateTime.Now,
                EditAt = DateTime.Now,
                EstimatedTimeInMinutes = model.EstimatedTimeInMinutes,
                StartAt = model.StartAt,
                KeyFactor = model.KeyFactor,
                Deleted = false,
                Critical = model.Important,
                Urgent = model.Urgent,
                Executor = selectedUser         
            };
            repo.TaskRepository.Add(task);
            project.Tasks.Add(task);
            project.Contributors.Add(selectedUser);
            project.Contributors.Add(currentUser);
            selectedUser.Tasks.Add(task);

            repo.NotificationRepository.GenerateFor(task, new HashSet<UserEntity> { currentUser, task.Executor });
            return RedirectToAction("Index", "Task", new { id = project.Id });
        }

        public ActionResult Delete(int id)
        {
            var t = repo.TaskRepository.FindById(id);
            var projectId = t.Project.Id; //needed to redirect!
            repo.TaskRepository.MarkDeleted(t);
            this.Flash("Task is deleted! ", FlashLevel.Warning);
            return RedirectToAction("Index", "Task", new { id = projectId });
        }

        public ActionResult MarkUndone(int id)
        {
            var t = repo.TaskRepository.FindById(id);
            var projectId = t.Project.Id; //needed to redirect!
            if (repo.ProjectRepository.IsActive(projectId))
            {
                repo.TaskRepository.MarkUndone(t);
            }
            else
            {
                this.Flash("The project is no more active!", FlashLevel.Warning);
            }
            
            return RedirectToAction("Index", "Task", new { id = projectId });
        }

        public ActionResult Postpone(int id)
        {
            var task = repo.TaskRepository.FindById(id);
            if (repo.ProjectRepository.IsActive(task.Project))
            {
                task.StartAt = null;
                repo.DbContext.SaveChanges();
            }
            else
            {
                this.Flash("The project is no more active!", FlashLevel.Warning);
            }
            return RedirectToAction("Index", "Dashboard", new { id = task.Id });
        }
        public ActionResult GenerateFromIssue(int projectId,int id)
        {
            var issue = repo.IssueRepository.FindById(id);
            issue.Deleted = true;
            issue.DeleteAt = DateTime.Now;
            var project = repo.ProjectRepository.FindById(projectId);
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
                Executor = currentUser
            };
            repo.TaskRepository.Add(task);
            project.Tasks.Add(task);
            project.Contributors.Add(currentUser);
            project.Contributors.Add(currentUser);
            currentUser.Tasks.Add(task);
            repo.NotificationRepository.Add(task.Executor, "An issue is transformed to a new task <" + task.Title + ">", "~/Task/" + task.Id, LevelFlag.Info);

            return Edit(task.Id);
        }
    }
}
