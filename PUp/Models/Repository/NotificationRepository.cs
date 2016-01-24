using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUp.Models.Entity;

namespace PUp.Models.Repository
{
    public class NotificationRepository : AbstractRepository<NotificationEntity>
    {
        

        public NotificationRepository():base()
        {
             
        }

        public NotificationRepository(DatabaseContext dbContext):base(dbContext)
        {
          
        }

        
        public override NotificationEntity FindById(int id)
        {
            return DbContext.NotificationSet.SingleOrDefault(e => e.Id == id);
        }

        public List<NotificationEntity> GetByUser(string id)
        {
            return DbContext.NotificationSet.Where(v => v.User.Id == id).ToList();
        }
        
   

        public void Add(UserEntity user, string message = "", string url = "#")
        {
            var notif = new NotificationEntity
            {
                User = user,
                Message = message,
                Url = url,
                CreateAt = DateTime.Now,
                Seen = false
            };
        }

        public List<NotificationEntity> GetNotSeen()
        {
            return DbContext.NotificationSet.Where(n => n.Seen == false).OrderByDescending(n => n.CreateAt).ToList();
        }

        public void RemoveAllForUser(UserEntity user)
        {
            var notifs = DbContext.NotificationSet.Where(n => n.User.Id == user.Id)
                 .ToList();
            notifs.ForEach(n => DbContext.NotificationSet.Remove(n));
            DbContext.SaveChanges();
        }


        public List<NotificationEntity> GetByUser(UserEntity user)
        {
            return DbContext.NotificationSet.Where(n => n.User.Id == user.Id).ToList();
        }

        public bool RemoveById(int id)
        {
            if (FindById(id) != null)
            {
                DbContext.NotificationSet.Remove(FindById(id));
                DbContext.SaveChanges();
                return true;
            }
            return false;

        }

        public void GenerateFor(ProjectEntity project, List<UserEntity> users)
        {
            foreach (var u in users)
            {
                NotificationEntity notification = new NotificationEntity
                {
                    User = u,
                    CreateAt = DateTime.Now,
                    Message = "New project: ' " + project.Name + "' Added by" + u.Email,
                    Url = "~/Home/Index/" + project.Id
                };
                Add(notification);
            }
        }

        public void GenerateFor(TaskEntity taskEntity, List<UserEntity> users)
        {
            foreach (var u in users)
            {
                NotificationEntity notification = new NotificationEntity
                {
                    User = u,
                    CreateAt = DateTime.Now,
                    Message = "New task: ' " + taskEntity.Title + "' Added by" + u.Email,
                    Url = "~/Task/Add/" + taskEntity.Id
                };
                Add(notification);
            }
        }

        public override void MarkDeleted(NotificationEntity e)
        {
            throw new NotImplementedException();
        }
    }
}