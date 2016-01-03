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
        private ITaskRepository taskRepository;
        private IProjectRepository projectRepository;
        private IUserRepository userRepository;
        public HomeController()
        {
            userRepository = new UserRepository();
            taskRepository = new TaskRepository();
            projectRepository = new ProjectRepository();
            //TODO  remove this as soon as adding DI
            userRepository.SetDbContext(taskRepository.GetDbContext());
            projectRepository.SetDbContext(taskRepository.GetDbContext());
        }
        public ActionResult Index()
        {
            var user = userRepository.GetCurrentUser();
            TableProjectModelView tableProject = new TableProjectModelView
            {
                CurrentUser = user,
                Projects = projectRepository.GetAll().Where(v=>v.User.Id==user.Id).OrderByDescending(p=>p.CreateAt).ToList()
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