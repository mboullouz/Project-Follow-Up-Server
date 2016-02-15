using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PUp.Controllers;
using System.Web.Mvc;
using PUp.Models.Entity;
using PUp.Models.SimpleObject;
using PUp.ViewModels;
using PUp.Models.Repository;
using PUp.Tests.Helpers;
using PUp.Models;

namespace PUp.Tests.TaskTest
{
    [TestClass]
    public class TaskControllerTest
    {
        private TaskController controller;
        private  ProjectRepository pRep;
        private  TaskRepository tRep;
        private  NotificationRepository nRep;
        private int idProject = 1;
        private DatabaseContext dbContext = new DatabaseContext();
     
        private UserEntity user;
        [TestInitialize]
        public void Init()
        {
            controller = new TaskController();
            ContextHelper.InitControllerContext(controller);
            pRep = new ProjectRepository(dbContext);
            tRep = new TaskRepository(dbContext);
            nRep = new NotificationRepository(dbContext);
           
            user = ContextHelper.CurrentUserEntity(dbContext);
        }

        [TestMethod]
        public void Test_taskController_index()
        {
            ViewResult view = (ViewResult)controller.Index(idProject);
            ProjectEntity viewModel = (ProjectEntity)view.Model;
            Assert.AreEqual(1, viewModel.Id);
            Assert.AreNotEqual(1, viewModel.Tasks.Count);
        }

        [TestMethod]
        public void Test_add_task()
        {
            int initialNumberOfTasks = tRep.GetAll().Count;
            int initialNumberOfNotifs = nRep.GetAll().Count;
           
            AddTaskViewModel model = new AddTaskViewModel
            {
                 
                Description = "Some description",
               
            
                Project = pRep.FindById(idProject),
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
           
        }

        [TestMethod]
        public void Test_taskController_changeState()
        {
            TaskBasic tBasic = new TaskBasic { Done = true, Id = 1 };
            JsonResult view = (JsonResult)controller.ChangeState(tBasic);
            GenericJsonResponse res = (GenericJsonResponse)view.Data;
            GenericJsonResponse resExpected = new GenericJsonResponse
            {
                Success = true,
                State = "OK",
                Message = "Task updated succefully",
                IdEntity = tBasic.Id
            };
            Assert.AreEqual(resExpected.ToString(), res.ToString());
        }

    }
}
