using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PUp.Models;
using PUp.Models.Entity;
using PUp.Models.Repository;
using PUp.Services;
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
        private ProjectService projectService;
        private RepositoryManager repo = new RepositoryManager();
        private UserEntity currentUser = null;

        public HomeController()
        {
            projectService = new ProjectService(new ModelStateWrapper(TempData, ModelState));
        }
        public ActionResult Index()
        {
            currentUser = repo.UserRepository.GetCurrentUser();
            if (currentUser==null)
            {
                this.Flash("Please register / or login if you already have an account", FlashLevel.Warning);
                return RedirectToAction("Register", "Account");
            }
            return View(projectService.GetTableProjectForCurrentUser());
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