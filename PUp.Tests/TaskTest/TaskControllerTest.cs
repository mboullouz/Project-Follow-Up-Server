using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PUp.Controllers;
using System.Web.Http.Results;
using System.Web.Mvc;
using PUp.Models.Entity;
using PUp.Models.SimpleObject;

namespace PUp.Tests.TaskTest
{
    [TestClass]
    public class TaskControllerTest
    {
        TaskController controller;

        [TestInitialize]
        public void Init()
        {
            controller = new TaskController();
        }

        [TestMethod]
        public void Test_taskController_index()
        {
            ViewResult view = (ViewResult)controller.Index(1);
            ProjectEntity viewModel = (ProjectEntity)view.Model;
            Assert.AreEqual(1, viewModel.Id);
            Assert.AreNotEqual(1,viewModel.Tasks.Count);
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
            Assert.AreEqual(resExpected.Success, res.Success);
            Assert.AreEqual(resExpected.State, res.State);
            Assert.AreEqual(resExpected.Message, res.Message);
            Assert.AreEqual(resExpected.IdEntity, res.IdEntity);
        }
    }
}
