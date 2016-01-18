using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PUp.Models;
using PUp.Models.Entity;
using PUp.Models.Repository;
using PUp.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private ITaskRepository taskRepository;
        private IProjectRepository projectRepository;
        private IUserRepository userRepository;
        private IContributionRepository contributionRepository;
        private DatabaseContext dbContext = new DatabaseContext();
        public HomeController()
        {
            userRepository = new UserRepository(dbContext);
            taskRepository = new TaskRepository(dbContext);
            projectRepository = new ProjectRepository(dbContext);
            contributionRepository = new ContributionRepository(dbContext);

        }
        public ActionResult Index()
        {
            var user = userRepository.GetCurrentUser();
            TableProjectModelView tableProject = new TableProjectModelView
            {
                CurrentUser = user,
                Projects = projectRepository.GetByUser(user),
                OtherProjects = projectRepository.GetAll(),
                UserContributions = user.Contributions.ToList()
            };

            return View(tableProject);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page.";
            return View();
        }
    }
}