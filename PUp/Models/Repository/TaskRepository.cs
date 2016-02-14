using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace PUp.Models.Repository
{
    public class TaskRepository : AbstractRepository<TaskEntity>
    {

        public TaskRepository() : base()
        {
        }

        public TaskRepository(DatabaseContext dbContext) : base(dbContext)
        {
        }

        public override TaskEntity FindById(int id)
        {
            return DbContext.TaskSet.SingleOrDefault(e => e.Id == id);
        }
        public void ChangeTaskState(int id, bool value)
        {
            var task = FindById(id);
            task.Done = value;
            task.EditAt = DateTime.Now;
            if (value)
            {
                task.FinishAt = DateTime.Now;
            }
            DbContext.SaveChanges();
           
        }
        public override void MarkDeleted(TaskEntity e)
        {
            throw new NotImplementedException();
        }
    }
}