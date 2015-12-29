using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.Facade
{
    public class TaskFacade :IGenericFacade<TaskEntity>
    {
        private DatabaseContext dbContext;
        public TaskFacade()
        {
            dbContext = new DatabaseContext();
        }
        public void SetDbContext(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public DatabaseContext GetDbContext()
        {
            return this.dbContext;
        }
        public void Add(TaskEntity e)
        {
            dbContext.TaskSet.Add(e);
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<TaskEntity> GetAll()
        {
            return dbContext.TaskSet.ToList();
        }

        public void Remove(TaskEntity e)
        {
            throw new NotImplementedException();
        }
    }
}