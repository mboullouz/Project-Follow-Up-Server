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
        public GroundInterval AvelaibleHorsForUserAndDate(UserEntity user,DateTime dateEndMin)
        {
            Console.WriteLine(dateEndMin.ToLongDateString());
            var currentTasks = GetAll().Where(
                t => t.Executor == user
                  && !t.Done && t.StartAt!=null
                  ).ToList();
            GroundInterval intervalManager = new GroundInterval();
            foreach (var t in currentTasks)
            {
                if (t.StartAt != null)
                {
                    intervalManager.AddDate((DateTime)t.StartAt, t.EstimatedTimeInMinutes / 60);
                }
            }
            return intervalManager;
        }
        public override void MarkDeleted(TaskEntity e)
        {
            throw new NotImplementedException();
        }
    }
}