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
        private TaskRepository taskRepository;
        private ProjectRepository projectRepository;
        private UserRepository userRepository;
     
        private NotificationRepository notifRepository;
        private DatabaseContext dbContext = new DatabaseContext();

        public ProjectController()
        {
            taskRepository = new TaskRepository(dbContext);
            projectRepository = new ProjectRepository(dbContext);
            userRepository = new UserRepository(dbContext);
           
            notifRepository = new NotificationRepository(dbContext);


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

        [HttpPost]
        public ActionResult Edit(AddProjectViewModel model)
        {
            if (!ModelState.IsValid)
            {

                return View(model);
            }
            ProjectEntity project = projectRepository.FindById(model.Id);
            project.Name = model.Name;
            project.StartAt = model.StartAt;
            project.EndAt = model.EndAt;
            project.Objective = model.Objective;
            project.Benifite = model.Benifite;
            projectRepository.DbContext.SaveChanges();
            var notif = new NotificationEntity ();
            notif.Message = "Project: " + project.Name + " updated";
            notif.Url = "~/Project/Details" + project.Id;
            notif.Level = LevelFlag.INFO;
            foreach(var u in project.Contributors)
            {
                notif.User = u;
                notifRepository.Add(notif);
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
 

            projectRepository.Add(project);
            
            notifRepository.GenerateFor(project, new HashSet<UserEntity>(userRepository.GetAll()));


            return RedirectToAction("Index", "Home");
        }

        public ActionResult Remove(int id)
        {
            projectRepository.MarkDeleted(projectRepository.FindById(id));
            return RedirectToAction("Index", "Home");
        }

        //Remove permenently a record 
        public ActionResult HardRemove(int id)
        {
            projectRepository.Remove(projectRepository.FindById(id));
            return RedirectToAction("Index", "Home");
        }


        public ActionResult Edit(int id)
        {
            ProjectEntity project = projectRepository.FindById(id);
            AddProjectViewModel projectModel = new AddProjectViewModel(project);
            projectModel.Id = id;
            return View("~/Views/Project/Add.cshtml", projectModel);
        }


        public ActionResult Details(int id)
        {

            ProjectEntity project = projectRepository.FindById(id);
            ProjectTimelineViewModel projectTimeline = new ProjectTimelineViewModel(project);

            return View("~/Views/Project/Details.cshtml", projectTimeline);
        }

        public ActionResult Matrix(int id)
        {
            ProjectEntity project = projectRepository.FindById(id);
            MatrixViewModel mVM = new MatrixViewModel(project);
            return View("~/Views/Project/Matrix.cshtml", mVM);
        }


    }
}
