using PUp.Models;
using PUp.Models.Entity;
using PUp.Models.Repository;
using PUp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PUp.Controllers
{
    public class IssueController : Controller
    {
        //TODO user a simple factory 
        private TaskRepository taskRepository;
        private ProjectRepository projectRepository;
        private UserRepository userRepository;
        private NotificationRepository notificationRepo;
        private IssueRepository issueRepository;
        private DatabaseContext dbContext = new DatabaseContext();

        public IssueController()
        {
            userRepository = new UserRepository(dbContext);
            taskRepository = new TaskRepository(dbContext);
            projectRepository = new ProjectRepository(dbContext);
            notificationRepo = new NotificationRepository(dbContext);
            issueRepository = new IssueRepository(dbContext);
        }
        // GET: liste of  Issues by project id
        public ActionResult Index(int id)
        {
            return View(projectRepository.FindById(id));
        }

        public ActionResult Add(int id)
        {
            AddIssueViewModel addIssueVM = new AddIssueViewModel(id);
            return View(addIssueVM);
        }

        [HttpPost]
        public ActionResult Add(AddIssueViewModel model)
        { 
            ProjectEntity project = projectRepository.FindById(model.IdProject);
            if (!ModelState.IsValid)
            {
                model.IdProject = project.Id;
                return View(model);
            }
            IssueEntity issue = new IssueEntity
            {
                AddAt= DateTime.Now,               
                EditAt  = DateTime.Now,
                Deleted = false,
                Project = project,
                Description = model.Description,
                RelatedArea = model.RelatedArea,
                Status      = model.Status,
            };
            issueRepository.Add(issue);
            project.Issues.Add(issue);
            string message = "New Issue is declared for the project <" + project.Name + ">";
            notificationRepo.NotifyAllUserInProject(project, message,LevelFlag.Danger);         
            return RedirectToAction("Index", "Issue", new { id = project.Id });
        }

        public ActionResult MarkResolved(int projectId, int issueId)
        {
            var issue =issueRepository.MarkResolved(issueId);
            return RedirectToAction("Index", "Issue", new { id = projectId });
        }

    }
}