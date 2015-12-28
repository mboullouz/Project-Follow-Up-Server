using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PUp.Models;
using PUp.Models.Entity;
using PUp.Models.Facade;
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
            DatabaseContext dbContext = uf.GetDbContext();
           TaskFacade tf = new TaskFacade();
             tf.SetDbContext(dbContext);
            ProjectFacade pf = new ProjectFacade();
            pf.SetDbContext(dbContext);

            List<TaskEntity> tasks = tf.GetAll();
            ProjectEntity project = new ProjectEntity
            {
                Name = "W 2: December 2015",
                Tasks = tasks,
                User  = uf.GetCurrentUser()
            };
             //pf.Add(project);
            TaskEntity t1 = new TaskEntity { EditionNumber = 0, Priority = 2, Title = "Reorganize the doc", Project = project };
            TaskEntity t2 = new TaskEntity { EditionNumber = 1, Priority = 3, Title = "Implements some interfaces", Project = project };
           // tf.Add(t1); tf.Add(t2);

            ViewBag.CurrentUser = uf.UsernameCurrent();
            ViewBag.Projects = pf.GetAll();
            
            return View();
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