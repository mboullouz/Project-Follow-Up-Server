using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PUp.Models.Entity;
using PUp.Models.Repository;

namespace PUp.Tests.NotificationTest
{
    [TestClass]
    public class NotificationModelTest
    {

        IUserRepository uRepo = new UserRepository();
        INotificationRepository nRepo = new NotificationRepository();
        UserEntity user;

        [TestInitialize]
        public void Init()
        {
            nRepo.SetDbContext(uRepo.GetDbContext());
            this.user = uRepo.GetFirstOrDefault();
           
        }

        [TestMethod]
        public void Test_user_not_null()
        {
            Assert.IsNotNull(this.user);
        }

        [TestMethod]
        public void Test_add_notification()
        {  
            NotificationEntity notif = new NotificationEntity
            {
                CreateAt = DateTime.Now,
                User = user,
                Message = "TEST message",
                Seen = false,
                Url = "#/a/b"
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

        
    }
}
