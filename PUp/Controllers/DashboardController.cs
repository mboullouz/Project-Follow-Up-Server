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
    public class DashboardController : Controller
    {
        private TaskRepository taskRepository;
        private ProjectRepository projectRepository;
        private UserRepository userRepository;
        private ContributionRepository contributionRepository;
        private NotificationRepository notifRepository;
        private DatabaseContext dbContext = new DatabaseContext();
        UserEntity currentUser;
        public DashboardController()
        {
            taskRepository = new TaskRepository(dbContext);
            projectRepository = new ProjectRepository(dbContext);
            userRepository = new UserRepository(dbContext);
            contributionRepository = new ContributionRepository(dbContext);
            notifRepository = new NotificationRepository(dbContext);
            //userName = ControllerContext.HttpContext.User.Identity.Name;
            currentUser = userRepository.GetCurrentUser();


        }
        // GET: Dashboard
        public ActionResult Index()
        {
            DashboardModelView dashboardMV = new DashboardModelView();
            //TODO this just for tests! write a true linq query
            var currentTasks = taskRepository.GetAll().Where(t => t.Executor.Id == currentUser.Id
                                     && t.EndAt >=DateTime.Now ).ToList();
            dashboardMV.CurrentTasks = currentTasks;
            var otherTasks = taskRepository.GetAll()
                                            .Where(t => t.Executor.Id == currentUser.Id )
                                            .Where(t=>!currentTasks.Contains(t)).ToList();
            dashboardMV.OtherTasks = otherTasks;
            return View(dashboardMV);
        }
    }
}