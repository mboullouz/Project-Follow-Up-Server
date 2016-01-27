using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PUp.Models.Entity;
using PUp.Models.Repository;
using PUp.Tests.Helpers;
using PUp.Models;

namespace PUp.Tests.NotificationTest
{
    [TestClass]
    public class NotificationModelTest
    {
        private  NotificationRepository nRepo;
        private UserEntity user;
        private DatabaseContext dbContext = new DatabaseContext();

        [TestInitialize]
        public void Init()
        {
            user = ContextHelper.CurrentUserEntity(dbContext);
            nRepo = new NotificationRepository(dbContext);
        }

        [TestMethod]
        public void Test_add_notification()
        {  
            NotificationEntity notif = new NotificationEntity
            {
                AddAt = DateTime.Now,
                User = user,
                Message = "TEST message",
                Seen = false,
                Url = "#/a/b",
                Deleted=false,
            };
            nRepo.Add(notif);
            int n = nRepo.GetByUser(user.Id).Count;
            Assert.AreNotEqual(n, 0);
        }

        [TestMethod]
        public void Test_remove_notifications_for_a_user()
        {
            nRepo.RemoveAllForUser(user);
            int n=nRepo.GetByUser(user).Count;
            Assert.AreEqual(n, 0);
        }

        [TestCleanup]
        public void Clean()
        {
            dbContext.Dispose();
        }
    }
}
