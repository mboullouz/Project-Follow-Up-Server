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
using PUp.Helpers;

namespace PUp.Tests.TaskTest
{


    [TestClass]
    public class TaskApiTest
    {
        private RepositoryManager rep;
        private SubScenarioBuilder subScenario;
        TaskApiController apiController;

        [TestInitialize]
        public void Init()
        {
            apiController = new TaskApiController();
            apiController.TaskService = new TaskService("m@boullouz.com",
                new ModelStateWrapper(new ValidationMessageHolder(), apiController.ModelState));

            rep = apiController.TaskService.GetRepositoryManager();
            subScenario = new SubScenarioBuilder(rep);
            ApiContextHelper.MockApiControllerRequest(apiController);
        }
        [TestCleanup]
        public void Clean()
        {
            rep = null;
            subScenario = null;
            apiController = null;
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
            Assert.AreEqual(taskOne.Id, 1);
            Assert.AreEqual(taskOneDto.Id, 1);
            Assert.AreEqual(taskOneDto.ToJson(), taskDtoResponse.ReadAsStringAsync().Result);
        }
        [TestMethod]
        public void GetTaskboardByProjectId_ShoudNotBeNull()
        {
            var taskboardSerialized = apiController.Taskboard(1).Content.ReadAsStringAsync().Result;
            var projectDto = new ProjectDto(rep.ProjectRepository.FindById(1));
            string projectDtoJson = AppJsonUtil<ProjectDto>.ToJson(projectDto);
            TaskboardViewModel tvm = AppJsonUtil<TaskboardViewModel>.FromJson(taskboardSerialized);
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
            var stateSerialized = apiController.Postpone(1).Content.ReadAsStringAsync().Result;
            Assert.IsNotNull(stateSerialized);
            var validationMessageHolder = AppJsonUtil<ValidationMessageHolder>.FromJson(stateSerialized);
            Assert.IsFalse(validationMessageHolder.State == 1);

        }

        [TestMethod]
        public void PostponeTask_ShouldPostoneOnActiveProject()
        {
            var taskEntity = subScenario.UnpostponedAndRunningTask(1);
            subScenario.PrepareActiveProject(taskEntity.Id);

            var stateSerialized = apiController.Postpone(1).Content.ReadAsStringAsync().Result;
            Assert.IsNotNull(stateSerialized);

            var validationMessageHolder = AppJsonUtil<ValidationMessageHolder>.FromJson(stateSerialized);
            Assert.IsNotNull(validationMessageHolder);
            Assert.AreEqual(1, validationMessageHolder.State);

        }

        [TestMethod]
        public void AddTask_ShouldReturnAnInstanceOfTaskViewOnActiveProject()
        {
            var p = subScenario.PrepareActiveProject(1);
            var vmSerialized = apiController.Add(p.Id).Content.ReadAsStringAsync().Result;
            var vmInstance = AppJsonUtil<AddTaskViewModel>.FromJson(vmSerialized);
            Assert.IsNotNull(vmSerialized);
            Assert.AreEqual(p.Id,vmInstance.ProjectId);

        }

        [TestMethod]
        public void AddTask_ShouldReturnVoidInstanceOfTaskVMOnInactiveProject()
        {
            var p = subScenario.PrepareInactiveProject(1);
            var vmSerialized = apiController.Add(p.Id).Content.ReadAsStringAsync().Result;
            var vmNewInstance = AppJsonUtil<AddTaskViewModel>.FromJson(vmSerialized);
            Assert.AreEqual(0,vmNewInstance.ProjectId);
            Assert.AreEqual(0,vmNewInstance.Id);
            Assert.AreEqual(null,vmNewInstance.Title);
        }

        [TestMethod]
        public void AddTask_ShouldThrowException_OnAddTaskWithUnitializedTaskVm()
        {
            var vm = new AddTaskViewModel(); 
            try
            {
                var resSerialized = apiController.Add(vm).Content.ReadAsStringAsync().Result;
            }
            catch(Exception e)
            {
                Assert.IsTrue(e.Message.Contains("property does not exist"));
            }
            
        }

        [TestMethod]
        public void AddTask_ShouldNotPass_OnAddTaskWithItializedTaskVm()
        {
            var p = subScenario.PrepareActiveProject(1);
            var vmSerialized = apiController.Add(p.Id).Content.ReadAsStringAsync().Result;
            var vmInitializedInstance = AppJsonUtil<AddTaskViewModel>.FromJson(vmSerialized);
            ValidationMessageHolder validationMessageHolder = new ValidationMessageHolder {State=0 };
            try
            {
                var resSerialized = apiController.Add(vmInitializedInstance).Content.ReadAsStringAsync().Result;
                validationMessageHolder = AppJsonUtil<ValidationMessageHolder>.FromJson(resSerialized);
            }
            catch (Exception e)
            {
                Assert.IsTrue(e.Message.Contains("property does not exist"));
                Assert.AreEqual(0, validationMessageHolder.State);
            }
        }

        [TestMethod]
        public void AddTask_ShouldSaveTask_OnInitializedTaskVM()
        {
            var vm = subScenario.InitAddTaskVMWithProjectAndUsersList(1);
            var resSerialized = apiController.Add(vm).Content.ReadAsStringAsync().Result;
            var validationMessageHolder = AppJsonUtil<ValidationMessageHolder>.FromJson(resSerialized);
            Assert.AreEqual(1, validationMessageHolder.State);
        }

        /**
        Test plan a task for the current day
        */


    }
}
