using PUp.Models;
using PUp.Models.Entity;
using PUp.Models.Repository;
using PUp.Models.SimpleObject;
using PUp.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace PUp.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {

        private TaskRepository taskRepository;
        private ProjectRepository projectRepository;
        private UserRepository userRepository;
        private NotificationRepository notificationRepository;
        private DatabaseContext dbContext = new DatabaseContext();
        // string userName = null;
        UserEntity user = null;

        //TODO Use a container to inject dependencies 
        public TaskController()
        {
            taskRepository = new TaskRepository(dbContext);
            projectRepository = new ProjectRepository(dbContext);
            notificationRepository = new NotificationRepository(dbContext);
            userRepository = new UserRepository(dbContext);
           // userName = this.ControllerContext.HttpContext.User.Identity.Name;
           user = userRepository.GetCurrentUser();

        }

        // GET: Task
        public ActionResult Index(int id)
        {   
            //TODO return a list of Tasks !
            ProjectEntity project = projectRepository.FindById(id);
            return View(project);
        }

        [HttpPost]
        public ActionResult ChangeState(TaskBasic taskBasic)
        {
            //TaskEntity taskEntity = taskRepository.FindById(taskBasic.Id); 
            taskRepository.ChangeTaskState(taskBasic.Id, taskBasic.Done);
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
            var task = taskRepository.FindById(id);
            task.Done = true;
            task.FinishAt = DateTime.Now;
            dbContext.SaveChanges();
            return RedirectToAction("Index", "Dashboard", new { id = task.Id });
        }


        public ActionResult SetDate(int id)
        {
            var task=taskRepository.FindById(id);
            var interval = taskRepository.AvelaibleHoursForUserAndDate(user, DateTime.Parse("00:01"));
            foreach(var vK in interval.Interval)
            {
                string dateStartStr = vK.Key + ":00";
                var dateForTest = DateTime.Parse(dateStartStr);
                if (!vK.Value && interval.CheckForDateAndDuration(dateForTest, task.EstimatedTimeInMinutes / 60)){
                    task.StartAt = dateForTest;
                    dbContext.SaveChanges();
                }
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
            ProjectEntity project = projectRepository.FindById(id);
            AddTaskViewModel addTaskVM = new AddTaskViewModel(project.Id, userRepository.GetAll());
            addTaskVM.Project = project;
            addTaskVM.AvelaibleDates = taskRepository.AvelaibleHoursForUserAndDate(user, DateTime.Parse("00:01"));
            return View(addTaskVM);
        }



        [HttpPost]
        public ActionResult Add(AddTaskViewModel model)
        {
            //This is needed for Unit test  so we can set the correct context!
            

            ProjectEntity project = projectRepository.FindById(model.IdProject);
            if (!ModelState.IsValid)
            {
                return Add(project.Id);
            }
            var executor = userRepository.FindById(model.ExecutorId);
            TaskEntity task = new TaskEntity
            {
                Title = model.Title,
                Description = model.Description,
                Done = false,
                Project = project,
                AddAt = DateTime.Now,
                EditAt = DateTime.Now,
                EstimatedTimeInMinutes= model.EstimatedTimeInMinutes,
                StartAt= model.StartAt,
                 

                KeyFactor = model.KeyFactor,
                Deleted = false,
                Critical= model.Important,
                Urgent= model.Urgent,
                Executor= executor!=null?executor:user,//if not found!
                
            };
            taskRepository.Add(task);
            project.Tasks.Add(task);
            project.Contributors.Add(user);
            
            
           
            notificationRepository.GenerateFor(task, new HashSet<UserEntity> { user, task.Executor });
            return RedirectToAction("Index", "Task", new { id = project.Id });
        }
    }
}