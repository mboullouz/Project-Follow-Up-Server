﻿using Microsoft.AspNet.Identity;
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

        //TODO user a simple factory 
        private TaskRepository taskRepository;
        private ProjectRepository projectRepository;
        private UserRepository userRepository;
        
        private DatabaseContext dbContext = new DatabaseContext();

        public HomeController()
        {
            userRepository = new UserRepository(dbContext);
            taskRepository = new TaskRepository(dbContext);
            projectRepository = new ProjectRepository(dbContext);
           

        }
        public ActionResult Index()
        {
            var user = userRepository.GetCurrentUser();
            if(user==null)
            {
                this.Flash("Please register / or login if you already have an account", FlashLevel.Warning);
                return RedirectToAction("Register", "Account");
            }
            var projectsByUser = projectRepository.GetAll().Where(p=>p.Owner==user|| p.Contributors.Contains(user)).ToList();
            TableProjectModelView tableProject = new TableProjectModelView
            {
                CurrentUser = user,
                Projects = projectsByUser,
                OtherProjects = projectRepository.GetActive().Where(p => !projectsByUser.Contains(p)).ToList(),
                
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