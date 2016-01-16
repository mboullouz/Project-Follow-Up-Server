using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PUp.Controllers;
using System.Web.Http.Results;
using System.Web.Mvc;
using PUp.Models.Entity;
using PUp.Models.SimpleObject;
using PUp.ViewModels;
using PUp.Models.Repository;
using PUp.Tests.Helpers;
using Moq;

namespace PUp.Tests.TaskTest
{
    [TestClass]
    public class TaskControllerTest
    {
        TaskController controller;
        IProjectRepository pRep;

        [TestInitialize]
        public void Init()
        {
            controller = new TaskController();
            pRep = new ProjectRepository();
        }

        [TestMethod]
        public void Test_taskController_index()
        {
            ViewResult view = (ViewResult)controller.Index(1);
            ProjectEntity viewModel = (ProjectEntity)view.Model;
            Assert.AreEqual(1, viewModel.Id);
            Assert.AreNotEqual(1, viewModel.Tasks.Count);
        }

        [TestMethod]
        public void Test_add_task()
        {
            var context = ContextHelper.FakeContext();
            Mock<ControllerContext> controllerContext = new Mock<ControllerContext>();
            controllerContext.Setup(p => p.HttpContext.Request.Browser.Browser).Returns("1");
            controller.ControllerContext = controllerContext.Object;
            var httpCxt = controller.ControllerContext.HttpContext;
            httpCxt.User = context.User;
            Assert.IsNotNull(context.User.Identity.Name);
             AddTaskViewModel model = new AddTaskViewModel
            {
                CreateAt = DateTime.Now,
                Description = "Some description",
                Done = false,
                EditionNumber=1,
                FinishAt= DateTime.Now.AddDays(7),
                Project = pRep.FindById(1),
                Priority= 1,
                Title="some task",
                IdProject= 1
            };
            System.Web.Mvc.RedirectToRouteResult result = (System.Web.Mvc.RedirectToRouteResult)controller.Add(model);
            Assert.AreEqual("Index", result.RouteValues["action"]);
            Assert.AreEqual("Task", result.RouteValues["controller"]);
            var data = new { id = pRep.FindById(1).Id };
            Assert.IsTrue(result.RouteValues.ContainsKey("id"));
            Assert.IsTrue(result.RouteValues.ContainsValue(data.id)); 
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
