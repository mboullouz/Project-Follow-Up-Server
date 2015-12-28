using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PUp.ViewModels;
using PUp.Models.Facade;
using PUp.Models.Entity;

namespace PUp.Controllers
{
    public class ProjectController : Controller
    {
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

            ProjectFacade pf = new ProjectFacade();
            UserFacade uf = new UserFacade();
            uf.SetDbContext(pf.GetDbContext());
            ProjectEntity project = new ProjectEntity();
            project.Name = model.Name;
            project.StartAt = model.StartAt;
            project.EndAt = model.EndAt;
            project.User = uf.GetCurrentUser();
            pf.Add(project);
            pf.Dispose();
            return RedirectToAction("Index", "Home");
        }
    }
}