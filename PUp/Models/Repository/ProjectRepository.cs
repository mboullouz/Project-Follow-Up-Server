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

        public void SetDbContext(DatabaseContext dbContext)
        {
            this.DbContext = dbContext;
        }
         
        
        public override ProjectEntity FindById(int id)
        {
            return DbContext.ProjectSet.SingleOrDefault(e => e.Id == id);
        }


        public override void Remove(ProjectEntity e)
        {
 
            foreach (var task in e.Tasks.ToList())
            {
                DbContext.TaskSet.Remove(task);
            }
            DbContext.ProjectSet.Remove(e);
            DbContext.SaveChanges();
        }

      
 

        public void Remove(int id)
        {
            DbContext.ProjectSet.Remove(FindById(id));
        }

        public override void MarkDeleted(ProjectEntity p)
        {
            var pr = FindById(p.Id);
            pr.Deleted = true;
            DbContext.SaveChanges();
        }

        public List<ProjectEntity> GetActive()
        {
            return DbContext.ProjectSet.Where(p => p.EndAt >= DateTime.Now && p.Deleted==false).ToList();
        }
    }
}