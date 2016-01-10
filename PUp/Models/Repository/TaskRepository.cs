using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PUp.Models.Repository
{
    public class TaskRepository :ITaskRepository
    {
        private DatabaseContext dbContext;
        public TaskRepository()
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

        public TaskEntity FindById(int id)
        {
            return dbContext.TaskSet.SingleOrDefault(e => e.Id == id);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public List<TaskEntity> GetAll()
        {
            return dbContext.TaskSet.Where(t=>t.Deleted==false).ToList();
        }

        public void Remove(TaskEntity e)
        {
            throw new NotImplementedException();
        }

        public void ChangeTaskState(int id, bool value)
        {
            var task = FindById(id);
            task.Done = value;
            task.EditionNumber += 1;
            task.EditAt = DateTime.Now;
            if (value) {
                task.FinishAt = DateTime.Now;
            }
            dbContext.SaveChanges();
        }
    }
}