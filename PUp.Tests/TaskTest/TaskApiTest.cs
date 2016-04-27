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
using PUp.ViewModels.Task;
using Moq;
using PUp.Services;
using PUp.Tests.SubScenario;

namespace PUp.Tests.TaskTest
{
    

    [TestClass]
    public class TaskApiTest
    {
        private RepositoryManager rep = new RepositoryManager();
        private  SubScenarioBuilder subScenario;
       TaskApiController apiController;

        [TestInitialize]
        public void Init()
        {
            apiController = new TaskApiController();
            subScenario = new  SubScenarioBuilder(rep); 
            ApiContextHelper.MockApiControllerRequest(apiController);
        }

        [TestMethod]
        public void GetTaskById_ShouldReturnSerializedTaskDto() 
        {
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
        [TestMethod]
        public void GetTaskboardByProjectId_ShoudNotBeNull()
        {
            var taskboardSerialized = apiController.Taskboard(1).Content.ReadAsStringAsync().Result;
            var projectDto = new ProjectDto(rep.ProjectRepository.FindById(1));
            string projectDtoJson = Util<ProjectDto>.ToJson(projectDto);
            TaskboardViewModel tvm =   Util< TaskboardViewModel>.FromJson(taskboardSerialized);
            Assert.IsNotNull(projectDtoJson);
            Assert.IsNotNull(taskboardSerialized);
            Assert.IsTrue(taskboardSerialized.Contains("1"));
            Assert.AreEqual(projectDto.Id, tvm.Project.Id);
        }

        [TestMethod]
        public void PostponeTask_ShouldNotPostponeOnNotActiveProject()
        {
            var taskEntity = subScenario.UnpostponedAndRunningTask(1);
            subScenario.PrepareInactiveProject(taskEntity.Id);

            Assert.AreEqual(taskEntity, rep.TaskRepository.FindById(1));
            
            var stateSerialized = apiController.Postpone(1).Content.ReadAsStringAsync().Result;
            Assert.IsNotNull(stateSerialized);
            var newTaskEntity = rep.TaskRepository.FindById(1);
            var validationMessageHolder= Util<ValidationMessageHolder>.FromJson(stateSerialized);
            Assert.IsFalse(validationMessageHolder.State==1);
            Assert.AreEqual(newTaskEntity.StartAt.GetValueOrDefault().Minute,DateTime.Now.Minute);
            
        }

        [TestMethod]
        public void PostponeTask_ShouldPostoneOnActiveProject()
        {
            var taskEntity = rep.TaskRepository.FindById(1);
            taskEntity.StartAt = DateTime.Now;
            taskEntity.EndAt = DateTime.Now.AddHours(2);
            taskEntity.Postponed = false;
            var project = rep.ProjectRepository.FindById(taskEntity.Project.Id);
            project.StartAt = DateTime.Now.AddHours(-100);
            project.EndAt = DateTime.Now.AddHours(50);
            rep.DbContext.SaveChanges();

            var stateSerialized = apiController.Postpone(1).Content.ReadAsStringAsync().Result;
            Assert.IsNotNull(stateSerialized);

            var validationMessageHolder = Util<ValidationMessageHolder>.FromJson(stateSerialized);
            Assert.IsNotNull(validationMessageHolder);
            Assert.AreEqual(1,validationMessageHolder.State);
           
        }

        [TestMethod]
        public void PostponeTask_ShouldPostoneOnActiveProject_UsingMockObject()
        {
             

        }

    }
}
