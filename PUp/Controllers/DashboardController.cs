using Newtonsoft.Json;
using PUp.Models;
using PUp.Models.Entity;
using PUp.Models.Repository;
using PUp.ViewModels;
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
        private TaskRepository taskRepository;
        private ProjectRepository projectRepository;
        private UserRepository userRepository;
       
        private NotificationRepository notifRepository;
        private DatabaseContext dbContext = new DatabaseContext();
        UserEntity currentUser;
        public DashboardController()
        {
            taskRepository = new TaskRepository(dbContext);
            projectRepository = new ProjectRepository(dbContext);
            userRepository = new UserRepository(dbContext);         
            notifRepository = new NotificationRepository(dbContext);
            currentUser = userRepository.GetCurrentUser();
        }
       
        // GET: Dashboard
        public ActionResult Index()
        {
            DashboardModelView dashboardMV = new DashboardModelView();
            var currentTasks = taskRepository.TodayTasksByUser(currentUser);
            dashboardMV.CurrentTasks = currentTasks;
            var otherTasks = taskRepository.GetAll()
                                           .Where(t => t.Executor == currentUser && !t.Done && t.StartAt==null )
                                           .ToList();
            dashboardMV.OtherTasks = otherTasks;
            return View(dashboardMV);
        }
 
    }
}