using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PUp.Models.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private DatabaseContext dbContext;
        public ProjectRepository()
        {
            dbContext = new DatabaseContext();
        }
        public void SetDbContext(DatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void Add(ProjectEntity e)
        {
           dbContext.ProjectSet.Add(e);
            dbContext.SaveChanges();
        }
        public ProjectEntity FindById(int id)
        {
            return dbContext.ProjectSet.SingleOrDefault(e => e.Id == id);
        }

        public List<ProjectEntity> GetAll()
        {
            return dbContext.ProjectSet.ToList();
        }

        public void Remove(ProjectEntity e)
        {
            dbContext.ProjectSet.Remove(e);
            dbContext.SaveChanges();
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public DatabaseContext GetDbContext()
        {
            return this.dbContext;
        }
    }
}