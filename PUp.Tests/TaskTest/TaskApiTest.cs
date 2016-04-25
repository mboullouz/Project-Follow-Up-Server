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
            string userName = "m@boullouz.com";
            string password = "123*Aa";
            var UserManager = new UserManager<UserEntity>(new UserStore<UserEntity>(new DatabaseContext()));
            var user = UserManager.Find(userName, password);

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new DatabaseContext()));
            string[] roles = new string[user.Roles.ToList().Count];
            var k = 0;
            foreach (var r in user.Roles.ToList())
            {
                roles[k] = r.RoleId; k++;
            }
            apiController.RequestContext.Principal = new GenericPrincipal(new GenericIdentity(userName, "Basic"), roles);
            apiController.Request= new HttpRequestMessage();
           // apiController.Request.Content = new HttpContent();
            var taskDtoResponse = apiController.Get(1).Content;
            var taskOne = rep.TaskRepository.FindById(1);
            var taskOneDto = new TaskDto(taskOne);
            Assert.IsNotNull(user);
            Assert.IsNotNull(taskDtoResponse);
            Assert.IsNotNull(taskOne);
            Assert.AreEqual(taskOne.Id,1);
            Assert.AreEqual(taskOneDto.Id,1);

            Assert.AreEqual(taskOneDto.ToJson(), taskDtoResponse.ReadAsStringAsync().Result);
        }

    }
}
