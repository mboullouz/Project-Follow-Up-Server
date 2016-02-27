using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PUp.ViewModels;
using PUp.Models.Repository;
using PUp.Models.Entity;
using PUp.Models;
using PUp.ViewModels.Project;

namespace PUp.Controllers
{
    public class ProjectController : Controller
    {
        private RepositoryManager repo = new RepositoryManager();
        private UserEntity currentUser = null;

        public ProjectController()
        {
            currentUser = repo.UserRepository.GetCurrentUser();
        }

        [HttpGet]
        public ActionResult Add()
        {
            AddProjectViewModel projectModel = new AddProjectViewModel
            {
                EndAt = DateTime.Now.AddDays(7),
                StartAt = DateTime.Now.AddHours(1),
                Name = "Week 2: Exciting project!",
                Benifite = "Define the benefits and related assumptions,associated with the project...",
                Objective = "what is the project trying to achieve? What functionalities or departments are involved?... "
            };
            return View(projectModel);
        }

        [HttpGet]
        public ActionResult Contributors(int id)
        {
            var project = repo.ProjectRepository.GetAll().Where(p=>p.Id== id).FirstOrDefault();
            return View(project);
        }

        [HttpPost]
        public ActionResult Edit(AddProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
              return View(model);
            }
            ProjectEntity project = repo.ProjectRepository.FindById(model.Id);
            project.Name = model.Name;
            project.StartAt = model.StartAt;
            project.EndAt = model.EndAt;
            project.Objective = model.Objective;
            project.Benifite = model.Benifite;
            repo.ProjectRepository.DbContext.SaveChanges();
            var notif = new NotificationEntity ();
            notif.Message = "Project: " + project.Name + " updated";
            notif.Url = "~/Project/Timeline" + project.Id;
            notif.Level = LevelFlag.Info;
            foreach(var u in project.Contributors)
            {
                notif.User = u;
                repo.NotificationRepository.Add(notif);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Add(AddProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            if (model.EndAt <= model.StartAt || model.StartAt <= DateTime.Now.AddMinutes(30))
            {
                ModelState.AddModelError("", "Dates are not valid! .");
                return View(model);
            }
            ProjectEntity project = new ProjectEntity();
            project.Name = model.Name;
            project.StartAt = model.StartAt;
            project.EndAt = model.EndAt;
            project.Objective = model.Objective;
            project.Benifite = model.Benifite;
            project.Owner = repo.UserRepository.GetCurrentUser();
            project.Contributors.Add(project.Owner);
            repo.ProjectRepository.Add(project);
            repo.NotificationRepository.GenerateFor(project, new HashSet<UserEntity>(repo.UserRepository.GetAll()));
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Remove(int id)
        {
            repo.ProjectRepository.MarkDeleted(repo.ProjectRepository.FindById(id));
            return RedirectToAction("Index", "Home");
        }

        //Remove permenently a record 
        public ActionResult HardRemove(int id)
        {
            repo.ProjectRepository.Remove(repo.ProjectRepository.FindById(id));
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int id)
        {
            ProjectEntity project = repo.ProjectRepository.FindById(id);
            AddProjectViewModel projectModel = new AddProjectViewModel(project);
            projectModel.Id = id;
            return View("~/Views/Project/Add.cshtml", projectModel);
        }

        public ActionResult Timeline(int id)
        {
            ProjectEntity project = repo.ProjectRepository.FindById(id);
            ProjectTimelineViewModel projectTimeline = new ProjectTimelineViewModel(project);
            return View(projectTimeline);
        }

        public ActionResult Info(int id)
        {
            ProjectEntity project = repo.ProjectRepository.FindById(id);
            return View(project);
        }

        public ActionResult Matrix(int id)
        {
            ProjectEntity project = repo.ProjectRepository.FindById(id);
            MatrixViewModel mVM = new MatrixViewModel(project, repo.UserRepository.GetCurrentUser());
            return View(mVM);
        }

    }
}
