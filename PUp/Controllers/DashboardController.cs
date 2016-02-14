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
            //userName = ControllerContext.HttpContext.User.Identity.Name;
            currentUser = userRepository.GetCurrentUser();


        }
        // GET: Dashboard
        public ActionResult Index()
        {
            DashboardModelView dashboardMV = new DashboardModelView();
            //TODO this just for tests! write a true linq query
            var currentTasks = taskRepository.GetAll().Where(
                t => t.Executor == currentUser
                 
                  && t.StartAt!=null
                  ).ToList();
            dashboardMV.CurrentTasks = currentTasks;
            var otherTasks = taskRepository.GetAll()
                                            .Where(t => t.Executor== currentUser && !t.Done && t.StartAt==null )
                                            .ToList();
            dashboardMV.OtherTasks = otherTasks;
            return View(dashboardMV);
        }

        /*
        //TODO clean it
        */
        public ActionResult Interval()
        {
            var currentTasks = taskRepository.GetAll().Where(
                t => t.Executor == currentUser
                  && t.EndAt >= DateTime.Now
                  && !t.Done
                  ).ToList(); 
            GroundInterval intervalManager = new GroundInterval();
            
            foreach(var t in currentTasks)
            {
 
                if (t.StartAt != null)
                {
                    intervalManager.AddDate((DateTime)t.StartAt, t.EstimatedTimeInMinutes/60);
                }
                
            }

            var list = JsonConvert.SerializeObject(intervalManager.Interval,
               Formatting.None,
               new JsonSerializerSettings()
               {
                   ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                   MaxDepth = 1,
               });
            return Json(list);
        }
    }
}