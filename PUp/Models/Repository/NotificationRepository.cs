using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PUp.Models.Entity;

namespace PUp.Models.Repository
{
    public class NotificationRepository : INotificationRepository
    {
        private DatabaseContext dbContext;

        public NotificationRepository()
        {
            dbContext = new DatabaseContext();
        }

        public NotificationRepository(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Add(NotificationEntity e)
        {
            dbContext.NotificationSet.Add(e);
            dbContext.SaveChanges();
        }
        public NotificationEntity FindById(int id)
        {
            return dbContext.NotificationSet.SingleOrDefault(e => e.Id == id);
        }

        public List<NotificationEntity> GetByUser(string id)
        {
            return dbContext.NotificationSet.Where(v => v.User.Id == id).ToList();
        }
        public List<NotificationEntity> GetAll()
        {
            return dbContext.NotificationSet.ToList();
        }
        public void Remove(NotificationEntity e)
        {
            dbContext.NotificationSet.Remove(e);
            dbContext.SaveChanges();
        }

        public DatabaseContext GetDbContext()
        {
            return dbContext;
        }

        //TODO Do a clean design
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void SetDbContext(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
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
            return dbContext.NotificationSet.Where(n => n.Seen == false).OrderByDescending(n => n.CreateAt).ToList();
        }

        public void RemoveAllForUser(UserEntity user)
        {
            var notifs = dbContext.NotificationSet.Where(n => n.User.Id == user.Id)
                 .ToList();
            notifs.ForEach(n => dbContext.NotificationSet.Remove(n));
            dbContext.SaveChanges();
        }


        public List<NotificationEntity> GetByUser(UserEntity user)
        {
            return dbContext.NotificationSet.Where(n => n.User.Id == user.Id).ToList();
        }

        public bool RemoveById(int id)
        {
            if (FindById(id) != null)
            {
                dbContext.NotificationSet.Remove(FindById(id));
                dbContext.SaveChanges();
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
    }
}