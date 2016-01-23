using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PUp.ViewModels;
using PUp.Models.Repository;
using PUp.Models.Entity;
using PUp.Models;

namespace PUp.Controllers
{
    public class ProjectController : Controller
    {
        ITaskRepository taskRepository;
        IProjectRepository projectRepository;
        IContributionRepository contributionRepository;
        IUserRepository userRepository;
        INotificationRepository notifRepository;
        DatabaseContext dbContext = new DatabaseContext();
        public ProjectController()
        {
            taskRepository = new TaskRepository(dbContext);
            projectRepository = new ProjectRepository(dbContext);
            userRepository = new UserRepository(dbContext);
            contributionRepository = new ContributionRepository(dbContext);
            notifRepository = new NotificationRepository(dbContext);
            
            
        }

        [HttpGet]
        public ActionResult Add()
        {

            AddProjectViewModel projectModel = new AddProjectViewModel
            {
                EndAt = DateTime.Now.AddDays(7),
                StartAt = DateTime.Now.AddHours(1),
                Name = "Week 2: Exciting project!"

            };
            return View(projectModel);

        }

        [HttpPost]
        public ActionResult Add(AddProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ProjectEntity project = new ProjectEntity();
            project.Name = model.Name;
            project.StartAt = model.StartAt;
            project.EndAt = model.EndAt;
            ContributionEntity contribution = new ContributionEntity();
            contribution.EndAt = project.EndAt;
            contribution.Project = project;
            contribution.ProjectId = project.Id;
            contribution.User = userRepository.GetCurrentUser();
            contribution.UserId = userRepository.GetCurrentUser().Id;
            contribution.StartAt = project.StartAt;
            contribution.Role = "FirstContributor";

            projectRepository.Add(project);
            project.Contributions.Add(contribution);
            notifRepository.GenerateFor(project, userRepository.GetAll());
             

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Remove(int id)
        {   
            projectRepository.SoftRemove(id);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult HardRemove(int id)
        {
            projectRepository.Remove(id);
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Edit(int id)
        {
            
            ProjectEntity project = projectRepository.FindById(id);
            AddProjectViewModel projectModel = new AddProjectViewModel
            {
                Id = project.Id,
                EndAt = project.EndAt,
                StartAt = project.StartAt,
                Name = project.Name
            };
            return View("~/Views/Project/Add.cshtml", projectModel);
        }

        public ActionResult Details(int id) 
        {

            ProjectEntity project = projectRepository.FindById(id);
            AddProjectViewModel projectModel = new AddProjectViewModel
            {
                Id = project.Id,
                EndAt = project.EndAt,
                StartAt = project.StartAt,
                Name = project.Name
            };
            return View("~/Views/Project/Details.cshtml", projectModel);
        }

        [HttpPost]
        public ActionResult Edit(AddProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
   
            ProjectEntity project = projectRepository.FindById(model.Id);
            project.Name = model.Name;
            project.StartAt = model.StartAt;
            project.EndAt = model.EndAt;
            projectRepository.GetDbContext().SaveChanges();


            return RedirectToAction("Index", "Home");
        }
    }
}