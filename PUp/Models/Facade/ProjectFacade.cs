using PUp.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace PUp.Models.Facade
{
    public class ProjectFacade : IGenericFacade<ProjectEntity>
    {
        private DatabaseContext dbContext;
        public ProjectFacade()
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

        public List<ProjectEntity> GetAll()
        {
            return dbContext.ProjectSet.ToList();
        }

        public void remove(ProjectEntity e)
        {
            throw new NotImplementedException();
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