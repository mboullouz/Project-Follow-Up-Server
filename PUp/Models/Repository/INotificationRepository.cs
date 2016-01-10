using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUp.Models.Repository
{
    interface INotificationRepository:IRepository<NotificationEntity>
    {
        List<NotificationEntity> GetByUser(string id);
        void Add(UserEntity user, string message = "", string url = "#");
        List<NotificationEntity> GetNotSeen();
    }
}
