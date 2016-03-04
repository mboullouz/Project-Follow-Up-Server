using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PUp.Models.Entity;
using PUp.Models;
using PUp.ViewModels.Project;
using PUp.Services;

namespace PUp.Controllers
{
    public class ProjectController : Controller
    {
        private ProjectService projectService;
        
        private UserEntity currentUser = null;

        public ProjectController()
        {
           
            projectService = new ProjectService(new ModelStateWrapper(TempData, ModelState));
            currentUser = projectService.GetRepositoryManager().UserRepository.GetCurrentUser();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View(projectService.InitProjectModel());
        }

        [HttpGet]
        public ActionResult Contributors(int id)
        {
            var project = projectService.GetRepositoryManager().ProjectRepository.GetAll().Where(p=>p.Id== id).FirstOrDefault();
            return View(project);
        }

        [HttpPost]
        public ActionResult Edit(AddProjectViewModel model)
        {    

            if (!projectService.IsModelValid(model))
            {
              return View(model);
            }
            ProjectEntity project = projectService.GetRepositoryManager().ProjectRepository.FindById(model.Id);
            project.Name = model.Name;
            project.StartAt = model.StartAt;
            project.EndAt = model.EndAt;
            project.Objective = model.Objective;
            project.Benifite = model.Benifite;
             projectService.GetRepositoryManager().ProjectRepository.DbContext.SaveChanges();
            
            foreach(var u in project.Contributors)
            {
                 projectService.GetRepositoryManager().NotificationRepository.Add(u, "Project: " + project.Name + " updated", "~/Project/Timeline" + project.Id, LevelFlag.Info);
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult Add(AddProjectViewModel model)
        {
            if (!projectService.IsModelValid(model))
            {
                return View(model);
            }
             
            var project = projectService.GetInitializedProjectFromModel(model); 
             projectService.GetRepositoryManager().ProjectRepository.Add(project);
             projectService.GetRepositoryManager().NotificationRepository.GenerateFor(project, new HashSet<UserEntity>( projectService.GetRepositoryManager().UserRepository.GetAll()));
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Remove(int id)
        {
             projectService.GetRepositoryManager().ProjectRepository.MarkDeleted( projectService.GetRepositoryManager().ProjectRepository.FindById(id));
            return RedirectToAction("Index", "Home");
        }

        //Remove permenently a record 
        public ActionResult HardRemove(int id)
        {
             projectService.GetRepositoryManager().ProjectRepository.Remove( projectService.GetRepositoryManager().ProjectRepository.FindById(id));
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Edit(int id)
        {
            ProjectEntity project =  projectService.GetRepositoryManager().ProjectRepository.FindById(id);
            AddProjectViewModel projectModel = new AddProjectViewModel(project);
            projectModel.Id = id;
            return View("~/Views/Project/Add.cshtml", projectModel);
        }

        public ActionResult Timeline(int id)
        {
            ProjectEntity project =  projectService.GetRepositoryManager().ProjectRepository.FindById(id);
            ProjectTimelineViewModel projectTimeline = new ProjectTimelineViewModel(project);
            return View(projectTimeline);
        }

        public ActionResult Info(int id)
        {
            ProjectEntity project =  projectService.GetRepositoryManager().ProjectRepository.FindById(id);
            return View(project);
        }

        public ActionResult Matrix(int id)
        {
            ProjectEntity project =  projectService.GetRepositoryManager().ProjectRepository.FindById(id);
            MatrixViewModel mVM = new MatrixViewModel(project,  projectService.GetRepositoryManager().UserRepository.GetCurrentUser());
            return View(mVM);
        }

    }
}
