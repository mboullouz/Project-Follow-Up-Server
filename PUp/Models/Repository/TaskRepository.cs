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
            return GetAll().SingleOrDefault(e => e.Id == id);
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
        //TODO Skip inactive project!
        public List<TaskEntity> TodayTasksByUser(UserEntity user)
        {
            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59

            return GetAll().Where(
                 t => t.Executor == user && t.StartAt != null
                      && t.StartAt.GetValueOrDefault() >= startDateTime
                      && t.StartAt.GetValueOrDefault() <= endDateTime
                      && t.Project.EndAt > DateTime.Now 
                      && t.Project.Deleted == false)
                .OrderBy(v => v.StartAt).ToList();
        }

        public HashSet<TaskEntity> Upcoming(ProjectEntity project)
        {
            var source = project.Tasks.Where(t => (t.StartAt == null || t.EndAt == null) 
                                                  && t.Deleted==false).OrderByDescending(v => v.StartAt);
            return new HashSet<TaskEntity>(source);
        }

        public HashSet<TaskEntity> TodayTasksByProject(ProjectEntity project) 
        {
            var source = project.Tasks.Where(t =>  t.StartAt != null && t.EndAt <= DateTime.Now 
                                                && t.Deleted == false ).OrderByDescending(v => v.StartAt);
            return new HashSet<TaskEntity>(source);
        }




        public HashSet<TaskEntity> TodayTasksByUserAndProject(ProjectEntity project)
        {

            DateTime startDateTime = DateTime.Today; //Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1); //Today at 23:59:59

            var source= GetAll().Where(
                 t => t.Project==project && t.StartAt != null
                      && t.StartAt.GetValueOrDefault() >= startDateTime
                      && t.StartAt.GetValueOrDefault() <= endDateTime
                      && t.Project.EndAt > DateTime.Now
                      && t.Project.Deleted == false)
                .OrderBy(v => v.StartAt).ToList();


            return new HashSet<TaskEntity>( source);
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

        public void MarkDone(int id)
        {
            var task = FindById(id);
            task.Done = true;
            task.FinishAt = DateTime.Now;
            task.EditAt = DateTime.Now;
            DbContext.SaveChanges();
            
        }

        /// <summary>
        /// Mark a task deleted: Deleted=true!
        /// </summary>
        /// <param name="t"></param>
        public override void MarkDeleted(TaskEntity t)
        {
            t.DeleteAt = DateTime.Now;
            t.EditAt = DateTime.Now;
            t.Deleted = true;
            DbContext.SaveChanges();
        }
        public void MarkUndone(int id)
        {
            var task = FindById(id);
            MarkUndone(task);
        }
        public void MarkUndone(TaskEntity t)
        {
            t.EditAt = DateTime.Now;          
            t.Done   = false;
            DbContext.SaveChanges();
        }
    }
}