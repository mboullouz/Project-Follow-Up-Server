using PUp.Models;
using PUp.Models.Entity;
using PUp.Models.Repository;
using PUp.ViewModels.Issue;
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
        private RepositoryManager repo = new RepositoryManager();
        private UserEntity currentUser = null;
        public IssueController()
        {
            currentUser = repo.UserRepository.GetCurrentUser();
        }
        // GET: liste of  Issues by project id
        public ActionResult Index(int id)
        {
            return View(repo.ProjectRepository.FindById(id));
        }

        public ActionResult Add(int id)
        {
            AddIssueViewModel addIssueVM = new AddIssueViewModel(id);
            return View(addIssueVM);
        }

        [HttpPost]
        public ActionResult Add(AddIssueViewModel model)
        { 
            ProjectEntity project = repo.ProjectRepository.FindById(model.ProjectId);
            if (!ModelState.IsValid)
            {
                model.ProjectId = project.Id;
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
                Submitter= currentUser,
            };
            repo.IssueRepository.Add(issue);
            project.Issues.Add(issue);
            project.Contributors.Add(currentUser);//add on submitting an issues
            string message = "New Issue is declared for the project <" + project.Name + ">";
            repo.NotificationRepository.NotifyAllUserInProject(project, message,LevelFlag.Danger);         
            return RedirectToAction("Index", "Issue", new { id = project.Id });
        }

        public ActionResult MarkResolved(int projectId, int issueId)
        {
            var issue = repo.IssueRepository.MarkResolved(issueId);
            return RedirectToAction("Index", "Issue", new { id = projectId });
        }

    }
}