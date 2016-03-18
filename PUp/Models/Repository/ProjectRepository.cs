using PUp.Models.Entity;
using System.Collections.Generic;
using System.Linq;
using System;

namespace PUp.Models.Repository
{
    public class ProjectRepository : AbstractRepository<ProjectEntity>
    {
         
        public ProjectRepository():base()
        {
        }

        public ProjectRepository(DatabaseContext dbContext):base(dbContext)
        {
        }

        /// <summary>
        /// Load fully!
        /// </summary>
        /// <returns></returns>
        public override List<ProjectEntity> GetAll()
        {
            return DbContext.ProjectSet
                           // .Include("Contributors")
                           // .Include("Owner")
                           // .Include("Tasks")
                           // .Include("Issues")
                            .ToList();
        }
        public void SetDbContext(DatabaseContext dbContext)
        {
            DbContext = dbContext;
        }
         
        
        public override ProjectEntity FindById(int id)
        {
            return GetAll().SingleOrDefault(e => e.Id == id); 
        }


        public override void Remove(ProjectEntity e)
        {
 
            foreach (var task in e.Tasks)
            {
                DbContext.TaskSet.Remove(task);
            }
            foreach(var issue in e.Issues)
            {
                DbContext.IssueSet.Remove(issue);
            }
            DbContext.ProjectSet.Remove(e);
            DbContext.SaveChanges();
        }

        public void Remove(int id)
        {
            Remove(FindById(id));
        }

        public override void MarkDeleted(ProjectEntity p)
        {
            var pr = FindById(p.Id);
            pr.Deleted = true;
            DbContext.SaveChanges();
        }

        public List<ProjectEntity> GetActive()
        {
            return GetAll().Where(p => p.EndAt > DateTime.Now && p.Deleted==false).ToList();
        }

         
        /// <summary>
        /// Check if a project is still active, usseful before editing related tasks!
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public bool IsActive(ProjectEntity p)
        {
            return GetActive().Contains(p);
               
        }
        public bool IsActive(int id)
        {
            return IsActive(FindById(id));
        }
    }
}