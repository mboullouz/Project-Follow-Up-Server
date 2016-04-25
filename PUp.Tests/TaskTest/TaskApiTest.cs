using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PUp.Controllers;
using Microsoft.AspNet.Identity;
using PUp.Models.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using PUp.Models;
using System.Linq;
using System.Security.Principal;
using System.Net.Http;
using PUp.Models.Dto;
using PUp.Tests.Helpers;

namespace PUp.Tests.TaskTest
{
    [TestClass]
    public class TaskApiTest
    {
        RepositoryManager rep = new RepositoryManager();
        [TestMethod]
        public void Access_api_task_controller()
        {
            TaskApiController apiController = new TaskApiController();
            ApiContextHelper.MockApiControllerRequest(apiController);
            var taskDtoResponse = apiController.Get(1).Content;
            var taskOne = rep.TaskRepository.FindById(1);
            var taskOneDto = new TaskDto(taskOne);
            Assert.IsNotNull(ApiContextHelper.CurrentUser);
            Assert.IsNotNull(taskDtoResponse);
            Assert.IsNotNull(taskOne);
            Assert.AreEqual(taskOne.Id,1);
            Assert.AreEqual(taskOneDto.Id,1);

            Assert.AreEqual(taskOneDto.ToJson(), taskDtoResponse.ReadAsStringAsync().Result);
        }

    }
}
