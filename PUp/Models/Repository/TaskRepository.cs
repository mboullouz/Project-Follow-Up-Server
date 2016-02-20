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

       

        public override  List<TaskEntity> GetAll()
        {
            return DbContext.TaskSet.Include("Executor").Include("Project").ToList();
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

        public List<TaskEntity> TodayTasksByUser(UserEntity user)
        {
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59

            return GetAll().Where(
                 t => t.Executor == user && t.StartAt != null
                      && t.StartAt.GetValueOrDefault() >= startDateTime
                      && t.StartAt.GetValueOrDefault() <= endDateTime)
                .OrderBy(v => v.StartAt).ToList();
        }

        public GroundInterval AvelaibleHoursForUserAndDate(UserEntity user,DateTime dateEndMin)
        {
            var currentTasks =  TodayTasksByUser(user);
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

        public TaskEntity MarkDone(int id)
        {
            var task = FindById(id);
            task.Done = true;
            task.FinishAt = DateTime.Now;
            DbContext.SaveChanges();
            return task;
        }

        /// <summary>
        /// Mark a task deleted: Deleted=true!
        /// </summary>
        /// <param name="t"></param>
        public override void MarkDeleted(TaskEntity t)
        {
            t.DeleteAt = DateTime.Now;
            t.Deleted = true;
            DbContext.SaveChanges();
        }
 
    }
}