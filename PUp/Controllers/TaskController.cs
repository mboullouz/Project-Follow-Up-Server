using PUp.Models.Entity;
using PUp.Models.Repository;
using PUp.Models.SimpleObject;
using PUp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUp.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {

        private ITaskRepository taskRepository;
        private IProjectRepository projectRepository;
        private INotificationRepository notificationRepository;
        private IUserRepository userRepository;
        private IContributionRepository contributionRepository;

        //TODO Use a container to inject dependencies 
        public TaskController()
        {
            taskRepository = new TaskRepository();
            projectRepository = new ProjectRepository();
            notificationRepository = new NotificationRepository();
            userRepository = new UserRepository();
            contributionRepository = new ContributionRepository();
            userRepository.SetDbContext(taskRepository.GetDbContext());
            projectRepository.SetDbContext(taskRepository.GetDbContext());
            notificationRepository.SetDbContext(taskRepository.GetDbContext());
            contributionRepository.SetDbContext(taskRepository.GetDbContext());
        }

        // GET: Task
        public ActionResult Index(int id)
        {
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

        public ActionResult Add(int id)
        {
            ProjectEntity project = projectRepository.FindById(id);
            AddTaskViewModel addTaskVM = new AddTaskViewModel { Project = project, IdProject = project.Id };
            return View(addTaskVM);
        }

        [HttpPost]
        public ActionResult Add(AddTaskViewModel model)
        {
            var user = userRepository.GetCurrentUser();
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
                EditionNumber = 1,
            };
            if (!contributionRepository.ContributionExists(project, user))
            {
                var contrib = new ContributionEntity
                {
                    StartAt = DateTime.Now,
                    EndAt = task.Project.EndAt,
                    ProjectId = project.Id,
                    UserId = user.Id,
                    Role = "Add-Task"
                };
                contributionRepository.Add(contrib);
            }
           
            
            taskRepository.Add(task);
            
            project.Tasks.Add(task);
            taskRepository.GetDbContext().SaveChanges();
            NotificationEntity notification = new NotificationEntity
            {
                User = user,
                CreateAt = DateTime.Now,
                Message = "New task added! ",
                Url = "~/Task/Add/" + task.Id
            };
            notificationRepository.Add(notification);
            return RedirectToAction("Index", "Task", new { id = project.Id });
        }
    }
}