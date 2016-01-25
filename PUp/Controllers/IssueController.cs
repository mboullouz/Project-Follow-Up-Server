using PUp.Models;
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
        private ContributionRepository contributionRepository;
        private DatabaseContext dbContext = new DatabaseContext();

        public IssueController()
        {
            userRepository = new UserRepository(dbContext);
            taskRepository = new TaskRepository(dbContext);
            projectRepository = new ProjectRepository(dbContext);
            contributionRepository = new ContributionRepository(dbContext);
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
    }
}