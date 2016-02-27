using Newtonsoft.Json;
using PUp.Models;
using PUp.Models.Entity;
using PUp.Models.Repository;
using PUp.ViewModels;
using PUp.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace PUp.Controllers
{
    public class DashboardController : Controller
    {
        private RepositoryManager repo = new RepositoryManager();
        private UserEntity currentUser = null;

        public DashboardController()
        {
            currentUser = repo.UserRepository.GetCurrentUser();
        }
       
        // GET: Dashboard
        public ActionResult Index()
        {
            DashboardModelView dashboardMV = new DashboardModelView();
            var currentTasks = repo.TaskRepository.TodayTasksByUser(currentUser);
            dashboardMV.CurrentTasks = currentTasks;
            var otherTasks = repo.TaskRepository.GetAll()
                                           .Where(t => t.Executor == currentUser && !t.Done && t.StartAt==null )
                                           .ToList();
            dashboardMV.MatrixVM = new MatrixViewModel(currentTasks, currentUser);
            dashboardMV.OtherTasks = otherTasks;
            return View(dashboardMV);
        }
 
    }
}