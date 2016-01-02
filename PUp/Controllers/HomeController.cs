using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PUp.Models;
using PUp.Models.Entity;
using PUp.Models.Facade;
using PUp.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUp.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            UserFacade uf = new UserFacade();
            ProjectFacade pf = new ProjectFacade(); 
            TableProjectModelView tableProject = new TableProjectModelView
            {
                CurrentUser = uf.GetCurrentUser(),
                Projects = pf.GetAll().OrderByDescending(p=>p.CreateAt).ToList()
            };

            return View(tableProject);
        }



        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}