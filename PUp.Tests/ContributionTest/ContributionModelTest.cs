﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PUp.Models.Repository;
using PUp.Models.Entity;
using PUp.Models;
using PUp.Tests.Helpers;
using Moq;
using System.Collections.Generic;

namespace PUp.Tests.ContributionTest
{
    [TestClass]
    public class ContributionModelTest
    {
        private INotificationRepository notifRepo;
        private IContributionRepository contribRepo;
        private IProjectRepository projectRepo;         
        private ITaskRepository taskRepo;
        private UserEntity user;
        private DatabaseContext dbContext = new DatabaseContext();

        [TestInitialize]
        public void Init()
        {
            user = ContextHelper.CurrentUserEntity(dbContext);
            notifRepo = new NotificationRepository(dbContext);
            projectRepo = new ProjectRepository(dbContext);
            contribRepo = new ContributionRepository(dbContext);
            taskRepo = new TaskRepository(dbContext);
        }

        [TestMethod]
        public void Test_contribution_not_exists_for_mock_project_entity()
        {
            Mock<ProjectEntity> projectEntityMock = new Mock<ProjectEntity>();
            Assert.IsFalse(contribRepo.ContributionExists(projectEntityMock.Object, user));
        }
        [TestMethod]
        public void Test_contibution_not_exists_new_created_project()
        {
            var projectEntity = new ProjectEntity ();
            projectEntity.Name = "Gen From Test!";
            projectRepo.Add(projectEntity);
            Assert.IsFalse(contribRepo.ContributionExists(projectEntity, user));
        }

        [TestMethod]
        public void Test_contibution_exists_by_adding_a_task()
        {
            var projectEntity = new ProjectEntity();
            projectEntity.Name = "Gen From Test!";
            projectRepo.Add(projectEntity);
            var contrib = new ContributionEntity
            {
                StartAt = DateTime.Now,
                EndAt = projectEntity.EndAt,
                ProjectId = projectEntity.Id,
                UserId = user.Id,
                Role = "Add-Task-f-test"
            };
            contribRepo.Add(contrib);

            Assert.IsTrue(contribRepo.ContributionExists(projectEntity, user));
            Assert.IsTrue(user.Contributions.Count>0);
            Assert.IsTrue(projectRepo.GetByUser(user).Count > 0);
            Assert.IsTrue(contribRepo.UsersByProject(projectEntity).Contains(user));
        }

        [TestMethod]
        public void Test_all_contibutions_removed_for_current_user()
        {
            contribRepo.RemoveAllForUser(user); //just to be sure?
            Assert.IsTrue(contribRepo.GetByUser(user).Count == 0);
            Assert.IsTrue(projectRepo.GetByUser(user).Count == 0);
        }

        [TestCleanup]
        public void Clean()
        {
            contribRepo.RemoveAllForUser(user);
            dbContext.Dispose();
           
        }
    }
}