using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PUp.Models.Entity;
using PUp.Models.Repository;

namespace PUp.Tests.NotificationTest
{
    [TestClass]
    public class NotificationTest
    {
        [TestMethod]
        public void Test_add_notification()
        {
            IUserRepository uRepo = new UserRepository();
            INotificationRepository nRepo = new NotificationRepository();
            UserEntity user = uRepo.GetFirstOrDefault();
            nRepo.SetDbContext(uRepo.GetDbContext());
            NotificationEntity notif = new NotificationEntity
            {
                CreateAt = DateTime.Now,
                User = user,
                Message = "TEST message",
                Seen = false,
                Url = "a/b"
            };
            nRepo.Add(notif);

            int n = nRepo.GetAll().Count;
            Assert.AreNotEqual(n, 0);
        }
    }
}
