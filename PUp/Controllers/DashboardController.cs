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
    [Authorize]
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
            var currentTasks = repo.TaskRepository.TodayTasksByUser(currentUser).Where(t =>t.Done == false).ToList();
            var doneToday    = repo.TaskRepository.TodayTasksByUser(currentUser).Where(t => t.Done == true).ToList();
            dashboardMV.CurrentTasks = currentTasks;
            var otherTasks   = repo.TaskRepository.GetAll()
                                           .Where(t => t.Executor == currentUser && !t.Done && t.StartAt==null && t.Project.EndAt > DateTime.Now && t.Project.Deleted == false)
                                           .ToList();
            dashboardMV.MatrixVM = new MatrixViewModel(currentTasks, currentUser);
            dashboardMV.OtherTasks = otherTasks;
            dashboardMV.TodayDoneTasks = doneToday;
            return View(dashboardMV);
        }
 
    }
}