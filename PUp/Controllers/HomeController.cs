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

        private RepositoryManager repo = new RepositoryManager();
        private UserEntity currentUser = null;

        public HomeController()
        {
            currentUser = repo.UserRepository.GetCurrentUser();
        }
        public ActionResult Index()
        {
            if(currentUser==null)
            {
                this.Flash("Please register / or login if you already have an account", FlashLevel.Warning);
                return RedirectToAction("Register", "Account");
            }
            var projectsByUser = repo.ProjectRepository.GetAll().Where(p=>p.Owner==currentUser|| p.Contributors.Contains(currentUser)).ToList();
            TableProjectModelView tableProject = new TableProjectModelView
            {
                CurrentUser = currentUser,
                Projects = projectsByUser,
                OtherProjects = repo.ProjectRepository.GetActive().Where(p => !projectsByUser.Contains(p)).ToList(),
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