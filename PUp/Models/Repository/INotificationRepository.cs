using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PUp.Models.Repository
{
    public interface INotificationRepository:IRepository<NotificationEntity>
    {
       
        void Add(UserEntity user, string message = "", string url = "#");
        List<NotificationEntity> GetNotSeen();
        void RemoveAllForUser(UserEntity user);
        List<NotificationEntity> GetByUser(string id);
        List<NotificationEntity> GetByUser(UserEntity user);
        bool RemoveById(int id);
    }
}
