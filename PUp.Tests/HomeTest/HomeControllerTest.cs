using Microsoft.VisualStudio.TestTools.UnitTesting;
using PUp.Controllers;
using PUp.Models.Entity;
using PUp.Models.Repository;
using PUp.Tests.Helpers;
using PUp.Models;

namespace PUp.Tests.HomeTest
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController controller;
        private IProjectRepository pRep;
        private ITaskRepository tRep;
        private INotificationRepository nRep;
        private int idProject = 1;
        private DatabaseContext dbContext = new DatabaseContext();
        private IContributionRepository contribRepo;
        private UserEntity user;
        [TestInitialize]
        public void Init()
        {
            controller = new HomeController();
            ContextHelper.InitControllerContext(controller);
            pRep = new ProjectRepository(dbContext);
            tRep = new TaskRepository(dbContext);
            nRep = new NotificationRepository(dbContext);
            contribRepo = new ContributionRepository(dbContext);
            user = ContextHelper.CurrentUserEntity(dbContext);
        }

        [TestMethod]
        public void Test_index_current_user_should_be_in_contibutors_list()
        {  
            /*
            int initialNumberOfTasks = tRep.GetAll().Count;
            int initialNumberOfNotifs = nRep.GetAll().Count;
            bool isContribBeforeAddingTask = contribRepo.ContributionExists(pRep.FindById(idProject), user);
            AddTaskViewModel model = new AddTaskViewModel
            {
                CreateAt = DateTime.Now,
                Description = "Some description",
                Done = false,
                EditionNumber = 1,
                FinishAt = DateTime.Now.AddDays(7),
                Project = pRep.FindById(idProject),
                Priority = 1,
                Title = "some task",
                IdProject = this.idProject
            };
            RedirectToRouteResult result = (RedirectToRouteResult)controller.Add(model);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Task", result.RouteValues["controller"]);
            Assert.IsTrue(result.RouteValues.ContainsKey("id"));
            Assert.IsTrue(result.RouteValues.ContainsValue(idProject));
            Assert.IsTrue(tRep.GetAll().Count > initialNumberOfTasks);
            //Add task musk generate a new notifi
            Assert.IsTrue(nRep.GetAll().Count > initialNumberOfNotifs);
            if (!isContribBeforeAddingTask) //in case no contrib must be true
            {
                Assert.IsTrue(contribRepo.ContributionExists(pRep.FindById(idProject), user));
            }
            */
        }

        

    }
}
