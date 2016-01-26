using PUp.Models;
using PUp.Models.Entity;
using PUp.Models.Repository;
using PUp.Models.SimpleObject;
using PUp.ViewModels;
using System;
using System.Web.Mvc;

namespace PUp.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {

        private TaskRepository taskRepository;
        private ProjectRepository projectRepository;
        private UserRepository userRepository;
        private ContributionRepository contributionRepository;
        private NotificationRepository notificationRepository;
        private DatabaseContext dbContext = new DatabaseContext();

        //TODO Use a container to inject dependencies 
        public TaskController()
        {
            taskRepository = new TaskRepository(dbContext);
            projectRepository = new ProjectRepository(dbContext);
            notificationRepository = new NotificationRepository(dbContext);
            userRepository = new UserRepository(dbContext);
            contributionRepository = new ContributionRepository(dbContext);
           
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


        /// <summary>
        /// Get Method, this is useful just to go the the view that contains the form
        /// id (idProject) must be in sent in the POST
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Add(int id)
        {
            ProjectEntity project = projectRepository.FindById(id);
            AddTaskViewModel addTaskVM = new AddTaskViewModel { Project = project, IdProject = project.Id };
            return View(addTaskVM);
        }

        [HttpPost]
        public ActionResult Add(AddTaskViewModel model)
        {
            //This is needed for Unit test  so we can set the correct context!
            var userName = this.ControllerContext.HttpContext.User.Identity.Name;
            var user = userRepository.FindByEmail(userName);
            ProjectEntity project = projectRepository.FindById(model.IdProject);
            if (!ModelState.IsValid)
            {
                model.Project = project;
                return View(model);
            }
            TaskEntity task = new TaskEntity
            {
                Title = model.Title,
                Description = model.Description,
                Priority = model.Priority,
                Done = model.Done,
                Project = project,
                CreateAt = DateTime.Now,
                EditAt = DateTime.Now,
                estimatedTimeInMinutes = model.estimatedTimeInMinutes,
                keyFactor = model.keyFactor,
                Deleted = false,
            };
            contributionRepository.AddContributionIfNotExists(project, user, task);                      
            taskRepository.Add(task);            
            project.Tasks.Add(task);
            notificationRepository.GenerateFor(task, userRepository.GetAll());
            return RedirectToAction("Index", "Task", new { id = project.Id });
        }
    }
}