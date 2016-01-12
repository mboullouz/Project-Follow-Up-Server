using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PUp.ViewModels;
using PUp.Models.Repository;
using PUp.Models.Entity;

namespace PUp.Controllers
{
    public class ProjectController : Controller
    {
        ITaskRepository taskRepository;
        IProjectRepository projectRepository;
        IContributionRepository contributionRepository;
        IUserRepository userRepository;
        public ProjectController()
        {
            taskRepository = new TaskRepository();
            projectRepository = new ProjectRepository();
            userRepository = new UserRepository();
            contributionRepository = new ContributionRepository();
            //TODO  remove this as soon as adding DI
            projectRepository.SetDbContext(taskRepository.GetDbContext());
            projectRepository.SetDbContext(taskRepository.GetDbContext());
            userRepository.SetDbContext(taskRepository.GetDbContext());
            contributionRepository.SetDbContext(taskRepository.GetDbContext());
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
            //contributionRepository.Add(contribution);
            //projectRepository.Dispose();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Remove(int id)
        {
            ProjectRepository pf = new ProjectRepository();
            pf.Remove(pf.FindById(id));
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Edit(int id)
        {
            ProjectRepository pf = new ProjectRepository();
            ProjectEntity project = pf.FindById(id);
            AddProjectViewModel projectModel = new AddProjectViewModel
            {
                Id = project.Id,
                EndAt = project.EndAt,
                StartAt = project.StartAt,
                Name = project.Name
            };
            return View("~/Views/Project/Add.cshtml", projectModel);
        }

        [HttpPost]
        public ActionResult Edit(AddProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            ProjectRepository pf = new ProjectRepository();
            ProjectEntity project = pf.FindById(model.Id);
            project.Name = model.Name;
            project.StartAt = model.StartAt;
            project.EndAt = model.EndAt;
            pf.GetDbContext().SaveChanges();


            return RedirectToAction("Index", "Home");
        }
    }
}